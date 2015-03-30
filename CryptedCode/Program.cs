using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using CryptedCode.Packing;

namespace CryptedCode
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            // custom assembly resolver
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            LoadAssembliesFromArchive();

            var runner = FindExecutionAssemblyType();

            MethodInfo method = runner.GetMethod("Init", BindingFlags.Static | BindingFlags.Public);
            
            method.Invoke(null, BindingFlags.Static | BindingFlags.InvokeMethod, null, null, null);
        }

        private static Type FindExecutionAssemblyType()
        {
            foreach (var assembly in _assemblies)
            {
                Type runner = assembly.Value.GetType("hidden.Runner", false, true);

                if (runner != null)
                {
                    return runner;
                }
            }

            throw new Exception("No such assembly in memory.");
        }

        private static void LoadAssembliesFromArchive(string archiveName = "AppData.pak")
        {
            var data = File.ReadAllText(archiveName);

            var packer = new DefaultPacker();

            var assemblies = packer.Unpack(data);

            foreach (var assembly in assemblies)
            {
                Assembly.Load(assembly);
            }
            
            _assemblies = AppDomain.CurrentDomain.GetAssemblies().ToDictionary(k => k.FullName, v => v);
        }

        private static Dictionary<string, Assembly> _assemblies;

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {

            if (_assemblies.ContainsKey(args.Name))
            {
                return _assemblies[args.Name];
            }

            throw new Exception("No such assembly in project");
        }
    }
}
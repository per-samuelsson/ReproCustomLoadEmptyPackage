
using System;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace Loader
{
    class CustomContext : AssemblyLoadContext {

        protected override Assembly Load(AssemblyName name) {
            Console.WriteLine(name);
            return null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Both context show same behaviour. Using the
            // custom one, we get a chance to resolve it, but how
            // can we? There's no assembly in there.

            // var context = AssemblyLoadContext.Default;
            var context = new CustomContext();

            // Get path of the assembly with a reference to a package
            // that has no assembly: Microsoft.CodeAnalysis
            var path = GetPathOfAssemblyToLoad();

            // Load it
            var assembly = context.LoadFromAssemblyPath(path);
            
            // Execute entrypoint: see it fail
            var entrypoint = assembly.EntryPoint;
            var entry = (Action<string[]>)entrypoint.CreateDelegate(typeof(Action<string[]>));
            entry(new string[0]);
        }

        static string GetPathOfAssemblyToLoad() {
            var path = Path.Combine(
                Path.GetDirectoryName(typeof(Program).Assembly.Location), 
                @"..\..\..\..\AppWithReferenceToPackageWithNoAssembly\bin\Debug\netcoreapp2.0\AppWithReferenceToPackageWithNoAssembly.dll");

            if (!File.Exists(path)) {
                Console.Error.WriteLine("dotnet build AppWithReferenceToPackageWithNoAssembly, no?");
                Environment.Exit(1);
            }

            return path;
        }
    }
}
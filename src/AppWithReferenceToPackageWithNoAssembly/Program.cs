using System;

namespace AppWithReferenceToPackageWithNoAssembly
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Microsoft.CodeAnalysis.Accessibility.Friend);
        }
    }
}

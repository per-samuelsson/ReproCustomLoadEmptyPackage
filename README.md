## About
Two projects provided, both targeting just `netcoreapp2.0`.

* **AppWithReferenceToPackageWithNoAssembly** is a simple application with a reference to `Microsoft.CodeAnalysis` package. This package contain no assembly (i.e. a "meta-package").
* **Loader** is an application using a custom `AssemblyLoadContext` to load `AppWithReferenceToPackageWithNoAssembly`. Loading works fine, but executing code in the loaded assembly will cause reference resolving to happen, and custom context get a chance to resolve a reference to `Microsoft.CodeAnalysis` via its `Load` override. But how can it, when there is no such assembly?

## Reproduce
1. CD src\AppWithReferenceToPackageWithNoAssembly
2. `dotnet build`
3. CD ..\src\Loader
4. `dotnet run`

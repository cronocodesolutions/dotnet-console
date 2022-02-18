using System.Reflection;
using System.Runtime.Loader;

public class CustomAssemblyLoadContext : AssemblyLoadContext
{
    public CustomAssemblyLoadContext() : base(isCollectible: true)
    { }
 
    protected override Assembly Load(AssemblyName assemblyName)
    {
        Console.WriteLine($"load null - {assemblyName.FullName}");
        return null;
    }
}

using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Loader;

Console.Clear();

// Console.WriteLine("Creating new AppDomain.");
//         AppDomain domain = AppDomain.CreateDomain("MyDomain");

//         Console.WriteLine("Host domain: " + AppDomain.CurrentDomain.FriendlyName);
//         Console.WriteLine("child domain: " + domain.FriendlyName);

//         return;

// Console.WriteLine("Creating new AppDomain.");
//         AppDomain domain = AppDomain.CreateDomain("MyDomain");

//         Console.WriteLine("Host domain: " + AppDomain.CurrentDomain.FriendlyName);
//         Console.WriteLine("child domain: " + domain.FriendlyName);

// var test = AppDomain.CurrentDomain.Load();

Console.WriteLine($"AppContext.BaseDirectory = {AppContext.BaseDirectory}");
Console.WriteLine($"Directory.GetCurrentDirectory() = {Directory.GetCurrentDirectory()}");
Console.WriteLine($"Environment.CurrentDirectory = {Environment.CurrentDirectory}");

// return;

var root = "/home/maxpro/infinity/api/src/Winerist.Cli/bin/Debug/net5.0/";
var root2 = "/home/maxpro/infinity/api/src/Winerist.Cli";
var path = "Winerist.Cli.dll";

//  var startInfo = new ProcessStartInfo
// {
//     FileName = "dotnet", 
//     Arguments = path, 
//     UseShellExecute = false, 
//     // RedirectStandardOutput = interceptOutput,
//     WorkingDirectory = root,

// };
// // if (workingDirectory != null)
// // {
// //     startInfo.WorkingDirectory = workingDirectory;
// // }

// var process = Process.Start(startInfo)!;
// var interceptOutput = true;
// if (interceptOutput)
// {
//     string? line;
//     while ((line = process.StandardOutput.ReadLine()) != null)
//     {
//         Console.WriteLine(line);
//         // Reporter.WriteVerbose(line);
//     }
// }

// process.WaitForExit();

// Console.WriteLine("DONE");



// var test = Assembly.GetExecutingAssembly();
// Console.WriteLine(Path.GetDirectoryName(test.Location));

// return;
// // Process.Start()
// // System.AppContext.BaseDirectory = root;
// Environment.CurrentDirectory = root;
// Directory.SetCurrentDirectory(Path.GetDirectoryName(root));

// var pType = typeof(Entity);
// IEnumerable<string> children = Enumerable.Range(1, iterations)
//    .SelectMany(i => Assembly.GetExecutingAssembly().GetTypes()
//                     .Where(t => t.IsClass && t != pType
//                             && pType.IsAssignableFrom(t))
//                     .Select(t => t.Name));

var context = new CustomAssemblyLoadContext();
// установка обработчика выгрузки
context.Unloading += (AssemblyLoadContext obj) => {Console.WriteLine("Библиотека MyApp выгружена \n");};

context.Resolving += (context, name) => {
    string assemblyPath = $"{root}\\{name.Name}.dll";                
    Console.WriteLine($"assemblyPath => {assemblyPath}");
    if (assemblyPath != null)   
        return context.LoadFromAssemblyPath(assemblyPath);     
    return null;
};

// получаем путь к сборке MyApp
// var assemblyPath = Path.Combine(Directory.GetCurrentDirectory(), "MyApp.dll");
var assemblyPath = Path.Combine(root, path);
// загружаем сборку
Assembly assembly = context.LoadFromAssemblyPath(assemblyPath);
// Assembly assembly = context.LoadFromAssemblyName.LoadFromAssemblyPath(assemblyPath);



// получаем тип Program из сборки MyApp.dll
// var type = assembly.GetType("MyApp.Program");
// получаем его метод Factorial
// var greetMethod = type.GetMethod("Factorial");

// вызываем метод
// var instance = Activator.CreateInstance(type);
// int result = (int)greetMethod.Invoke(instance, new object[] { number });
// // выводим результат метода на консоль
// Console.WriteLine($"Факториал числа {number} равен {result}");

// Console.WriteLine(assembly.FullName);

// var classes2 = assembly.GetExportedTypes();

var classes = assembly.GetExportedTypes()
    .Where(t => t.IsClass)
    .Where(t => t.Name == "WineristContextMigrations")
    .ToList();

foreach (var item in classes)
{
    Console.WriteLine(item);
}


    var type = classes.First();
var initiatedObject = Activator.CreateInstance(type);
// var myMethod = type.GetProperty("Roles");

PropertyInfo property= type.GetProperty("Roles");

Console.WriteLine(initiatedObject);
// Console.WriteLine(myMethod);

var list = ((IEnumerable<object>)property.GetValue(initiatedObject, null)).ToList();

// staticMethod.Invoke(null, null);
foreach (var item in list)
{
    Console.WriteLine(item);
}


// смотим, какие сборки у нас загружены
foreach (Assembly asm in AppDomain.CurrentDomain.GetAssemblies())
    Console.WriteLine(asm.GetName().Name);

// выгружаем контекст
context.Unload();

return;

// var a = Assembly.LoadFrom(root + path);

// var classes = a.GetExportedTypes()
//     .Where(t => t.IsClass)
//     .Where(t => t.Name == "WineristContextMigrations")
//     .ToList();




// for (int i = classes.Count - 1; i >= 0; i--)
//     {
//         Console.WriteLine(classes[i].Name);
//     }

    return;




var commands = new [] { "show", "get", "create" };
var currentIndex = 0;
var command = "";

bool showMenu = true;
while (showMenu)
{
    Console.Clear();
    for (int i = commands.Length - 1; i >= 0; i--)
    {
        Console.Write(i == currentIndex ? "> " : "  ");
        Console.WriteLine(commands[i]);
    }

    Console.Write(command);

    var c = Console.ReadKey();

    if(c.Key == ConsoleKey.UpArrow) {
        currentIndex++;
    } else if(c.Key == ConsoleKey.DownArrow) {
        currentIndex--;
    } else if(c.Key == ConsoleKey.Backspace) {
        command = command.Remove(command.Length - 1); 
    } else if(c.Key == ConsoleKey.Tab) {
        command = commands[currentIndex];
    } else {
        command += c.KeyChar;
    }
}

// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");
var path = @"C:\Users\moham\source\repos\EdehaConsole\EdehaConsole\Program.cs";
// want to print EdehaConsole only
Console.WriteLine(path.Split('\\').Last());
path = path[..path.LastIndexOf('\\')];
Console.WriteLine(path);



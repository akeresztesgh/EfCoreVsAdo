// See https://aka.ms/new-console-template for more information
using database;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using DocoptNet;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Configs;

Console.WriteLine("Starting...");

const string usage = @"efcorevsado.

Usage:
  efcorevsado.exe --seed
  efcorevsado.exe --run  
  efcorevsado.exe (-h | --help)
  efcorevsado.exe --version

Options:
  -h --help     Show this screen.
  --version     Show version.
  --seed        Run Seed
  --run         Run benchmarks

";
var arguments = new Docopt().Apply(usage, args, version: "EfCoreVsAdo 1.0", exit: true)!;
var seedValue = arguments["--seed"];
var runValue = arguments["--run"];

if (seedValue.IsTrue)
{
    Console.WriteLine("Running seed...");
    await new RunSeed().Run(5000, 25);
    Console.WriteLine("Done...");
    return;
}

if(runValue.IsTrue)
{
    Console.WriteLine("Running benchmarks...");
    var summary = BenchmarkRunner.Run(typeof(Program).Assembly);
    return;
}
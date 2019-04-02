using System;
using System.Linq;

namespace LogEnvTool
{
    class Program
    {
        static void Main(string[] args)
        {
            LogEnv();
        }
        
        private static void LogEnv()
        {
            var envVariables = Environment.GetEnvironmentVariables();
            var dict = envVariables.Keys
                .Cast<object>()
                .ToDictionary(k => k, k => envVariables[k])
                .OrderBy(x => x.Key);
            
            foreach (var (key, value) in dict)
            {
                if (value is string stringValue && 
                    (stringValue.Contains(";") || stringValue.Contains(":")))
                {
                    var innerValues = stringValue
                        .Split(new[] {":", ";"}, StringSplitOptions.RemoveEmptyEntries);

                    if (innerValues.Length > 1)
                    {
                        Console.WriteLine($"{key}");

                        Console.ForegroundColor = ConsoleColor.Magenta;
                        
                        innerValues
                            .OrderBy(v => v)
                            .ToList()
                            .ForEach(x => Console.WriteLine($" -> {x}"));
                        
                        Console.ResetColor();
                        
                        continue;
                    }
                }

                Console.Write($"{key} = ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write(value);
                Console.ResetColor();
                Console.Write(Environment.NewLine);
            }
        }
    }
}

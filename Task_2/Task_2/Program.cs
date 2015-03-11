using Entities;
using Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Task_2
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Count() > 0)
                {
                    string variable1 = args[0];

                    //Call Console Manager
                    List<Employee> emps = ConsoleManager.ExecuteConsole(variable1);
                    if (emps.Count > 0)
                    {
                        foreach (Employee emp in emps)
                        {
                            Console.Write(ConsoleManager.FormatResults(emp));
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine("No results returned.");
                    }

                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Error: No variable supplied.");
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: - " + ex.Message);
                Console.ReadKey();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroDiffusionProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            int option = 3;
            while(true)
            {
                Console.WriteLine("MENU:");
                Console.WriteLine("1. Read from test file.");
                Console.WriteLine("2. Manual input.");
                Console.WriteLine("3. Quit.");
                Console.WriteLine("");
                Console.WriteLine("Choose an option:");
                option = Convert.ToInt32(Console.ReadLine());

                List<CoinsDiffusionUtils> simulations = null;

                if (option == 3)
                    return;
                else if (option == 2)
                {
                    try
                    {
                        simulations = InputProcessor.GetManualInput();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                    
                else
                    try
                    {
                        simulations = InputProcessor.ReadFromFile("input.txt");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                

                if (simulations != null)
                {
                    foreach (CoinsDiffusionUtils MemberStateSimulation in simulations)
                        MemberStateSimulation.SimulateDiffusion();

                    Console.WriteLine("");
                    Console.WriteLine("RESULT:");
                    Console.WriteLine(BuildResult(simulations));
                }
                
                Console.WriteLine("");
            }

        }
      

        private static string BuildResult(List<CoinsDiffusionUtils> Simulations)
        {
            StringBuilder stringBuilder = new StringBuilder();

            int i = 0;
            foreach (CoinsDiffusionUtils simulation in Simulations)
            {
                i++;
                if (simulation.getNumberOfCountries() == 0)
                    continue;
                stringBuilder.Append($"Case number {i} \n{simulation.GetResults()}");
            }

            return stringBuilder.ToString();
        }
    }
}

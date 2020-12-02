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
            while (true)
            {
                Console.WriteLine("MENU:");
                Console.WriteLine("1. Read from test file.");
                Console.WriteLine("2. Manual input.");
                Console.WriteLine("3. Quit.");
                Console.WriteLine("");
                Console.WriteLine("Choose an option:");
                option = Convert.ToInt32(Console.ReadLine());

                List<CoinsDiffusionUtils> simulations = null;

                try
                {
                    switch (option)
                    {
                        case 3:
                            return;
                        case 2:
                            simulations = InputProcessor.GetManualInput();
                            break;
                        default:
                            simulations = InputProcessor.ReadFromFile("input.txt");
                            break;
                    }
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

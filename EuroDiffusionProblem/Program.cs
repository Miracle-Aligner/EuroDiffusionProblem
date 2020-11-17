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

                List<CoinsDiffusionUtils> Simulations;

                if (option == 3)
                    return;
                else if (option == 2)
                    Simulations = InputProcessor.GetManualInput();
                else
                    Simulations = InputProcessor.ReadFromFile("input.txt");

                if (Simulations != null)
                {
                    foreach (CoinsDiffusionUtils MemberStateSimulation in Simulations)
                        MemberStateSimulation.SimulateDiffusion();

                    Console.WriteLine("");
                    Console.WriteLine("RESULT:");
                    Console.WriteLine(BuildResult(Simulations));
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
                stringBuilder.Append("Case number")
                        .Append(" ")
                        .Append(i)
                        .Append("\n")
                        .Append(simulation.GetResults());
            }

            return stringBuilder.ToString();
        }
    }
}

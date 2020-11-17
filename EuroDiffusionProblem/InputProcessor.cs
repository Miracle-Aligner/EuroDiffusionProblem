using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroDiffusionProblem
{
    class InputProcessor
    {
        public static List<CoinsDiffusionUtils> ReadFromFile(string filename)
        {
            try
            {
                List<CoinsDiffusionUtils> result = new List<CoinsDiffusionUtils>();
                int countriesQuantity = 0;

                StreamReader sr = new StreamReader(filename);

                while (!sr.EndOfStream)
                {
                    countriesQuantity = Convert.ToInt32(sr.ReadLine());
                    if (countriesQuantity < 0)
                        throw new Exception("Countries quantity should be greater than 0.");

                    Country[] countries = new Country[countriesQuantity];
                    for (int i = 0; i < countriesQuantity; i++)
                    {
                        string[] splitLine = sr.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                        Country newCountry = new Country(
                                                         splitLine[0],
                                                         Convert.ToInt32(splitLine[1]),
                                                         Convert.ToInt32(splitLine[2]),
                                                         Convert.ToInt32(splitLine[3]),
                                                         Convert.ToInt32(splitLine[4]));
                        if (i == 0)
                            countries[i] = newCountry;
                        else if (newCountry.IsUnique(countries))
                        {
                            countries[i] = newCountry;
                        }
                        else
                        {
                            throw new Exception("Countries can't have the same name.");
                        }
                    }
                    CoinsDiffusionUtils simulation = new CoinsDiffusionUtils(countries);
                    result.Add(simulation);
                }
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        public static List<CoinsDiffusionUtils> GetManualInput()
        {
            try
            {
                List<CoinsDiffusionUtils> result = new List<CoinsDiffusionUtils>();

                Console.WriteLine("Enter countries' quantity:");
                int countriesQuantity = Convert.ToInt32(Console.ReadLine());

                if (countriesQuantity < 0)
                    throw new ArgumentOutOfRangeException("Countries quantity should be greater than 0.");

                else
                    while (countriesQuantity != 0)
                    {
                        Country[] countries = new Country[countriesQuantity];
                        for (int i = 0; i < countriesQuantity; i++)
                        {
                            Console.WriteLine("Enter country info (name, xl, yl, xh, yh):");
                            string[] splitLine = Console.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                            Country newCountry = new Country(
                                                        splitLine[0],
                                                        Convert.ToInt32(splitLine[1]),
                                                        Convert.ToInt32(splitLine[2]),
                                                        Convert.ToInt32(splitLine[3]),
                                                        Convert.ToInt32(splitLine[4]));
                            if (newCountry.IsUnique(countries))
                            {
                                countries[i] = newCountry;
                                Console.WriteLine("Ima here");
                            }
                            else
                            {
                                throw new Exception("Countries can't have the same name.");
                            }
                        }

                        CoinsDiffusionUtils simulation = new CoinsDiffusionUtils(countries);
                        result.Add(simulation);

                        Console.WriteLine("Enter countries' quantity:");
                        countriesQuantity = Convert.ToInt32(Console.ReadLine());
                        if (countriesQuantity < 0)
                            throw new Exception("Countries quantity should be greater than 0.");
                    }


                return result;
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }
    }
}

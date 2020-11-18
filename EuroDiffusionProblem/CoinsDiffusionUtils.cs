using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroDiffusionProblem
{
    class CoinsDiffusionUtils
    {
        private int countriesQuantity;
        private Country[] countries;
        private bool[,] generalCountriesGrid;

        public CoinsDiffusionUtils(Country[] countries)
        {
            countriesQuantity = countries.Length;
            this.countries = countries;
            CheckInputData();
            FillGeneralCountriesGrid();
            SetGeneralCountriesGrid();
        }

        private void CheckInputData()
        {
            if (countriesQuantity > Constants.MAX_NUM_OF_COUNTRIES)
                throw new Exception("Countries quantity should be less than 12.");
        }
        
        private void FillGeneralCountriesGrid()
        {
            generalCountriesGrid = new bool[Constants.MAX_CITY_COORDINATE, Constants.MAX_CITY_COORDINATE];
            foreach (Country country in countries)
            {
                for (int x = country.GetXl(); x <= country.GetXh(); x++)
                    for (int y = country.GetYl(); y <= country.GetYh(); y++)
                        generalCountriesGrid[x, y] = true;
            }
        }

        private void SetGeneralCountriesGrid()
        {
            foreach (Country country in countries)
                country.SetGeneralCountriesGrid(generalCountriesGrid);
        }

        public void SimulateDiffusion()
        {
            int day = 0;
            while (!IsEnd())
            {
                day++;
                for (int i = 0; i < countriesQuantity; i++)
                {
                    countries[i].NextDay();
                }
                if (day > Constants.MAX_NUM_OF_DAYS)
                    throw new ArgumentException("Countries should be connected.");
            }            
        }

        private bool IsEnd()
        {
            bool result = true;
            for (int i = 0; i < countriesQuantity; i++)
            {
                if (!countries[i].IsComplete() && !CheckCountryComplete(countries[i]))
                    result = false;
            }
            return result;
        }

        private bool CheckCountryComplete(Country country)
        {
            for (int x = country.GetXl(); x <= country.GetXh(); x++)
            {
                for (int y = country.GetYl(); y <= country.GetYh(); y++)
                {
                    for (int j = 0; j < countriesQuantity; j++)
                    {
                        if (countries[j].GetCityCoins(x, y) == 0)
                            return false;
                    }
                }
            }
            country.SetComplete(true);
            return true;
        }

        public String GetResults()
        {
            var sortedCountries = countries.OrderBy(c => c.Name).OrderBy(c => c.NumberOfDays).ToList();
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < countriesQuantity; i++)
            {
                stringBuilder.Append($"{sortedCountries[i].GetName()} {sortedCountries[i].GetNumberOfDays()}\n");
            }
            return stringBuilder.ToString();
        }

        public int getNumberOfCountries()
        {
            return countriesQuantity;
        }
    }
}

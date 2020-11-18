using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EuroDiffusionProblem
{
    class Country
    {
        public string Name;
        private int[,] countryGrid;
        private bool[,] generalCountriesGrid;
        private int xl;
        private int yl;
        private int xh;
        private int yh;
        public int NumberOfDays = 0;
        private bool isComplete;

        public Country(String name, int xl, int yl, int xh, int yh)
        {
            this.Name = name;
            this.xl = xl - 1;
            this.yl = yl - 1;
            this.xh = xh - 1;
            this.yh = yh - 1;
            if (!CheckInputData())
                return;
            countryGrid = InitCoinsInCities();
        }

        private bool CheckInputData()
        {
            if (Name.Length > Constants.MAX_COUNTRYNAME_LENGTH)
                throw new Exception("Name of a country should be less than 25 characters long.");

            if (xl < Constants.MIN_CITY_COORDINATE - 1 ||
                yl < Constants.MIN_CITY_COORDINATE - 1 ||
                xh < Constants.MIN_CITY_COORDINATE - 1 ||
                yh < Constants.MIN_CITY_COORDINATE - 1)
            {
                throw new Exception("Coordinates should be greater than 0.");
            }

            if (xl >= Constants.MAX_CITY_COORDINATE ||
                yl >= Constants.MAX_CITY_COORDINATE ||
                xh >= Constants.MAX_CITY_COORDINATE ||
                yh >= Constants.MAX_CITY_COORDINATE)
            {
                throw new Exception("Coordinates should NOT be greater than 10.");
            }
            
            return true;
        }

        public void NextDay()
        {
            int[,] result = new int[Constants.MAX_CITY_COORDINATE, Constants.MAX_CITY_COORDINATE];
            for (int x = 0; x < Constants.MAX_CITY_COORDINATE; x++)
            {
                for (int y = 0; y < Constants.MAX_CITY_COORDINATE; y++)
                {
                    int amountToTransport = countryGrid[x, y] / Constants.TO_REPRESENTATIVE_COEF;
                    int transportedInGeneral = TransportToNeighbors(result, x, y, amountToTransport);
                    result[x, y] += countryGrid[x, y] - transportedInGeneral * amountToTransport;
                }
            }
            if (!IsComplete())
                NumberOfDays++;
            countryGrid = result;
        }

        private int TransportToNeighbors(int[,] resultGrid, int x, int y, int amountToTransport)
        {
            int transportedInGeneral = 0;
            if (amountToTransport <= 0)
                return transportedInGeneral;
            if (UpdateNeighborCoins(resultGrid, x - 1, y, amountToTransport))
                transportedInGeneral++;
            if (UpdateNeighborCoins(resultGrid, x, y - 1, amountToTransport))
                transportedInGeneral++;
            if (UpdateNeighborCoins(resultGrid, x + 1, y, amountToTransport))
                transportedInGeneral++;
            if (UpdateNeighborCoins(resultGrid, x, y + 1, amountToTransport))
                transportedInGeneral++;
            return transportedInGeneral;
        }

        private bool UpdateNeighborCoins(int[,] resultGrid, int x, int y, int amountToTransport)
        {
            if (!CheckIsCityAvailable(x, y))
                return false;
            resultGrid[x, y] += amountToTransport;
            return true;
        }

        private int[,] InitCoinsInCities()
        {
            int[,] result = new int[Constants.MAX_CITY_COORDINATE, 
                                    Constants.MAX_CITY_COORDINATE];

            for (int x = xl; x <= xh; x++)
                for (int y = yl; y <= yh; y++)
                    result[x, y] = Constants.INITIAL_CITY_COINS;
            return result;
        }

        private bool CheckIsCityAvailable(int x, int y)
        {
            if (x < 0 || y < 0 || x >= Constants.MAX_CITY_COORDINATE || y >= Constants.MAX_CITY_COORDINATE)
                return false;
            return generalCountriesGrid[x, y];
        }

        public int CompareTo(Object c)
        {
            if (c is Country)
            {
                Country country = (Country)c;
                if (NumberOfDays > country.NumberOfDays)
                    return 1;
                if (NumberOfDays < country.NumberOfDays)
                    return -1;
                return (Name.CompareTo(country.Name));
            }
            else
                return -1;
        }

        public int GetCityCoins(int x, int y)
        {
            return countryGrid[x, y];
        }

        public String GetName()
        {
            return Name;
        }

        public int GetXl()
        {
            return xl;
        }

        public int GetYl()
        {
            return yl;
        }

        public int GetXh()
        {
            return xh;
        }

        public int GetYh()
        {
            return yh;
        }

        public int GetNumberOfDays()
        {
            return NumberOfDays;
        }

        public bool IsComplete()
        {
            return isComplete;
        }

        public void SetComplete(bool complete)
        {
            isComplete = complete;
        }

        public void SetGeneralCountriesGrid(bool[,] generalCountriesGrid)
        {
            this.generalCountriesGrid = generalCountriesGrid;
        }

        public Boolean IsUnique(Country[] countries)
        {
            for (int i = 0; countries[i] != null; i++)
            {
                if (countries[i].GetName().CompareTo(this.Name) == 0)
                    return false;
            }
            return true;
        }
    }
}

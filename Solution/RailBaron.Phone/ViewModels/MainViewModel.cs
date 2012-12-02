using System;
using System.Collections.Generic;
using System.ComponentModel;
using RailBaron.Shared.Models;


namespace RailBaron.Phone.ViewModels
{
    /// <summary>
    /// Our main view model for the app
    /// </summary>
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Fields
        private List<Trip> _trips;
        Random random = new Random();
        #endregion

        #region RollRegionButtonEnabled Property
        private bool _rollRegionButtonEnabled = true;

        public bool RollRegionButtonEnabled
        {
            get
            {
                return _rollRegionButtonEnabled;
            }
            set
            {
                _rollRegionButtonEnabled = value;
                NotifyPropertyChanged("RollRegionButtonEnabled");
            }
        }
        #endregion

        #region RollCityButtonEnabled Property
        private bool _rollCityButtonEnabled = false;

        public bool RollCityButtonEnabled
        {
            get
            {
                return _rollCityButtonEnabled;
            }
            set
            {
                _rollCityButtonEnabled = value;
                NotifyPropertyChanged("RollCityButtonEnabled");
            }
        }
        #endregion

        #region RegionDiceRoll Property
        private string _regionDiceRoll = "None";
        
        public string RegionDiceRoll
        {
            get
            {
                return _regionDiceRoll;
            }
            set
            {
                _regionDiceRoll = value;
                NotifyPropertyChanged("RegionDiceRoll");
            }
        }
        #endregion

        #region RolledRegion Property
        private string _rolledRegion = "None";

        public string RolledRegion
        {
            get
            {
                return _rolledRegion;
            }
            set
            {
                _rolledRegion = value;
                NotifyPropertyChanged("RolledRegion");
            }
        }
        #endregion

        #region CityDiceRoll Property
        private string _cityDiceRoll = "None";

        public string CityDiceRoll
        {
            get
            {
                return _cityDiceRoll;
            }
            set
            {
                _cityDiceRoll = value;
                NotifyPropertyChanged("CityDiceRoll");
            }
        }
        #endregion

        #region RolledCity Property
        private string _rolledCity = "None";

        public string RolledCity
        {
            get
            {
                return _rolledCity;
            }
            set
            {
                _rolledCity = value;
                NotifyPropertyChanged("RolledCity");
            }
        }
        #endregion

        #region Cities Property
        public List<City> Cities { get; set; }
        #endregion

        #region StartCity Property
        private City _startCity;

        public City StartCity
        {
            get
            {
                return _startCity;
            }
            set
            {
                _startCity = value;
                NotifyPropertyChanged("Payout");
            }
        }
        #endregion

        #region EndCity Property
        private City _endCity;

        public City EndCity
        {
            get
            {
                return _endCity;
            }
            set
            {
                _endCity = value;
                NotifyPropertyChanged("Payout");
            }
        }
        #endregion

        #region Payout Property
        public int Payout
        {
            get
            {
                if (StartCity == null || EndCity == null)
                {
                    return 0;
                }

                if (StartCity == EndCity)
                {
                    return 0;
                }

                foreach (var trip in _trips)
                {
                    if (StartCity == trip.StartCity && EndCity == trip.EndCity)
                    {
                        return trip.Payout;
                    }
                }

                return -1;
            }
        }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public MainViewModel()
        {
            // add our cities
            Cities = new List<City>();
            var boston = new City() { Name = "Boston" };
            Cities.Add(boston);
            var losAngeles = new City() { Name = "Los Angeles" };
            Cities.Add(losAngeles);
            var sanFrancisco = new City() { Name = "San Francisco" };
            Cities.Add(sanFrancisco);

            // add our trips
            _trips = new List<Trip>();
            _trips.Add(new Trip() { StartCity = boston, EndCity = losAngeles, Payout = 21000 });
            _trips.Add(new Trip() { StartCity = boston, EndCity = sanFrancisco, Payout = 20000 });
        }

        /// <summary>
        /// Rolls a die and returns true if it's odd
        /// </summary>
        /// <returns></returns>
        private bool RollOddEven()
        {
            return random.Next(2) == 1;
        }

        /// <summary>
        /// Rolls two dice and returns the sum
        /// </summary>
        /// <returns></returns>
        private int RollNumeric()
        {
            return random.Next(6) + random.Next(6) + 2;
        }

        /// <summary>
        /// Gets a region given a dice roll
        /// </summary>
        /// <param name="isOdd"></param>
        /// <param name="numeric"></param>
        /// <returns></returns>
        private string GetRegion(bool isOdd, int numeric)
        {
            if (isOdd)
            {
                if (numeric == 2)
                {
                    return "Plains";
                }
                else if (numeric == 3 || numeric == 4 || numeric == 5)
                {
                    return "South East";
                }
                else if (numeric == 6 || numeric == 7)
                {
                    return "North Central";
                }
                else // (numeric == 8 || numeric == 9 || numeric == 10 || numeric == 11 || numeric == 12)
                {
                    return "North East";
                }
            }
            else
            {
                if (numeric == 2 || numeric == 6 || numeric == 7)
                {
                    return "South West";
                }
                else if (numeric == 3 || numeric == 4 || numeric == 5)
                {
                    return "South Central";
                }
                else if (numeric == 6 || numeric == 7)
                {
                    return "South West";
                }
                else if (numeric == 8 || numeric == 11)
                {
                    return "Plains";
                }
                else //(numeric == 9 || numeric == 10 || numeric == 12)
                {
                    return "North West";
                }
            }
        }

        private string GetCity(bool isOdd, int numeric, string region)
        {
            if (region == "North West")
            {
                if (isOdd)
                {
                    if (numeric == 2 || numeric == 3 || numeric == 12)
                    {
                        return "Spokane";
                    }
                    else if (numeric == 4 || numeric == 5 || numeric == 6 || numeric == 7)
                    {
                        return "Seattle";
                    }
                    else if (numeric == 8)
                    {
                        return "Rapid City";
                    }
                    else if (numeric == 9)
                    {
                        return "Casper";
                    }
                    else //if (numeric == 10 || numeric == 11)
                    {
                        return "Billings";
                    }
                }
                else
                {
                    if (numeric == 2)
                    {
                        return "Spokane";
                    }
                    else if (numeric == 3 || numeric == 4 || numeric == 5)
                    {
                        return "Salt Lake City";
                    }
                    else if (numeric == 6 || numeric == 7 || numeric == 8 || numeric == 12)
                    {
                        return "Portland";
                    }
                    else if (numeric == 9)
                    {
                        return "Pocatello";
                    }
                    else //if (numeric == 10 || numeric == 11)
                    {
                        return "Butte";
                    }
                }
            }
            else if (region == "North East")
            {
                if (isOdd)
                {
                    if (numeric == 2 || numeric == 3 || numeric == 4 || numeric == 10 || numeric == 11 || numeric == 12)
                    {
                        return "New York";
                    }
                    else if (numeric == 5)
                    {
                        return "Albany";
                    }
                    else if (numeric == 6 || numeric == 8)
                    {
                        return "Boston";
                    }
                    else if (numeric == 7)
                    {
                        return "Buffalo";
                    }
                    else //if (numeric == 9)
                    {
                        return "Portland";
                    }
                }
                else
                {
                    if (numeric == 2 || numeric == 12)
                    {
                        return "New York";
                    }
                    else if (numeric == 3 || numeric == 7)
                    {
                        return "Washington";
                    }
                    else if (numeric == 4 || numeric == 5)
                    {
                        return "Pittsburgh";
                    }
                    else if (numeric == 6 || numeric == 8)
                    {
                        return "Philadelphia";
                    }
                    else //if (numeric == 9 || numeric == 10 || numeric == 11)
                    {
                        return "Baltimore";
                    }
                }
            }
            else if (region == "South West")
            {
                if (isOdd)
                {
                    if (numeric == 2 || numeric == 3 || numeric == 5)
                    {
                        return "San Diego";
                    }
                    else if (numeric == 4)
                    {
                        return "Reno";
                    }
                    else if (numeric == 6)
                    {
                        return "Sacramento";
                    }
                    else if (numeric == 7)
                    {
                        return "Las Vegas";
                    }
                    else if (numeric == 8 || numeric == 11 || numeric == 12)
                    {
                        return "Phoenix";
                    }
                    else if (numeric == 9)
                    {
                        return "El Paso";
                    }
                    else //if (numeric == 10)
                    {
                        return "Tucumcari";
                    }
                }
                else
                {
                    if (numeric == 2 || numeric == 6 || numeric == 7 || numeric == 8)
                    {
                        return "Los Angeles";
                    }
                    else if (numeric == 3 || numeric == 4 || numeric == 5)
                    {
                        return "Oakland";
                    }
                    else //if (numeric == 9 || numeric == 10 || numeric == 11 || numeric == 12)
                    {
                        return "San Francisco";
                    }
                }
            }
            else if (region == "South East")
            {
                if (isOdd)
                {
                    if (numeric == 2 || numeric == 3)
                    {
                        return "Charlotte";
                    }
                    else if (numeric == 4)
                    {
                        return "Chattanooga";
                    }
                    else if (numeric == 5 || numeric == 6 || numeric == 7)
                    {
                        return "Atlanta";
                    }
                    else if (numeric == 8)
                    {
                        return "Richmond";
                    }
                    else if (numeric == 9 || numeric == 11)
                    {
                        return "Knoxville";
                    }
                    else //if (numeric == 10 || numeric == 12)
                    {
                        return "Mobile";
                    }
                }
                else
                {
                    if (numeric == 2 || numeric == 3 || numeric == 4 || numeric == 12)
                    {
                        return "Norfolk";
                    }
                    else if (numeric == 5)
                    {
                        return "Charleston";
                    }
                    else if (numeric == 6 || numeric == 8)
                    {
                        return "Miami";
                    }
                    else if (numeric == 7)
                    {
                        return "Jacksonville";
                    }
                    else if (numeric == 9 || numeric == 10)
                    {
                        return "Tampa";
                    }
                    else //if (numeric == 11)
                    {
                        return "Mobile";
                    }
                }
            }
            else if (region == "Plains")
            {
                if (isOdd)
                {
                    if (numeric == 2 || numeric == 3 || numeric == 7 || numeric == 8 || numeric == 9)
                    {
                        return "Kansas City";
                    }
                    else if (numeric == 4 || numeric == 5 || numeric == 6)
                    {
                        return "Denver";
                    }
                    else if (numeric == 10 || numeric == 11)
                    {
                        return "Pueblo";
                    }
                    else //if (numeric == 12)
                    {
                        return "Oklahoma City";
                    }
                }
                else
                {
                    if (numeric == 2 || numeric == 7)
                    {
                        return "Oklahoma City";
                    }
                    else if (numeric == 3 || numeric == 5)
                    {
                        return "St. Paul";
                    }
                    else if (numeric == 4 || numeric == 6)
                    {
                        return "Minneapolis";
                    }
                    else if (numeric == 8)
                    {
                        return "Des Moines";
                    }
                    else if (numeric == 9 || numeric == 10)
                    {
                        return "Omaha";
                    }
                    else //if (numeric == 11 || numeric == 12)
                    {
                        return "Fargo";
                    }
                }
            }
            else if (region == "North Central")
            {
                if (isOdd)
                {
                    if (numeric == 2 || numeric == 3 || numeric == 4 || numeric == 5)
                    {
                        return "Cleveland";
                    }
                    else if (numeric == 6 || numeric == 7)
                    {
                        return "Detroit";
                    }
                    else if (numeric == 8)
                    {
                        return "Indianapolis";
                    }
                    else if (numeric == 9 || numeric == 10 || numeric == 12)
                    {
                        return "Milwaukee";
                    }
                    else //if (numeric == 11)
                    {
                        return "Chicago";
                    }
                }
                else
                {
                    if (numeric == 2 || numeric == 4 || numeric == 5)
                    {
                        return "Cincinnati";
                    }
                    else if (numeric == 3 || numeric == 7 || numeric == 8 || numeric == 12)
                    {
                        return "Chicago";
                    }
                    else if (numeric == 6)
                    {
                        return "Columbus";
                    }
                    else //if (numeric == 9 || numeric == 10 || numeric == 11)
                    {
                        return "St. Louis";
                    }
                }
            }
            else // if (region == "South Central")
            {
                if (isOdd)
                {
                    if (numeric == 2 || numeric == 3 || numeric == 4 || numeric == 12)
                    {
                        return "Memphis";
                    }
                    else if (numeric == 5)
                    {
                        return "Little Rock";
                    }
                    else if (numeric == 6)
                    {
                        return "New Orleans";
                    }
                    else if (numeric == 7)
                    {
                        return "Birmingham";
                    }
                    else if (numeric == 8 || numeric == 11)
                    {
                        return "Louisville";
                    }
                    else //if (numeric == 9 || numeric == 10)
                    {
                        return "Nashville";
                    }
                }
                else
                {
                    if (numeric == 2 || numeric == 3)
                    {
                        return "Shreveport";
                    }
                    else if (numeric == 4 || numeric == 6)
                    {
                        return "Dallas";
                    }
                    else if (numeric == 5)
                    {
                        return "New Orleans";
                    }
                    else if (numeric == 7)
                    {
                        return "San Antonio";
                    }
                    else if (numeric == 8 || numeric == 9)
                    {
                        return "Houston";
                    }
                    else //if (numeric == 10 || numeric == 11 || numeric == 12)
                    {
                        return "Fort Worth";
                    }
                }
            }
        }

        /// <summary>
        /// Called when the roll region button is clicked
        /// </summary>
        public void RollRegionButton_Click()
        {
            RollRegionButtonEnabled = false;

            bool oddEvenRoll = RollOddEven();
            int numericRoll = RollNumeric();
            if (oddEvenRoll)
            {
                RegionDiceRoll = "Odd ";
            }
            else
            {
                RegionDiceRoll = "Even ";
            }
            RegionDiceRoll += numericRoll.ToString();
            RolledRegion = GetRegion(oddEvenRoll, numericRoll);
            RollCityButtonEnabled = true;
        }

        /// <summary>
        /// Called when the roll city button is clicked
        /// </summary>
        public void RollCityButton_Click()
        {
            RollCityButtonEnabled = false;
            bool oddEvenRoll = RollOddEven();
            int numericRoll = RollNumeric();
            if (oddEvenRoll)
            {
                CityDiceRoll = "Odd ";
            }
            else
            {
                CityDiceRoll = "Even ";
            }
            CityDiceRoll += numericRoll.ToString();
            RolledCity = GetCity(oddEvenRoll, numericRoll, RolledRegion);
        }

        /// <summary>
        /// Called when the reset button is clicked
        /// </summary>
        public void ResetButton_Click()
        {
            RollRegionButtonEnabled = true;
            RegionDiceRoll = "None";
            RolledRegion = "None";
            RollCityButtonEnabled = false;
            CityDiceRoll = "None";
            RolledCity = "None";
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
        #endregion
    }
}
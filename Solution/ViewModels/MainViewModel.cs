using System;
using System.Collections.Generic;
using System.ComponentModel;
using RailBaron.Models;


namespace RailBaron
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
        /// <param name="oddEven"></param>
        /// <param name="numeric"></param>
        /// <returns></returns>
        private string GetRegion(bool oddEven, int numeric)
        {
            if (numeric == 1)
            {
                return "Northwest";
            }
            else if (numeric == 2)
            {
                return "Northeast";
            }
            else if (numeric == 3)
            {
                return "Plains";
            }
            else if (numeric == 4)
            {
                return "Southwest";
            }
            else if (numeric == 5)
            {
                return "Southeast";
            }
            else
            {
                return "North Central";
            }
        }

        private string GetCity(bool oddEven, int numeric, string region)
        {
            if (region == "Northwest")
            {
                return "Seattle";
            }
            else if (region == "Northeast")
            {
                return "Boston";
            }
            else if (region == "Southwest")
            {
                return "Los Angeles";
            }
            else if (region == "Southeast")
            {
                return "Tampa";
            }
            else if (region == "Plains")
            {
                return "Nashville";
            }
            else
            {
                return "Des Moines";
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
            RegionDiceRoll = oddEvenRoll.ToString() + " " + numericRoll.ToString();
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
            CityDiceRoll = oddEvenRoll.ToString() + " " + numericRoll.ToString();
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
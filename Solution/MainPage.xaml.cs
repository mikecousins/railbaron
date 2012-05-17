using Microsoft.Phone.Controls;

namespace RailBaron
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            DataContext = App.ViewModel;
        }

        private void ResetButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            App.ViewModel.ResetButton_Click();
        }

        private void RollCityButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            App.ViewModel.RollCityButton_Click();
        }

        private void RollRegionButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            App.ViewModel.RollRegionButton_Click();
        }
    }
}
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using weatherwpf.Data;

namespace weatherwpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Weather> _days;
        private List<SortType> _sortTypes;
        private List<WeatherStatus> _statuses;

        private List<WeatherStatus> _addStatus;

        public MainWindow()
        {
            InitializeComponent();

            _days = Data.DataContext.Days;
            _sortTypes = Data.DataContext.SortTypes;
            _statuses = Data.DataContext.Statuses;
            _addStatus = Data.DataContext.Statuses;
            _addStatus.RemoveAt(3);


            LstView.ItemsSource = _days;
            FilterCB.ItemsSource = _statuses;
            SortCB.ItemsSource = _sortTypes;
            StatusAddCB.ItemsSource = _addStatus;
        }

        private void FilterCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var filter = _days;

            var status = (WeatherStatus)FilterCB.SelectedItem;

            if (status == null) return;

            if (status.Title != "По умолчанию")
            {
                filter = filter.Where(x => x.weatherStatus.Id == status.Id).ToList();
            }
            else
            {
                filter = _days;
            }

            LstView.ItemsSource = filter;
            LstView.Items.Refresh();
        }

        private void SortCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sorted = _days;

            var type = (SortType)SortCB.SelectedItem;

            if (type == null) return;

            if (type.SortTitle == "По умолчанию")
            {
                sorted = _days;
            }
            else if (type.SortTitle == "По возрастанию даты")
            {
                sorted = _days.OrderBy(x => x.dateTime).ToList();
            }
            else if (type.SortTitle == "По убыванию даты")
            {
                sorted = _days.OrderByDescending(x => x.dateTime).ToList();
            }
            else if (type.SortTitle == "По возрастанию температуры")
            {
                sorted = _days.OrderBy(x => x.Temp).ToList();
            }
            else if (type.SortTitle == "По убыванию температуры")
            {
                sorted = _days.OrderByDescending(x => x.Temp).ToList();
            }

            LstView.ItemsSource = sorted;
            LstView.Items.Refresh();
        }

        private void AddDayBTN_Click(object sender, RoutedEventArgs e)
        {
            bool dayReq = true;
            bool tempReq = true;
            bool statusReq = false;

            if (DateAddValue.Text == null && TempAddValue.Text == null && StatusAddCB.SelectedItem == null)
            {
                return;
            }

            if (Convert.ToInt32(DateAddValue.Text?.Substring(0, 2)) < 32 && Convert.ToInt32(DateAddValue.Text?.Substring(0, 2)) > 0
                && Convert.ToInt32(DateAddValue.Text?.Substring(3, 2)) < 13 && Convert.ToInt32(DateAddValue.Text?.Substring(3, 2)) > 0
                && Convert.ToInt32(DateAddValue.Text?.Substring(6, 4)) < 2100 && Convert.ToInt32(DateAddValue.Text?.Substring(6, 4)) > 1999)
            {
                dayReq = true;
            } 
            else
            {
                dayReq = false;
            }

            if (Convert.ToInt32(TempAddValue.Text) > -100 && Convert.ToInt32(TempAddValue.Text) < 100)
            {
                tempReq = true;
            }
            else
            {
                tempReq = false;
            }

            if (StatusAddCB.SelectedItem == null)
            {
                statusReq = false;
            }
            else
            {
                statusReq = true;
            }

            if (dayReq && tempReq && statusReq)
            {
                _days.Add(new Weather(DateAddValue.Text,
                    Convert.ToInt32(TempAddValue.Text),
                    (WeatherStatus)StatusAddCB.SelectedItem));

                DateAddValue.Text = "01.01.2001";
                TempAddValue.Text = "0";
                StatusAddCB.SelectedItem = null;
            }
            else
            {
                MessageBox.Show("Incorrect date, temperature or status.");
            }

            LstView.ItemsSource = _days;
            LstView.Items.Refresh();
        }

        private void ResetSumBTN_Click(object sender, RoutedEventArgs e)
        {
            MaxEqualsCount();

            TempArithmeticMean();

            TempAnomalies();

            MaxAndMinTemps();
        }

        public void MaxEqualsCount()
        {
            var dayTemps = _days;
            int maxCount = 0;

            Dictionary<int, int> tempCount = new Dictionary<int, int>();

            foreach (var day in dayTemps)
            {
                int temperature = day.Temp;
                if (tempCount.ContainsKey(temperature))
                {
                    tempCount[temperature]++;
                }
                else
                {
                    tempCount[temperature] = 1;
                }
            }

            foreach (var count in tempCount.Values)
            {
                if (count > maxCount)
                {
                    maxCount = count;
                }
            }

            numOfEquals.Text = maxCount.ToString();
        }
        public void TempArithmeticMean()
        {
            int s = 0;

            for (int i = 0; i < _days.Count; i++)
            {
                s += _days[i].Temp;
            }

            tempSum.Text = (s / _days.Count).ToString();
        }
        public void TempAnomalies()
        {
            string anP = "";
            string anM = "";

            for (int j = 1; j < _days.Count; j++)
            {
                if ((_days[j].Temp - _days[j - 1].Temp) >= 10)
                {
                    anP += " " + _days[j].dateTime;
                }
                else if ((_days[j].Temp - _days[j - 1].Temp) <= -10)
                {
                    anM += " " + _days[j].dateTime;
                }
                else
                {
                    anP = anP;
                    anM = anM;
                }
            }

            anomPlus.Text = anP;
            anomMinus.Text = anM;
        }
        public void MaxAndMinTemps()
        {
            sumMax.Text = _days.Max(x => x.Temp).ToString();
            sumMin.Text = _days.Min(x => x.Temp).ToString();
        }
    }
}
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

        public MainWindow()
        {
            InitializeComponent();

            _days = Data.DataContext.Days;
            _sortTypes = Data.DataContext.SortTypes;
            _statuses = Data.DataContext.Statuses;

            LstView.ItemsSource = _days;
        }
    }
}
using LiveCharts;
using LiveCharts.Wpf;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pp4course
{
    /// <summary>
    /// Логика взаимодействия для GraphStars.xaml
    /// </summary>
    public partial class GraphStars : Page
    {
        DataTable dataTable = new DataTable();
        List<string> list = new List<string>();
        List<double> list1 = new List<double>();
        int ID;
        
        static string constr = "server=localhost;user=root;database=pp4course;password=;convert zero datetime=True;";
        MySqlConnection connect = new MySqlConnection(constr);
        MainWindow Mw = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        MySqlCommand command = new MySqlCommand();
        public GraphStars(int iD)
        {
            InitializeComponent();
            ID = iD;
            connect.Open();
            command = new MySqlCommand($"select Star_Name, Star_Brightness FROM Star", connect);
            dataTable = new DataTable();
            dataTable.Load(command.ExecuteReader());
            MySqlDataReader dataReader = null;
            dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                list.Add(dataReader[$@"Star_Name"].ToString());
                list1.Add(Convert.ToDouble(dataReader[$@"Star_Brightness"]));
            }

            SeriesCollection = new SeriesCollection
            {
            new ColumnSeries
{
            Title="Светимость",
            Values = new ChartValues<double>(list1),
            }
            };

            BarLabels = new string[list.Count];
            for (int i = 0; i < BarLabels.Length; i++)
                BarLabels[i] = list[i];
            Formatter = values => values.ToString("N");
            DataContext = this;
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] BarLabels { get; set; }

        public Func<double, string> Formatter { get; set; }
    


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new Stars(ID));
        }
    }
}

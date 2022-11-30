using MySql.Data.MySqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace pp4course
{
    /// <summary>
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        int ID;
        static string constr = "server=localhost;user=root;database=pp4course;password=;";
        MySqlConnection connect = new MySqlConnection(constr);
        MainWindow Mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        /// <summary>
        /// Инициализация окна
        /// </summary>
        /// <param name="iD_POST">Код сотрудника</param>
        public Menu(int iD_POST)
        {
            InitializeComponent();
            ID = iD_POST;
            
        }
        /// <summary>
        /// Метод обновления окна
        /// </summary>

        /// <summary>
        /// Переход на окно авторизации
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет значение для событий, не содержащих данных.</param>
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new Auth());
        }


        private void btn_stars_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new Stars(ID));
        }

        private void btn_planets_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new Planets(ID));
        }

        private void btn_galaxies_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new Galaxies(ID));
        }
    }
}

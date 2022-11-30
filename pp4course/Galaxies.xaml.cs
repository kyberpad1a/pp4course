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
    /// Логика взаимодействия для Galaxies.xaml
    /// </summary>
    public partial class Galaxies : Page
    {
        int ID;
        DataTable dt = new DataTable();
        static string constr = "server=localhost;user=root;database=pp4course;password=;convert zero datetime=True;";
        MySqlConnection connect = new MySqlConnection(constr);
        MainWindow Mw = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        /// <summary>
        /// Инициализация окна, заполнение датагрида, настройка оповещений
        /// </summary>
        /// <param name="iD">Код сотрудника</param>
        public Galaxies(int iD)
        {
            InitializeComponent();
            ID = iD;
            connect.Open();
            MySqlCommand command = new MySqlCommand($@"select ID_Galaxy as 'Код галактики', Galaxy_Name as 'Название галактики', Galaxy_Distance as 'Расстояние', Galaxy_Spectre as 'Спектр излучения', Galaxy_RotationSpeed as 'Скорость вращения', concat(Employee.Employee_Surname,' ',Employee.Employee_Name) as 'Добавил' FROM Galaxy join Employee on Employee_ID=ID_Employee", connect);

            dt.Load(command.ExecuteReader());
            dg_galaxies.ItemsSource = dt.DefaultView;
            connect.Close();

        }

        /// <summary>
        /// Обновление датагрида
        /// </summary>
        public void Refresh()
        {

            connect.Open();
            MySqlCommand command = new MySqlCommand($@"select ID_Galaxy as 'Код галактики', Galaxy_Name as 'Название галактики', Galaxy_Distance as 'Расстояние', Galaxy_Spectre as 'Спектр излучения', Galaxy_RotationSpeed as 'Скорость вращения', concat(Employee.Employee_Surname,' ',Employee.Employee_Name) as 'Добавил' FROM Galaxy join Employee on Employee_ID=ID_Employee", connect);
            dt = new DataTable();
            dt.Load(command.ExecuteReader());
            dg_galaxies.ItemsSource = dt.DefaultView;

            connect.Close();

        }
        /// <summary>
        /// Переход на окно добавления задачи
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет значение для событий, не содержащих данных.</param>
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (tb_name.Text != null && tb_distance.Text != null && tb_spectre.Text != null && tb_rotationspeed.Text != null)
            {


                connect.Open();
                MySqlCommand command = new MySqlCommand($@"Call Galaxy_Insert('{tb_name.Text}','{tb_distance.Text}', '{tb_spectre.Text}', '{tb_rotationspeed.Text}', '{ID}')", connect);
                command.ExecuteNonQuery();

                connect.Close();
                Refresh();

            }
        }
        /// <summary>
        /// Обновление
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет значение для событий, не содержащих данных.</param>
        private void btn_upd_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)dg_galaxies.SelectedItem;
            if (row != null)
            {
                if (tb_name.Text != null && tb_distance.Text != null && tb_spectre.Text != null && tb_rotationspeed.Text != null)
                {
                    connect.Open();
                    MySqlCommand command2 = new MySqlCommand($@"Call Galaxy_Update({(int)row["Код галактики"]}, '{tb_name.Text}','{tb_distance.Text}', '{tb_spectre.Text}', '{tb_rotationspeed.Text}')", connect);
                    command2.ExecuteNonQuery();

                    connect.Close();
                    Refresh();
                }
            }
        }
        /// <summary>
        /// Удаление задачи
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет значение для событий, не содержащих данных.</param>
        private void btn_del_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)dg_galaxies.SelectedItem;
            if (row != null)
            {
                connect.Open();



                string com = $@"call Galaxy_Delete ({(int)row["Код галактики"]})";
                MySqlCommand command2 = new MySqlCommand(com, connect);
                command2.ExecuteNonQuery();

                connect.Close();
                Refresh();

            }
        }


        /// <summary>
        /// Переход на окно меню
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет значение для событий, не содержащих данных.</param>
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new Auth());
        }

        private void tb_src_TextChanged(object sender, TextChangedEventArgs e)
        {
            connect.Open();
            string com = $@"select ID_Galaxy as 'Код галактики', Galaxy_Name as 'Название галактики', Galaxy_Distance as 'Расстояние', Galaxy_Spectre as 'Спектр излучения', Galaxy_RotationSpeed as 'Скорость вращения', concat(Employee.Employee_Surname,' ',Employee.Employee_Name) as 'Добавил' FROM Galaxy join Employee on Employee_ID=ID_Employee where Galaxy.Galaxy_Name like '%{tb_src.Text}%'";
            MySqlCommand command = new MySqlCommand(com, connect);
            DataTable datatbl = new DataTable();
            datatbl.Load(command.ExecuteReader());
            dg_galaxies.ItemsSource = datatbl.DefaultView;
            connect.Close();
        }
    }
}

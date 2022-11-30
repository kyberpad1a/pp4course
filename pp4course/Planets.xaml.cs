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
    /// Логика взаимодействия для Planets.xaml
    /// </summary>
    public partial class Planets : Page
    {
        int ID;
        DataTable dt = new DataTable();
        static string constr = "server=localhost;user=root;database=pp4course;password=;convert zero datetime=True;";
        MySqlConnection connect = new MySqlConnection(constr);
        MainWindow Mw = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        /// <summary>
        /// Инициализация окна, заполнение датагрид
        /// </summary>
        /// <param name="iD">Код сотрудника</param>
        public Planets(int iD)
        {
            InitializeComponent();
            ID = iD;
            connect.Open();
            MySqlCommand command = new MySqlCommand($@"select ID_Planet as 'Код планеты', Planet_Name as 'Название планеты', Planet_Mass as 'Масса звезды', Planet_Radius as 'Радиус', Planet_CoreMaterial as 'Материал ядра', concat(Employee.Employee_Surname,' ',Employee.Employee_Name) as 'Добавил' FROM Planet join Employee on Employee_ID=ID_Employee", connect);

            dt.Load(command.ExecuteReader());
            dg_planets.ItemsSource = dt.DefaultView;
            connect.Close();
            
        }
        
        /// <summary>
        /// Обновление датагрида
        /// </summary>
        public void Refresh()
        {

            connect.Open();
            MySqlCommand command = new MySqlCommand($@"select ID_Planet as 'Код планеты', Planet_Name as 'Название планеты', Planet_Mass as 'Масса звезды', Planet_Radius as 'Радиус', Planet_CoreMaterial as 'Материал ядра', concat(Employee.Employee_Surname,' ',Employee.Employee_Name) as 'Добавил' FROM Planet join Employee on Employee_ID=ID_Employee", connect);
            dt = new DataTable();
            dt.Load(command.ExecuteReader());
            dg_planets.ItemsSource = dt.DefaultView;

            connect.Close();

        }
        /// <summary>
        /// Переход на окно добавления задачи
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет значение для событий, не содержащих данных.</param>
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (tb_name.Text != null && tb_mass.Text != null && tb_radius.Text != null && tb_corematerial.Text != null)
            {


                connect.Open();
                MySqlCommand command = new MySqlCommand($@"Call Planet_Insert('{tb_name.Text}','{tb_mass.Text}', '{tb_radius.Text}', '{tb_corematerial.Text}', '{ID}')", connect);
                command.ExecuteNonQuery();

                connect.Close();
                Refresh();
            }
        }
        /// <summary>
        /// Переход на окно обновления задачи
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет значение для событий, не содержащих данных.</param>
        private void btn_upd_Click(object sender, RoutedEventArgs e)
        {
            DataRowView row = (DataRowView)dg_planets.SelectedItem;
            if (row != null)
            {
                if (tb_name.Text != null && tb_mass.Text != null && tb_radius.Text != null && tb_corematerial.Text != null)
                {
                    connect.Open();
                    MySqlCommand command2 = new MySqlCommand($@"Call Planet_Update({(int)row["Код планеты"]}, '{tb_name.Text}','{tb_mass.Text}', '{tb_radius.Text}', '{tb_corematerial.Text}')", connect);
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
            DataRowView row = (DataRowView)dg_planets.SelectedItem;
            if (row != null)
            {
                connect.Open();



                string com = $@"call Planet_Delete ({(int)row["Код планеты"]})";
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
            string com = $@"select ID_Planet as 'Код планеты', Planet_Name as 'Название планеты', Planet_Mass as 'Масса звезды', Planet_Radius as 'Радиус', Planet_CoreMaterial as 'Материал ядра', concat(Employee.Employee_Surname,' ',Employee.Employee_Name) as 'Добавил' FROM Planet join Employee on Employee_ID=ID_Employee where Planet.Planet_Name like '%{tb_src.Text}%'";
            MySqlCommand command = new MySqlCommand(com, connect);
            DataTable datatbl = new DataTable();
            datatbl.Load(command.ExecuteReader());
            dg_planets.ItemsSource = datatbl.DefaultView;
            connect.Close();
        }
    }
}

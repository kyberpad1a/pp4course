using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        static string constr = "server=localhost;user=root;database=pp4course;password=;";
        MySqlConnection connect = new MySqlConnection(constr);
        MySqlConnection connect2 = new MySqlConnection(constr);
        MySqlConnection connect3 = new MySqlConnection(constr);
        MainWindow Mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        /// <summary>
        /// Инициализация окна
        /// </summary>
        public Auth()
        {

            InitializeComponent();
        }
        /// <summary>
        /// Процедура авторизации
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет значение для событий, не содержащих данных.</param>
        private void btn_auth_Click(object sender, RoutedEventArgs e)
        {
            connect.Open();
            MySqlCommand employee = new MySqlCommand($"select count(*) from Employee where Employee_Login = '{tb_login.Text}' and Employee_Password = '{tb_password.Password}'", connect);

            if (employee.ExecuteScalar().ToString() == "1")
            {
                MySqlCommand employeeid = new MySqlCommand($"select ID_Employee from Employee where Employee_Login = '{tb_login.Text}' and Employee_Password = '{tb_password.Password}'", connect);
                MySqlCommand postid = new MySqlCommand($"select Post_ID from Employee where Employee_Login = '{tb_login.Text}' and Employee_Password = '{tb_password.Password}'", connect);
                int ID_EMPLOYEE = (int)employeeid.ExecuteScalar();
                if (postid.ExecuteScalar().ToString() == "1")
                {
                    
                    
                    Mw.MainFrame.NavigationService.Navigate(new Menu(ID_EMPLOYEE));
                }
                if (employeeid.ExecuteScalar().ToString() == "2")
                {
                    
                    Mw.MainFrame.NavigationService.Navigate(new Stars(ID_EMPLOYEE));
                }
                if (employeeid.ExecuteScalar().ToString() == "3")
                {
                    
                    Mw.MainFrame.NavigationService.Navigate(new Planets(ID_EMPLOYEE));
                }
                if (employeeid.ExecuteScalar().ToString() == "4")
                {
                    
                    Mw.MainFrame.NavigationService.Navigate(new Galaxies(ID_EMPLOYEE));
                }
            }
            else
            {
                MessageBox.Show("Проверьте введенные данные");
            }
            connect.Close();


        }
        /// <summary>
        /// Переход на окно регистрации
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет значение для событий, не содержащих данных.</param>
        private void btn_reg_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new Reg());
        }
    }
}

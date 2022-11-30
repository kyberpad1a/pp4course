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
    /// Логика взаимодействия для Reg.xaml
    /// </summary>
    public partial class Reg : Page
    {
        static string constr = "server=localhost;user=root;database=pp4course;password=;";
        MySqlConnection connect = new MySqlConnection(constr);
        MainWindow Mw = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        private void BindComboBox()
        {

            connect.Open();
            DataTable datatbl1 = new DataTable();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter("select * from Post", connect);
            mySqlDataAdapter.Fill(datatbl1);
            cb_post.ItemsSource = datatbl1.DefaultView;
            cb_post.DisplayMemberPath = "Post_name";
            cb_post.SelectedValuePath = "ID_Post";
            

            connect.Close();

        }
        /// <summary>
        /// Инициализация окна
        /// </summary>
        public Reg()
        {
            InitializeComponent();
            BindComboBox();
        }
        /// <summary>
        /// Регистрация
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет значение для событий, не содержащих данных.</param>
        private void btn_reg_Click(object sender, RoutedEventArgs e)
        {
            if (IsValidPassword(tb_password.Text) != true)
            {
                MessageBox.Show("Пароль не соответствет условию");
                return;
            }
            connect.Open();
            MySqlCommand com = new MySqlCommand("", connect);
            com = new MySqlCommand($"select count(*) from Employee where Employee_Login = '{tb_login.Text}'", connect);
            if (com.ExecuteScalar().ToString() == "1")
            {
                MessageBox.Show("Такой логин уже существует");
                return;
            }
            if (tb_login.Text != "" && tb_password.Text != "" && tb_name.Text != "" && tb_surname.Text != "")
            {
                MySqlCommand command = new MySqlCommand($@"Call Employee_Insert('{tb_surname.Text}','{tb_name.Text}','{tb_patronymic.Text}','{tb_login.Text}', '{tb_password.Text}', '{cb_post.SelectedValue}')", connect);
                command.ExecuteNonQuery();
                connect.Close();
            }
            Mw.MainFrame.NavigationService.Navigate(new Auth());
        }
        /// <summary>
        /// Переход на окно авторизации
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет значение для событий, не содержащих данных.</param>
        private void btn_back_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new Auth());
        }
        /// <summary>
        /// Метод проверки пароля
        /// </summary>
        /// <param name="pswd">Пароль</param>
        /// <returns>True если соответствует условиям false если нет</returns>
        public static bool IsValidPassword(string pswd)
        {


            bool b1 = pswd.Length >= 8;


            bool b3 = false;
            foreach (char c in pswd)
            {
                if (Char.IsDigit(c))
                {
                    b3 = true;
                    break;
                }
            }

            return b1 && b3;
        }
    }
}

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
    /// Логика взаимодействия для Stars.xaml
    /// </summary>
    public partial class Stars : Page
    {
        int ID;
        DataTable dt = new DataTable();
        static string constr = "server=localhost;user=root;database=pp4course;password=;convert zero datetime=True;";
        MySqlConnection connect = new MySqlConnection(constr);
        MainWindow Mw = System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
        Microsoft.Office.Interop.Excel.Application excel;
        Microsoft.Office.Interop.Excel.Workbook workBook;
        Microsoft.Office.Interop.Excel.Worksheet workSheet;
        Microsoft.Office.Interop.Excel.Range cellRange;
        /// <summary>
        /// Инициализация окна, заполнение датагрида, настройка оповещений
        /// </summary>
        /// <param name="iD">Код сотрудника</param>
        public Stars(int iD)
        {
            InitializeComponent();
            ID = iD;
            connect.Open();
            MySqlCommand command = new MySqlCommand($@"select ID_Star as 'Код звезды', Star_Name as 'Название звезды', Star_Mass as 'Масса звезды', Star_Radius as 'Радиус', Star_Brightness as 'Светимость', Star_Magnetization as 'Магнетизм', StarClass_name as 'Класс', concat(Employee.Employee_Surname,' ',Employee.Employee_Name) as 'Добавил' FROM Star join StarClass on StarClass_ID=ID_StarClass join Employee on Employee_ID=ID_Employee", connect);

            dt.Load(command.ExecuteReader());
            dg_stars.ItemsSource = dt.DefaultView;
           connect.Close();
            BindComboBox();
        }
        private void BindComboBox()
        {

            connect.Open();
            DataTable datatbl1 = new DataTable();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter("select * from StarClass", connect);
            mySqlDataAdapter.Fill(datatbl1);
            cb_class.ItemsSource = datatbl1.DefaultView;
            cb_class.DisplayMemberPath = "StarClass_name";
            cb_class.SelectedValuePath = "ID_StarClass";


            connect.Close();

        }
        /// <summary>
        /// Обновление датагрида
        /// </summary>
        public void Refresh()
        {

            connect.Open();
            MySqlCommand command = new MySqlCommand($@"select ID_Star as 'Код звезды', Star_Name as 'Название звезды', Star_Mass as 'Масса звезды', Star_Radius as 'Радиус', Star_Brightness as 'Светимость', Star_Magnetization as 'Магнетизм', StarClass_name as 'Класс', concat(Employee.Employee_Surname,' ',Employee.Employee_Name) as 'Добавил' FROM Star join StarClass on StarClass_ID=ID_StarClass join Employee on Employee_ID=ID_Employee", connect);
            dt = new DataTable();
            dt.Load(command.ExecuteReader());
            dg_stars.ItemsSource = dt.DefaultView;
           
            connect.Close();

        }
        /// <summary>
        /// Переход на окно добавления задачи
        /// </summary>
        /// <param name="sender">ссылка на элемент управления/объект, вызвавший событие</param>
        /// <param name="e">экземпляр класса для классов, содержащих данные событий, и предоставляет значение для событий, не содержащих данных.</param>
        private void btn_add_Click(object sender, RoutedEventArgs e)
        {
            if (tb_name.Text != null && tb_mass.Text != null && tb_radius.Text != null && tb_brightness.Text != null && tb_magnetization.Text != null && cb_class.SelectedValue != null)
            {

                
               connect.Open();
                MySqlCommand command = new MySqlCommand($@"Call Star_Insert('{tb_name.Text}','{tb_mass.Text}', '{tb_radius.Text}', '{tb_brightness.Text}', '{tb_magnetization.Text}', '{ID}', '{cb_class.SelectedValue}')", connect);
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
            DataRowView row = (DataRowView)dg_stars.SelectedItem;
            if (row != null)
            {
                if (tb_name.Text != null && tb_mass.Text != null && tb_radius.Text != null && tb_brightness.Text != null && tb_magnetization.Text != null && cb_class.SelectedValue != null)
                {
                   connect.Open();
                    MySqlCommand command2 = new MySqlCommand($@"Call Star_Update({(int)row["Код звезды"]}, '{tb_name.Text}', '{tb_mass.Text}', '{tb_radius.Text}', '{tb_brightness.Text}', '{tb_magnetization.Text}', '{cb_class.SelectedValue}')", connect);
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
            
            
                DataRowView row = (DataRowView)dg_stars.SelectedItem;
                if (row != null)
                {
                   connect.Open();



                    string com = $@"call Star_Delete ({(int)row["Код звезды"]})";
                    MySqlCommand command2 = new MySqlCommand(com, connect);
                    command2.ExecuteNonQuery();

                    connect.Close();
                }
            
                
                Refresh();
                MessageBox.Show("A");
            

            
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
            string com = $@"select ID_Star as 'Код звезды', Star_Name as 'Название звезды', Star_Mass as 'Масса звезды', Star_Radius as 'Радиус', Star_Brightness as 'Светимость', Star_Magnetization as 'Магнетизм', StarClass_name as 'Класс', concat(Employee.Employee_Surname,' ',Employee.Employee_Name) as 'Добавил' FROM Star join StarClass on StarClass_ID=ID_StarClass join Employee on Employee_ID=ID_Employee where  Star.Star_Name like '%{tb_src.Text}%'";
            MySqlCommand command = new MySqlCommand(com, connect);
            DataTable datatbl = new DataTable();
            datatbl.Load(command.ExecuteReader());
            dg_stars.ItemsSource = datatbl.DefaultView;
            connect.Close();

        }

        private void btn_exp_Click(object sender, RoutedEventArgs e)
        {
            GenerateExcel(dt);
        }

        private void btn_graph_Click(object sender, RoutedEventArgs e)
        {
            Mw.MainFrame.NavigationService.Navigate(new GraphStars(ID));
        }

        private void GenerateExcel(DataTable DtIN)
        {
            try
            {
                excel = new Microsoft.Office.Interop.Excel.Application();
                excel.DisplayAlerts = false;
                excel.Visible = false;
                workBook = excel.Workbooks.Add(Type.Missing);
                workSheet = (Microsoft.Office.Interop.Excel.Worksheet)workBook.ActiveSheet;
                workSheet.Name = "LearningExcel";
                System.Data.DataTable tempDt = DtIN;
                dg_stars.ItemsSource = tempDt.DefaultView;
                workSheet.Cells.Font.Size = 11;
                int rowcount = 1;
                for (int i = 1; i <= tempDt.Columns.Count; i++)   
                {
                    workSheet.Cells[1, i] = tempDt.Columns[i - 1].ColumnName;
                }
                foreach (System.Data.DataRow row in tempDt.Rows)  
                {
                    rowcount += 1;
                    for (int i = 0; i < tempDt.Columns.Count; i++) 
                    {
                        workSheet.Cells[rowcount, i + 1] = row[i].ToString();
                    }
                }
                cellRange = workSheet.Range[workSheet.Cells[1, 1], workSheet.Cells[rowcount, tempDt.Columns.Count]];
                cellRange.EntireColumn.AutoFit();
                excel.Visible = true;
                excel.UserControl = true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

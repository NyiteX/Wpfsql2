using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace Wpfsql2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string str = @"Data Source = WIN-U669V8L9R5E; Initial Catalog = Dz; Trusted_Connection=True";
        public MainWindow()
        {
            InitializeComponent();
        }

        async void Save()
        {
            try
            {
                if (Name_TB.Text != "First Name" && Email_TB.Text != "Email" && Login_TB.Text != "Login" && Pass_TB.Text != "Password" && Proverka_gmail(Email_TB.Text))
                {
                    using (SqlConnection connection = new SqlConnection(str))
                    {
                        await connection.OpenAsync();
                        SqlCommand command = new SqlCommand();

                        command.CommandText = "INSERT INTO LoginsINI VALUES('" + Name_TB.Text + "','" + Login_TB.Text + "','" + Pass_TB.Text + "','" + Email_TB.Text + "')";
                        command.Connection = connection;

                        await command.ExecuteNonQueryAsync();
                    }
                    MessageBox.Show("Добавлено");
                }
                else
                    MessageBox.Show("Неверно заполненые поля");
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
        }
        bool Proverka_gmail(string str)
        {
            bool b = false;          
            if (str.Length > 10)
            {
                string test = "@gmail.com";
                string tmp = "";
                bool b2 = false;
                for (int i = 0; i < str.Length - 10; i++)
                    if (!char.IsDigit(str[i]) && !char.IsLetter(str[i]) && str[i] != '.')
                    {
                        MessageBox.Show(str[i].ToString());
                        b2 = true;
                        break;
                    }
                if (!b2)
                {
                    for (int i = str.Length - 10; i < str.Length; i++)
                        tmp += str[i];

                    if (tmp == test) b = true;
                    MessageBox.Show(tmp + '\n' + test);
                }
            }
            return b;
        }
        string Proverka_probel(string str)
        {
            str = str.Replace(" ","");                ;

            return str;
        }

        bool Proverka_kol(string str)
        {
            bool b = false;
            for (int i = 0; i < str.Length; i++)
                if (str[i] != ' ') b = true;           

            return b;
        }

        private void Name_TB_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Name_TB.Text == "First Name")
                Name_TB.Text = "";
        }

        private void Name_TB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Name_TB.Text == "" || !Proverka_kol(Name_TB.Text))
                Name_TB.Text = "First Name";
            Name_TB.Text = Proverka_probel(Name_TB.Text);
        }

        private void Email_TB_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Email_TB.Text == "Email")
                Email_TB.Text = "";
        }

        private void Email_TB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Email_TB.Text == "" || !Proverka_kol(Email_TB.Text))
                Email_TB.Text = "Email";
            Email_TB.Text = Proverka_probel(Email_TB.Text);
        }

        private void Login_TB_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Login_TB.Text == "Login")
                Login_TB.Text = "";
        }

        private void Login_TB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Login_TB.Text == "" || !Proverka_kol(Login_TB.Text))
                Login_TB.Text = "Login";
            Login_TB.Text = Proverka_probel(Login_TB.Text);
        }

        private void Pass_TB_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Pass_TB.Text == "Password")
                Pass_TB.Text = "";
        }

        private void Pass_TB_LostFocus(object sender, RoutedEventArgs e)
        {
            if (Pass_TB.Text == "" || !Proverka_kol(Pass_TB.Text))
                Pass_TB.Text = "Password";
            Pass_TB.Text = Proverka_probel(Pass_TB.Text);
        }

        private void BTN_Login_Click(object sender, RoutedEventArgs e)
        {
            Save();
        }
    }
}

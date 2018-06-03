using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
using InformationSecurity.Models;

namespace InformationSecurity
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int authCount = 0;
        private System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Subject subject = Methods.Subjects.Authorization(textBox1.Text, passBox1.Password);

            if (null != subject && CheckDay(subject) && CheckTime(subject))
            {
                MessageBox.Show("Authorization Complete!");
                if(subject.RoleId == 1)
                {
                    Admin admin = new Admin(subject);
                    admin.Show();
                }
                else
                {
                    try
                    {
                        MainProgram mainProgram = new MainProgram(subject);
                        mainProgram.Show();
                    }
                    catch { }
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Error! Check your input data!");
                authCount++;
                if(authCount == 3)
                {
                    button1.IsEnabled = false;   
                    timer.Tick += new EventHandler(blockSubject);
                    timer.Interval = new TimeSpan(0, 0, 10);
                    timer.Start();
                    authCount = 0;
                }
            }

        }

        private void blockSubject(object sender, EventArgs e)
        {
            button1.IsEnabled = true;
            timer.Stop();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            SignUp signUp = new SignUp();
            signUp.Show();
        }

        private bool CheckDay(Subject subject)
        {
            try
            {
                DayOfWeek currDay = DateTime.Now.DayOfWeek;
                int intDay = (int)currDay > 0 ? 7 - (int)currDay : (int)DayOfWeek.Sunday;
                

                List<char> list = new List<char>();

                using (var db = new Context.ContextDB())
                {
                    var days = db.Roles.FirstOrDefault(r => r.Id == subject.RoleId).AllowedDays;

                    list.AddRange(ToNotation(days));
                }

                for (int i = 0; i < list.Count; ++i)
                {
                    if (list.ElementAt(i) == '1' && intDay == i)
                    {
                        return true;
                    }
                }

            }
            catch { }

            return false;
        }

        private bool CheckTime(Subject subject)
        {
            try
            {
                using (var db = new Context.ContextDB())
                {
                    var sTime = db.Levels.FirstOrDefault(t => t.Id == subject.LevelId).StartTime;
                    var eTime = db.Levels.FirstOrDefault(t => t.Id == subject.LevelId).EndTime;

                    if ((DateTime.Now.TimeOfDay >= sTime && DateTime.Now.TimeOfDay <= eTime) || (null == sTime || null == eTime))
                    {
                        return true;
                    }
                }
            }
            catch { }

            return false;
        }

        private string ToNotation(int number)
        {
            string temp = Convert.ToString(number, 2);
            temp = temp.PadLeft(7, '0');
            return temp;
        }

    }
}

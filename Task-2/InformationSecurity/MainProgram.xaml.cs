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
using System.Windows.Shapes;

namespace InformationSecurity
{
    /// <summary>
    /// Логика взаимодействия для MainProgram.xaml
    /// </summary>
    public partial class MainProgram : Window
    {
        private Models.Subject Subject { get; set; }
        private TimeSpan allowedTime;
        private System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        public MainProgram(Models.Subject subject)
        {
            InitializeComponent();
            this.Subject = subject;
            label1.Content = subject.Login;

            try
            {
                using (var db = new Context.ContextDB())
                {
                    var time = db.Roles.FirstOrDefault(t => t.Id == Subject.RoleId).AllowedTime;
                    allowedTime = (TimeSpan)time;
                }
            }
            catch { }

            TimeUp(allowedTime);

            label2.Content = allowedTime.ToString();
            timer.Tick += new EventHandler(TimeLeft);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void TimeLeft(object sender, EventArgs e)
        {
            label2.Content = allowedTime.ToString();
            allowedTime -= new TimeSpan(0, 0, 1);
            TimeUp(allowedTime);
        }

        private void TimeUp(TimeSpan time)
        {
            if (time <= new TimeSpan(0, 0, 0))
            {
                timer.Stop();
                FixTime();
                MessageBox.Show("Your time is up!");
                this.Close();
            }
        }

        private void FixTime()
        {
            try
            {
                using (var db = new Context.ContextDB())
                {
                    var time = db.Roles.FirstOrDefault(t => t.Id == Subject.RoleId);
                    time.AllowedTime = allowedTime;
                    db.SaveChanges();
                }
            }
            catch { }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            FixTime();

            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (Methods.Subjects.ChangePassword(Subject, passBox1.Password))
            {
                MessageBox.Show("Password changed successful");
            }
            else
            {
                MessageBox.Show("Something got wrong");
            }
        }
    }
}

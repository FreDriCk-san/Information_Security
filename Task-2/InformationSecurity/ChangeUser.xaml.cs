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
    /// Логика взаимодействия для ChangeUser.xaml
    /// </summary>
    public partial class ChangeUser : Window
    {
        private Models.Subject Subject { get; set; }

        public ChangeUser()
        {
            InitializeComponent();
        }

        private void listView1_Loaded(object sender, RoutedEventArgs e)
        {
            listUpdate();
        }

        private void listView1_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            foreach (object obj in listView1.SelectedItems)
            {
                this.Subject = (obj as Models.Subject);
                textBox1.Text = ((obj as Models.Subject).Login);
                passBox1.Password = ((obj as Models.Subject).Password);
                comboBox3.SelectedItem = ((obj as Models.Subject).BanId);

                using (var db = new Context.ContextDB())
                {
                    var levelName = db.Levels.FirstOrDefault(l => l.Id == Subject.LevelId).Name;
                    var roleName = db.Roles.FirstOrDefault(r => r.Id == Subject.RoleId).Name;

                    comboBox1.SelectedItem = levelName;
                    comboBox2.SelectedItem = roleName;
                }

            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            this.Subject.Login = textBox1.Text;
            this.Subject.Password = passBox1.Password;
            this.Subject.BanId = Convert.ToInt32(comboBox3.SelectedItem);

            using (var db = new Context.ContextDB())
            {
                var levelId = db.Levels.FirstOrDefault(l => l.Name == comboBox1.SelectedItem.ToString()).Id;
                var roleId = db.Roles.FirstOrDefault(r => r.Name == comboBox2.SelectedItem.ToString()).Id;

                this.Subject.LevelId = levelId;
                this.Subject.RoleId = roleId; 
            }

            if (Methods.Admin.UpdateSubject(Subject))
            {
                MessageBox.Show("Updated successful");
                listUpdate();
            }
            else
            {
                MessageBox.Show("Error, something got wrong");
            }

        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            if (Methods.Admin.DeleteSubject(Subject))
            {
                MessageBox.Show("Deleted successful");
                listUpdate();
            }
            else
            {
                MessageBox.Show("Error, something got wrong");
            }
        }

        private void listUpdate()
        {
            listView1.Items.Clear();

            var gridView = new GridView();
            listView1.View = gridView;

            gridView.Columns.Add(new GridViewColumn { Header = "Id", DisplayMemberBinding = new Binding("Id") });
            gridView.Columns.Add(new GridViewColumn { Header = "Login", DisplayMemberBinding = new Binding("Login") });
            gridView.Columns.Add(new GridViewColumn { Header = "Password", DisplayMemberBinding = new Binding("Password") });
            gridView.Columns.Add(new GridViewColumn { Header = "AuthCount", DisplayMemberBinding = new Binding("Login") });

            using (var db = new Context.ContextDB())
            {
                foreach (var user in db.Subjects)
                {
                    listView1.Items.Add(new Models.Subject {
                        Id = user.Id,
                        Login = user.Login,
                        Password = user.Password,
                        AuthCount = user.AuthCount,
                        BanId = user.BanId,
                        LevelId = user.LevelId,
                        RoleId = user.RoleId
                    });
                }
            }
        }

        private void comboBox1_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new Context.ContextDB())
            {
                foreach (var level in db.Levels)
                {
                    comboBox1.Items.Add(level.Name);
                }
            }
        }

        private void comboBox2_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new Context.ContextDB())
            {
                foreach (var role in db.Roles)
                {
                    comboBox2.Items.Add(role.Name);
                }
            }
        }

        private void comboBox3_Loaded(object sender, RoutedEventArgs e)
        {
            using (var db = new Context.ContextDB())
            {
                foreach (var ban in db.Bans)
                {
                    comboBox2.Items.Add(ban.Id);
                }
            }
        }
    }
}

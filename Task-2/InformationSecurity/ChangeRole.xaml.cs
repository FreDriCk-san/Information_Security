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
    /// Логика взаимодействия для ChangeRole.xaml
    /// </summary>
    public partial class ChangeRole : Window
    {
        private Models.Role Role { get; set; }
        private CheckBox[] checkBoxes;

        public ChangeRole()
        {
            InitializeComponent();
            checkBoxes = new CheckBox[] { checkBox1, checkBox2, checkBox3, checkBox4, checkBox5, checkBox6, checkBox7 };
        }

        private void listView1_Loaded(object sender, RoutedEventArgs e)
        {
            listUpdate();
        }

        private void listUpdate()
        {
            listView1.Items.Clear();
            var defaultTime = new TimeSpan(00, 00, 00);

            var gridView = new GridView();
            listView1.View = gridView;

            gridView.Columns.Add(new GridViewColumn { Header = "Id", DisplayMemberBinding = new Binding("Id") });
            gridView.Columns.Add(new GridViewColumn { Header = "Name", DisplayMemberBinding = new Binding("Name") });
            gridView.Columns.Add(new GridViewColumn { Header = "Priority", DisplayMemberBinding = new Binding("Priority") });
            gridView.Columns.Add(new GridViewColumn { Header = "AllowedDays", DisplayMemberBinding = new Binding("AllowedDays") });
            gridView.Columns.Add(new GridViewColumn { Header = "AllowedTime", DisplayMemberBinding = new Binding("AllowedTime") });

            using (var db = new Context.ContextDB())
            {
                foreach (var role in db.Roles)
                {
                    listView1.Items.Add(new Models.Role
                    {
                        Id = role.Id,
                        Name = role.Name,
                        Priority = role.Priority,
                        AllowedDays = role.AllowedDays,
                        AllowedTime = role.AllowedTime.HasValue ? role.AllowedTime : defaultTime
                    });
                }
            }
        }

        private void listView1_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            foreach (object obj in listView1.SelectedItems)
            {
                textBox1.Text = ((obj as Models.Role).Name);
                textBox2.Text = ((obj as Models.Role).Priority.ToString());
                textBox3.Text = ((obj as Models.Role).AllowedTime.ToString());
                this.Role = (obj as Models.Role);
            }

            foreach(var box in checkBoxes)
            {
                box.IsChecked = false;
            }

            string stringDays = ToNotation(Role.AllowedDays);

            List<char> list = new List<char>();
            list.AddRange(stringDays);

            for(int i = 0; i < list.Count; ++i)
            {
                if (list.ElementAt(i) == '1') checkBoxes[i].IsChecked = true;
            }

        }

        private string ToNotation(int number)
        {
            string temp = Convert.ToString(number, 2);
            temp = temp.PadLeft(7, '0');
            return temp;
        }

        private int TakeDays()
        {
            int days = 0;
            int i;
            int j = checkBoxes.Length - 1;
            for (i = 0; i < checkBoxes.Length; ++i, --j)
            {
                if(checkBoxes[j].IsChecked == true)
                {
                    days += (int)Math.Pow(2, i);
                }
            }

            return days;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (Methods.Admin.AddRole(textBox1.Text, Convert.ToInt32(textBox2.Text), TimeSpan.Parse(textBox3.Text), TakeDays()))
            {
                MessageBox.Show("Role " + textBox1.Text + " was added successful");
                listUpdate();
            }
            else
            {
                MessageBox.Show("Something got wrong, call your system admin");
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            this.Role.Name = textBox1.Text;
            this.Role.Priority = Convert.ToInt32(textBox2.Text);
            this.Role.AllowedTime = TimeSpan.Parse(textBox3.Text);
            this.Role.AllowedDays = TakeDays();

            if (Methods.Admin.UpdateRole(Role))
            {
                MessageBox.Show("Updating complete");
                listUpdate();
            }
            else
            {
                MessageBox.Show("Something got wrong, call your system admin");
            }
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            if (Methods.Admin.DeleteRole(Role))
            {
                MessageBox.Show("Deleted successful");
            }
            else
            {
                MessageBox.Show("Something got wrong, call your system admin");
            }
        }
    }
}

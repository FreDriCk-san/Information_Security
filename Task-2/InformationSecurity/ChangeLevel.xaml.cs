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
    /// Логика взаимодействия для ChangeLevel.xaml
    /// </summary>
    public partial class ChangeLevel : Window
    {
        public ChangeLevel()
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
                textBox1.Text = ((obj as Models.Level).Name);
                textBox2.Text = ((obj as Models.Level).CountOfEnter).ToString();
                textBox3.Text = ((obj as Models.Level).StartTime).ToString();
                textBox4.Text = ((obj as Models.Level).EndTime).ToString();
            }
        }

        private void listUpdate()
        {
            listView1.Items.Clear();

            var gridView = new GridView();
            listView1.View = gridView;

            gridView.Columns.Add(new GridViewColumn { Header = "Id", DisplayMemberBinding = new Binding("Id") });
            gridView.Columns.Add(new GridViewColumn { Header = "Name", DisplayMemberBinding = new Binding("Name") });
            gridView.Columns.Add(new GridViewColumn { Header = "CountOfEnter", DisplayMemberBinding = new Binding("CountOfEnter") });
            gridView.Columns.Add(new GridViewColumn { Header = "StartTime", DisplayMemberBinding = new Binding("StartTime") });
            gridView.Columns.Add(new GridViewColumn { Header = "EndTime", DisplayMemberBinding = new Binding("EndTime") });

            using (var db = new Context.ContextDB())
            {
                foreach (var level in db.Levels)
                {
                    listView1.Items.Add(new { Id = level.Id,
                        Name = level.Name,
                        CountOfEnter = level.CountOfEnter.HasValue ? level.CountOfEnter : 0,
                        StartTime = level.StartTime.HasValue ? level.StartTime : DateTime.MinValue,
                        EndTime = level.EndTime.HasValue ? level.EndTime : DateTime.MinValue  });
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            //
        }
    }
}

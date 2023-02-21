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

namespace To_do_list_wpf
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<Zametka> zametki = new List<Zametka>();
        public List<Zametka> todayzametki = new List<Zametka>();
        public MainWindow()
        {
            InitializeComponent();
            zametki = Jdaughter.Deserialize();
            Date.Text = DateTime.Now.ToString();
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListBox1.SelectedItems.Count != 0)
            {
                todayzametki[ListBox1.SelectedIndex].name = Name.Text;
                todayzametki[ListBox1.SelectedIndex].description = Description.Text;
                clear();
                Name.Text = "";
                Description.Text = "";
            }
            else MessageBox.Show("Не выбрана заметка.");
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text != "" && Description.Text != "")
            {
                Zametka zametka = new Zametka(Name.Text, Description.Text, Convert.ToDateTime(Date.SelectedDate));
                zametki.Add(zametka);
                clear();
                Name.Text = "";
                Description.Text = "";
            }
            else MessageBox.Show("Вы не ввели имя или описание заметки.");
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (this.ListBox1.SelectedItems.Count != 0)
            {
                zametki.RemoveAt(zametki.IndexOf(todayzametki[ListBox1.SelectedIndex]));
                clear();
                Name.Text = "";
                Description.Text = "";
            }
            else MessageBox.Show("Не выбрана заметка.");
        }

        private void ListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListBox1.SelectedIndex != -1)
            {
                Name.Text = todayzametki[ListBox1.SelectedIndex].name;
                Description.Text = todayzametki[ListBox1.SelectedIndex].description;
            }
        }

        private void Date_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            clear();
        }
        private void clear()
        {
            ListBox1.Items.Clear();
            todayzametki.Clear();
            foreach (Zametka item in zametki)
            {
                if (Convert.ToDateTime(Date.SelectedDate).Day == item.data.Day)
                {
                    ListBox1.Items.Add(item.name);
                    todayzametki.Add(item);
                }
            }
            Jdaughter.Serialize(zametki);
        }
    }
}

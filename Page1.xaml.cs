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

namespace WpfLCC
{
    /// <summary>
    /// Page1.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page1 : Page
    {
        MainWindow m;
        public Page1(MainWindow m)
        {
            InitializeComponent();
            this.m = m;
        }
        public void LoadPage()
        {
            m.Content = this;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Iogin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            m.db.id = tbID.Text;
            m.p2.LoadPage();
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            m.OpenHelp();
        }
    }
}

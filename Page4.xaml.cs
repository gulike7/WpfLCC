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
    /// Page4.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page4 : Page
    {
        MainWindow m;
        public TextBox []tbs = new TextBox[4];
        public Page4(MainWindow m)
        {
            InitializeComponent();
            this.m = m;
            tbs[0] = tb1;
            tbs[1] = tb2;
            tbs[2] = tb3;
            tbs[3] = tb4;
        }
        public void LoadPage()
        {
            m.Content = this;
        }

        private void preBtn(object sender, MouseButtonEventArgs e){
            m.p3.LoadPage();
        }
        private void postBtn(object sender, MouseButtonEventArgs e){
            int d;
            if (int.TryParse(m.p5.ctb113.Text, out d) &&
                d >= 0)
                m.p5.LoadPage();
            else
                MessageBox.Show("입력값이 잘못되었습니다");
        }
        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            m.OpenHelp();
        }
        private void Grid_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            m.newPj();
        }

        private void Grid_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            m.savePj();
        }

        private void Grid_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            m.loadPj();
        }

        private void Grid_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            m.saveNewPj();
        }
    }
}

using Microsoft.Win32;
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
using System.IO;

namespace WpfLCC
{
    /// <summary>
    /// Page3.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page3 : Page
    {
        MainWindow m;
        public TextBox[] tbs=new TextBox[11];
        public Page3(MainWindow m)
        {
            InitializeComponent();
            this.m = m;
            tbs[0] = tb1;
            tbs[1] = tb2;
            tbs[2] = tb3;
            tbs[3] = tb4;
            tbs[4] = tb5;
            tbs[5] = tb6;
            tbs[6] = tb7;
            tbs[7] = tb8;
            tbs[8] = tb9;
            tbs[9] = tb10;
            tbs[10] = tb11;

            cb1.Items.Clear();
            cb2.Items.Clear();
            cb1.Items.Add("일반 건물");
            cb1.Items.Add("주택");
            cb2.Items.Add("고정식");
            cb2.Items.Add("추정식");
            cb2.Items.Add("BIPV");
            cb1.SelectedIndex = 0;
            cb2.SelectedIndex = 0;
            cb1.SelectionChanged += cb1_SelectionChanged;
            tb6.TextChanged += tb6_TextChanged;
        }
        public void LoadPage()
        {
            m.Content = this;
        }
        public void save1()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            saveFileDialog.Filter = "Set1 files (*.set1)|*.set1|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                string[] str = new string[8];
                for (int i = 0; i < 8; i++)
                    str[i] = tbs[i + 3].Text;
                File.WriteAllLines(saveFileDialog.FileName, str);
            }
            else
                MessageBox.Show("파일을 저장하는데 실패했습니다");
        }
        public void load1()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            openFileDialog.Filter = "Set1 files (*.set1)|*.set1|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string[] str = File.ReadAllLines(openFileDialog.FileName);
                for (int i = 0; i < 8; i++)
                    tbs[i + 3].Text = str[i];
            }
            else
                MessageBox.Show("파일을 불러오는데 실패했습니다");
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            m.OpenHelp();
        }

        private void preBtn(object sender, MouseButtonEventArgs e)
        {
            m.p2.LoadPage();
        }

        private void postBtn(object sender, MouseButtonEventArgs e)
        {
            double d;
            if (double.TryParse(m.p3.tbs[5].Text, out d) &&
                double.TryParse(m.p3.tbs[6].Text, out d) &&
                double.TryParse(m.p3.tbs[7].Text, out d) &&
                double.TryParse(m.p3.tbs[8].Text, out d) &&
                double.TryParse(m.p3.tbs[9].Text, out d) &&
                double.TryParse(m.p3.tbs[10].Text, out d))
                m.p4.LoadPage();
            else
                MessageBox.Show("입력값이 잘못되었습니다");
        }

        private void btnSave1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            save1();
        }
        private void btnLoad1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            load1();
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

        private void cb1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cb1.SelectedIndex == 0)
            {
                if(!cb2.Items.Contains("BIPV"))
                    cb2.Items.Add("BIPV");
            }
            else if (cb1.SelectedIndex == 1)
            {
                if (cb2.SelectedIndex == 2)
                    cb2.SelectedIndex = 0;
                cb2.Items.Remove("BIPV");
            }
        }

        private void tb6_TextChanged(object sender, TextChangedEventArgs e)
        {
            int electricity_rate=0;
            double capacity;
            if (!double.TryParse(tb6.Text, out capacity))
                return;
            if (cb1.SelectedIndex == 0)
            {
                if (cb2.SelectedIndex == 0)
                    electricity_rate = 4972;
                else if (cb2.SelectedIndex == 1)
                    electricity_rate = 5604;
                else if (cb2.SelectedIndex == 2)
                    electricity_rate = 9553;
            }
            else if(cb1.SelectedIndex == 1)
            {
                if (cb2.SelectedIndex == 0)
                    electricity_rate = 3913;
                else if (cb2.SelectedIndex == 1)
                    electricity_rate = 4647;
            }
            tb7.Text = (electricity_rate * capacity).ToString("N0");
            tb8.Text = (1495.6 * capacity - 252.61).ToString("N0");
            tb9.Text = (146.37 * capacity + 252.62).ToString("N0");
            tb10.Text = (0).ToString();
        }
    }
}
;
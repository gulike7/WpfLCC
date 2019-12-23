using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Page5.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page5 : Page
    {
        MainWindow m;
        public TabItem[] tc1items = new TabItem[5];
        public TabItem[] tc2items = new TabItem[5];
        public RadioButton[,,] tc1rbs = new RadioButton[5, 2, 2];
        public TextBox[,,] tc1tbs = new TextBox[5, 2, 2];
        public TextBox[,,] tc2ctbs = new TextBox[5, 3, 3];
        public Page5(MainWindow m)
        {
            InitializeComponent();
            this.m = m;
            tc1items[0] = tb1item1;
            tc1items[1] = tb1item2;
            tc1items[2] = tb1item3;
            tc1items[3] = tb1item4;
            tc1items[4] = tb1item5;

            tc2items[0] = tb2item1;
            tc2items[1] = tb2item2;
            tc2items[2] = tb2item3;
            tc2items[3] = tb2item4;
            tc2items[4] = tb2item5;

            tc1rbs[0, 0, 0] = rb111;
            tc1rbs[0, 0, 1] = rb112;
            tc1rbs[0, 1, 0] = rb121;
            tc1rbs[0, 1, 1] = rb122;
            tc1rbs[1, 0, 0] = rb211;
            tc1rbs[1, 0, 1] = rb212;
            tc1rbs[1, 1, 0] = rb221;
            tc1rbs[1, 1, 1] = rb222;
            tc1rbs[2, 0, 0] = rb311;
            tc1rbs[2, 0, 1] = rb312;
            tc1rbs[2, 1, 0] = rb321;
            tc1rbs[2, 1, 1] = rb322;
            tc1rbs[3, 0, 0] = rb411;
            tc1rbs[3, 0, 1] = rb412;
            tc1rbs[3, 1, 0] = rb421;
            tc1rbs[3, 1, 1] = rb422;
            tc1rbs[4, 0, 0] = rb511;
            tc1rbs[4, 0, 1] = rb512;
            tc1rbs[4, 1, 0] = rb521;
            tc1rbs[4, 1, 1] = rb522;

            tc1tbs[0, 0, 0] = tb111;
            tc1tbs[0, 0, 1] = tb112;
            tc1tbs[0, 1, 0] = tb121;
            tc1tbs[0, 1, 1] = tb122;
            tc1tbs[1, 0, 0] = tb211;
            tc1tbs[1, 0, 1] = tb212;
            tc1tbs[1, 1, 0] = tb221;
            tc1tbs[1, 1, 1] = tb222;
            tc1tbs[2, 0, 0] = tb311;
            tc1tbs[2, 0, 1] = tb312;
            tc1tbs[2, 1, 0] = tb321;
            tc1tbs[2, 1, 1] = tb322;
            tc1tbs[3, 0, 0] = tb411;
            tc1tbs[3, 0, 1] = tb412;
            tc1tbs[3, 1, 0] = tb421;
            tc1tbs[3, 1, 1] = tb422;
            tc1tbs[4, 0, 0] = tb511;
            tc1tbs[4, 0, 1] = tb512;
            tc1tbs[4, 1, 0] = tb521;
            tc1tbs[4, 1, 1] = tb522;

            tc2ctbs[0, 0, 0] = ctb111;
            tc2ctbs[0, 0, 1] = ctb112;
            tc2ctbs[0, 0, 2] = ctb113;
            tc2ctbs[0, 1, 0] = ctb121;
            tc2ctbs[0, 1, 1] = ctb122;
            tc2ctbs[0, 1, 2] = ctb123;
            tc2ctbs[0, 2, 0] = ctb131;
            tc2ctbs[0, 2, 1] = ctb132;
            tc2ctbs[0, 2, 2] = ctb133;
            tc2ctbs[1, 0, 0] = ctb211;
            tc2ctbs[1, 0, 1] = ctb212;
            tc2ctbs[1, 0, 2] = ctb213;
            tc2ctbs[1, 1, 0] = ctb221;
            tc2ctbs[1, 1, 1] = ctb222;
            tc2ctbs[1, 1, 2] = ctb223;
            tc2ctbs[1, 2, 0] = ctb231;
            tc2ctbs[1, 2, 1] = ctb232;
            tc2ctbs[1, 2, 2] = ctb233;
            tc2ctbs[2, 0, 0] = ctb311;
            tc2ctbs[2, 0, 1] = ctb312;
            tc2ctbs[2, 0, 2] = ctb313;
            tc2ctbs[2, 1, 0] = ctb321;
            tc2ctbs[2, 1, 1] = ctb322;
            tc2ctbs[2, 1, 2] = ctb323;
            tc2ctbs[2, 2, 0] = ctb331;
            tc2ctbs[2, 2, 1] = ctb332;
            tc2ctbs[2, 2, 2] = ctb333;
            tc2ctbs[3, 0, 0] = ctb411;
            tc2ctbs[3, 0, 1] = ctb412;
            tc2ctbs[3, 0, 2] = ctb413;
            tc2ctbs[3, 1, 0] = ctb421;
            tc2ctbs[3, 1, 1] = ctb422;
            tc2ctbs[3, 1, 2] = ctb423;
            tc2ctbs[3, 2, 0] = ctb431;
            tc2ctbs[3, 2, 1] = ctb432;
            tc2ctbs[3, 2, 2] = ctb433;
            tc2ctbs[4, 0, 0] = ctb511;
            tc2ctbs[4, 0, 1] = ctb512;
            tc2ctbs[4, 0, 2] = ctb513;
            tc2ctbs[4, 1, 0] = ctb521;
            tc2ctbs[4, 1, 1] = ctb522;
            tc2ctbs[4, 1, 2] = ctb523;
            tc2ctbs[4, 2, 0] = ctb531;
            tc2ctbs[4, 2, 1] = ctb532;
            tc2ctbs[4, 2, 2] = ctb533;

            for (int i=0;i<5;i++)
            {
                tc1rbs[i, 0, 0].Click += rb1_Checked;
                tc1rbs[i, 1, 0].Click += rb2_Checked;
                tc1rbs[i, 0, 1].Click += rb1_Unchecked;
                tc1rbs[i, 1, 1].Click += rb2_Unchecked;
                //tc1rbs[i, 1, 0].Click
            }
            for(int i = 0; i < 5; i++){
                for(int r = 0; r < 3; r++){
                    tc2ctbs[i, r, 1].GotFocus+=ctb_GotFocus;
                    tc2ctbs[i, r, 1].LostFocus+= ctb_LostFocus;
                }
            }
        }
        public void LoadPage()
        {
            m.Content = this;
        }
        public void tabPlus1()
        {
            if (m.db.p5_1_tabNum<5)
            {
                tc1items[m.db.p5_1_tabNum].Visibility = Visibility.Visible;
                rbChecked(m.db.p5_1_tabNum, 0);
                rbChecked(m.db.p5_1_tabNum, 1);
                m.db.p5_1_tabNum += 1;
                tc1.SelectedIndex = m.db.p5_1_tabNum-1;
            }
        }
        public void tabMinus1()
        {
            if (m.db.p5_1_tabNum > 1)
            {
                if (tc1.SelectedIndex == m.db.p5_1_tabNum-1)
                    tc1.SelectedIndex = tc1.SelectedIndex - 1;
                tc1items[m.db.p5_1_tabNum-1].Visibility = Visibility.Hidden;
                m.db.p5_1_tabNum -= 1;
                tc2.SelectedIndex = m.db.p5_2_tabNum - 1;
            }
        }
        public void tabPlus2()
        {
            if (m.db.p5_2_tabNum < 5)
            {
                tc2items[m.db.p5_2_tabNum].Visibility = Visibility.Visible;
                for (int i = 0; i < 3; i++){
                    for(int j = 0; j < 3; j++){
                        tc2ctbs[m.db.p5_2_tabNum, i, j].Text = "";
                    }
                    tc2ctbs[m.db.p5_2_tabNum, i, 1].Text = "(추정치 적용)";
                }
                tc2ctbs[m.db.p5_2_tabNum, 0, 2].Text = "0";
                tc2ctbs[m.db.p5_2_tabNum, 1, 2].Text = "0";
                tc2ctbs[m.db.p5_2_tabNum, 2, 2].Text = "0";
                m.db.p5_2_tabNum += 1;
                tc2.SelectedIndex = m.db.p5_2_tabNum - 1;
            }
        }
        public void tabMinus2()
        {
            if (m.db.p5_2_tabNum > 1)
            {
                if (tc2.SelectedIndex == m.db.p5_2_tabNum - 1)
                    tc2.SelectedIndex = tc2.SelectedIndex - 1;
                tc2items[m.db.p5_2_tabNum - 1].Visibility = Visibility.Hidden;
                m.db.p5_2_tabNum--;
            }
        }
        public void rbChecked(int tabNum, int rowNum)
        {
            if (rowNum == 0)
            {
                tc1tbs[tabNum, rowNum, 0].Text = "0.18";
                tc1tbs[tabNum, rowNum, 1].Text = "-0.0036";
            }
            else
            {
                tc1tbs[tabNum, rowNum, 0].Text = "0.94";
                tc1tbs[tabNum, rowNum, 1].Text = "-0.0060";
            }
            tc1tbs[tabNum, rowNum, 0].IsEnabled = false;
            tc1tbs[tabNum, rowNum, 1].IsEnabled = false;
            tc1rbs[tabNum, rowNum, 0].IsChecked = true;
        }
        public void rbUnChecked(int tabNum, int rowNum)
        {
            tc1tbs[tabNum, rowNum, 0].IsEnabled = true;
            tc1tbs[tabNum, rowNum, 1].IsEnabled = true;
            tc1rbs[tabNum, rowNum, 1].IsChecked = true;
        }
        public void save2()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            saveFileDialog.Filter = "Set2 files (*.set2)|*.set2|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                List<string> str = new List<string>();
                str.Add(m.db.p5_1_tabNum.ToString());
                for (int i = 0; i < m.db.p5_1_tabNum; i++)
                {
                    str.Add(tc1rbs[i, 0, 0].IsChecked.ToString());
                    str.Add(tc1rbs[i, 1, 0].IsChecked.ToString());
                    str.Add(tc1tbs[i, 0, 0].Text);
                    str.Add(tc1tbs[i, 0, 1].Text);
                    str.Add(tc1tbs[i, 1, 0].Text);
                    str.Add(tc1tbs[i, 1, 1].Text);
                }
                File.WriteAllLines(saveFileDialog.FileName, str);
            }
            else
                MessageBox.Show("파일을 저장하는데 실패했습니다");
        }
        public void load2()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            openFileDialog.Filter = "Set2 files (*.set2)|*.set2|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string[] str = File.ReadAllLines(openFileDialog.FileName);
                int tabNum= int.Parse(str[0]);

                for (int i = 0; i < 5; i++)
                    tabMinus1();
                for (int i = 0; i < tabNum - 1; i++)
                    tabPlus1();
                for (int i = 0; i < tabNum; i++)
                {
                    if (str[i * 6 + 1 + 0] == "True")
                    {
                        rbChecked(i, 0);
                    }
                    else
                    {
                        rbUnChecked(i, 0);
                        tc1tbs[i, 0, 0].Text = str[i * 6 + 1 + 2];
                        tc1tbs[i, 0, 1].Text = str[i * 6 + 1 + 3];
                    }
                    if (str[i * 6 + 1 + 1] == "True")
                    {
                        rbChecked(i, 1);
                    }
                    else
                    {
                        rbUnChecked(i, 1);
                        tc1tbs[i, 1, 0].Text = str[i * 6 + 1 + 4];
                        tc1tbs[i, 1, 1].Text = str[i * 6 + 1 + 5];
                    }
                }
            }
            else
                MessageBox.Show("파일을 불러오는데 실패했습니다");
        }
        public void save3()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            saveFileDialog.Filter = "Set3 files (*.set3)|*.set3|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                List<string> str = new List<string>();
                str.Add(m.db.p5_2_tabNum.ToString());
                for (int i = 0; i < m.db.p5_2_tabNum; i++)
                {
                    for(int j = 0; j < 3; j++)
                    {
                        for(int k = 0; k < 3; k++)
                        {
                            str.Add(tc2ctbs[i, j, k].Text);
                        }
                    }
                }
                File.WriteAllLines(saveFileDialog.FileName, str);
            }
            else
                MessageBox.Show("파일을 저장하는데 실패했습니다");
        }
        public void load3()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            openFileDialog.Filter = "Set3 files (*.set3)|*.set3|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                string[] str = File.ReadAllLines(openFileDialog.FileName);
                int tabNum = int.Parse(str[0]);

                for (int i = 0; i < 5; i++)
                    tabMinus2();
                for (int i = 0; i < tabNum - 1; i++)
                    tabPlus2();
                for (int i = 0; i < tabNum; i++)
                {
                    for(int j = 0; j < 3; j++)
                    {
                        for(int k = 0; k < 3; k++)
                        {
                            tc2ctbs[i, j, k].Text = str[i * 9 + j * 3 + k + 1];
                        }
                    }
                }
            }
            else
                MessageBox.Show("파일을 불러오는데 실패했습니다");
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e){
            m.OpenHelp();
        }
        private void preBtn(object sender, MouseButtonEventArgs e){
            m.p4.LoadPage();
        }
        private void postBtn(object sender, MouseButtonEventArgs e){
            int d;
            for(int i = 0; i < m.db.p5_2_tabNum ; i++)
            {
                if (!int.TryParse(tc2ctbs[i, 0, 2].Text, out d) ||
                d < 0){
                    MessageBox.Show("입력값이 잘못되었습니다");
                    return;
                }
            }
            m.p6.LoadPage();
        }

        private void btnPlus1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e){
            tabPlus1();
        }
        private void btnMinus1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e){
            tabMinus1();
        }
        private void btnPlus2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tabPlus2();
        }
        private void btnMinus2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e){
            tabMinus2();
        }


        private void ctb_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = ((TextBox)sender);
            if (tb.Text == "(추정치 적용)")
                tb.Text = "";
        }
        private void ctb_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = ((TextBox)sender);
            if(tb.Text == "")
                tb.Text = "(추정치 적용)";
        }
        private void rb1_Checked(object sender, RoutedEventArgs e)
        {
            rbChecked(tc1.SelectedIndex, 0);
        }
        private void rb1_Unchecked(object sender, RoutedEventArgs e)
        {
            rbUnChecked(tc1.SelectedIndex, 0);
        }
        private void rb2_Checked(object sender, RoutedEventArgs e)
        {
            rbChecked(tc1.SelectedIndex, 1);
        }
        private void rb2_Unchecked(object sender, RoutedEventArgs e)
        {
            rbUnChecked(tc1.SelectedIndex, 1);
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

        private void btnSave2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            save2();
        }

        private void btnLoad2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            load2();
        }

        private void btnSave3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            save3();
        }

        private void btnLoad3_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            load3();
        }
    }
}

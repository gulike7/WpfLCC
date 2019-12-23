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
using System.Windows.Forms.DataVisualization.Charting;

namespace WpfLCC
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public Page1 p1;
        public Page2 p2;
        public Page3 p3;
        public Page4 p4;
        public Page5 p5;
        public Page6 p6;
        public DB db;
        public List<List<List<string>>> csv = new List<List<List<string>>>();

        public MainWindow()
        {
            InitializeComponent();
            p1 = new Page1(this);
            p2 = new Page2(this);
            p3 = new Page3(this);
            p4 = new Page4(this);
            p5 = new Page5(this);
            p6 = new Page6(this);
            db = new DB();
            p1.LoadPage();
            ReadCSVdata(AppDomain.CurrentDomain.BaseDirectory + "DB");
        }
        public void ReadCSVdata(string path)
        {
            StreamReader sr = new StreamReader(path);
            int sheet = -1;
            int row = -1;
            while (!sr.EndOfStream)
            {
                // 한 줄씩 읽어온다.

                string line = sr.ReadLine();
                string[] str;

                if (line == "//////////")
                {
                    csv.Add(new List<List<string>>());
                    sheet++;
                    row = -1;
                }
                else
                {
                    csv[sheet].Add(new List<string>());
                    row++;
                    str = line.Split('\t');
                    for (int col = 0; col < str.Length; col++)
                        csv[sheet][row].Add(str[col]);
                }
            }
        }
        public Solution getCSV(int ca, int cb)
        {
            double generation;
            double electricity_rate = double.Parse(p4.tb2.Text);
            double capacity = double.Parse(p3.tb6.Text);
            double operating_time = double.Parse(p4.tb1.Text);
            double eNum = 1 + double.Parse(p4.tb4.Text) / 100; // 1.017;
            double iNum = 1 + double.Parse(p4.tb3.Text) / 100; // 1.020;


            generation = electricity_rate * capacity * operating_time * 365;
            double initial_cost = (
                double.Parse(p3.tb7.Text) + double.Parse(p3.tb8.Text) +
                double.Parse(p3.tb9.Text) + double.Parse(p3.tb10.Text)); //패널 가격 + 인버터 가격 + 초기 투자비 + 기타 초기 비용
            double rNum =  eNum / iNum;
            int[,] maintenance = new int[3, 50];
            for (int i = 0; i < 50; i++)
            {
                int tmp;
                if (int.Parse(p5.tc2ctbs[cb, 0, 2].Text) == 0)
                    maintenance[0, i] = 0;
                else if ((i + 1) % int.Parse(p5.tc2ctbs[cb, 0, 2].Text) == 0)
                    maintenance[0, i] = 1;
                if (int.TryParse(p5.tc2ctbs[cb, 1, 2].Text, out tmp))
                {
                    if ((i + 1) == tmp)
                        maintenance[1, i] = 1;
                }
                if (int.TryParse(p5.tc2ctbs[cb, 2, 2].Text, out tmp))
                {
                    if ((i + 1) == tmp)
                        maintenance[2, i] = 1;
                }
            }

            double[] pn_panel = new double[50];
            double[] pn_invter = new double[50];
            double[] price_panel = new double[50];
            double[] price_inverter = new double[50];

            for (int i = 0; i < 50; i++)
            {
                //if(년도<2010)
                //  년도=2010
                //else if(년도>2050)
                //  년도=2050
                pn_panel[i] = double.Parse(csv[0][i + 1][19]);
                pn_invter[i] = double.Parse(csv[0][i + 1][20]);
                price_panel[i] = double.Parse(csv[0][i + 1][28]);
                price_inverter[i] = double.Parse(csv[0][i + 1][29]);//??우선은 10년 입력이라고 치고 하자
            }

            double[] cumprod_r = new double[50];
            for (int i = 0; i < 50; i++)
                cumprod_r[i] = Math.Pow(rNum, i);

            double[] pm_panel = new double[50];
            double[] pm_inverter = new double[50];

            for (int i = 0; i < 50; i++)
            {
                if (i == 0)
                {
                    pm_panel[0] = double.Parse(p5.tc1tbs[ca, 0, 0].Text);
                    pm_inverter[0] = double.Parse(p5.tc1tbs[ca, 1, 0].Text);
                }
                else
                {
                    if (maintenance[0, i] == 1 || maintenance[1, i] == 1 || maintenance[2, i] == 1)
                    {
                        pm_panel[i] = pn_panel[i];
                        pm_inverter[i] = pn_invter[i];
                    }
                    else
                    {
                        pm_panel[i] = pm_panel[i - 1] + double.Parse(p5.tc1tbs[ca, 0, 1].Text);
                        pm_inverter[i] = pm_inverter[i - 1] + double.Parse(p5.tc1tbs[ca, 1, 1].Text); ;
                    }
                }
            }
            double[] gc = new double[50];
            for (int i = 0; i < 50; i++)
            {
                gc[i] = generation * (pm_panel[i] / pm_panel[0]) * pm_inverter[i] * cumprod_r[i] / 1000.0;
            }
            double[] mc = new double[50];
            for (int i = 0; i < 50; i++)
            {
                mc[i] = 0;
                if (maintenance[0, i] == 1)
                {
                    if (p5.tc2ctbs[cb, 0, 1].Text == "(추정치 적용)")
                    {
                        mc[i] += (price_panel[i] + price_inverter[i]) * capacity * cumprod_r[i];
                    }
                    else
                        mc[i] += (double.Parse(p5.tc2ctbs[cb, 0, 1].Text)) * cumprod_r[i];
                }
                if (maintenance[1, i] == 1)
                {
                    if (p5.tc2ctbs[cb, 0, 1].Text == "(추정치 적용)")
                    {
                        mc[i] += (price_panel[i] + price_inverter[i]) * capacity * cumprod_r[i];
                    }
                    else
                        mc[i] += (double.Parse(p5.tc2ctbs[cb, 1, 1].Text)) * cumprod_r[i];
                }
                if (maintenance[2, i] == 1)
                {
                    if (p5.tc2ctbs[cb, 0, 1].Text == "(추정치 적용)")
                    {
                        mc[i] += (price_panel[i] + price_inverter[i]) * capacity * cumprod_r[i];
                    }
                    else
                        mc[i] += (double.Parse(p5.tc2ctbs[cb, 2, 1].Text)) * cumprod_r[i];
                }
            }

            double[] lcc = new double[50];
            double[] lcc_per_year = new double[50];
            for (int i = 0; i < 50; i++)
            {
                if (i == 0)
                {

                    lcc[i] = (gc[i] - mc[i] - 1.7 * initial_cost) / 100000;

                }
                else
                {
                    lcc[i] = lcc[i - 1] + (gc[i] - mc[i]) / 100000;

                }
                lcc_per_year[i] = lcc[i] / (i + 1);
            }
            return new Solution(pm_panel, pm_inverter, lcc, lcc_per_year,ca,cb);

        }
        public void OpenHelp()
        {
            try
            {
                System.Diagnostics.Process.Start("AcroRd32.exe", AppDomain.CurrentDomain.BaseDirectory + "\\manual_ver.1.0.pdf");
            }
            catch
            {
                try
                {
                    System.Diagnostics.Process.Start("Acrobat.exe", AppDomain.CurrentDomain.BaseDirectory + "\\manual_ver.1.0.pdf");
                }
                catch
                {
                    try
                    {
                        System.Diagnostics.Process.Start("AcroRd64.exe", AppDomain.CurrentDomain.BaseDirectory + "\\manual_ver.1.0.pdf");
                    }
                    catch
                    {
                        MessageBox.Show("Acrobat Reader를 실행하지 못했습니다");
                    }
                }
            }
        }
        public void newPj()
        {
            p3 = new Page3(this);
            p4 = new Page4(this);
            p5 = new Page5(this);
            db = new DB();
            p3.LoadPage();

            for (int i = 0; i < 5; i++)
            {
                p5.tabMinus1();
                p5.tabMinus2();
            }
        }
        public void savePj()
        {
            string filePath = "";
            if (db.file_path == "")
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                saveFileDialog.Filter = "Project files (*.proj)|*.proj|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == true)
                {
                    filePath = saveFileDialog.FileName;
                    db.file_path = filePath;
                }
                else
                {
                    MessageBox.Show("프로젝트 저장에 실패했습니다");
                    return;
                }
            }
            else
                filePath = db.file_path;

            save(filePath);
        }
        public void saveNewPj()
        {
            string filePath = "";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            saveFileDialog.Filter = "Project files (*.proj)|*.proj|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                filePath = saveFileDialog.FileName;
                db.file_path = filePath;
            }
            else
            {
                MessageBox.Show("프로젝트 저장에 실패했습니다");
                return;
            }
            save(filePath);
        }
        public void save(string filePath)
        {
            List<string> str = new List<string>();

            for (int i = 0; i < p3.tbs.Length; i++)
                str.Add(p3.tbs[i].Text);
            str.Add(p3.cb1.SelectedIndex.ToString());
            str.Add(p3.cb2.SelectedIndex.ToString());

            for (int i = 0; i < p4.tbs.Length; i++)
                str.Add(p4.tbs[i].Text);

            str.Add(db.p5_1_tabNum.ToString());
            for (int i = 0; i < 5; i++)
            {
                str.Add(p5.tc1rbs[i, 0, 0].IsChecked.ToString());
                str.Add(p5.tc1rbs[i, 1, 0].IsChecked.ToString());
                str.Add(p5.tc1tbs[i, 0, 0].Text);
                str.Add(p5.tc1tbs[i, 0, 1].Text);
                str.Add(p5.tc1tbs[i, 1, 0].Text);
                str.Add(p5.tc1tbs[i, 1, 1].Text);
            }
            str.Add(db.p5_2_tabNum.ToString());
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        str.Add(p5.tc2ctbs[i, j, k].Text);
                    }
                }
            }
            File.WriteAllLines(filePath, str);
            MessageBox.Show("프로젝트 저장 성공");
        }
        public void loadPj()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            openFileDialog.Filter = "Project files (*.proj)|*.proj|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                db.file_path = openFileDialog.FileName;
                string[] str = File.ReadAllLines(openFileDialog.FileName);
                int offset = 0;

                for (int i = 0; i < p3.tbs.Length; i++)
                    p3.tbs[i].Text = str[i];
                offset += p3.tbs.Length;

                p3.cb1.SelectedIndex = int.Parse(str[offset]);
                offset++;
                p3.cb2.SelectedIndex = int.Parse(str[offset]);
                offset++;
                for (int i = 0; i < p4.tbs.Length; i++)
                    p4.tbs[i].Text = str[i + offset];
                offset += p4.tbs.Length;

                int tabNum = int.Parse(str[offset]);
                offset++;
                for (int i = 0; i < 5; i++)
                    p5.tabMinus1();
                for (int i = 0; i < tabNum - 1; i++)
                    p5.tabPlus1();
                for (int i = 0; i < 5; i++)
                {
                    p5.tc1.SelectedIndex = i;
                    if (str[offset] == "True")
                    {
                        p5.rbChecked(i, 0);
                    }
                    else
                    {
                        p5.rbUnChecked(i, 0);
                    }
                    offset++;
                    if (str[offset] == "True")
                    {
                        p5.rbChecked(i, 1);
                    }
                    else
                    {
                        p5.rbUnChecked(i, 1);
                    }
                    offset++;
                    p5.tc1tbs[i, 0, 0].Text = str[offset];
                    offset++;
                    p5.tc1tbs[i, 0, 1].Text = str[offset];
                    offset++;
                    p5.tc1tbs[i, 1, 0].Text = str[offset];
                    offset++;
                    p5.tc1tbs[i, 1, 1].Text = str[offset];
                    offset++;
                }
                p5.tc1.SelectedIndex = 0;
                tabNum = int.Parse(str[offset]);
                offset++;
                for (int i = 0; i < 5; i++)
                    p5.tabMinus2();
                for (int i = 0; i < tabNum - 1; i++)
                    p5.tabPlus2();
                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            p5.tc2ctbs[i, j, k].Text = str[offset];
                            offset++;
                        }
                    }
                }
                p3.LoadPage();
            }
            else
                MessageBox.Show("파일을 불러오는데 실패했습니다");
        }
    }
    public class DB
    {
        public string file_path;
        public string id;
        public int p5_1_tabNum;
        public int p5_2_tabNum;
        public DB()
        {
            p5_1_tabNum = 5;
            p5_2_tabNum = 5;
            file_path = "";
            id = "";
        }
    }
    public class Solution
    {
        public double[] pm_panel = new double[50];
        public double[] pm_inverter = new double[50];
        public double[] lcc = new double[50];
        public double[] lcc_per_year = new double[50];
        public int CaseA, CaseB;

        public Solution(double[] pmp, double[] pmi, double[] lc, double[] lcpy, int ca, int cb)
        {
            CaseA = ca;
            CaseB = cb;
            for(int i = 0; i < 50; i++)
            {
                pm_panel [i]= pmp[i];
                pm_inverter[i] = pmi[i];
                lcc[i] = lc[i];
                lcc_per_year[i] = lcpy[i];
            }
        }
    }
}

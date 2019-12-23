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
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Win32;

namespace WpfLCC
{
    /// <summary>
    /// Page6.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class Page6 : Page
    {
        MainWindow m;
        bool[,] tf = new bool[2,5];
        public Button[,] btns = new Button[2, 5];
        public List<Chart> charts = new List<Chart>();
        public List<Solution> solutions = new List<Solution>();
        public Page6(MainWindow m)
        {
            InitializeComponent();
            btns[0, 0] = btn1;
            btns[0, 1] = btn2;
            btns[0, 2] = btn3;
            btns[0, 3] = btn4;
            btns[0, 4] = btn5;
            btns[1, 0] = btn6;
            btns[1, 1] = btn7;
            btns[1, 2] = btn8;
            btns[1, 3] = btn9;
            btns[1, 4] = btn10;
            this.m = m;
        }
        public void DrawSeries(Series ser, Solution sol, double[] values)
        {
            
            ser.ChartType = SeriesChartType.Line;
            ser.BorderWidth = 2;
            
            ser.LegendText = "기기" + (sol.CaseA + 1).ToString() + " 관리" + (sol.CaseB + 1).ToString() + " ";
            ser.IsVisibleInLegend = true;
            //ser.Color = System.Drawing.Color.Red;
            for (int i = 0; i < values.Length; i++)
            {
                ser.Points.AddXY(i + int.Parse(m.p3.tb11.Text), values[i]);
            }
        }
        public void DrawChart()
        {
            charts.Clear();
            solutions.Clear();

            // 기기 12345 검사, true면 관리 12345 검사
            for (int i = 0; i < 5; i++)
            {
                if (tf[0, i])
                {
                    for (int j = 0; j < 5; j++)
                    {
                        if (tf[1, j])
                            solutions.Add(m.getCSV(i, j));
                    }
                }
            }
            Chart chart = FindName("MyWinformChart1") as Chart;
            charts.Add(chart);
            chart.ChartAreas.Clear();
            chart.Series.Clear();
            chart.Legends.Clear();
            chart.ChartAreas.Add("Draw");
            chart.ChartAreas[0].AxisY.Title = "패널 효율";
            chart.ChartAreas[0].AxisX.Minimum = int.Parse(m.p3.tb11.Text);

            chart.Legends.Add("l");

            for (int i = 0; i < solutions.Count; i++)
            {
                string sol = "s" + (i + 1).ToString();
                chart.Series.Add(sol);
                DrawSeries(chart.Series[sol], solutions[i], solutions[i].pm_panel);
                chart.Series[sol].ChartType = SeriesChartType.Line;
                chart.Series[sol].BorderWidth = 2;
            }


            chart = FindName("MyWinformChart2") as Chart;
            charts.Add(chart);
            chart.ChartAreas.Clear();
            chart.Series.Clear();
            chart.Legends.Clear();
            chart.ChartAreas.Add("Draw");
            chart.ChartAreas[0].AxisY.Title = "인버터 효율";
            chart.ChartAreas[0].AxisX.Minimum = int.Parse(m.p3.tb11.Text);

            chart.Legends.Add("l");

            for (int i = 0; i < solutions.Count; i++)
            {
                string sol = "s" + (i + 1).ToString();
                chart.Series.Add(sol);
                DrawSeries(chart.Series[sol], solutions[i], solutions[i].pm_inverter);
                chart.Series[sol].ChartType = SeriesChartType.Line;
                chart.Series[sol].BorderWidth = 2;
            }


            chart = FindName("MyWinformChart3") as Chart;
            charts.Add(chart);
            chart.ChartAreas.Clear();
            chart.Series.Clear();
            chart.Legends.Clear();
            chart.ChartAreas.Add("Draw");
            chart.ChartAreas[0].AxisY.Title = "연간 LCC(억원)";
            chart.ChartAreas[0].AxisX.Minimum = int.Parse(m.p3.tb11.Text);

            chart.Legends.Add("l");

            for (int i = 0; i < solutions.Count; i++)
            {
                string sol = "s" + (i + 1).ToString();
                chart.Series.Add(sol);
                DrawSeries(chart.Series[sol], solutions[i], solutions[i].lcc_per_year);
                chart.Series[sol].ChartType = SeriesChartType.Line;
                chart.Series[sol].BorderWidth = 2;
            }


            chart = FindName("MyWinformChart4") as Chart;
            charts.Add(chart);
            chart.ChartAreas.Clear();
            chart.Series.Clear();
            chart.Legends.Clear();
            chart.ChartAreas.Add("Draw");
            chart.ChartAreas[0].AxisY.Title = "총 LCC(억원)";
            chart.ChartAreas[0].AxisX.Minimum = int.Parse(m.p3.tb11.Text);

            chart.Legends.Add("l");

            for (int i = 0; i < solutions.Count; i++)
            {
                string sol = "s" + (i + 1).ToString();
                chart.Series.Add(sol);
                DrawSeries(chart.Series[sol], solutions[i], solutions[i].lcc);
                chart.Series[sol].ChartType = SeriesChartType.Line;
                chart.Series[sol].BorderWidth = 2;
            }
        }
        public void LoadPage()
        {
            for (int i = 0; i < 5; i++)
            {
                btns[0, i].Visibility = Visibility.Hidden;
                btns[1, i].Visibility = Visibility.Hidden;
            }
            for (int i = 0; i < m.db.p5_1_tabNum; i++)
            {
                btns[0, i].Visibility = Visibility.Visible;
            }
            for (int i = 0; i < m.db.p5_2_tabNum; i++)
            {
                btns[1, i].Visibility = Visibility.Visible;
            }
            clickT(0, 0);

            m.Content = this;
        }

        public void ChangeTF(int row, int col, bool b)
        {
            tf[row,col] = b;
            if (row == 0 && m.db.p5_1_tabNum <= col)
                tf[row, col] = false;
            else if (row == 1 && m.db.p5_2_tabNum <= col)
                tf[row, col] = false;
            var bc = new BrushConverter();
            if (tf[row, col])
            {
                btns[row, col].Background = (Brush)bc.ConvertFrom("#FF83C1DC");
                btns[row, col].Foreground= (Brush)bc.ConvertFrom("#FFFFFFFF");
            }
            else
            {
                btns[row, col].Background = (Brush)bc.ConvertFrom("#FFFFFFFF");
                btns[row, col].Foreground = (Brush)bc.ConvertFrom("#FF000000");
            }
        }

        private void preBtn(object sender, MouseButtonEventArgs e)
        {
            m.p5.LoadPage();
        }
        private void postBtn(object sender, MouseButtonEventArgs e)
        {
            string filePath = "";
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            saveFileDialog.Filter = "Chart Image (*.png)|*.png|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == true)
            {
                for (int i = 0; i < 4; i++)
                {
                    filePath = saveFileDialog.FileName.Replace(".png", (i + 1).ToString() + ".png");
                    // charts[i].Size = new Size();
                    charts[i].SaveImage(filePath, System.Windows.Forms.DataVisualization.Charting.ChartImageFormat.Png);
                }       
            }
                
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
        public void clickT(int row, int col)
        {
            if (row == 0)
            {
                ChangeTF(0, 0, false);
                ChangeTF(0, 1, false);
                ChangeTF(0, 2, false);
                ChangeTF(0, 3, false);
                ChangeTF(0, 4, false);
                ChangeTF(1, 0, true);
                ChangeTF(1, 1, true);
                ChangeTF(1, 2, true);
                ChangeTF(1, 3, true);
                ChangeTF(1, 4, true);
            }
            else
            {
                ChangeTF(0, 0, true);
                ChangeTF(0, 1, true);
                ChangeTF(0, 2, true);
                ChangeTF(0, 3, true);
                ChangeTF(0, 4, true);
                ChangeTF(1, 0, false);
                ChangeTF(1, 1, false);
                ChangeTF(1, 2, false);
                ChangeTF(1, 3, false);
                ChangeTF(1, 4, false);
            }
            ChangeTF(row, col, true);
            DrawChart();
        }
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            clickT(0, 0);
        }

        private void btn2_Click(object sender, RoutedEventArgs e)
        {
            clickT(0, 1);
        }
        private void btn3_Click(object sender, RoutedEventArgs e)
        {
            clickT(0, 2);
        }
        private void btn4_Click(object sender, RoutedEventArgs e)
        {
            clickT(0, 3);
        }
        private void btn5_Click(object sender, RoutedEventArgs e)
        {
            clickT(0, 4);
        }
        private void btn6_Click(object sender, RoutedEventArgs e)
        {
            clickT(1, 0);
        }
        private void btn7_Click(object sender, RoutedEventArgs e)
        {
            clickT(1, 1);
        }
        private void btn8_Click(object sender, RoutedEventArgs e)
        {
            clickT(1, 2);
        }
        private void btn9_Click(object sender, RoutedEventArgs e)
        {
            clickT(1, 3);
        }
        private void btn10_Click(object sender, RoutedEventArgs e)
        {
            clickT(1, 4);
        }
    }
}

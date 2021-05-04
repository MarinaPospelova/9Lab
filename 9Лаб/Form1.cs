using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Лаб9
{
    public partial class Form1 : Form
    {
        Statistical_processing SP = new Statistical_processing();
        public Form1()
        {
            InitializeComponent();
        }

        private void go_Click(object sender, EventArgs e)
        {
            if ((prob1.Value + prob2.Value + prob3.Value + prob4.Value) > 1M)
            {
                check.Text = "Ошибка нормировки";
            }
            else
            {
                check.Text = "Все прошло успешно";
                SP.SetData((int)number.Value, prob1.Value, prob2.Value, prob3.Value, prob4.Value);
                chart1.Series[0].Points.Clear();
                decimal[] pointsY = SP.GetData();
                for (int i = 0; i < pointsY.Length; i++)
                { 
                    chart1.Series[0].Points.AddXY(i + 1, pointsY[i]);
                }
                prob5.Text = Convert.ToString(1M - (prob1.Value + prob2.Value + prob3.Value + prob4.Value));
            }
            
        }
       
    }
    public class Statistical_processing
    {
        Random rnd = new Random();
        int N;
        int[] statistics;
        decimal[] prob;
        public void SetData(int N, params decimal[] m)
        {
            prob = new decimal[m.Length + 1];
            this.N = N;
            statistics = new int[m.Length + 1];
            statistics[m.Length] = 0;
            for (int i = 0; i < m.Length; i++)
            {
                prob[i] = m[i];
                statistics[i] = 0;
            }
            prob[m.Length] = 1 - prob[0] - prob[1] - prob[2] - prob[3];
        }
        public decimal[] GetData()
        {
            decimal A;
            int k = -1;
            decimal[] freq = new decimal[statistics.Length];
            for (int i = 0; i < N; i++)
            {
                A = (decimal)rnd.NextDouble();
                for (k = -1; A > 0; k++)
                {
                    A -= prob[k + 1];
                }
                statistics[k]++;
            }
            for (int i = 0; i < statistics.Length; i++)
            {
                freq[i] = (decimal)statistics[i] / N;
            }
            return freq;
        }
    }
}

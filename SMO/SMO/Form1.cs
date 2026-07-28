using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SMO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private double Factorial(int n)
        {
            double result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }

        private double Summ(int c, double p)
        {
            double r = 0;
            for (int i = 1; i <= c; i++)
            {
                r += Math.Pow(p, i) / Factorial(i);
            }
            return r;
        }
        public void СостоянияОтказы(int v, double p0, double p)
        {
            dataGridView1.Rows.Clear();
            int m = v;
            double[] pk = new double[m];
            double sumPk = 0;

            for (int k = 1; k <= m; k++)
            {
                pk[k-1] = (Math.Pow(p, k)/Factorial(k))*p0;
                sumPk += pk[k-1];
            }

            dataGridView1.Rows.Add(0, p0.ToString("F6"));
            for (int k = 0; k < m; k++)
            {
                dataGridView1.Rows.Add(k+1, pk[k].ToString("F6"));
            }
            dataGridView1.Rows.Add("Сумма", sumPk.ToString("F3"));
        }
        public void СостоянияОчередь(int v, double p0, double p, double w, int o)
        {
            dataGridView1.Rows.Clear();
            int m = v;
            double[] pk = new double[m];
            double[] pko = new double[o];
            double sumPk = 0;

            for (int k = 1; k <= m; k++)
            {
                pk[k - 1] = (Math.Pow(p, k) / Factorial(k)) * p0 * Math.Pow(w, m);
                sumPk += pk[k - 1];
            }
            for (int k = m+1; k <= m+o; k++)
            {
                pko[k - 1-m] = (Math.Pow(m, m) / Factorial(m)) * p0 * Math.Pow(w, k);                
            }

            dataGridView1.Rows.Add(0, p0.ToString("F6"));
            for (int k = 0; k < m; k++)
            {
                dataGridView1.Rows.Add(k + 1, pk[k].ToString("F6"));
            }
            for (int k = m; k < m+o; k++)
            {
                dataGridView1.Rows.Add($"Очередь {m} + {k-m+1}", pko[k-m].ToString("F6"));
            }
        }
        public double ЗаявкиОчередь(double w, int v, int m, double p0)
        {
            double res = 0;
            if (w == 1)
            {
                res = (Math.Pow(v, v) / Factorial(v)) * ((m * (m + 1)) / 2) * p0;
            }
            else
            {
                res = (Math.Pow(v, v) / Factorial(v)) * (Math.Pow(w, v + 1)) * ((1-(m+1)*Math.Pow(w,m)+m*Math.Pow(w,m+1))/Math.Pow(1-w,2)) * p0;
            }
            return res;
        }
        public void СостоянияОжидание(int v, double p0, double p, double w)
        {
            dataGridView1.Rows.Clear();
            int m = v;
            double[] pk = new double[m];
            double[] pko = new double[10];
            double sumPk = 0;

            for (int k = 1; k <= m; k++)
            {
                pk[k - 1] = (Math.Pow(p, k) / Factorial(k)) * p0 * Math.Pow(w, m);
                sumPk += pk[k - 1];
            }
            for (int k = m + 1; k <= m + 10; k++)
            {
                pko[k - 1 - m] = (Math.Pow(m, m) / Factorial(m)) * p0 * Math.Pow(w, k);
            }

            dataGridView1.Rows.Add(0, p0.ToString("F6"));
            for (int k = 0; k < m; k++)
            {
                dataGridView1.Rows.Add(k + 1, pk[k].ToString("F6"));
            }
            for (int k = m; k < m + 10; k++)
            {
                dataGridView1.Rows.Add($"Очередь {m} + {k - m + 1}", pko[k - m].ToString("F6"));
            }
        }
        public void СостоянияОграничение(int v, double p0, double p, double B)
        {
            dataGridView1.Rows.Clear();
            int m = v;
            double[] pk = new double[m];
            double[] pko = new double[10];
            double sumPk = 0;
            double rp = 1;

            for (int k = 1; k <= m; k++)
            {
                pk[k - 1] = (Math.Pow(p, k) / Factorial(k)) * p0;
                sumPk += pk[k - 1];
            }
            for (int k = m + 1; k <= m + 10; k++)
            {
                for (int j = v + 1; j <= k - v; j++)
                {
                    rp *= (v + j * B);
                }
                pko[k - 1 - m] = (1 / Factorial(m)) * Math.Pow(p, k) / rp;
                rp = 1;
            }

            dataGridView1.Rows.Add(0, p0.ToString("F6"));
            for (int k = 0; k < m; k++)
            {
                dataGridView1.Rows.Add(k + 1, pk[k].ToString("F6"));
            }
            for (int k = m; k < m + 10; k++)
            {
                dataGridView1.Rows.Add($"Очередь {m} + {k - m + 1}", pko[k - m].ToString("F6"));
            }
        }
        public double Summm(int v, double p, double B)
        {
            double r = 0;
            double rp = 1;
           
            for (int i = v+1; i <= v+100; i++)
            {
                for (int j = v+1; j <= i - v; j++)
                {
                    rp *= (v + j * B);
                }
                r += Math.Pow(p, i - v) / rp;
                rp = 1;
            }
            return r;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem != null)
            {
                string selectedValue = comboBox1.SelectedItem.ToString();
                if (double.TryParse(textBox1.Text, out double λ) &&
                    double.TryParse(textBox2.Text, out double μ) &&
                    int.TryParse(textBox3.Text, out int m) &&
                    int.TryParse(textBox4.Text, out int v))
                {
                    switch (selectedValue)
                    {
                        case "Одноканальная с отказами":
                            {
                                richTextBox1.Clear();
                                double p = λ / μ;
                                double to = 1 / μ;
                                double p0 = μ / (μ + λ);
                                double po = 1 - p0;
                                double Q = p0;
                                double A = Q * λ;
                                double tpr = po * to;
                                double L = p * Q;
                                double T = Q / μ;
                                richTextBox1.AppendText($"Интенсивность нагрузки: {p}\n");
                                richTextBox1.AppendText($"Время обслуживания заявки: {to}\n");
                                richTextBox1.AppendText($"Вероятность простоя системы: {p0}\n");
                                richTextBox1.AppendText($"Вероятность отказа: {po}\n");
                                richTextBox1.AppendText($"Относительная пропускная способность: {Q}\n");
                                richTextBox1.AppendText($"Абсолютная пропускная способность: {A}\n");
                                richTextBox1.AppendText($"Среднее время простоя системы: {tpr}\n");
                                richTextBox1.AppendText($"Среднее число обслуживаемых заявок: {L}\n");
                                richTextBox1.AppendText($"Среднее время обслуживания заявки: {T}\n");

                                break;
                            }                        
                        case "Многоканальная с отказами":
                            {
                                richTextBox1.Clear();
                                double p = λ / μ;
                                double p0 = 1/Summ(v, p);
                                СостоянияОтказы(v, p0, p);
                                double po = (Math.Pow(p, v) / Factorial(v))*p0;
                                double pobs = 1 - po;
                                double Q = pobs;
                                double A = λ * Q;
                                double pv = A;
                                double K = A / μ;
                                double T = K / λ;
                                richTextBox1.AppendText($"Интенсивность нагрузки: {p}\n");
                                richTextBox1.AppendText($"Вероятность простоя системы: {p0}\n");
                                richTextBox1.AppendText($"Вероятность отказа: {po}\n");
                                richTextBox1.AppendText($"Относительная пропускная способность: {Q}\n");
                                richTextBox1.AppendText($"Абсолютная пропускная способность: {A}\n");
                                richTextBox1.AppendText($"Вероятность обслуживания заявки: {pobs}\n");
                                richTextBox1.AppendText($"Интенсивность выходящего потока: {pv}\n");
                                richTextBox1.AppendText($"Среднее число занятых каналов: {K}\n");
                                richTextBox1.AppendText($"Среднее время пребывания заявки в системе: {T}\n");
                                break;
                            }
                        case "Многоканальная с ограниченной очередью":
                            {
                                richTextBox1.Clear();
                                double p = λ / μ;
                                double w = p / v;
                                double p0 = 1 / (Summ(v, p) + m * (p * p / Factorial(v)));
                                СостоянияОчередь(v, p0, p, w, m);
                                double po = (Math.Pow(v, v) / Factorial(v)) * (Math.Pow(w, v+m)*p0);
                                double pobs = 1 - po;
                                double Q = pobs;
                                double A = Q * λ;
                                double K = p*Q;
                                double No = ЗаявкиОчередь(w, v, m, p0);
                                double N = K + No;
                                double To = No / λ;
                                double Ts = N / λ;
                                double Tobs = Q / μ;
                                richTextBox1.AppendText($"Интенсивность нагрузки: {p}\n");
                                richTextBox1.AppendText($"Вероятность простоя системы: {p0}\n");
                                richTextBox1.AppendText($"Вероятность отказа: {po}\n");
                                richTextBox1.AppendText($"Относительная пропускная способность: {Q}\n");
                                richTextBox1.AppendText($"Абсолютная пропускная способность: {A}\n");
                                richTextBox1.AppendText($"Вероятность обслуживания заявки: {pobs}\n");
                                richTextBox1.AppendText($"Нагрузка на один канал: {w}\n");
                                richTextBox1.AppendText($"Среднее число занятых каналов: {K}\n");
                                richTextBox1.AppendText($"Среднее число заявок в очереди: {No}\n");
                                richTextBox1.AppendText($"Среднее число заявок в системе: {N}\n");
                                richTextBox1.AppendText($"Среднее время пребывания заявки в системе: {Ts}\n");
                                richTextBox1.AppendText($"Среднее время пребывания заявки в очереди: {To}\n");
                                richTextBox1.AppendText($"Среднее время обслуживания заявки: {Tobs}\n");
                                break;
                            }
                        case "Многоканальная с ожиданием":
                            {
                                richTextBox1.Clear();
                                double p = λ / μ;
                                double w = p / v;
                                double p0 = 1 / ((Summ(v, p)+(Math.Pow(p,v+1)*Factorial(v)*(v-p))));
                                СостоянияОжидание(v, p0, p, w);
                                int po = 0;
                                int pobs = 1;
                                double Q = 1;
                                double A = λ;
                                double K = p;
                                double No = (Math.Pow(v, v) / Factorial(v)) * p0 * (Math.Pow(w,v+1)/Math.Pow(1.0-w,2));
                                double Ns = K + No;
                                double To = No / λ;
                                double Ts = Ns / λ;
                                double T = p / λ;
                                richTextBox1.AppendText($"Интенсивность нагрузки: {p}\n");
                                richTextBox1.AppendText($"Вероятность простоя системы: {p0}\n");
                                richTextBox1.AppendText($"Вероятность отказа: {po}\n");
                                richTextBox1.AppendText($"Относительная пропускная способность: {Q}\n");
                                richTextBox1.AppendText($"Абсолютная пропускная способность: {A}\n");
                                richTextBox1.AppendText($"Вероятность обслуживания заявки: {pobs}\n");
                                richTextBox1.AppendText($"Нагрузка на один канал: {w}\n");
                                richTextBox1.AppendText($"Среднее число занятых каналов: {K}\n");
                                richTextBox1.AppendText($"Среднее число заявок в очереди: {No}\n");
                                richTextBox1.AppendText($"Среднее число заявок в системе: {Ns}\n");
                                richTextBox1.AppendText($"Среднее время пребывания заявки в системе: {Ts}\n");
                                richTextBox1.AppendText($"Среднее время пребывания заявки в очереди: {To}\n");
                                richTextBox1.AppendText($"Среднее время обслуживания заявки: {T}\n");
                                break;
                            }
                        case "Многоканальная с ограниченным временем ожидания":
                            {
                                double i = double.Parse(textBox5.Text);
                                richTextBox1.Clear();
                                double p = λ / μ;
                                double B = i / μ;
                                double w = p / v;
                                double p0 = 1/(Summ(v, p) + (Math.Pow(p, v) / Factorial(v)) * Summm(v, p, B));
                                СостоянияОграничение(v, p0, p, B);
                                double No = (Math.Pow(p, v) / Factorial(v)) * p0 * Summm(v, p, B);
                                double K = p - B * No;
                                double Ns = No + K;
                                double A = λ - i * No;
                                double Q = A / λ;
                                double pobs = Q;
                                double pu = 1 - pobs;
                                double Tch = 0;
                                double To = 1 / μ;
                                double T = Q / μ;
                                double Ts = T + Tch;                                
                                richTextBox1.AppendText($"приведенная интенсивность ухода заявок: {B}\n");
                                richTextBox1.AppendText($"Интенсивность нагрузки: {p}\n");
                                richTextBox1.AppendText($"Вероятность простоя системы: {p0}\n");                                
                                richTextBox1.AppendText($"Относительная пропускная способность: {Q}\n");
                                richTextBox1.AppendText($"Абсолютная пропускная способность: {A}\n");
                                richTextBox1.AppendText($"Вероятность обслуживания заявки: {pobs}\n");
                                richTextBox1.AppendText($"Нагрузка на один канал: {w}\n");
                                richTextBox1.AppendText($"Среднее число занятых каналов: {K}\n");
                                richTextBox1.AppendText($"Среднее число заявок в очереди: {No}\n");
                                richTextBox1.AppendText($"Среднее число заявок в системе: {Ns}\n");
                                richTextBox1.AppendText($"Среднее время пребывания заявки в системе: {Ts}\n");
                                richTextBox1.AppendText($"Среднее время пребывания заявки в очереди: {Tch}\n");
                                richTextBox1.AppendText($"Среднее время обслуживания заявки: {To}\n");
                                richTextBox1.AppendText($"Среднее время обслуживания заявки для всех: {T}\n");
                                break;
                            }
                        case "Замкнутая многоканальная":
                            {
                                break;
                            }
                        case "Многоканальная без очереди все как один":
                            {
                                break;
                            }
                        case "Многоканальная с очередью все как один":
                            {
                                break;
                            }
                    }
                }
                else
                {
                    MessageBox.Show("Одно или несколько значений в текстбоксах не являются числами.");
                }
            }
            else
            {
                MessageBox.Show("Не выбран элемент в комбобоксе.");
            }
        }
    }
}

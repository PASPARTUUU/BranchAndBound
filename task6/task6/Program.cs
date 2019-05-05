using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace task6
{
    class Program
    {
        static void Main(string[] args)
        {
            m1:
            Console.WriteLine("Введите количество переменных:");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Введите количество строк (целевая функция и ограничения):");
            int m = Convert.ToInt32(Console.ReadLine());
            //double p1 = Task6_Gain_Memory_Min(n, m);
            //double p2 = Task6_Gain_Memory_Max(n,m);
            //double p3 = Task6_Gain_Memory_Avr(n, m);

            //double p4 = Task6_Gain_Time_Min(n, m);
            //double p5 = Task6_Gain_Time_Max(n, m);
            //double p6 = Task6_Gain_Time_Avr(n, m);

            //double p1 = Task6_Analitic_Time_Average1(n, m);
            //double p2 = Task6_Analitic_Time_Average2(n, m);
            //double p3 = Task6_Analitic_Time_Max1(n, m);

            //double p4 = Task6_Analitic_Time_Max2(n, m);
            //double p5 = Task6_Analitic_Time_Min1(n, m);
            //double p6 = Task6_Analitic_Time_Min2(n, m);
            //Console.WriteLine("Task6_Analitic_Time_Average1 " + p1.ToString());
            //Console.WriteLine("Task6_Analitic_Time_Average2 " + p2.ToString());
            //Console.WriteLine("Task6_Analitic_Time_Max1 " + p3.ToString());

            //Console.WriteLine("Task6_Analitic_Time_Max2 " + p4.ToString());
            //Console.WriteLine("Task6_Analitic_Time_Min1 " + p5.ToString());
            //Console.WriteLine("Task6_Analitic_Time_Min2 " + p6.ToString());
            //goto m1;
            Console.WriteLine("Введите систему");
            string[] system = new string[m];
            for (int i = 0; i < m; i++)
            {
                system[i] = Console.ReadLine();
            }
            string[,] input = new string[m, n + 2];
            for (int i = 0; i < m; i++)
            {
                string[] spl = system[i].Split(' ');
                for (int j = 0; j < n + 2; j++)
                    input[i, j] = spl[j];
            }
            ;
            string[] result_traditional = new string[5];
            result_traditional = Task6_1(input, n, m);
            string[] result_kompozit = Task6_2(input, n, m);

            for (int i = 0; i < 5; i++)
            {
                Console.Write(result_traditional[i] + "; ");
            }
            Console.WriteLine();
            for (int i = 0; i < 5; i++)
            {
                Console.Write(result_kompozit[i] + "; ");
            }
            Console.WriteLine(" ");
            Console.ReadKey();
            //goto m1;
        }
        //-------------время--------------
        //аналитическое среднее время традиционного метода
        //n-количество переменных
        //m-число ограничений
        public static double Task6_Analitic_Time_Average1(int n, int m)
        {
            return (0.00005952 * m + 0.00085804)* Math.Pow(Math.E, n * (0.77863234 * Math.Pow(0.99931568,m)));
        }
        //аналитическое среднее время композитного метода
        public static double Task6_Analitic_Time_Average2(int n, int m)
        {
            return (0.00001951 * m + 0.00228743) * Math.Pow(Math.E, n * (0.01625316 * m + 0.57157309));
        }
        //аналитическое максимальное время традиционного метода
        public static double Task6_Analitic_Time_Max1(int n, int m)
        {
            return 0.00164365 * Math.Pow(1.01388602,m)* Math.Pow(Math.E, n * (0.85124279* Math.Pow(0.99833193,m)));
        }
        //аналитическое максимальное время композитного метода
        public static double Task6_Analitic_Time_Max2(int n, int m)
        {
            return 0.00599185 * Math.Pow(m, -0.1890484)* Math.Pow(Math.E, n * (0.5507342 * Math.Pow(m, 0.14352338)));
        }
        //аналитическое минимальное время традиционного метода
        public static double Task6_Analitic_Time_Min1(int n, int m)
        {
            return 0.00136152 * Math.Pow(1.01818566, m) * Math.Pow(Math.E, n * (0.00995677 * m + 0.54331116));
        }
        //аналитическое минимальное время композитного метода
        public static double Task6_Analitic_Time_Min2(int n, int m)
        {
            return (0.00002023 * Math.Pow(m, 2) - 0.00022163 * m + 0.00220231) * Math.Pow(Math.E, n * (0.48914653 * Math.Pow(m, 0.12506814)));
        }
        //-------------объем оп. памяти--------------
        //аналитический средний объем оп. памяти традиционного метода
        public static double Task6_Analitic_Memory_Average1(int n, int m)
        {
            return 0.88816256 * Math.Pow(m, -0.31794292) * Math.Pow(Math.E, n * (0.49777442 - 0.05418289/m));
        }
        //аналитический среднее объем оп. памяти композитного метода
        public static double Task6_Analitic_Memory_Average2(int n, int m)
        {
            return 1.66259565 * Math.Pow(0.87334426, m) * Math.Pow(Math.E, n * 0.18935298 * Math.Pow(m, 0.37424554));
        }
        //аналитический максимальный объем оп. памяти традиционного метода
        public static double Task6_Analitic_Memory_Max1(int n, int m)
        {
            return 0.98218148 * Math.Pow(0.94997828, m) * Math.Pow(Math.E, n * (0.00013783 * Math.Pow(m, 3) - 0.00246449 * Math.Pow(m, 2) + 0.01376966 * m + 0.49818591));
        }
        //аналитический максимальный объем оп. памяти композитного метода
        public static double Task6_Analitic_Memory_Max2(int n, int m)
        {
            return Math.Pow(Math.E, 1.28926863 - 0.16146426 * m) * Math.Pow(Math.E, n * 0.18067668 * Math.Pow(m, 0.42485622));
        }
        //аналитический минимальный объем оп. памяти традиционного метода
        public static double Task6_Analitic_Memory_Min1(int n, int m)
        {
            return (0.00857201 * Math.Pow(m, 2) - 0.13185945 * m + 0.76287231) * Math.Pow(Math.E, n * (0.46617288 - 0.0855267 / m));
        }
        //аналитический минимальный объем оп. памяти композитного метода
        public static double Task6_Analitic_Memory_Min2(int n, int m)
        {
            return (0.15531629 + 0.4551922 / m) * Math.Pow(Math.E, n * 0.2111227 * Math.Pow(m, 0.30445901));
        }
        //Выигрыш в объеме использованной оперативной памяти
        //Минимальный выигрыш в объеме использованной оперативной памяти
        public static double Task6_Gain_Memory_Min(int n, int m)
        {
            return Task6_Analitic_Memory_Min1(n, m) / Task6_Analitic_Memory_Min2(n, m);
        }
        //Максимальный выигрыш в объеме использованной оперативной памяти
        public static double Task6_Gain_Memory_Max(int n, int m)
        {
            return Task6_Analitic_Memory_Max1(n, m) / Task6_Analitic_Memory_Max2(n, m);
        }
        //Средний выигрыш в объеме использованной оперативной памяти
        public static double Task6_Gain_Memory_Avr(int n, int m)
        {
            return Task6_Analitic_Memory_Average1(n, m) / Task6_Analitic_Memory_Average2(n, m);
        }
        //Выигрыш во времени счета
        //Минимальный выигрыш во времени счета
        public static double Task6_Gain_Time_Min(int n, int m)
        {
            return Task6_Analitic_Time_Min1(n, m) / Task6_Analitic_Time_Min2(n, m);
        }
        //Максимальный выигрыш во времени счета
        public static double Task6_Gain_Time_Max(int n, int m)
        {
            return Task6_Analitic_Time_Max1(n, m) / Task6_Analitic_Time_Max2(n, m);
        }
        //Средний выигрыш во времени счета
        public static double Task6_Gain_Time_Avr(int n, int m)
        {
            return Task6_Analitic_Time_Average1(n, m) / Task6_Analitic_Time_Average2(n, m);
        }
        private static string[] Task6_1(string[,] input, int n, int m)
        {
            string[] output = new string[5];
            module6 metod1 = new module6();
            double[,] dateMatrix = new double[m, n + 1];

            string[] sign = new string[m];
            for (int i = 1; i < m; i++)
            {
                if (input[i, n] == "<")
                {
                    sign[i] = "<=";
                    dateMatrix[i, n] = Convert.ToDouble(input[i, n + 1]) - 1;
                }
                else if (input[i, n] == ">")
                {
                    sign[i] = ">=";
                    dateMatrix[i, n] = (Convert.ToDouble(input[i, n + 1])) + 1;
                }
                else
                {
                    dateMatrix[i, n] = Convert.ToDouble(input[i, n + 1]);
                    sign[i] = input[i, n];
                }
            }

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    dateMatrix[i, j] = Convert.ToDouble(input[i, j]);
            ;
            string temp;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            temp = metod1.modifiedClassicFrontDescent(dateMatrix, n + 1, m, input[0, n + 1] == "max" ? true : false, sign);
            sw.Stop();
            //количество всех вершин в готовом графе
            output[0] = "";
            for (int i = temp.IndexOf("|X1| = "); i < temp.IndexOf("; |X1^T| = "); i++)
            {
                output[0] += temp[i];
            }
            output[0] = output[0].Replace("|X1| = ", "");
            //количество висячих вершин в готовом графе
            output[1] = "";
            for (int i = temp.IndexOf("|X1^T| = "); i < temp.Length; i++)
            {
                output[1] += temp[i];
            }
            output[1] = output[1].Replace("|X1^T| = ", "");
            //Затраченное время
            output[2] = sw.Elapsed.TotalMilliseconds.ToString();
            //результат решения 1 (сумма)
            output[3] = "";
            for (int i = 4; i < temp.IndexOf("; Z = "); i++)
            {
                output[3] += temp[i];
            }
            //результат решения 2 (путь)
            output[4] = "";
            for (int i = temp.IndexOf("Z = "); i < temp.IndexOf("; |X1| = "); i++)
            {
                output[4] += temp[i];
            }
            output[4] = output[4].Replace("Z = ", "");
            return output;
        }


        private static string[] Task6_2(string[,] input, int n, int m)
        {
            string[] output = new string[5];
            module6 metod2 = new module6();
            double[,] dateMatrix = new double[m, n + 1];

            string[] sign = new string[m];
            for (int i = 1; i < m; i++)
            {
                if (input[i, n] == "<")
                {
                    sign[i] = "<=";
                    dateMatrix[i, n] = Convert.ToDouble(input[i, n + 1]) - 1;
                }
                else if (input[i, n] == ">")
                {
                    sign[i] = ">=";
                    dateMatrix[i, n] = Convert.ToDouble(input[i, n + 1]) + 1;
                }
                else
                {
                    dateMatrix[i, n] = Convert.ToDouble(input[i, n + 1]);
                    sign[i] = input[i, n];
                }
            }
            ;
                      
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    dateMatrix[i, j] = Convert.ToDouble(input[i, j]);

            string temp;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            temp = metod2.modifiedCompositeFrontDescent(dateMatrix, n + 1, m, input[0, n + 1] == "max" ? true : false, sign);
            sw.Stop();
            //количество всех вершин в готовом графе
            output[0] = "";
            for (int i = temp.IndexOf("|X2| = "); i < temp.IndexOf("; |X2^T| = "); i++)
            {
                output[0] += temp[i];
            }
            output[0] = output[0].Replace("|X2| = ", "");
            //количество висячих вершин в готовом графе
            output[1] = "";
            for (int i = temp.IndexOf("|X2^T| = "); i < temp.Length; i++)
            {
                output[1] += temp[i];
            }
            output[1] = output[1].Replace("|X2^T| = ", "");
            //Затраченное время
            output[2] = sw.Elapsed.TotalMilliseconds.ToString();
            //результат решения 1 (сумма)
            output[3] = "";
            for (int i = 4; i < temp.IndexOf("; Z = "); i++)
            {
                output[3] += temp[i];
            }
            //результат решения 2 (путь)
            output[4] = "";
            for (int i = temp.IndexOf("Z = "); i < temp.IndexOf("; |X2| = "); i++)
            {
                output[4] += temp[i];
            }
            output[4] = output[4].Replace("Z = ", "");
            return output;
        }
    }
}

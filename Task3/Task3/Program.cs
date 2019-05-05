using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
           Console.WriteLine("Введите количество переменных:");
           int n=Convert.ToInt32( Console.ReadLine());
           Console.WriteLine("Введите количество строк (целевая функция и ограничения):");
            int m=Convert.ToInt32( Console.ReadLine());
             Console.WriteLine("Введите систему");
             string[] system = new string[m];
             for (int i = 0; i < m; i++)
             {
                 system[i] = Console.ReadLine();
             }
            string[,] input=new string[m,n+2];
            for (int i = 0; i < m; i++)
            {
                string[] spl = system[i].Split(' ');
                for (int j = 0; j < n + 2; j++)
                    input[i,j] = spl[j];
            }


            string[] result_traditional=new string[5];
            result_traditional = Task3_1(input, n, m);
            string[] result_kompozit = Task3_2(input, n, m);

            for (int i = 0; i < 5; i++)
            {
                Console.Write(result_traditional[i] + "; ");
            }
            Console.WriteLine();
            for (int i = 0; i < 5; i++)
            {
                Console.Write(result_kompozit[i] + "; ");
            }
            Console.ReadKey();
        }

        private static string[] Task3_1(string[,] input, int n, int m)
        {
            string[] output = new string[5];
            FrontalDescend metod1 = new FrontalDescend();

            double[,] dateMatrix = new double[m, n+2];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                {
                    
                    dateMatrix[i, j] = Convert.ToDouble(input[i,j]);
                }
            for (int i = 1; i < m; i++)
            { dateMatrix[i, n+1]=  Convert.ToDouble(input[i, n + 1]);
                switch(input[i, n])
            {
                              case ">=":
                               dateMatrix[i,n]=0;
                                    break;
                                case "<=":
                               dateMatrix[i,n]=1;
                                    break;
                                case ">":
                                    dateMatrix[i, n] = 0;
                                dateMatrix[i, n+1]=  Convert.ToDouble(input[i, n + 1])+1;              
                                    break;
                                case "<":
                                    dateMatrix[i, n] = 1; 
                              dateMatrix[i, n+1]=  Convert.ToDouble(input[i, n + 1])-1;                 
                                    break;
                                default:
                                dateMatrix[i,n]=-1;
                    Exception exep=new Exception("Неверно заданы знаки сравнения в ограничениях!",new Exception());                    
                                    throw(exep);
                                    break;
            }                
            }

            string temp;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            temp = metod1.Calculations(dateMatrix, n+2, m, input[0, n + 1] == "max" ? true : false);
            sw.Stop();
            //количество всех вершин в готовом графе
            output[0]="";
            for(int i=temp.IndexOf("N1=");i<temp.IndexOf("; N2=");i++)
            {
            output[0]+=temp[i];
            }
            output[0] = output[0].Replace("N1=", "");
            //количество висячих вершин в готовом графе
            output[1] = "";
            for (int i = temp.IndexOf("N2="); i < temp.Length; i++)
            {
                output[1] += temp[i];
            }
            output[1] = output[1].Replace("N2=", "");
            //Затраченное время
            output[2] = sw.ElapsedMilliseconds.ToString();
            //результат решения 1 (сумма)
            output[3] = "";
            for (int i = 2; i < temp.IndexOf("; z="); i++)
            {
                output[3] += temp[i];
            }
            //результат решения 2 (путь)
            output[4] = "";
            for (int i = temp.IndexOf("z="); i < temp.IndexOf("; N1="); i++)
            {
                output[4] += temp[i];
            }
            output[4] = output[4].Replace("z-", "");
            return output;
        }


        private static string[] Task3_2(string[,] input, int n, int m)
        {
            string[] output = new string[5];
            FrontalDescend metod2 = new FrontalDescend();

            double[,] dateMatrix = new double[m, n + 2];
            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                {

                    dateMatrix[i, j] = Convert.ToDouble(input[i, j]);
                }
            for (int i = 1; i < m; i++)
            {
                dateMatrix[i, n + 1] = Convert.ToDouble(input[i, n + 1]);
                switch (input[i, n])
                {
                    case ">=":
                        dateMatrix[i, n] = 0;
                        break;
                    case "<=":
                        dateMatrix[i, n] = 1;
                        break;
                    case ">":
                        dateMatrix[i, n] = 0;
                        dateMatrix[i, n + 1] = Convert.ToDouble(input[i, n + 1]) + 1;
                        break;
                    case "<":
                        dateMatrix[i, n] = 1;
                        dateMatrix[i, n + 1] = Convert.ToDouble(input[i, n + 1]) - 1;
                        break;
                    default:
                        dateMatrix[i, n] = -1;
                        Exception exep = new Exception("Неверно заданы знаки сравнения в ограничениях!", new Exception());
                        throw (exep);
                        break;
                }
            }


            string temp;
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            temp = metod2.Calculations2(dateMatrix, n + 2, m, input[0, n + 1] == "max" ? true : false);
            sw.Stop();
            //количество всех вершин в готовом графе
            output[0] = "";
            for (int i = temp.IndexOf("N1="); i < temp.IndexOf("; N2="); i++)
            {
                output[0] += temp[i];
            }
            output[0] = output[0].Replace("N1=", "");
            //количество висячих вершин в готовом графе
            output[1] = "";
            for (int i = temp.IndexOf("N2="); i < temp.Length; i++)
            {
                output[1] += temp[i];
            }
            output[1] = output[1].Replace("N2=", "");
            //Затраченное время
            output[2] = sw.ElapsedMilliseconds.ToString();
            //результат решения 1 (сумма)
            output[3] = "";
            for (int i = 2; i < temp.IndexOf("; z="); i++)
            {
                output[3] += temp[i];
            }
            //результат решения 2 (путь)
            output[4] = "";
            for (int i = temp.IndexOf("z="); i < temp.IndexOf("; N1="); i++)
            {
                output[4] += temp[i];
            }
            output[4] = output[4].Replace("z-", "");
            return output;
        }
    }
}

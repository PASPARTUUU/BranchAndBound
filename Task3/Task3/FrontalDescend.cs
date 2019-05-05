using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class FrontalDescend
    {
        const int infinity = int.MaxValue;//99999999;
        //описывает текущие "висячие" вершины (узлы)
        //string показывает имя узла, то есть путь к этому узлу (напр. "1011" значит что z1=1;z2=0;z3=1;z4=1)
        Dictionary<string, Node> hangingNodes;        
        //матрица условий
        double[,] givenConditionalMatrix;
        //копируем матрицу условий из функции
        private void CopyMatrix(double[,] conditionalMatrix, int columnsNumber, int rowsNumber)
        {
            givenConditionalMatrix = new double[rowsNumber,columnsNumber];

            for (int i = 0; i < rowsNumber; i++)
                for (int j = 0; j < columnsNumber; j++)
                    givenConditionalMatrix[i, j] = conditionalMatrix[i, j];
        }
        //копируем матрицу условий из функции
        private void CopyMatrix2(double[,] conditionalMatrix, int columnsNumber, int rowsNumber)
        {
            //если это композитный метод и количество переменных нечетное (без столбца "а"), то добавляем нулевую переменную
            if(columnsNumber%2!=0)            
            givenConditionalMatrix = new double[rowsNumber, columnsNumber+1];
            else
                givenConditionalMatrix = new double[rowsNumber, columnsNumber];
            //копирование матрицы
            for (int i = 0; i < rowsNumber; i++)
                for (int j = 0; j < columnsNumber; j++)
                    givenConditionalMatrix[i, j] = conditionalMatrix[i, j];
            //добавление нулевой переменной
            if (columnsNumber % 2 != 0)
            {
                for (int i = 0; i < rowsNumber; i++)
                {
                    givenConditionalMatrix[i, columnsNumber] = conditionalMatrix[i, columnsNumber - 1];
                    givenConditionalMatrix[i, columnsNumber-1] = conditionalMatrix[i, columnsNumber - 2];

                    givenConditionalMatrix[i, columnsNumber - 2] = 0;
                }
            }
        }
        //объявление hangingNodes
        private void HangingNodesDeclare()
        {
            hangingNodes = new Dictionary<string, Node>();
        }
        //функция вычисления результата
        //на вход подается матрица условий и к чему стремиться функция (если true-max,false-min)
        //матрица условий содержит:
        //1-я строка это все "z" функции
        //следующие строки "z" условий
        //последний столбец равен "а" (то есть ограничениям условий)
        //в первой строке последний столбец "а" не используется, т. к. предназначен не для функции
        public string Calculations(double[,] conditionalMatrix, int columnsNumber, int rowsNumber, bool maxMin)
        {

            bool stop = false;
            bool noSolution = false;
            Node resultNode=new Node();
            string resultPath="";
            int numberOfNodes=0;
            int numberOfHangingNodes=0;

            HangingNodesDeclare();
            CopyMatrix(conditionalMatrix,columnsNumber,rowsNumber);
            while (!stop && !noSolution)
            {

                //1.ищем максимальный/минимальный узел и запоминаем его
                //2.от него пускаем 2 новых
                //3.удаляем текущий
                //идем в 1
                double max=0,min = infinity;
                string maxPath = "";
                if(maxMin)
                foreach (var hangingNode in hangingNodes)
                {
                    

                    if (hangingNode.Value.sum > max || (hangingNode.Value.sum==max && String.Compare(hangingNode.Key,maxPath)>0))
                    {
                        
                        max = hangingNode.Value.sum;
                        maxPath = hangingNode.Key;
                    }
                }
                else
                    foreach (var hangingNode in hangingNodes)
                    {
                            //long temp = -1;

                       // if (maxPath != "")
                           // temp = Convert.ToInt64(maxPath);
                        if (hangingNode.Value.sum < min || (hangingNode.Value.sum == max && String.Compare(hangingNode.Key, maxPath) > 0))
                        {
                            min = hangingNode.Value.sum;
                            maxPath = hangingNode.Key;
                        }
                    }
                //если программа только начинается и узлов еще нет
                if (maxPath == "")
                {
                    if (hangingNodes.Count() > 0)
                        noSolution = true;
                    else
                    {
                        Node newNode = GetNode1("1", columnsNumber, rowsNumber, maxMin);
                        hangingNodes.Add("1", newNode);
                        newNode = GetNode1("0", columnsNumber, rowsNumber, maxMin);
                        hangingNodes.Add("0", newNode);
                        numberOfNodes += 2;
                    }
                }
                else
                {
                    if (maxPath.Length < columnsNumber - 2)
                    {
                        Node newNode = GetNode1(maxPath + "1", columnsNumber, rowsNumber, maxMin);
                        hangingNodes.Add(maxPath + "1", newNode);
                        newNode = GetNode1(maxPath + "0", columnsNumber, rowsNumber, maxMin);
                        hangingNodes.Add(maxPath + "0", newNode);
                        hangingNodes.Remove(maxPath);
                        numberOfNodes += 2;
                       /* string[] paths;
                        int n = 0;
                        foreach (var hangingNode in hangingNodes)
                        {
                            n++;
                        }
                        paths = new string[n];
                        n = 0;
                        Node[] newNode;
                        string[] newPath;
                        foreach (var hangingNode in hangingNodes)
                        {
                            n+=2;
                        }
                        newNode = new Node[n];
                        newPath=new string[n];
                        int k = 0;
                        n = 0;
                        foreach (var hangingNode in hangingNodes)
                        {
                            //запоминаем в string уже не нужные пути
                            paths[n] = hangingNode.Key;
                            n++;
                            //добавляем 2 вершины из каждой текущей висячей
                            newPath[k]=hangingNode.Key+"1";
                             newNode[k] = GetNode1(hangingNode.Key+"1", columnsNumber, rowsNumber, maxMin);
                             k++;
                             newNode[k] = GetNode1(hangingNode.Key + "0", columnsNumber, rowsNumber, maxMin); 
                            newPath[k]=hangingNode.Key+"0";
                            k++;
                            numberOfNodes += 2;
                        }
                        k = 0;
                        foreach (var hangingNode in newNode)
                        {
                            hangingNodes.Add(newPath[k], hangingNode);
                            k++;
                        }
                        foreach (var path in paths)
                        {
                            hangingNodes.Remove(path);
                        }*/
                    }
                }

                max = 0;
                min = int.MaxValue-1;
                if (maxMin)
                    foreach (var hangingNode in hangingNodes)
                    {
                        if (hangingNode.Value.sum > max)
                        {
                            max = hangingNode.Value.sum;
                        }
                    }
                else                
                    foreach (var hangingNode in hangingNodes)
                    {
                        if (hangingNode.Value.sum < min)
                        {
                            min = hangingNode.Value.sum;
                        }
                    }
                
                stop = false;
                numberOfHangingNodes = 0;
                if(maxMin)
                foreach (var hangingNode in hangingNodes)
                {
                    
                    if (hangingNode.Value.sum == max && hangingNode.Key.Length == columnsNumber - 2 && String.Compare(hangingNode.Key,resultPath)>0)
                    {
                        stop = true;
                        resultNode = hangingNode.Value;
                        resultPath = hangingNode.Key;
                    }
                    numberOfHangingNodes++;
                }
                else
                foreach (var hangingNode in hangingNodes)
                {
                   // int temp = -1;
                    //if (resultPath != "")
                    //    temp = Convert.ToInt32(resultPath);
                    if (hangingNode.Value.sum == min && hangingNode.Key.Length == columnsNumber - 2 && String.Compare(hangingNode.Key,resultPath)>0)
                    {
                        stop = true;
                        resultNode = hangingNode.Value;
                        resultPath = hangingNode.Key;
                    }
                    numberOfHangingNodes++;
                }
            }     
           
            
                
            //строка результата содержит: 
            //1.конечную сумму;
            //2.конечное значение z;
            //3.конечное число вершин построенного дерева;
            //4.конечное число "висячих" вершин
            string result = "F="+resultNode.sum.ToString()+"; z="+resultPath+"; N1="+(numberOfNodes+1).ToString()+"; N2="+numberOfHangingNodes.ToString();
            if(noSolution)
                result = "F=" + "no solution" + "; z=" + "no solution" + "; N1=" + (numberOfNodes + 1).ToString() + "; N2=" + numberOfHangingNodes.ToString();
            return result;
        }
        //функция вычисления результата
        //на вход подается матрица условий и к чему стремиться функция (если true-max,false-min)
        //матрица условий содержит:
        //1-я строка это все "z" функции
        //следующие строки "z" условий
        //последний столбец равен "а" (то есть ограничениям условий)
        //в первой строке последний столбец "а" не используется, т. к. предназначен не для функции
        public string Calculations2(double[,] conditionalMatrix, int columnsNumber, int rowsNumber, bool maxMin)
        {

            bool stop = false;
            Node resultNode = new Node();
            bool noSolution = false;
            string resultPath = "";
            int numberOfNodes = 0;
            int numberOfHangingNodes = 0;

            HangingNodesDeclare();
            CopyMatrix2(conditionalMatrix, columnsNumber, rowsNumber);
            while (!stop && !noSolution)
            {

                //1.ищем максимальный/минимальный узел и запоминаем его
                //2.от него пускаем 2 новых и от них по 2 новых, захватывая 2 вершины
                //3.считаем прибавленное количество узлов
                //4.удаляем текущий и 2 следующих (таким образом остается 4 новых) шаг 1                
                double max = 0, min = infinity;
                string maxPath = "";
                if (maxMin)
                    foreach (var hangingNode in hangingNodes)
                    {
                       
                        if (hangingNode.Value.sum > max || (hangingNode.Value.sum == max && String.Compare(hangingNode.Key,maxPath)>0))
                        {
                            max = hangingNode.Value.sum;
                            maxPath = hangingNode.Key;
                        }
                    }
                else
                    foreach (var hangingNode in hangingNodes)
                    {
                       
                        if (hangingNode.Value.sum < min || (hangingNode.Value.sum == max && String.Compare(hangingNode.Key,maxPath)>0))
                        {
                            min = hangingNode.Value.sum;
                            maxPath = hangingNode.Key;
                        }
                    }
                //если программа только начинается и узлов еще нет
                if (maxPath == "")
                {
                    if (hangingNodes.Count() > 0)
                        noSolution = true;
                    else
                    {
                        //если число переменных больше 1
                        if (givenConditionalMatrix.GetLength(1) - 2 > 1)
                        {
                            Node newNode1 = GetNode("11", givenConditionalMatrix.GetLength(1), rowsNumber, maxMin);
                            hangingNodes.Add("11", newNode1);
                            Node newNode2 = GetNode("10", givenConditionalMatrix.GetLength(1), rowsNumber, maxMin);
                            hangingNodes.Add("10", newNode2);
                            Node newNode3 = GetNode("01", givenConditionalMatrix.GetLength(1), rowsNumber, maxMin);
                            hangingNodes.Add("01", newNode3);
                            Node newNode4 = GetNode("00", givenConditionalMatrix.GetLength(1), rowsNumber, maxMin);
                            hangingNodes.Add("00", newNode4);
                            numberOfNodes += 6;

                            if (ToDelete("11", newNode1, givenConditionalMatrix.GetLength(1), rowsNumber, maxMin))
                            {
                                hangingNodes.Remove("11");
                            }
                            if (ToDelete("10", newNode2, givenConditionalMatrix.GetLength(1), rowsNumber, maxMin))
                            {
                                hangingNodes.Remove("10");
                            }
                            if (ToDelete("01", newNode3, givenConditionalMatrix.GetLength(1), rowsNumber, maxMin))
                            {
                                hangingNodes.Remove("01");
                            }
                            if (ToDelete("00", newNode4, givenConditionalMatrix.GetLength(1), rowsNumber, maxMin))
                            {
                                hangingNodes.Remove("00");
                            }
                        }
                        else
                        {
                            Node newNode = GetNode("1", givenConditionalMatrix.GetLength(1), rowsNumber, maxMin);
                            hangingNodes.Add("1", newNode);
                            newNode = GetNode("0", givenConditionalMatrix.GetLength(1), rowsNumber, maxMin);
                            hangingNodes.Add("0", newNode);
                            numberOfNodes += 2;
                        }
                    }
                }
                else
                {
                    //если максимальный путь еще не все вершины
                    if (maxPath.Length < givenConditionalMatrix.GetLength(1) - 2)
                    {
                        hangingNodes.Remove(maxPath);
                        //если количество переменных еще позволяет, от максимального пускаем 2 новых, а от них еще по 2 новых
                        //if (maxPath.Length < givenConditionalMatrix.GetLength(1) - 2)
                        // это неверное условие,т.к. если число переменных нечетное, то мы добавляем нулевую
                        {
                            //к общему количеству узлов прибавляем 2 новых исходящих от максимального и по 2 от них то есть 6   
                            numberOfNodes += 2;
                            Node newNode1 = new Node();
                            Node newNode2 = new Node();
                            Node newNode3 = new Node();
                            Node newNode4 = new Node();
                            //пускаем 4 конечных узла если исходящий от максимального не бесконечность
                           // Node temp=GetNode(maxPath + "1", givenConditionalMatrix.GetLength(1), rowsNumber, maxMin);
                            //if ((temp.sum > 0 && maxMin) || (temp.sum != infinity && !maxMin))
                            {
                                newNode1 = GetNode(maxPath + "11", givenConditionalMatrix.GetLength(1), rowsNumber, maxMin);
                                newNode2 = GetNode(maxPath + "10", givenConditionalMatrix.GetLength(1), rowsNumber, maxMin);
                                hangingNodes.Add(maxPath + "11", newNode1);
                                hangingNodes.Add(maxPath + "10", newNode2);
                                numberOfNodes += 2;
                            }
                           /* else
                                //в противном случае добавляем только узел с бесконечностью к висячим
                            {
                                hangingNodes.Add(maxPath + "1", temp);                               
                            }*/
                            //temp=GetNode(maxPath + "0", givenConditionalMatrix.GetLength(1), rowsNumber, maxMin);
                            //if ((temp.sum > 0 && maxMin) || (temp.sum != infinity && !maxMin))
                            {                               
                                newNode3 = GetNode(maxPath + "01", givenConditionalMatrix.GetLength(1), rowsNumber, maxMin);
                                newNode4 = GetNode(maxPath + "00", givenConditionalMatrix.GetLength(1), rowsNumber, maxMin);
                                hangingNodes.Add(maxPath + "01", newNode3);
                                hangingNodes.Add(maxPath + "00", newNode4);
                                numberOfNodes += 2;
                            }
                            //else
                           // {
                            //    hangingNodes.Add(maxPath + "0", temp);                                
                           // }

                            //проверяем стоит ли удалять их из висячих вершин если они не проходят проверку
                            if (newNode1.sum!=0 )
                                if (ToDelete(maxPath + "11", newNode1, givenConditionalMatrix.GetLength(1), rowsNumber, maxMin))
                                {
                                    hangingNodes.Remove(maxPath + "11");                                    
                                }
                            if (newNode2.sum != 0)
                                if (ToDelete(maxPath + "10", newNode2, givenConditionalMatrix.GetLength(1), rowsNumber, maxMin))
                                {
                                    hangingNodes.Remove(maxPath + "10");                                    
                                }
                            if (newNode3.sum != 0)
                                if (ToDelete(maxPath + "01", newNode3, givenConditionalMatrix.GetLength(1), rowsNumber, maxMin))
                                {
                                    hangingNodes.Remove(maxPath + "01");                                   
                                }
                            if (newNode4.sum != 0)
                                if (ToDelete(maxPath + "00", newNode4, givenConditionalMatrix.GetLength(1), rowsNumber, maxMin))
                                {
                                    hangingNodes.Remove(maxPath + "00");                                   
                                }
                        }
                       /* //если нельзя добавить 6 вершин, добавляем 2, заполняя последний уровень
                        else
                        {
                            Node newNode1 = GetNode(maxPath + "1", givenConditionalMatrix.GetLength(1), rowsNumber, maxMin);
                            Node newNode2 = GetNode(maxPath + "0", givenConditionalMatrix.GetLength(1), rowsNumber, maxMin);
                            hangingNodes.Add(maxPath + "1", newNode1);
                            hangingNodes.Add(maxPath + "0", newNode2);
                            numberOfNodes += 2;
                            //по алгоритму даже на последнем шаге проверяим стоит ли удалять узлы из висячих вершин
                            if (ToDelete(maxPath + "1", newNode1, givenConditionalMatrix.GetLength(1), rowsNumber, maxMin))
                                hangingNodes.Remove(maxPath + "1");
                            if (ToDelete(maxPath + "0", newNode1, givenConditionalMatrix.GetLength(1), rowsNumber, maxMin))
                                hangingNodes.Remove(maxPath + "0");

                        }*/
                    }

                }
               //проверяем         
                max = 0;
                min = int.MaxValue-1;
                if (maxMin)
                    foreach (var hangingNode in hangingNodes)
                    {
                        if (hangingNode.Value.sum > max)
                        {
                            max = hangingNode.Value.sum;
                        }
                    }
                else
                    foreach (var hangingNode in hangingNodes)
                    {
                        if (hangingNode.Value.sum < min)
                        {
                            min = hangingNode.Value.sum;
                        }
                    }

                stop = false;
                numberOfHangingNodes = 0;                
                if (maxMin)
                    foreach (var hangingNode in hangingNodes)
                    {

                        if (hangingNode.Value.sum == max && hangingNode.Key.Length == givenConditionalMatrix.GetLength(1) - 2 && String.Compare(hangingNode.Key, resultPath) > 0)                                            
                        {
                            
                            stop = true;
                            resultNode = hangingNode.Value;
                            resultPath = hangingNode.Key;
                        }
                        numberOfHangingNodes++;
                    }
                else
                    foreach (var hangingNode in hangingNodes)
                    {

                        if (hangingNode.Value.sum == min && hangingNode.Key.Length == givenConditionalMatrix.GetLength(1) - 2 && String.Compare(hangingNode.Key, resultPath) > 0)
                        {
                            stop = true;
                            resultNode = hangingNode.Value;
                            resultPath = hangingNode.Key;
                        }
                        numberOfHangingNodes++;
                    }
                    
                }
            //удаляем нулевую переменную в строке
            if(resultPath!="")
            resultPath = (columnsNumber % 2 != 0) ? resultPath.Remove(resultPath.Length - 1) : resultPath;
            //строка результата содержит: 
            //1.конечную сумму;
            //2.конечное значение z;
            //3.конечное число вершин построенного дерева;
            //4.конечное число "висячих" вершин            
            string result = "F=" + resultNode.sum.ToString() + "; z=" + resultPath + "; N1=" + (numberOfNodes + 1).ToString() + "; N2=" + numberOfHangingNodes.ToString();
            if (noSolution)
                result = "F=" + "no solution" + "; z=" + "no solution" + "; N1=" + (numberOfNodes + 1).ToString() + "; N2=" + numberOfHangingNodes.ToString();
            return result;
        }
       private Node GetNode(string path, int columnsNumber, int rowsNumber,bool maxMin)
        {
            Node calculatedNode=new Node();

            calculatedNode.sum = 0;            
            for (int i = 0; i < columnsNumber - 2; i++)
                if (maxMin)
                {
                    int a=path.Length > i ? Convert.ToInt32(path[i].ToString()) : 1;
                    calculatedNode.sum += givenConditionalMatrix[0, i] * (a);
                }
                else
                    calculatedNode.sum += givenConditionalMatrix[0, i] * (path.Length > i ? Convert.ToInt32(path[i].ToString()) : 0);

            calculatedNode.mi = new double[rowsNumber - 1];
            bool greaterOrEquel;
            for (int i = 1; i < rowsNumber; i++)
            {
                greaterOrEquel = givenConditionalMatrix[i, columnsNumber - 2] == 1 ? true : false;
                if (greaterOrEquel)
                    calculatedNode.mi[i - 1] = givenConditionalMatrix[i, columnsNumber - 1];
                else
                    calculatedNode.mi[i - 1] = -givenConditionalMatrix[i, columnsNumber - 1];
               
                for (int j = 0; j < columnsNumber - 2; j++)
                    if (greaterOrEquel)
                        calculatedNode.mi[i - 1] -= givenConditionalMatrix[i, j] * (path.Length > j ? Convert.ToInt32(path[j].ToString()) : 0);
                    else
                        calculatedNode.mi[i - 1] += givenConditionalMatrix[i, j] * (path.Length > j ? Convert.ToInt32(path[j].ToString()) : 1);               

            }

           //проверяем на ограничения условий
            for (int i = 0; i < rowsNumber-1; i++)
            {
                if (calculatedNode.mi[i] < 0)                
                    if(maxMin)
                    calculatedNode.sum = -infinity;
                    else
                    calculatedNode.sum = infinity;
                

            }
            calculatedNode.delta = 0;
            for (int i = 0; i < path.Length; i++)
                if (maxMin)
                    calculatedNode.delta += givenConditionalMatrix[0, i] * Convert.ToInt32(path[i].ToString());
                else
                    calculatedNode.delta += givenConditionalMatrix[0, i] * Convert.ToInt32(path[i].ToString());
           

                return calculatedNode;
        }
        // получить узел для традиционного метода, где нужна только сумма
       private Node GetNode1(string path, int columnsNumber, int rowsNumber, bool maxMin)
       {
           Node calculatedNode = new Node();

           calculatedNode.sum = 0;
           for (int i = 0; i < columnsNumber - 2; i++)
               if (maxMin)
               {
                   int a = path.Length > i ? Convert.ToInt32(path[i].ToString()) : 1;
                   calculatedNode.sum += givenConditionalMatrix[0, i] * (a);
               }
               else
               {
                   int a = path.Length > i ? Convert.ToInt32(path[i].ToString()) : 0;
                   calculatedNode.sum += givenConditionalMatrix[0, i] * a;
               }
           calculatedNode.mi = new double[rowsNumber - 1];
           bool greaterOrEquel;
           for (int i = 1; i < rowsNumber; i++)
           {
               greaterOrEquel = givenConditionalMatrix[i, columnsNumber - 2] == 1 ? true : false;
               if (greaterOrEquel)
                   calculatedNode.mi[i - 1] = givenConditionalMatrix[i, columnsNumber - 1];
               else
                   calculatedNode.mi[i - 1] = -givenConditionalMatrix[i, columnsNumber - 1];

               for (int j = 0; j < columnsNumber - 2; j++)
                   if (greaterOrEquel)
                       calculatedNode.mi[i - 1] -= givenConditionalMatrix[i, j] * (path.Length > j ? Convert.ToInt32(path[j].ToString()) : 0);
                   else
                       calculatedNode.mi[i - 1] += givenConditionalMatrix[i, j] * (path.Length > j ? Convert.ToInt32(path[j].ToString()) : 1);   
               if (calculatedNode.mi[i - 1] < 0)
               if(maxMin)
               {
                   calculatedNode.sum = -infinity;
                   return calculatedNode;
               }
               else              
               {
                   calculatedNode.sum = infinity;
                   return calculatedNode;
               }
           }
          
           return calculatedNode;
       }
        //функция проверки на удаления узла из висячих вершин в композитной версии алгоритма
       private bool ToDelete(string path, Node checkNode, int columnsNumber, int rowsNumber, bool maxMin)
       {
           bool delete = false;
           if (maxMin)
           {
               if (checkNode.sum < 0) return false;
               //для максимума
               foreach (var hangingNode in hangingNodes)
               {


                   if (checkNode.sum <= hangingNode.Value.sum && hangingNode.Key != path && hangingNode.Key.Length == path.Length && hangingNode.Value.sum > 0 && path.Length != columnsNumber - 1 && checkNode.mi[0] <= hangingNode.Value.mi[0])
                   {
                       delete = true;
                       for (int i = 1; i < rowsNumber - 1; i++)
                       {
                           if (checkNode.mi[i] <= hangingNode.Value.mi[i])
                               delete = true;
                           else
                           {
                               delete = false;
                               break;
                           }
                       }                       
                   }
                  // if (checkNode.sum <= hangingNode.Value.delta && path != hangingNode.Key && hangingNode.Value.sum > 0 && path.Length != columnsNumber - 1)
                      // return true;
               }
           }
           else
           {
               if (checkNode.sum == infinity) return false;
                   //для минимума
                   foreach (var hangingNode in hangingNodes)
                   {

                       if (checkNode.delta >= hangingNode.Value.delta && hangingNode.Key != path && checkNode.mi[0] <= hangingNode.Value.mi[0] && hangingNode.Key.Length == path.Length && hangingNode.Value.sum != infinity && path.Length != columnsNumber - 1)
                       {
                           delete = true;
                           for (int i = 1; i < rowsNumber - 1; i++)
                           {
                               if (checkNode.mi[i] <= hangingNode.Value.mi[i])
                                   delete = true;
                               else
                               {
                                   delete = false;
                                   break;
                               }
                           }
                       }
                      // if (checkNode.delta >= hangingNode.Value.sum && hangingNode.Key != path && hangingNode.Value.sum != infinity && path.Length != columnsNumber - 1)
                         //  return true;
                   }
           }
           return delete;
       }

    }
}

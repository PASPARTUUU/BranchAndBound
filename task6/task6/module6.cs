using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace task6
{   
    //описание узла
    class Node
    {
        // описывает сумму, получившуюся при подстановки всех "z"
        public double sum;
        //описывает делта по условию
        public double delta;
        //описывает мю по условию
        public double[] mi;
    }
    class module6
    {
        const int infinity = 99999999;
        //описывает текущие "висячие" вершины (узлы)
        //string показывает имя узла, то есть путь к этому узлу (напр. "1011" значит что z1=1;z2=0;z3=1;z4=1)
        Dictionary<string, Node> hangingNodes;
        //матрица условий
        double[,] givenConditionalMatrix;
        //изменение
        //знаки ограничений
        string[] givenSign;
        //
        //копируем матрицу условий из функции
        private void CopyMatrix(double[,] conditionalMatrix, int columnsNumber, int rowsNumber, string[] sign)
        {
            givenSign = new string[rowsNumber];
            for (int i = 1; i < rowsNumber; i++)
                givenSign[i] = sign[i];
            givenConditionalMatrix = new double[rowsNumber, columnsNumber];
            for (int i = 0; i < rowsNumber; i++)
                for (int j = 0; j < columnsNumber; j++)
                    givenConditionalMatrix[i, j] = conditionalMatrix[i, j];
            ;
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
        //изменено
        public string modifiedClassicFrontDescent(double[,] conditionalMatrix, int columnsNumber, int rowsNumber, bool maxMin, string[] sign)
        {
            bool stop = false;
            Node resultNode = new Node();
            string resultPath = "";
            int numberOfNodes = 0;
            int numberOfHangingNodes = 0;
            HangingNodesDeclare();
            //изменено
            CopyMatrix(conditionalMatrix, columnsNumber, rowsNumber, sign);
            while (!stop)
            {
                //1.ищем максимальный/минимальный узел и запоминаем его
                //2.от него пускаем 2 новых
                //3.удаляем текущий
                //идем в 1
                double max = 0, min = infinity;
                string maxPath = "";
                if (maxMin)
                    foreach (var hangingNode in hangingNodes)
                    {
                        if (hangingNode.Value.sum > max || (hangingNode.Value.sum == max && String.Compare(hangingNode.Key, maxPath) > 0))
                        {
                            max = hangingNode.Value.sum;
                            maxPath = hangingNode.Key;
                        }
                    }
                else
                    foreach (var hangingNode in hangingNodes)
                    {
                        if (hangingNode.Value.sum < min || (hangingNode.Value.sum == max && String.Compare(hangingNode.Key, maxPath) > 0))
                        {
                            min = hangingNode.Value.sum;
                            maxPath = hangingNode.Key;
                        }
                    }
                //если программа только начинается и узлов еще нет
                if (maxPath == "")
                {
                    int droppedVariables = (int) (0.5 * ((columnsNumber - 1)+1));
                    var count = Enumerable.Repeat(2, droppedVariables).Aggregate((i, next) => 2 * i);
                    string permutations = string.Join(" ", Enumerable.Range(0, count).Reverse().Select(i => Convert.ToString(i, 2).PadLeft(droppedVariables, '0')).ToArray());
                    string[] permutationsArray = permutations.Split(' ');
                    Node newNode;
                    foreach (var permutation in permutationsArray)
                    {
                        newNode = GetNodeClassic(permutation, columnsNumber, rowsNumber, maxMin);
                        hangingNodes.Add(permutation, newNode);
                    }
                    numberOfNodes += count;
                 m1:
                    foreach (var hangingNode in hangingNodes)
                    {
                        if (ToDeleteClassic(hangingNode.Key, hangingNode.Value, givenConditionalMatrix.GetLength(1), rowsNumber, maxMin))
                        {
                            hangingNodes.Remove(hangingNode.Key);
                            goto m1;
                        }
                    }
                }
                else
                {
                    if (maxPath.Length < columnsNumber - 1)
                    {
                        Node newNode1 = GetNodeClassic(maxPath + "1", columnsNumber, rowsNumber, maxMin);
                        hangingNodes.Add(maxPath + "1", newNode1);
                        Node newNode2 = GetNodeClassic(maxPath + "0", columnsNumber, rowsNumber, maxMin);
                        hangingNodes.Add(maxPath + "0", newNode2);
                        hangingNodes.Remove(maxPath);
                        numberOfNodes += 2;
                        if (ToDeleteClassic(maxPath + "1", newNode1, givenConditionalMatrix.GetLength(1), rowsNumber, maxMin))
                            hangingNodes.Remove(maxPath + "1");
                        if (ToDeleteClassic(maxPath + "0", newNode2, givenConditionalMatrix.GetLength(1), rowsNumber, maxMin))
                            hangingNodes.Remove(maxPath + "0");
                    }
                }
                max = 0;
                min = infinity;
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

                        if (hangingNode.Value.sum == max && hangingNode.Key.Length == columnsNumber - 1 && String.Compare(hangingNode.Key, resultPath) > 0)
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
                        if (hangingNode.Value.sum == min && hangingNode.Key.Length == columnsNumber - 1 && String.Compare(hangingNode.Key, resultPath) > 0)
                        {
                            stop = true;
                            resultNode = hangingNode.Value;
                            resultPath = hangingNode.Key;
                        }
                        numberOfHangingNodes++;
                    }
                // проверка, все ли -+infinity
                bool allInfinity = true;
                foreach (var hangingNode in hangingNodes)
                    if (hangingNode.Value.sum == -infinity || hangingNode.Value.sum == infinity) { } else allInfinity = false;
                if (allInfinity)
                    return "F = no solutions; Z = " + "no solutions" + "; |X1| = " + (numberOfNodes).ToString() + "; |X1^T| = " + numberOfHangingNodes.ToString();
                //
            }
            //строка результата содержит: 
            //1.конечную сумму;
            //2.конечное значение z;
            //3.конечное число вершин построенного дерева;
            //4.конечное число "висячих" вершин
            string result = "F = " + resultNode.sum.ToString() + "; Z = " + resultPath + "; |X1| = " + (numberOfNodes).ToString() + "; |X1^T| = " + numberOfHangingNodes.ToString();
            return result;
        }
        //функция вычисления результата
        //на вход подается матрица условий и к чему стремиться функция (если true-max,false-min)
        //матрица условий содержит:
        //1-я строка это все "z" функции
        //следующие строки "z" условий
        //последний столбец равен "а" (то есть ограничениям условий)
        //в первой строке последний столбец "а" не используется, т. к. предназначен не для функции
        //изменено
        public string modifiedCompositeFrontDescent(double[,] conditionalMatrix, int columnsNumber, int rowsNumber, bool maxMin, string[] sign)
        {
            bool stop = false;
            Node resultNode = new Node();
            string resultPath = "";
            int numberOfNodes = 0;
            int numberOfHangingNodes = 0;
            HangingNodesDeclare();
            //изменено
            CopyMatrix(conditionalMatrix, columnsNumber, rowsNumber, sign); //для h=1
            while (!stop)
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
                        if (hangingNode.Value.sum > max || (hangingNode.Value.sum == max && String.Compare(hangingNode.Key, maxPath) > 0))
                        {
                            max = hangingNode.Value.sum;
                            maxPath = hangingNode.Key;
                        }
                    }
                else
                    foreach (var hangingNode in hangingNodes)
                    {
                        if (hangingNode.Value.sum < min || (hangingNode.Value.sum == max && String.Compare(hangingNode.Key, maxPath) > 0))
                        {
                            min = hangingNode.Value.sum;
                            maxPath = hangingNode.Key;
                        }
                    }
                //если программа только начинается и узлов еще нет
                if (maxPath == "")
                {
                    //если число переменных больше 1

                        int droppedVariables = (int)(0.5 * ((columnsNumber - 1) + 1));
                        var count = Enumerable.Repeat(2, droppedVariables).Aggregate((i, next) => 2 * i);
                        string permutations = string.Join(" ", Enumerable.Range(0, count).Reverse().Select(i => Convert.ToString(i, 2).PadLeft(droppedVariables, '0')).ToArray());
                        string[] permutationsArray = permutations.Split(' ');
                        Node newNode;
                        foreach (var permutation in permutationsArray)
                        {
                            newNode = GetNodeComposite(permutation, givenConditionalMatrix.GetLength(1), rowsNumber, maxMin);
                            hangingNodes.Add(permutation, newNode);
                        }
                        numberOfNodes += count;
                     m2:
                        foreach (var hangingNode in hangingNodes)
                        {
                            if (ToDeleteComposite(hangingNode.Key, hangingNode.Value, givenConditionalMatrix.GetLength(1), rowsNumber, maxMin))
                            {
                                hangingNodes.Remove(hangingNode.Key);
                                goto m2;
                            }
                        }
                }
                else
                {
                    //если максимальный путь еще не все вершины
                    if (maxPath.Length < givenConditionalMatrix.GetLength(1) - 1)
                    {
                        hangingNodes.Remove(maxPath);
                        //h = 1.пускаем 2 конечных узла
                        Node newNode1 = GetNodeComposite(maxPath + "1", givenConditionalMatrix.GetLength(1), rowsNumber, maxMin);
                        Node newNode2 = GetNodeComposite(maxPath + "0", givenConditionalMatrix.GetLength(1), rowsNumber, maxMin);
                        hangingNodes.Add(maxPath + "1", newNode1);
                        hangingNodes.Add(maxPath + "0", newNode2);
                        numberOfNodes += 2;
                        if (ToDeleteComposite(maxPath + "1", newNode1, givenConditionalMatrix.GetLength(1), rowsNumber, maxMin))
                            hangingNodes.Remove(maxPath + "1");
                        if (ToDeleteComposite(maxPath + "0", newNode2, givenConditionalMatrix.GetLength(1), rowsNumber, maxMin))
                            hangingNodes.Remove(maxPath + "0");
                    }
                }
                //проверяем    
                max = 0;
                min = infinity;
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
                        if (hangingNode.Value.sum == max && hangingNode.Key.Length == givenConditionalMatrix.GetLength(1) - 1 && String.Compare(hangingNode.Key, resultPath) > 0)
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
                        if (hangingNode.Value.sum == min && hangingNode.Key.Length == givenConditionalMatrix.GetLength(1) - 1 && String.Compare(hangingNode.Key, resultPath) > 0)
                        {
                            stop = true;
                            resultNode = hangingNode.Value;
                            resultPath = hangingNode.Key;
                        }
                        numberOfHangingNodes++;
                    }
                // проверка, все ли -+infinity
                bool allInfinity = true;
                foreach (var hangingNode in hangingNodes)
                    if (hangingNode.Value.sum == -infinity || hangingNode.Value.sum == infinity) { } else allInfinity = false;
                if (allInfinity)
                    return "F = no solutions; Z = " + "no solutions" + "; |X2| = " + (numberOfNodes).ToString() + "; |X2^T| = " + numberOfHangingNodes.ToString();
                //
            }
            //строка результата содержит: 
            //1.конечную сумму;
            //2.конечное значение z;
            //3.конечное число вершин построенного дерева;
            //4.конечное число "висячих" вершин      
            string result = "F = " + resultNode.sum.ToString() + "; Z = " + resultPath + "; |X2| = " + (numberOfNodes).ToString() + "; |X2^T| = " + numberOfHangingNodes.ToString();
            return result;
        }
        private Node GetNodeComposite(string path, int columnsNumber, int rowsNumber, bool maxMin)
        {
            Node calculatedNode = new Node();
            calculatedNode.sum = 0;
            for (int i = 0; i < columnsNumber - 1; i++)
                if (maxMin)
                {
                    int a = path.Length > i ? Convert.ToInt32(path[i].ToString()) : 1;
                    calculatedNode.sum += givenConditionalMatrix[0, i] * (a);
                }
                else
                    calculatedNode.sum += givenConditionalMatrix[0, i] * (path.Length > i ? Convert.ToInt32(path[i].ToString()) : 0);
            calculatedNode.mi = new double[rowsNumber - 1];
            for (int i = 1; i < rowsNumber; i++)
            {
                //if (maxMin)
                if (givenSign[i] == "<=")
                    calculatedNode.mi[i - 1] = givenConditionalMatrix[i, columnsNumber - 1];
                else
                    calculatedNode.mi[i - 1] = -givenConditionalMatrix[i, columnsNumber - 1];

                for (int j = 0; j < columnsNumber - 1; j++)
                    //if (maxMin)
                    if (givenSign[i] == "<=")
                        calculatedNode.mi[i - 1] -= givenConditionalMatrix[i, j] * (path.Length > j ? Convert.ToInt32(path[j].ToString()) : 0);
                    else
                        calculatedNode.mi[i - 1] += givenConditionalMatrix[i, j] * (path.Length > j ? Convert.ToInt32(path[j].ToString()) : 1);
            }
            //проверяем на ограничения условий
            for (int i = 0; i < rowsNumber - 1; i++)
            {
                if (calculatedNode.mi[i] < 0)
                    if (maxMin)
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
        private Node GetNodeClassic(string path, int columnsNumber, int rowsNumber, bool maxMin)
        {
            Node calculatedNode = new Node();
            calculatedNode.sum = 0;
            for (int i = 0; i < columnsNumber - 1; i++)
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
            //изменено
            for (int i = 1; i < rowsNumber; i++)
            {
                //if (maxMin)
                if (givenSign[i]=="<=")
                {
                    calculatedNode.mi[i - 1] = givenConditionalMatrix[i, columnsNumber - 1];
                }
                else
                {
                    calculatedNode.mi[i - 1] = -givenConditionalMatrix[i, columnsNumber - 1];
                }
                for (int j = 0; j < columnsNumber - 1; j++)
                    //if (maxMin)
                    if (givenSign[i] == "<=")
                    {
                        calculatedNode.mi[i - 1] -= givenConditionalMatrix[i, j] * (path.Length > j ? Convert.ToInt32(path[j].ToString()) : 0);
                    }
                    else
                    {
                        calculatedNode.mi[i - 1] += givenConditionalMatrix[i, j] * (path.Length > j ? Convert.ToInt32(path[j].ToString()) : 1);
                    }
                if (calculatedNode.mi[i - 1] < 0)
                    if (maxMin)
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
        private bool ToDeleteComposite(string path, Node checkNode, int columnsNumber, int rowsNumber, bool maxMin)
        {
            bool delete = false;
            if (maxMin)
            {
                if (checkNode.sum < 0) return true;
                //для максимума
                foreach (var hangingNode in hangingNodes)
                {
                    if (checkNode.sum <= hangingNode.Value.sum && hangingNode.Key != path && hangingNode.Key.Length == path.Length && hangingNode.Value.sum > 0 && path.Length != columnsNumber - 1 && checkNode.mi[0] <= hangingNode.Value.mi[0])
                    {
                        delete = true;
                        for (int i = 1; i < rowsNumber - 1; i++)
                        {
                            ;
                            if (checkNode.mi[i] <= hangingNode.Value.mi[i])
                                delete = true;
                            else
                            {
                                delete = false;
                                break;
                            }
                        }
                        if (delete) return true; //быстрее работает
                    }
                    //checkNode.sum <= hangingNode.Value.delta
                    //if (checkNode.sum <= hangingNode.Value.delta && hangingNode.Key != path && hangingNode.Key.Length == path.Length && hangingNode.Value.sum > 0 && path.Length != columnsNumber - 1)
                    //{
                    //    return true;
                    //}

                    //if (checkNode.sum <= hangingNode.Value.delta && hangingNode.Key != path && hangingNode.Key.Length == path.Length && hangingNode.Value.sum > 0 && path.Length != columnsNumber - 1 && checkNode.mi[0] <= hangingNode.Value.mi[0])
                    //{
                    //    delete = true;
                    //    for (int i = 1; i < rowsNumber - 1; i++)
                    //    {
                    //        if (checkNode.mi[i] <= hangingNode.Value.mi[i])
                    //            delete = true;
                    //        else
                    //        {
                    //            delete = false;
                    //            break;
                    //        }
                    //    }
                    //    if (delete) return true; //быстрее работает
                    //}
                }
                ;
            }
            else
            {
                if (checkNode.sum == infinity) return true;
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
                        if (delete) return true; //быстрее работает
                    }
                    // if (checkNode.delta >= hangingNode.Value.sum && hangingNode.Key != path && hangingNode.Value.sum != infinity && path.Length != columnsNumber - 1)
                    //  return true;
                }
            }
            return delete;
        }
        private bool ToDeleteClassic(string path, Node checkNode, int columnsNumber, int rowsNumber, bool maxMin)
        {
            if (maxMin)
            {
                if (checkNode.sum < 0) return true;
            }
            else
            {
                if (checkNode.sum == infinity) return true;
            }
            return false;
        }
    }
}
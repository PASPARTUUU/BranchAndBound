using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    //описание узла
    class Node
    {
        // описывает сумму, получившуюся при подстановки всех "z"
        public double sum;
        //описывает количество условий в задаче
        public int conditNum;        
        //описывает делта по условию
        public double delta;
        //описывает мю по условию
        public double[] mi;
    }
}

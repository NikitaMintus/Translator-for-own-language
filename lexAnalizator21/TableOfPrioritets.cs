using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    class TableOfPrioritets
    {
        
        private List<StrPrioritetOperator> tableOfPrioritets = new List<StrPrioritetOperator>()
        {
            new StrPrioritetOperator("enter", 0),
            new StrPrioritetOperator("(", 0),
            new StrPrioritetOperator("if", 0),
            new StrPrioritetOperator("do", 0),
            new StrPrioritetOperator("[", 0),
            new StrPrioritetOperator("begin", 0),
            new StrPrioritetOperator("label", 0),
            new StrPrioritetOperator(")", 1),
            new StrPrioritetOperator("then", 1),
            new StrPrioritetOperator("next", 1),
            new StrPrioritetOperator("to", 1),
            new StrPrioritetOperator("]", 1),
            new StrPrioritetOperator("end", 1),
            new StrPrioritetOperator("=", 2),
            new StrPrioritetOperator("write", 2),
            new StrPrioritetOperator("read", 2),
            new StrPrioritetOperator("goto", 2),
            new StrPrioritetOperator("or", 3),
            new StrPrioritetOperator("and", 4),
            new StrPrioritetOperator("!", 5),
            new StrPrioritetOperator("<", 6),
            new StrPrioritetOperator("<=", 6),
            new StrPrioritetOperator("==", 6),
            new StrPrioritetOperator(">=", 6),
            new StrPrioritetOperator("!=", 6),
            new StrPrioritetOperator(">", 6),
            new StrPrioritetOperator("+", 7),
            new StrPrioritetOperator("-", 7),
            new StrPrioritetOperator("*", 8),
            new StrPrioritetOperator("/", 8)
        };


        public int GetPrioritet(String oper)
        {
           foreach (StrPrioritetOperator curElem in tableOfPrioritets)
           {
               if (curElem.oper == oper)
               {
                   return curElem.prioritet;
               }
           }
           return -1;
        }

        public List<StrPrioritetOperator> GetTableOfPrioritets()
        {
            return this.tableOfPrioritets;
        }
    }
}

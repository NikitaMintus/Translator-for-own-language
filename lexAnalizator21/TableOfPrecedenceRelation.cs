using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    class TableOfPrecedenceRelation
    {
        private List<String> headersOfTable;
        private List<List<String>> tableOfPrRelation;

        public TableOfPrecedenceRelation()
        {
            SetHeadersOfTable();
            SetTableOfPrRelation();
        }

        public List<List<String>> GetTableOfPrRelation()
        {
            return this.tableOfPrRelation;
        }

        private void SetHeadersOfTable()
        {
            headersOfTable = new List<String>();
            foreach(StrChangedGrammar curStr in ChangedGrammar.GetChangedGrammar())
            {
                if (CheckAlreadyExist(curStr.leftPart))
                {
                    headersOfTable.Add(curStr.leftPart);
                }
            }

            foreach (StrChangedGrammar curStr in ChangedGrammar.GetChangedGrammar())
            {
                //добавляем нетерминалы
                String[] elems = curStr.rightPart.Split(' ');
                foreach (String curElem in elems)
                {
                    if (CheckAlreadyExist(curElem))
                    {
                        headersOfTable.Add(curElem);
                    }
                }
            }

            headersOfTable.Add("#"); // добавляем в конец признак "конца"
           
        }

        private void SetTableOfPrRelation()
        {
            tableOfPrRelation = new List<List<String>>();
            int size = headersOfTable.Count() + 1;
            for (int i = 0; i < size; i++)
            {
                tableOfPrRelation.Add(new List<String>(size));
            }

            foreach (List<String> curStr in tableOfPrRelation)
            {
                for (int i = 0; i < size; i++)
                {
                    curStr.Add("");
                }
            }

            // заполняем верхний заголовок с первой пустой ячейкой
            for (int i = 1, j = 0; i < size && j < size - 1; i++, j++)
            {
                tableOfPrRelation[0][i] = headersOfTable[j];
            }

            // заполняем левый заголовок (первая ячейка пуста)
            for (int i = 1, j = 0; i < size && j < size - 1; i++, j++)
            {
                tableOfPrRelation[i][0] = headersOfTable[j];
            }

            String str = tableOfPrRelation[0][2];
        }

        private bool CheckAlreadyExist(String newElem)
        {
            foreach (String curElem in headersOfTable)
            {
                if (curElem == newElem)
                {
                    return false;
                }
            }
            return true;
        }

        public void FillFirstNetermEqualNeterm(List<StrLastPLus> lastPlusArr, List<StrFirstPlus> firstPlusArr)
        {
            
            for (int i = 0; i < lastPlusArr.Count; i++)
            {
                List<String> arrLast = new List<string>(lastPlusArr[i].arrayOfLastPlus);
                List<String> arrFirst = new List<string>(firstPlusArr[i].arrayOfFirstPlus);

                foreach (String curLast in arrLast)
                {
                    foreach (String curFirst in arrFirst)
                    {
                        FillCell(curLast, ">", curFirst);
                    }
                }
            }
        }

        public void FillAllStartedEquals(List<StrEquals> equals)
        {
            foreach (StrEquals curStr in equals)
            {
                FillCell(curStr.first, curStr.relation, curStr.second); 
            }
        }

        public void FillEquals(List<StrFirstNeterminal> firstNeterminalsEquals)
        {
            foreach (StrFirstNeterminal curStr in firstNeterminalsEquals)
            {
                FillCell(curStr.neterminal, curStr.relation, curStr.terminal);
            }
        }

        public void FillEqualsSecondNeterm(List<StrSecondNeterminal> secondNeterminalEquals)
        {
            foreach (StrSecondNeterminal curStr in secondNeterminalEquals)
            {
                FillCell(curStr.terminal, curStr.relation, curStr.neterminal);
            }
        }

        public void FillLastPlusLarger(List<StrLastPLus> lastPlus)
        {
            foreach(StrLastPLus curStr in lastPlus)
            {
                foreach (String curElem in curStr.arrayOfLastPlus)
                {
                    FillCell(curElem, ">", curStr.terminal);
                }
            }
        }

        public void FillFirstPlusLess(List<StrFirstPlus> firstPlus)
        {
            foreach (StrFirstPlus curStr in firstPlus)
            {
                foreach (String curElem in curStr.arrayOfFirstPlus)
                {
                    FillCell(curStr.terminal, "<", curElem);
                }
            }
        }

        public void FillGridLess() // решетка меньше всех остальных символов
        {
            foreach (String curElem in headersOfTable)
            {
                if(curElem != "#")
                {
                    FillCell("#", "<", curElem);
                }
            }
        }

        public void FillGridLarger() // каждый элемент больше чем решетка
        {
            foreach (String curElem in headersOfTable)
            {
                if (curElem != "#")
                {
                    FillCell(curElem, ">", "#");
                }
            }
        }

        public void FillCell(String rowName, String relation, String colName)
        {
            int rowInd = TakeIndexForName(rowName);
            int colInd = TakeIndexForName(colName);
            String view = tableOfPrRelation[rowInd][colInd];
            if (tableOfPrRelation[rowInd][colInd].Length == 0 || tableOfPrRelation[rowInd][colInd] == relation)
            {
                tableOfPrRelation[rowInd][colInd] = relation;
            }
            else
            {
                tableOfPrRelation[rowInd][colInd] += " " + "konf" + " " + relation;
            }
        }

        public String GetRelation(String rowName, String colName)
        {
            int rowInd = TakeIndexForName(rowName);
            int colInd = TakeIndexForName(colName);

            return tableOfPrRelation[rowInd][colInd];
        }

        private int TakeIndexForName(String name)
        {
            for (int i = 0; i < headersOfTable.Count(); i++)
            {
                if (headersOfTable[i] == name)
                {
                    return i + 1;
                }
            }
            return -1;
        }
    }
}

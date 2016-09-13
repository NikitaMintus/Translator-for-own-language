using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    class FirstPlus
    {
        List<StrSecondNeterminal> secondNetermEqual;
        List<StrFirstPlus> firstPlus = new List<StrFirstPlus>();

        public List<StrFirstPlus> GetFirstPlus()
        {
            return this.firstPlus;
        }

        public void CalculateFirstPlus()
        {
            foreach (StrSecondNeterminal curStrNeterm in secondNetermEqual)
            {
                //if (CheckLastPlusWasSearched(curStrNeterm.neterminal, lastPlus))
                //{
                List<String> arrFirstPlus = new List<String>();
                SearchAllFirstPlus(curStrNeterm.neterminal, arrFirstPlus);
                firstPlus.Add(new StrFirstPlus(curStrNeterm.neterminal, arrFirstPlus, curStrNeterm.relation, curStrNeterm.terminal));
                //}
            }
            int i = 1;
        }

        public void SearchAllFirstPlus(String neterminal, List<String> arrayOfLastPlus)
        {
            foreach (StrChangedGrammar curStr in ChangedGrammar.GetChangedGrammar())
            {
                if (curStr.leftPart == neterminal) //нашли нетерминал слева
                {
                    String[] rightPartArr = curStr.rightPart.Split(' ');
                    int indOfFirstElem = 0;
                    if (rightPartArr[indOfFirstElem].IndexOf("$") != -1) //первый елемент нетерминал
                    {
                        if (CheckIsAlreadyExist(rightPartArr[indOfFirstElem], arrayOfLastPlus))
                        {
                            arrayOfLastPlus.Add(rightPartArr[indOfFirstElem]);
                            SearchAllFirstPlus(rightPartArr[indOfFirstElem], arrayOfLastPlus);
                        }
                    }
                    else
                    {
                        if (CheckIsAlreadyExist(rightPartArr[indOfFirstElem], arrayOfLastPlus))
                        {
                            arrayOfLastPlus.Add(rightPartArr[indOfFirstElem]);
                        }
                    }
                }
            }
        }

        public bool CheckIsAlreadyExist(String neterminal, List<String> arrayOfFirstPlus)
        {
            foreach (String curStr in arrayOfFirstPlus)
            {
                if (neterminal == curStr)
                {
                    return false;
                }
            }
            return true;
        }

        public FirstPlus(List<StrSecondNeterminal> secondNetermEqual)
        {
            this.secondNetermEqual = secondNetermEqual;
        }
    }
}

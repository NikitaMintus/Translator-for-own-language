using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    class LastPlus
    {
        List<StrFirstNeterminal> firstNetermEqual;
        List<StrLastPLus> lastPlus = new List<StrLastPLus>();

        public List<StrLastPLus> GetLastPlus()
        {
            return this.lastPlus;
        }

        public void CalculateLastPlus()
        {
            //StrLastPLus lastPlusItem = new StrLastPLus(firstNetermEqual[4].neterminal, new List<String>(), "<", firstNetermEqual[4].terminal);
            //lastPlusItem.arrayOfLastPlus.Add("T");
            //lastPlusItem.arrayOfLastPlus.Add("F");

            foreach(StrFirstNeterminal curStrNeterm in firstNetermEqual)
            {
                //if (CheckLastPlusWasSearched(curStrNeterm.neterminal, lastPlus))
                //{
                    List<String> arrLastPlus = new List<String>();
                    SearchAllLastPlus(curStrNeterm.neterminal, arrLastPlus);
                    lastPlus.Add(new StrLastPLus(curStrNeterm.neterminal, arrLastPlus, curStrNeterm.relation, curStrNeterm.terminal));
                //}
            }
           // int i = 1;
        }



        public List<String> SearchAllLastPlus(String neterminal, List<String> arrayOfLastPlus)
        {
            foreach (StrChangedGrammar curStr in ChangedGrammar.GetChangedGrammar())
            {
                if (curStr.leftPart == neterminal) //нашли нетерминал слева
                {
                    String[] rightPartArr = curStr.rightPart.Split(' ');
                    int indOfLastElem = rightPartArr.Length - 1;
                    if (rightPartArr[indOfLastElem].IndexOf("$") != -1) //последний елемент нетерминал
                    {
                        if (CheckIsAlreadyExist(rightPartArr[indOfLastElem], arrayOfLastPlus))
                        {
                            arrayOfLastPlus.Add(rightPartArr[indOfLastElem]);
                            SearchAllLastPlus(rightPartArr[indOfLastElem], arrayOfLastPlus);
                        }
                    }
                    else
                    {
                        if (CheckIsAlreadyExist(rightPartArr[indOfLastElem], arrayOfLastPlus))
                        {
                            arrayOfLastPlus.Add(rightPartArr[indOfLastElem]);
                        }
                    }
                }
            }
            return arrayOfLastPlus;
        }

        public bool CheckLastPlusWasSearched(String neterminal, List<StrLastPLus> lastPlus) // находили ли множество last+ для этого нетерминала
        {
            foreach (StrLastPLus curElem in lastPlus)
            {
                if(curElem.neterminal == neterminal)
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckIsAlreadyExist(String neterminal, List<String> arrayOfLastPlus)
        {
            foreach(String curStr in arrayOfLastPlus)
            {
                if (neterminal == curStr)
                {
                    return false;
                }
            }
            return true;
        }

        public LastPlus(List<StrFirstNeterminal> firstNetermEqual)
        {
            this.firstNetermEqual = firstNetermEqual;
        }
    }
}

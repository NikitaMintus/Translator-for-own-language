using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    class FirstNeterminalEqualNeterminal
    {
        List<StrFirstNeterminal> firstNetermEqualNeterm;
        List<StrLastPLus> lastPlusArr;
        List<StrFirstPlus> firstPlusArr;
        LastPlus lastPlus;
        FirstPlus firstPlus;


        public void CalculateLastPlus()
        {
            lastPlus.CalculateLastPlus();
            lastPlusArr = new List<StrLastPLus>(lastPlus.GetLastPlus());
            firstPlus.CalculateFirstPlus();
            firstPlusArr = new List<StrFirstPlus>(firstPlus.GetFirstPlus());
        }

        public List<StrLastPLus> GetLastPlusArr()
        {
            return this.lastPlusArr;
        }

        public List<StrFirstPlus> GetFirstPlusArr()
        {
            return this.firstPlusArr;
        }

        public FirstNeterminalEqualNeterminal(List<StrFirstNeterminal> firstNetermEqualNeterm)
        {
            lastPlus = new LastPlus(firstNetermEqualNeterm);
            firstPlus = new FirstPlus(ConvertToSecondNeterminal(firstNetermEqualNeterm));
            this.firstNetermEqualNeterm = firstNetermEqualNeterm;
        }

        private List<StrSecondNeterminal> ConvertToSecondNeterminal(List<StrFirstNeterminal> firstNetermEqualNeterm)
        {
            List<StrSecondNeterminal> secondNeterm = new List<StrSecondNeterminal>();

            foreach (StrFirstNeterminal curStr in firstNetermEqualNeterm)
            {
                secondNeterm.Add(new StrSecondNeterminal(curStr.neterminal, curStr.relation, curStr.terminal));
            }

            return secondNeterm;
        }
    }
}

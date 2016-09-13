using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    class SecondNetermEqualNeterm
    {
        List<StrSecondNeterminal> secondNetermEqualNeterm;
        FirstPlus firstPlus;
        List<StrFirstPlus> arrFirst;

        public List<StrFirstPlus> GetArrFirst()
        {
            return this.arrFirst;
        }

        public void CalculateFirstPlus()
        {
            firstPlus.CalculateFirstPlus();
            arrFirst = new List<StrFirstPlus>(firstPlus.GetFirstPlus());
        }

        public SecondNetermEqualNeterm(List<StrSecondNeterminal> secondNetermEqualNeterm)
        {
            firstPlus = new FirstPlus(secondNetermEqualNeterm);
            this.secondNetermEqualNeterm = secondNetermEqualNeterm;
        }
    }
}

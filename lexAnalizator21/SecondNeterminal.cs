using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    class SecondNeterminal
    {
        private List<StrSecondNeterminal> secondNeterminalEqual = new List<StrSecondNeterminal>();

        private List<StrSecondNeterminal> secondNeterminalEqualNeterm = new List<StrSecondNeterminal>();

        public void SearchEqual()
        {
            foreach(StrChangedGrammar curStr in ChangedGrammar.GetChangedGrammar())
            {
                String[] splitedStr = curStr.rightPart.Split(' ');
                for (int i = 0; i < splitedStr.Length; i++)
                {
                    if (splitedStr.Length >= 2)
                    {
                        if (splitedStr[i].IndexOf("$") != -1) // если нетерминал
                        {
                            if(i > 0)
                            {
                                if (splitedStr[i - 1].IndexOf("$") == -1) // если первый терминал
                                {
                                    secondNeterminalEqual.Add(new StrSecondNeterminal(splitedStr[i - 1], "=", splitedStr[i]));
                                }
                                else
                                {
                                    secondNeterminalEqualNeterm.Add(new StrSecondNeterminal(splitedStr[i - 1], "=", splitedStr[i]));
                                }
                            }
                        }
                    }
                }   
            }
        }

        public List<StrSecondNeterminal> GetSecondNeterminalEqualNeterm()
        {
            return this.secondNeterminalEqualNeterm;
        }

        public List<StrSecondNeterminal> GetSecondNeterminalEqual()
        {
            return this.secondNeterminalEqual;
        }
    }
}

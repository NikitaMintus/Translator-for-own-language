using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    class FirstNeterminal
    {
        private List<StrFirstNeterminal> firstNetermEqual = new List<StrFirstNeterminal>();

        private List<StrFirstNeterminal> firstNetermEqualNeterm = new List<StrFirstNeterminal>();

        public void SearchEqual()
        {
            foreach(StrChangedGrammar curStr in ChangedGrammar.GetChangedGrammar())
            {
                String[] splitedStr = curStr.rightPart.Split(' '); 
                for(int i = 0; i < splitedStr.Length; i++)
                {
                    if (splitedStr[i].IndexOf("$") != -1 && (i + 1) < splitedStr.Length) // если нетерминал
                    {
                        if (splitedStr[i+1].IndexOf("$") == -1) // если второй терминал
                        {
                            firstNetermEqual.Add(new StrFirstNeterminal(splitedStr[i], "=", splitedStr[i + 1]));
                        }
                        else
                        {
                            firstNetermEqualNeterm.Add(new StrFirstNeterminal(splitedStr[i], "=", splitedStr[i + 1]));
                        }
                    }
                }
            }
        }

        public List<StrFirstNeterminal> GetFirstNetermEqualNeterm()
        {
            return this.firstNetermEqualNeterm;
        }

        public List<StrFirstNeterminal> GetFirstNetermEqual()
        {
            return this.firstNetermEqual;
        }
    }
}

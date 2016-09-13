using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    class Equals
    {
        private List<StrEquals> equals = new List<StrEquals>();

        public List<StrEquals> GetEquals()
        {
            return this.equals;
        }

        public void SearchEquals()
        {
            foreach (StrChangedGrammar curStr in ChangedGrammar.GetChangedGrammar())
            {
                String[] splitedStr = curStr.rightPart.Split(' ');
                for (int i = 0; i < splitedStr.Length; i++)
                {
                    if ((i + 1) != splitedStr.Length)
                    {
                        equals.Add(new StrEquals(splitedStr[i], "=", splitedStr[i+1]));
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    class Cycle
    {
        private bool cycle = false;
        private String cycleParam = "";
        private Stack<String> rLabels = new Stack<String>();
        private static int numOfRLabel = 0;

        public bool GetCycle()
        {
            return this.cycle;
        }

        public String GetCycleParam()
        {
            return this.cycleParam;
        }

        public void SetCycle(bool cycle)
        {
            this.cycle = cycle;
        }

        public void SetCycleParam(String cycleParam)
        {
            this.cycleParam = cycleParam;
        }

        public String GenerateRLabel()
        {
            ++numOfRLabel;
            String curRLabel = "r" + numOfRLabel;
            rLabels.Push("r" + numOfRLabel);
            return curRLabel;
        }

        public Stack<String> GetRLabels()
        {
            return this.rLabels;
        }
    }
}

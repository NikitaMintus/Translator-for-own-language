using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    class TableOfConstant
    {
        private List<StrTableOfConstant> tableOfConstant;
        private static int numOfCons;
        private static int numOfLexem = 36;

        public static void ClearNumOfCons() {
            numOfCons = 0;
        }

        public void AddRecord(StrTableOfConstant newCons) {
            this.tableOfConstant.Add(newCons);
        }

        public List<StrTableOfConstant> GetTableOfConstant() {
            return this.tableOfConstant;
        }

        public static int GenerateNumOfCons()
        {
            return ++numOfCons;
        }

        public static int GetNumOfLexemCons()
        {
            return numOfLexem;
        }

        public int CheckIndentityConstant(String cons) {

            foreach (StrTableOfConstant curCons in tableOfConstant) {
                if (curCons.cons == cons) {
                    return curCons.numOfCons;
                }
            }
            return 0;
        }

        public void AddConstant(String curCons, int numOfStr, TableOfConstant tableOfConstant, OutputTable outputTable) {
            int numOfCons = tableOfConstant.CheckIndentityConstant(curCons);
            if (numOfCons == 0)
            {
                StrTableOfConstant curRecCons = new StrTableOfConstant(curCons, TableOfConstant.GenerateNumOfCons());
                tableOfConstant.AddRecord(curRecCons);
                StrOutputTable curRec = new StrOutputTable(numOfStr, curCons, TableOfConstant.GetNumOfLexemCons(), curRecCons.numOfCons);
                outputTable.AddRecord(curRec);
            }
            else
            {
                StrOutputTable curRec = new StrOutputTable(numOfStr, curCons, TableOfConstant.GetNumOfLexemCons(), numOfCons);
                outputTable.AddRecord(curRec);
            }
        }

        public TableOfConstant(){
            this.tableOfConstant = new List<StrTableOfConstant>();
        }
    }
}

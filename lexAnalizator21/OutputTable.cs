using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    class OutputTable
    {
        private List<StrOutputTable> outputTable;

        public OutputTable() {
            this.outputTable = new List<StrOutputTable>();
        }

        public void AddRange(List<StrOutputTable> range){
            this.outputTable.AddRange(range);
        }

        public void AddRecord(StrOutputTable record) {
            this.outputTable.Add(record);
        }

        public List<StrOutputTable> GetOutputTable() {
            return this.outputTable;
        }

        

        public bool SearchOutputLexem(String outLexem){
            foreach (StrOutputTable curLexem in outputTable) {
                if (curLexem.substring == outLexem) {
                    return true;
                }
            }
            return false;
        }

        public void AddSimpleLexem(String curLex, int numOfStr, TableOfLexems tableOfLexems) {
            int numOfLex = tableOfLexems.SearchLexem(curLex);
            StrOutputTable curRec = new StrOutputTable(numOfStr, curLex, numOfLex, 0);
            AddRecord(curRec);
        }
    }
}

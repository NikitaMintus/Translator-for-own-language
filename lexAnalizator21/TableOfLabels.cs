using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    class TableOfLabels
    {
        private List<StrTableOfLabels> tableOfLabels;
        private static int numOfLabel;
        private static int numOfLexem = 3;

        public static void ClearNumOfLabel() {
            numOfLabel = 0;
        }

        public void AddRecord(StrTableOfLabels newLabel){
            this.tableOfLabels.Add(newLabel);
        }

        public List<StrTableOfLabels> GetTableOfLabels() {
            return this.tableOfLabels;
        }

        public int GetNumOfLabel(String label)
        {
            foreach (StrTableOfLabels curLabel in tableOfLabels)
            {
                if (curLabel.label == label)
                {
                    return curLabel.numOfLabel;
                }
            }
            return -1;
        }

        public TableOfLabels() {
            this.tableOfLabels = new List<StrTableOfLabels>();
        }

        public bool CheckIsLabelDef(OutputTable outputTable) { //определяет раздел объявления меток
            if (outputTable.SearchOutputLexem("label") && !outputTable.SearchOutputLexem("begin")) {
                return true;
            }
            return false;
        }

        public int CheckIsLabelAfterGoto(String label, OutputTable outputTable, int numOfStr) {
            int numOfLabel = CheckIndentityLabel(label);
            if (numOfLabel != 0) { // если это объявленая метка
                if (outputTable.GetOutputTable().ElementAt(outputTable.GetOutputTable().Count - 1).substring == "goto") { // если последняя лексема goto
                    //this.tableOfLabels[numOfLabel - 1].usedGoto = true;
                    StrTableOfLabels newRec = new StrTableOfLabels(label, numOfLabel, true, false);
                    this.tableOfLabels[numOfLabel - 1] = newRec;
                    StrOutputTable outRec = new StrOutputTable(numOfStr, label, TableOfLabels.GetNumOfLexemLabel(), newRec.numOfLabel);
                    outputTable.AddRecord(outRec);
                    return numOfLabel;
                }
            }
            return 0;
        }

        public int CheckIsLabelUsedInProgram(String label, char nextSymbol, int numOfStr, OutputTable outputTable)
        { //использована ли метка в программе
            int numOfLabel = CheckIndentityLabel(label);
            if (numOfLabel != 0) {
                if(tableOfLabels[numOfLabel - 1].usedGoto && nextSymbol == ':'){ //если метка была использована в разделе goto и след символ двоеточие
                    StrTableOfLabels newRec = new StrTableOfLabels(label, numOfLabel, true, true);
                    this.tableOfLabels[numOfLabel - 1] = newRec;
                    StrOutputTable outRec = new StrOutputTable(numOfStr, label, TableOfLabels.GetNumOfLexemLabel(), newRec.numOfLabel);
                    outputTable.AddRecord(outRec);
                    return numOfLabel;
                }
            }
            return 0;
        }

        public int CheckIndentityLabel(String label) {

            foreach (StrTableOfLabels curLabel in tableOfLabels) {
                if (curLabel.label == label) {
                    return curLabel.numOfLabel;
                }
            }
            return 0;
        }

        public static int GenerateNumOfLabel(){
            return ++numOfLabel;
        }

        public static int GetNumOfLexemLabel() {
            return numOfLexem;
        }

        public void SetAllNullNumOfLabel()
        {
            for (int i = 0; i < tableOfLabels.Count; i++)
            {
                tableOfLabels[i] = new StrTableOfLabels(tableOfLabels[i].label, 0, tableOfLabels[i].usedGoto, tableOfLabels[i].usedInProgram);
            }
        }

        public List<String> GetLabelWithNullNum() // которые были объявлены в разделе label
        {
            List<String> res = new List<String>();

            foreach (StrTableOfLabels curLabel in tableOfLabels)
            {
                if(curLabel.numOfLabel == 0)
                {
                    res.Add(curLabel.label);
                }
            }
            return res;
        }

        public void SetNumInPoliz(String label, int num)
        {
            for (int i = 0; i < tableOfLabels.Count; i++)
            {
                if (tableOfLabels[i].label == label)
                {
                    tableOfLabels[i] = new StrTableOfLabels(label, num, tableOfLabels[i].usedGoto, tableOfLabels[i].usedInProgram);
                    break;
                }
            }
        }


    }
}

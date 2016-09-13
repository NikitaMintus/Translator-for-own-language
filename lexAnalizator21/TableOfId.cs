using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    class TableOfId
    {
        private List<StrTableOfId> tableOfId;
        private static int numOfId;
        private static int numOfLexem = 35;

        public static void ClearNumOfId() {
            numOfId = 0;
        }

        public TableOfId() {
            this.tableOfId = new List<StrTableOfId>();
        }

        public double GetIdValue(String id)
        {
            foreach (StrTableOfId curId in tableOfId)
            {
                if(curId.id == id)
                {
                    return curId.value;
                }
            }
            return -1;
        }

        public void SetIdValue(String id, double value)
        {
            for (int i = 0; i < tableOfId.Count; i++)
            {
                if (tableOfId[i].id == id)
                {
                    tableOfId.Insert(i, new StrTableOfId(tableOfId[i].id, tableOfId[i].numOfId, value));
                    tableOfId.RemoveAt(i + 1);
                }
            }
        }

        public void AddRecord(StrTableOfId record) {
            this.tableOfId.Add(record);
        }

        public void AddRange(List<StrTableOfId> range) {
            this.tableOfId.AddRange(range);
        }

        public List<StrTableOfId> GetTableOfId() {
            return this.tableOfId;
        }

        public  static int GenerateNumOfId() {
            return ++numOfId;
        }

        public static int GetNumOfLexemId() {
            return numOfLexem;
        }

        public int CheckIdentityId(String id){

            foreach (StrTableOfId record in tableOfId){
                if (record.id == id){
                    return record.numOfId; // такой идентификатор уже есть
                }
            }
            return 0;
        }

        public int CheckIdentityIdInDeclaration(String id, TableOfId tableOfId, OutputTable outputTable)
        {
            if (outputTable.SearchOutputLexem("var") && !(outputTable.SearchOutputLexem("begin"))) { //если есть var и нет begin
                int numOfId = CheckIdentityId(id);
                if (numOfId != 0) {
                    return numOfId;
                }
            }
            return 0;
        }

        public bool CheckIsDeclarationId(String id, TableOfId tableOfId, OutputTable outputTable)
        {

            if (outputTable.SearchOutputLexem("begin"))
            {
                if (tableOfId.CheckIdentityId(id) == 0)
                { //необъявленный идентификатор
                    return false;
                }
            }
            return true;
        }
    }
}

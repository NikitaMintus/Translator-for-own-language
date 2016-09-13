using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace lexAnalizator21
{
    class TableOfLexems 
    {
        private StrTableOfLexems[] tableOfLexems;

        public void SetLexems(StrTableOfLexems[] tableOfLexems)
        {
            this.tableOfLexems = tableOfLexems;
        }

        public StrTableOfLexems[] GetLexems()
        {
            return this.tableOfLexems;
        }

        public StrTableOfLexems[] GetLexemsFromFile()
        {
            String[] lexems;
            StrTableOfLexems[] tableOfLexems;
            try
            {  
                lexems = File.ReadAllLines("tableOfLexems.txt").Select(s => s.Trim()).ToArray();
                tableOfLexems = new StrTableOfLexems[lexems.Count()];
                int count = 1;
                foreach (String str in lexems)
                {
                    tableOfLexems[count - 1].lexem = str;
                    tableOfLexems[count - 1].numOfLexem = count;
                    count++;
                }
                return tableOfLexems;
            }
            catch (Exception e)
            {
                tableOfLexems = new StrTableOfLexems[1];
                tableOfLexems[0].lexem = "-1";
                MessageBox.Show("File not open!!!", "Error", MessageBoxButtons.OK);
                return tableOfLexems;
            }
        }

        public int SearchLexem(String lexem) {

            foreach (StrTableOfLexems record in tableOfLexems) {
                if (lexem == record.lexem) {
                    return record.numOfLexem;
                }
            }

            return 0;
        }

        public String ChangeEnter(String lexem) {
            if (lexem == " ")
            {
                lexem = "space";
            }

            if (lexem == "\n")
            {
                lexem = "enter";
            }
            return lexem;
        }
    }
}

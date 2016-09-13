using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    static class CurLexem
    {
        private static OutputTable outputTable;

        private static int curLexem = 0;

        public static int GetNumOfCurLexem() 
        {
            return curLexem;
        }

        public static void SetOutputTable(OutputTable outputTable1)
        {
            outputTable = outputTable1;
        }

        public static void SetNullCurLexem()
        {
            curLexem = 0;
        }

        public static int GetNumOfStrInProgramm()
        {
            return outputTable.GetOutputTable()[curLexem].numOfStr; 
        }

        public static int GetCurLexem()
        {
            return outputTable.GetOutputTable()[curLexem].numOfLexem;
        }

        public static StrOutputTable GetCurStr() //получить текущию строку
        {
            return outputTable.GetOutputTable()[curLexem];
        }

        public static StrOutputTable GetNextStr() // получить следующую строку
        {
            return outputTable.GetOutputTable()[++curLexem];
        }

        public static String GetCurSubstring() // возращает текущую лексему(строку)
        {
            return outputTable.GetOutputTable()[curLexem].substring; 
        }

        public static String GetNextSubstring() // возращает следующую лексему(строку)
        {
            return outputTable.GetOutputTable()[++curLexem].substring;
        }

        public static int TakeNextLexem()
        {
            try
            {
                return outputTable.GetOutputTable()[++curLexem].numOfLexem;
            }
            catch (Exception e)
            {
               return --curLexem;
            }
        }

        public static int TakePreviousLexem()
        {
            return outputTable.GetOutputTable()[--curLexem].numOfLexem;
        }

        

    }
}

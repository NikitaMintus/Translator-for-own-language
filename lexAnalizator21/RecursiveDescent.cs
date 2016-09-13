using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lexAnalizator21
{
    class RecursiveDescent
    {
        private OutputTable outputTable;
        private int curLexem = 0;

        private void InitCurLexem()
        {
            this.curLexem = 0;
        }

        private int GetStrCurLexem(List<StrOutputTable> outputTable) 
        {
            return outputTable[curLexem].numOfStr;
        }

        private int TakeFirstLexem(List<StrOutputTable> outputTable)
        {
            return outputTable[0].numOfLexem;
        }

        private int TakeNextLexem(List<StrOutputTable> outputTable)
        {
            try
            {
                return outputTable[++curLexem].numOfLexem;
            }
            catch (Exception e)
            {
                --curLexem;
                return -1;
            }
        }

        private int TakePreviousLexem(List<StrOutputTable> outputTable)
        {
            return outputTable[--curLexem].numOfLexem;
        }

        public String AksiomaProgram(List<StrOutputTable> outputTable, TableOfRecursiveErrors tableOfRecursiveErrors) {  // функция соответствующая правилу "программа"
            
            bool found = false;
            InitCurLexem();
            int numOfLex = TakeFirstLexem(outputTable);
            if (numOfLex == 1)  // program
            {
                numOfLex = TakeNextLexem(outputTable); //берем слел лексему
                if (numOfLex == 35)  // id
                {
                    numOfLex = TakeNextLexem(outputTable);
                    if (numOfLex == 33) // enter
                    {
                        numOfLex = TakeNextLexem(outputTable);
                        if (numOfLex == 2) // var
                        {
                            numOfLex = TakeNextLexem(outputTable);
                            if (numOfLex == 33) //enter
                            {
                                numOfLex = TakeNextLexem(outputTable);
                                if (ListOfDeclaration(outputTable, numOfLex) != 0) // список объявлений
                                {
                                    numOfLex = TakeNextLexem(outputTable);
                                    if (numOfLex == 33) //enter
                                    {
                                        numOfLex = TakeNextLexem(outputTable);
                                        if (numOfLex == 3) // label
                                        {
                                            numOfLex = TakeNextLexem(outputTable);
                                            if (numOfLex == 33) // enter
                                            {
                                                numOfLex = TakeNextLexem(outputTable);
                                                numOfLex = ListOfLabels(outputTable, numOfLex);
                                                if (numOfLex != 0) // список меток
                                                {
                                                    //numOfLex = TakeNextLexem(outputTable);
                                                    if (numOfLex == 33) //enter
                                                    {
                                                        numOfLex = TakeNextLexem(outputTable);
                                                        if (numOfLex == 4) //begin
                                                        {
                                                            numOfLex = TakeNextLexem(outputTable);
                                                            if (numOfLex == 33) // enter
                                                            {
                                                                numOfLex = TakeNextLexem(outputTable);
                                                                if ((numOfLex = ListOfOperators(outputTable, numOfLex)) != 0 && numOfLex != -1) //список операторов
                                                                {
                                                                    if (numOfLex == 33) // enter
                                                                    {
                                                                        numOfLex = TakeNextLexem(outputTable);
                                                                        if (numOfLex == 5) //end
                                                                        {
                                                                            found = true;
                                                                        }
                                                                        else // end
                                                                        {
                                                                            StrTableOfRecursiveErrors newErr = new StrTableOfRecursiveErrors(GetStrCurLexem(outputTable), "Error in operator");
                                                                            tableOfRecursiveErrors.AddError(newErr);
                                                                        }
                                                                    }
                                                                    else // enter
                                                                    {
                                                                        StrTableOfRecursiveErrors newErr = new StrTableOfRecursiveErrors(GetStrCurLexem(outputTable), "Missed separator \"enter\" ");
                                                                        tableOfRecursiveErrors.AddError(newErr);
                                                                    }
                                                                }
                                                                else // список операторов
                                                                {
                                                                    if (numOfLex == -1) // нет завершающего end
                                                                    {
                                                                        StrTableOfRecursiveErrors newErr = new StrTableOfRecursiveErrors(GetStrCurLexem(outputTable), "Missed keyword \"end\" ");
                                                                        tableOfRecursiveErrors.AddError(newErr);
                                                                    }
                                                                    else
                                                                    {
                                                                        StrTableOfRecursiveErrors newErr = new StrTableOfRecursiveErrors(GetStrCurLexem(outputTable), "Error in operator");
                                                                        tableOfRecursiveErrors.AddError(newErr);
                                                                    }
                                                                }
                                                            }
                                                            else // enter
                                                            {
                                                                StrTableOfRecursiveErrors newErr = new StrTableOfRecursiveErrors(GetStrCurLexem(outputTable), "Missed separator \"enter\" ");
                                                                tableOfRecursiveErrors.AddError(newErr);
                                                            }
                                                        }
                                                        else // begin
                                                        {
                                                            StrTableOfRecursiveErrors newErr = new StrTableOfRecursiveErrors(GetStrCurLexem(outputTable), "Missed keyword \"begin\" ");
                                                            tableOfRecursiveErrors.AddError(newErr);
                                                        }
                                                    }
                                                    else // enter
                                                    {
                                                        StrTableOfRecursiveErrors newErr = new StrTableOfRecursiveErrors(GetStrCurLexem(outputTable), "Missed separator \"enter\" ");
                                                        tableOfRecursiveErrors.AddError(newErr);
                                                    }
                                                }
                                                else // список меток
                                                {
                                                    StrTableOfRecursiveErrors newErr = new StrTableOfRecursiveErrors(GetStrCurLexem(outputTable), "Error in label declaration ");
                                                    tableOfRecursiveErrors.AddError(newErr);
                                                }
                                            }
                                            else // enter
                                            {
                                                StrTableOfRecursiveErrors newErr = new StrTableOfRecursiveErrors(GetStrCurLexem(outputTable), "Missed separator \"enter\" ");
                                                tableOfRecursiveErrors.AddError(newErr);
                                            }
                                        }
                                        else // label
                                        {
                                            StrTableOfRecursiveErrors newErr = new StrTableOfRecursiveErrors(GetStrCurLexem(outputTable), "Missed keyword \"label\" ");
                                            tableOfRecursiveErrors.AddError(newErr);
                                        }
                                    } // enter
                                    else 
                                    {
                                        StrTableOfRecursiveErrors newErr = new StrTableOfRecursiveErrors(GetStrCurLexem(outputTable), "Missed separator \"enter\" ");
                                        tableOfRecursiveErrors.AddError(newErr);
                                    }
                                } // список объявлений
                                else
                                {
                                    StrTableOfRecursiveErrors newErr = new StrTableOfRecursiveErrors(GetStrCurLexem(outputTable), "Error in var declaration ");
                                    tableOfRecursiveErrors.AddError(newErr);
                                }
                            }
                            else // enter
                            {
                                StrTableOfRecursiveErrors newErr = new StrTableOfRecursiveErrors(GetStrCurLexem(outputTable), "Missed separator \"enter\" ");
                                tableOfRecursiveErrors.AddError(newErr);
                            }
                        }
                        else // var
                        {
                            StrTableOfRecursiveErrors newErr = new StrTableOfRecursiveErrors(GetStrCurLexem(outputTable), "Missed keyword \"var\" ");
                            tableOfRecursiveErrors.AddError(newErr);
                        }
                    }
                    else // enter
                    {
                        StrTableOfRecursiveErrors newErr = new StrTableOfRecursiveErrors(GetStrCurLexem(outputTable), "Missed separator \"enter\" ");
                        tableOfRecursiveErrors.AddError(newErr);
                    }
                }
                else // id название программы
                {
                    StrTableOfRecursiveErrors newErr = new StrTableOfRecursiveErrors(GetStrCurLexem(outputTable), "Missed name of program ");
                    tableOfRecursiveErrors.AddError(newErr);
                }
            }
            else // program
            {
                StrTableOfRecursiveErrors newErr = new StrTableOfRecursiveErrors(GetStrCurLexem(outputTable), "Missed \"program\" ");
                tableOfRecursiveErrors.AddError(newErr);
            }

            if (found == true)
            {
                MessageBox.Show("Successful syntatic analysis !!!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return "Successful completion !!!";
            }
            else 
            {
                MessageBox.Show("Error of syntatic analysis !!!", "Result", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return "Error of compilation !!!";
            }
        }



        private int ListOfOperators(List<StrOutputTable> outputTable, int numOfLex) //список операторов
        {
            bool found = false;
            numOfLex = Operator(outputTable, numOfLex);
            if(numOfLex != 0)
            {
                found = true;
                numOfLex = TakeNextLexem(outputTable);
                while (numOfLex == 33 && found == true)
                {
                    numOfLex = TakeNextLexem(outputTable);
                    if (numOfLex == -1)
                    {
                        return -1; // нет end
                    }
                    numOfLex = Operator(outputTable, numOfLex);
                    if (numOfLex != 0)
                    {
                        numOfLex = TakeNextLexem(outputTable);
                    }
                    else
                    {
                        found = false;
                    }
                }
            }

            if (found)
            {
                return TakePreviousLexem(outputTable);
            }
            else
            {
                return 0;
            }
        }

        private int Operator(List<StrOutputTable> outputTable, int numOfLex) //оператор
        {
            bool found = false;
            if (numOfLex == 3) // 3 - label
            {
                numOfLex = TakeNextLexem(outputTable);
                if (numOfLex == 34) // 34 - ":"
                {
                    numOfLex = TakeNextLexem(outputTable);
                    if (numOfLex == 33) // enter
                    {
                        numOfLex = TakeNextLexem(outputTable);
                        numOfLex = NotTaggedOperator(outputTable, numOfLex); // непомеченный оператор
                        if (numOfLex != 0)
                        {
                            found = true;
                        }
                    }
                }
            }
            else
            {
                numOfLex = NotTaggedOperator(outputTable, numOfLex);
                if (numOfLex != 0)
                {
                    found = true;
                }
            }

            if (found)
            {
                return numOfLex;
            }
            else
            {
                return 0;
            }
        }

        private int NotTaggedOperator(List<StrOutputTable> outputTable, int numOfLex) // непомеченный оператор
        {
            bool found = false;
            switch(numOfLex)
            {
                case 8: //read
                    {
                        numOfLex = TakeNextLexem(outputTable);
                        if (numOfLex == 28) // ( 
                        {
                            numOfLex = TakeNextLexem(outputTable);
                            numOfLex = ListOfId(outputTable, numOfLex);
                            if (numOfLex != 0) 
                            {
                                if (numOfLex == 29) // )
                                {
                                    found = true;
                                }
                                else
                                {
                                    found = false;
                                }
                            }
                        }
                        break;
                    }
                case 9: //write
                    {
                        numOfLex = TakeNextLexem(outputTable);
                        if (numOfLex == 28) // ( 
                        {
                            numOfLex = TakeNextLexem(outputTable);
                            numOfLex = ListOfId(outputTable, numOfLex);
                            if (numOfLex != 0)
                            {
                                if (numOfLex == 29) // )
                                {
                                    found = true;
                                }
                                else
                                {
                                    found = false;
                                }
                            }
                        }
                        break;
                    }
                case 35: //id
                    {
                        numOfLex = TakeNextLexem(outputTable);
                        if (numOfLex == 37) // =
                        {
                            numOfLex = TakeNextLexem(outputTable);
                            numOfLex = Expression(outputTable, numOfLex);
                            if(numOfLex != 0)
                            {
                                found = true;
                            }
                        }
                        break;
                    }
                case 10: //do
                    {
                        numOfLex = TakeNextLexem(outputTable);
                        if (numOfLex == 35) // id
                        {
                            numOfLex = TakeNextLexem(outputTable);
                            if(numOfLex == 37) // =
                            {
                                numOfLex = TakeNextLexem(outputTable);
                                numOfLex = Expression(outputTable, numOfLex); //выражение
                                if(numOfLex != 0)
                                {
                                    numOfLex = TakeNextLexem(outputTable);
                                    if(numOfLex == 11) // to
                                    {
                                        numOfLex = TakeNextLexem(outputTable);
                                        numOfLex = Expression(outputTable, numOfLex); //выражение
                                        if (numOfLex != 0)
                                        {
                                            numOfLex = TakeNextLexem(outputTable);
                                            if(numOfLex == 33) // enter
                                            {
                                                numOfLex = TakeNextLexem(outputTable);
                                                numOfLex = ListOfOperators(outputTable, numOfLex); //список операторов
                                                if (numOfLex != 0)
                                                {
                                                   //numOfLex = TakeNextLexem(outputTable);
                                                   if(numOfLex == 33) // enter
                                                   {
                                                        numOfLex = TakeNextLexem(outputTable);
                                                        if (numOfLex == 12) // next
                                                        {
                                                            found = true;
                                                        }
                                                   }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    }
                case 13: //if
                    {
                        numOfLex = TakeNextLexem(outputTable);
                        numOfLex = LogicalExpression(outputTable, numOfLex);
                        if (numOfLex != 0)
                        {
                            numOfLex = TakeNextLexem(outputTable);
                            if (numOfLex == 14) // 14 - then
                            {
                                numOfLex = TakeNextLexem(outputTable);
                                if (numOfLex == 15) // 15 - goto
                                {
                                    numOfLex = TakeNextLexem(outputTable);
                                    if(numOfLex == 3) // 3 - label
                                    {
                                        found = true;
                                    }
                                }
                            }
                        }
                       
                        break;
                    }
                case 12: // next
                    {
                        return TakePreviousLexem(outputTable);
                    }
                case 5: // end
                    {
                        return TakePreviousLexem(outputTable);
                    }
            }

            if (found)
            {
                return numOfLex;
            }
            else
            {
                return 0;
            }
        }

        private int LogicalExpression(List<StrOutputTable> outputTable, int numOfLex) //логическое выражение
        {
            bool found = false;

            numOfLex = LogicalTerm(outputTable, numOfLex);
            if(numOfLex != 0)
            {
                found = true;
                numOfLex = TakeNextLexem(outputTable);
                while(numOfLex == 16 && found == true) // 16 - or
                {
                    numOfLex = TakeNextLexem(outputTable);
                    numOfLex = LogicalTerm(outputTable, numOfLex);
                    if (numOfLex != 0)
                    {
                        numOfLex = TakeNextLexem(outputTable);
                    }
                    else
                    {
                        found = false;
                    }
                }
            }

            if (found)
            {
                return TakePreviousLexem(outputTable);
            }
            else
            {
                return 0;
            }
        }

        private int LogicalTerm(List<StrOutputTable> outputTable, int numOfLex) // логический терм
        {
            bool found = false;

            numOfLex = LogicalMultiplier(outputTable, numOfLex); // логический множитель
            if (numOfLex != 0)
            {
                found = true;
                numOfLex = TakeNextLexem(outputTable);
                while (numOfLex == 17 && found == true) // 17 - and
                {
                    numOfLex = TakeNextLexem(outputTable);
                    numOfLex = LogicalMultiplier(outputTable, numOfLex); // логический множитель
                    if (numOfLex != 0)
                    {
                        numOfLex = TakeNextLexem(outputTable);
                    }
                    else
                    {
                        found = false;
                    }
                }
            }

            if (found)
            {
                return TakePreviousLexem(outputTable);
            }
            else
            {
                return 0;
            }
        }

        private int LogicalMultiplier(List<StrOutputTable> outputTable, int numOfLex) // логический множитель
        {
            bool found = false;
           
            switch (numOfLex)
            {
                case 30: // [лв]
                    {
                        numOfLex = TakeNextLexem(outputTable);
                        numOfLex = LogicalExpression(outputTable, numOfLex);
                        if (numOfLex != 0)
                        {
                            numOfLex = TakeNextLexem(outputTable);
                            if (numOfLex == 31) // ]
                            {
                                found = true;
                            }
                        }
                        break;
                    }
                case 23: // !
                    {
                        numOfLex = TakeNextLexem(outputTable);
                        numOfLex = LogicalMultiplier(outputTable, numOfLex); // логический множитель
                        if(numOfLex != 0)
                        {
                            found = true;
                        }
                        break;
                    }
                default:
                    {
                        numOfLex = Ratio(outputTable, numOfLex);
                        if(numOfLex != 0)
                        {
                            found = true;
                        }
                        break;
                    }
            }

            if (found)
            {
                return numOfLex;
            }
            else
            {
                return 0;
            }
        }

        private int Ratio(List<StrOutputTable> outputTable, int numOfLex) // отношение
        {
            bool found = false;
            numOfLex = Expression(outputTable, numOfLex);
            if (numOfLex != 0)
            {
                numOfLex = TakeNextLexem(outputTable);
                numOfLex = SignOfRatio(outputTable, numOfLex);
                if (numOfLex != 0)
                {
                    numOfLex = TakeNextLexem(outputTable);
                    numOfLex = Expression(outputTable, numOfLex);
                    if (numOfLex != 0)
                    {
                        found = true;
                    }
                }
            }

            if (found)
            {
                return numOfLex;
            }
            else
            {
                return 0;
            }   
        }

        private int SignOfRatio(List<StrOutputTable> outputTable, int numOfLex) //знак отношения
        {
            bool found = false;
            if(numOfLex == 18 || numOfLex == 19 || numOfLex == 20 || numOfLex == 21 || numOfLex == 22 || numOfLex == 38)
            {
                found = true;
            }

            if (found)
            {
                return numOfLex;
            }
            else
            {
                return 0;
            }
        }

        private int Expression(List<StrOutputTable> outputTable, int numOfLex) // выражение
        {
            bool found = false;
            numOfLex = Term(outputTable, numOfLex);
            if (numOfLex != 0)
            {
                found = true;
                numOfLex = TakeNextLexem(outputTable);
                while ((numOfLex == 24 || numOfLex == 25) && found == true) // 24 - "+"  25 - "-"
                {
                    numOfLex = TakeNextLexem(outputTable);
                    numOfLex = Term(outputTable, numOfLex);
                    if (numOfLex != 0)
                    {
                        numOfLex = TakeNextLexem(outputTable);
                    }
                    else
                    {
                        found = false;
                    }
                }
            }

            if (found)
            {
                return TakePreviousLexem(outputTable);
            }
            else
            {
                return 0;
            }
        }

        private int Term(List<StrOutputTable> outputTable, int numOfLex) // терм  //НЕПРАВИЛЬНО ПОСЛЕ ПЕРВОГО МНОЖ СРАЗУ ТРУ
        {
            bool found = false;
            numOfLex = Multiplier(outputTable, numOfLex);
            if (numOfLex != 0)
            {
                found = true;
                numOfLex = TakeNextLexem(outputTable);
                while ((numOfLex == 26 || numOfLex == 27) && found == true) // 26 - "*" 27 - "/"
                {
                    numOfLex = TakeNextLexem(outputTable);
                    numOfLex = Multiplier(outputTable, numOfLex);
                    if (numOfLex != 0)
                    {
                        numOfLex = TakeNextLexem(outputTable);
                    }
                    else
                    {
                        found = false;
                    }
                }
            }

            if (found)
            {
                numOfLex = TakePreviousLexem(outputTable);
                return numOfLex;
            }
            else
            {
                return 0;
            }
        }

        private int Multiplier(List<StrOutputTable> outputTable, int numOfLex) // множитель
        {
            bool found = false;
            switch(numOfLex)
            {
                case 35: //id
                    {
                        found = true;
                        break;
                    }
                case 36: // cons
                    {
                        found = true;
                        break;
                    }
                case 28: // выражение 28 - "("
                    {
                        numOfLex = TakeNextLexem(outputTable);
                        numOfLex = Expression(outputTable, numOfLex);
                        if (numOfLex != 0)
                        {
                            numOfLex = TakeNextLexem(outputTable);
                            if(numOfLex == 29) // 29 - ")"
                            {
                                found = true;
                            }
                        }
                        break;
                    }
            }

            if (found)
            {
                return numOfLex;
            }
            else
            {
                return 0;
            }
        }

        private int ListOfId(List<StrOutputTable> outputTable, int numOfLex) //список идентификаторов
        {
            bool found = false;
            if (numOfLex == 35) // иден-р
            {
                found = true;
                numOfLex = TakeNextLexem(outputTable);
                while (numOfLex == 32 && found == true) // 32 - ","
                {
                    numOfLex = TakeNextLexem(outputTable);
                    if (numOfLex == 35) // иден-р
                    {
                        numOfLex = TakeNextLexem(outputTable);
                    }
                    else
                    {
                        found = false;
                    }
                }
            }
            else
            {
                found = false;
            }

            if (found)
            {
                return numOfLex;
            }
            else
            {
                return 0;
            }
        }

        

        private int ListOfDeclaration(List<StrOutputTable> outputTable, int numOfLex)  //список объявлений
        {
            bool found = false;
            numOfLex = Declaration(outputTable, numOfLex);
            if (numOfLex != 0) // объявление
            {
                found = true;
                while (numOfLex == 33 && found == true) // 33 - enter
                {
                    numOfLex = TakeNextLexem(outputTable);
                    numOfLex = Declaration(outputTable, numOfLex);
                    if (numOfLex != 0) // объявление
                    {
                        numOfLex = TakeNextLexem(outputTable);
                    }
                    else
                    {
                        found = false;
                    }
                }
            }

            if (found)
            {
                return numOfLex;
            }
            else
            {
                return 0;
            }
        }

        private int Declaration(List<StrOutputTable> outputTable, int numOfLex) //объявление
        {
            bool found = false;
            numOfLex = ListOfId(outputTable, numOfLex);
            if (numOfLex != 0) //список id
            {
                if (numOfLex == 34) // 34 - ":"
                {
                    numOfLex = TakeNextLexem(outputTable);
                    if (numOfLex == 7) // 37 - real
                    {
                        found = true;
                    }
                }
            }

            if (found)
            {
                return numOfLex;
            }
            else
            {
                return 0;
            }
        }

        private int ListOfLabels(List<StrOutputTable> outputTable, int numOfLex) //список меток
        {
            bool found = false;
            if (numOfLex == 3)
            {
                numOfLex = TakeNextLexem(outputTable);
                found = true;
                while (numOfLex == 32 && found == true)
                {
                    numOfLex = TakeNextLexem(outputTable);
                    if (numOfLex == 3) // метка label
                    {
                        numOfLex = TakeNextLexem(outputTable);
                    }
                    else 
                    {
                        found = false;
                    }
                }
            }

            if (found)
            {
                return numOfLex;
            }
            else
            {
                return 0;
            }
        }

        public RecursiveDescent(OutputTable outputTable) {
            this.outputTable = outputTable;
        }
    }
}

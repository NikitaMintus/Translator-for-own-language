using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace lexAnalizator21
{
    class BottomUpAnalysis
    {
        private TableOfPrecedenceRelation tableOfPrecedenceRelation;
        private TableOfId tableOfId;
        private StrBottomUpTable bottomUpTable;
        private List<StrOutputBottomUpTable> allNotes = new List<StrOutputBottomUpTable>();
        private List<String> poliz = new List<String>();
        private String idContainer = "";

        public BottomUpAnalysis(TableOfPrecedenceRelation tableOfPrecedenceRelation, OutputTable outputTable, TableOfId tableOfId)
        {
            this.tableOfPrecedenceRelation = tableOfPrecedenceRelation;
            this.bottomUpTable.inputChane = outputTable.GetOutputTable();
            this.bottomUpTable.stack = new Stack<string>();
            this.tableOfId = tableOfId;
            this.bottomUpTable.stack.Push("#"); //ложим сначала в стек решетку
            CurLexem.SetOutputTable(outputTable);
            int len = outputTable.GetOutputTable().Count();
            outputTable.GetOutputTable().RemoveAt(len-1); // удаляем последний ненужный enter
            outputTable.GetOutputTable().Add(new StrOutputTable(-1, "#", -1, -1)); //добавляем в конец решетку
        }

        public List<StrOutputBottomUpTable> GetAllNotes()
        {
            return this.allNotes;
        }

        public void AnalyzeTable() // главная функция разбора
        {
            String curPoliz = "";
            bool flagArifmVyr = false;
            CurLexem.SetNullCurLexem();
            int step = 0;
            
            while (true)
            {
                StrOutputTable curLexem = CurLexem.GetCurStr();
                String elem = CheckIsId(curLexem.numOfLexem);
                if (elem != "-1") // если это id, metka или const
                {
                    if (curLexem.substring != "label") // label - ключевое слово так и остается
                    {
                        curLexem.substring = elem;
                    }
                }
                String elemInStack = bottomUpTable.stack.Peek();
                String relation = tableOfPrecedenceRelation.GetRelation(elemInStack, curLexem.substring);
                String curStack = bottomUpTable.stack.Aggregate((i, j) => j + " " + i).ToString();
                allNotes.Add(new StrOutputBottomUpTable(step, curStack, relation, bottomUpTable.inputChane[CurLexem.GetNumOfCurLexem()].substring, curPoliz));
                curPoliz = "";
                if (relation == "<" || relation == "=")
                {
                    bottomUpTable.stack.Push(curLexem.substring);
                    CurLexem.GetNextStr();
                }
                if (relation == ">")
                {
                    String basis = "";
                    basis = GetBasisInStack(basis);
                    if (flagArifmVyr)
                    {
                        DoPoliz(basis);
                    }
                    String leftPart = SearchBasisInGrammar(basis);
                    if (leftPart != "-1")
                    {
                        bottomUpTable.stack.Push(leftPart); // заменяем основу в стеке левой частью (нетерминалом)
                    }
                }

                if (bottomUpTable.stack.Peek() == "=") //если началось арифметическое выражение
                {
                    flagArifmVyr = true;
                    idContainer = bottomUpTable.inputChane[CurLexem.GetNumOfCurLexem() - 2].substring;
                }

                if ((bottomUpTable.stack.Peek() == "$vyr1$") && (bottomUpTable.stack.ElementAt(1) != "(") && (flagArifmVyr == true)) //конец арифметического выражения
                {
                    double resD = 0;
                    flagArifmVyr = false;
                    String resPoliz = CalculatePoliz(poliz);
                    try
                    {
                        resD = Double.Parse(resPoliz);
                        tableOfId.SetIdValue(idContainer, resD);
                    }
                    catch (Exception e)
                    {
                        resPoliz = "division by zero";
                    }
                    curPoliz = idContainer + " = ";
                    curPoliz += poliz.Aggregate((i, j) => i + " " + j).ToString();
                    curPoliz += " = " + resPoliz;
                    poliz.Clear();
                }

                ++step;
 
                if (relation == "")
                {
                    int num = bottomUpTable.inputChane[CurLexem.GetNumOfCurLexem()].numOfStr;
                    if (num != -1)
                    {
                        MessageBox.Show("Error at " + num + " string!!!!!", "Finish", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        (Application.OpenForms[0] as Form1).labelResBottom.ForeColor = System.Drawing.Color.Red;
                        (Application.OpenForms[0] as Form1).labelResBottom.Text = "Result: Error at " + num + " string!!!!!";
                    }
                    else
                    {
                        MessageBox.Show("Error at list of operator!!!!! Maybe missing 'next'", "Finish", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        (Application.OpenForms[0] as Form1).labelResBottom.ForeColor = System.Drawing.Color.Red;
                        (Application.OpenForms[0] as Form1).labelResBottom.Text = "Result: Error at list of operator!!!!! Maybe missing 'next'";
                    }
                    break;
                }

                if (bottomUpTable.stack.Peek() == "$programma$")
                {
                    curStack = bottomUpTable.stack.Aggregate((i, j) => j + " " + i).ToString();
                    allNotes.Add(new StrOutputBottomUpTable(step, curStack, relation, bottomUpTable.inputChane[CurLexem.GetNumOfCurLexem()].substring, curPoliz));
                    MessageBox.Show("Success syntactic analysis!!!", "Finish", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    (Application.OpenForms[0] as Form1).labelResBottom.ForeColor = System.Drawing.Color.Green;
                    (Application.OpenForms[0] as Form1).labelResBottom.Text = "Result: Success syntactic analysis!!!";
                    break;
                }
            }
        }

        public String GetBasisInStack(String basis) // получить основу из стека
        {
            String firstElem = bottomUpTable.stack.Pop();
            String secondElem = bottomUpTable.stack.Peek();
            String relation = tableOfPrecedenceRelation.GetRelation(secondElem, firstElem);
            if (relation == "=")
            {
                //basis += " ";
                basis = String.Format("{0}{1}", firstElem, basis);
                basis = String.Format("{0}{1}", " ", basis);
                basis = GetBasisInStack(basis);
                
            }
            if (relation == "<")
            {
                basis = String.Format("{0}{1}", firstElem, basis);
            }
            return basis;
        }

        public String SearchBasisInGrammar(String basis)
        {
            String viewBasis = ChangedGrammar.GetChangedGrammar()[3].rightPart.Trim(' ');
            foreach(StrChangedGrammar curStr in ChangedGrammar.GetChangedGrammar())
            {
                if (curStr.rightPart == basis)
                {
                    return curStr.leftPart;
                }
            }
            return "-1";
        }

        public String CalculatePoliz(List<String> poliz) //вычисление полиз
        {
            double first = 0;
            double second = 0;
            Stack<String> stack = new Stack<String>();
            foreach (String curElem in poliz)
            {
                if (curElem != "*" && curElem != "/" && curElem != "+" && curElem != "-")
                {
                    stack.Push(curElem);
                }
                else
                {
                    //try
                    //{
                        String firstStr = stack.Pop();
                        String secondStr = stack.Pop();
                        if (firstStr.IndexOf('.') != -1)
                        {
                            firstStr = firstStr.Replace('.', ',');
                        }
                        if (secondStr.IndexOf('.') != -1)
                        {
                            secondStr = secondStr.Replace('.', ',');
                        }
                        try
                        {
                            first = Double.Parse(firstStr);
                        }
                        catch (Exception e)
                        {
                            first = tableOfId.GetIdValue(firstStr);
                        }
                        try
                        {
                            second = Double.Parse(secondStr);
                        }
                        catch (Exception e)
                        {
                            second = tableOfId.GetIdValue(secondStr);
                        }
                        //second = Double.Parse(secondStr);
                    //}
                   // catch (Exception e)
                   // {
                   //     return "id";
                   // }
                    String result = CalculateExpression(first, second, curElem);
                    stack.Push(result);
                }
            }
            return stack.Pop();
        }

        public String CalculateExpression(double first, double second, String sign)
        {
            double res = 0;
            switch(sign)
            {
                case "+":
                    res = second + first;
                    break;
                case "-":
                    res = second - first;
                    break;
                case "*":
                    res = second * first;
                    break;
                case "/":
                    try
                    {
                        res = second / first;
                    }
                    catch (DivideByZeroException)
                    {
                        return "null";
                    }
                   
                    break;
            }
            return res.ToString();
        }

        public String DoPoliz(String basis)  //формирование полиз
        {
            if (basis == "id" || basis == "cons")
            {
                poliz.Add(bottomUpTable.inputChane[CurLexem.GetNumOfCurLexem() - 1].substring);
            }
            if(basis == "$term$ * $mnozh1$")
            {
                poliz.Add("*");
            }
            if (basis == "$term$ / $mnozh1$")
            {
                poliz.Add("/");
            }
            if(basis == "$vyr$ + $term1$")
            {
                poliz.Add("+");
            }
            if (basis == "$vyr$ - $term1$")
            {
                poliz.Add("-");
            }
            return "";
        }

        public String CheckIsId(int codeLexem) // проверка это id, metka или const
        {
            if (codeLexem == 35)
            {
                return "id";
            }
            if (codeLexem == 3)
            {
                return "metka";
            }
            if (codeLexem == 36)
            {
                return "cons";
            }
            return "-1";
        }

    }
}

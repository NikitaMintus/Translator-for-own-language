using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    class Poliz
    {
        private List<String> poliz = new List<String>();
        private Stack<StrPrioritetOperator> stack = new Stack<StrPrioritetOperator>();
        private TableOfPrioritets tableOfPrioritets = new TableOfPrioritets();
        private TableOfLabels tableOfLabels;
        private TableOfId tableOfId;
        private Stack<String> curWorkLabels = new Stack<String>();  
        private int end = 0;
        private bool wasTo = false; // для первого enter в цикле
        //private bool cycle = false;
        //private String cycleParam = "";
        private Cycle cycle;

        public List<String> GetPoliz()
        {
            return this.poliz;
        }

        public void GetCurLexem()
        {
            String cur = CurLexem.GetCurSubstring();
            String next = CurLexem.GetNextSubstring();
        }

        public void DoPoliz()
        {
            int start = CurLexem.GetNumOfCurLexem();
            for (int i = start; i < end; i++)
            {
                int curLexemNum = CurLexem.GetCurStr().numOfLexem;

                if (i == 63)
                {
                    i = 63;
                }

                if (curLexemNum == 33) //enter
                {
                    //выталкивает из стека все что выше приоритетом
                    int prioritet = tableOfPrioritets.GetPrioritet(CurLexem.GetCurSubstring());
                    PushEnter(new StrPrioritetOperator(CurLexem.GetCurSubstring(), prioritet));
                    CurLexem.TakeNextLexem();
                    continue;
                }
                if (curLexemNum == 35 || curLexemNum == 36 || curLexemNum == 3 || curLexemNum == 34) // id или const или метка или :
                {
                    if (curLexemNum == 34)
                    {
                        poliz[poliz.Count - 1] = poliz[poliz.Count - 1] + CurLexem.GetCurSubstring();
                        CurLexem.TakeNextLexem();
                        continue;
                    }
                    poliz.Add(CurLexem.GetCurSubstring()); //сразу в Полиз
                    CurLexem.TakeNextLexem();
                    continue;
                }
                if (curLexemNum == 28 || curLexemNum == 13) // ( или if
                {
                    int prioritet = tableOfPrioritets.GetPrioritet(CurLexem.GetCurSubstring()); //ложится сверху стека
                    stack.Push(new StrPrioritetOperator(CurLexem.GetCurSubstring(), prioritet));
                    CurLexem.TakeNextLexem();
                    continue;
                }
                if (curLexemNum == 14) // then
                {
                    PushThen();
                    CurLexem.TakeNextLexem();
                    continue;
                }
                if (curLexemNum == 29) // )
                {
                    PushClosedBracket();
                    CurLexem.TakeNextLexem();
                    continue;
                }
                if (curLexemNum == 32) // ,
                {
                    CurLexem.TakeNextLexem();  // пропускаем
                    continue;
                }
                if (curLexemNum == 10) // do
                {
                    int prioritet = tableOfPrioritets.GetPrioritet(CurLexem.GetCurSubstring());
                    PushDo(new StrPrioritetOperator(CurLexem.GetCurSubstring(), prioritet));
                    CurLexem.TakeNextLexem();
                    continue;
                }
                if (curLexemNum == 11) // to
                {
                    int prioritet = tableOfPrioritets.GetPrioritet(CurLexem.GetCurSubstring());
                    PushTo(new StrPrioritetOperator(CurLexem.GetCurSubstring(), prioritet));
                    CurLexem.TakeNextLexem();
                    continue;
                }
                if (curLexemNum == 12) // next
                {
                    String m1 = curWorkLabels.Pop();
                    curWorkLabels.Pop();
                    String m3 = curWorkLabels.Pop();
                    poliz.Add(m3);
                    poliz.Add("БП");
                    poliz.Add(m1 + ":");
                    tableOfLabels.SetNumInPoliz(m1, poliz.Count - 1);
                    CurLexem.TakeNextLexem();
                    continue;
                }
                else //  для всех остальных операторов
                {
                    if (curLexemNum == 37 && this.cycle.GetCycle() == true) // = и это цикл
                    {
                        cycle.SetCycleParam(poliz[poliz.Count - 1]);
                        cycle.SetCycle(false);
                    }
                    int prioritet = tableOfPrioritets.GetPrioritet(CurLexem.GetCurSubstring());
                    PushOperator(new StrPrioritetOperator(CurLexem.GetCurSubstring(), prioritet));
                    CurLexem.TakeNextLexem();
                }
            }
           
            SearchLabelsAndSetPozition(tableOfLabels.GetLabelWithNullNum()); // ставим номер меткам которые были объявлены в разделе label
        }

        public void PushOperator(StrPrioritetOperator oper)  // В общем случае
        {
            if (stack.Count == 0)
            {
                stack.Push(oper);
                return;
            }
            StrPrioritetOperator curHead = stack.Peek();
            if (curHead.oper == "begin" && oper.oper == "end")
            {
                stack.Pop();
            }
            if (oper.prioritet <= curHead.prioritet)
            {
                String headOper = stack.Pop().oper;
                poliz.Add(headOper);
                PushOperator(oper);
            }
            else
            {
                stack.Push(oper);
            }
        }

        public void PopAllThatLess(StrPrioritetOperator oper) // выталкивает все что больше по приоритету, но потом оператор не добавляет в стек
        {
            StrPrioritetOperator headOper = stack.Peek();
            if (oper.prioritet < headOper.prioritet)
            {
                poliz.Add(stack.Pop().oper);
                PopAllThatLess(oper);
            }
        }

        public void PushClosedBracket() // ) выталкивает все до (
        {
            String headOper = stack.Pop().oper;
            if (headOper != "(")
            {
                poliz.Add(headOper);
                PushClosedBracket();
            }
            else // нашли открывающую скобку, ее в Полиз не пишем
            {
                return;
            }
        }

        public void PushThen() // then
        {
            String headOper = stack.Peek().oper;
            if (headOper != "if")
            {
                poliz.Add(headOper);
                stack.Pop();
                PushThen();
            }
            else // нашли if
            {
                String curLabel = GenerateLabel();
                int prioritetLabel = tableOfPrioritets.GetPrioritet("label");
                stack.Push(new StrPrioritetOperator(curLabel, prioritetLabel)); // должно ложитьс сверху на if
                poliz.Add(curLabel);
                poliz.Add("УПЛ");
            }
        }

        private String GenerateLabel() //генерация след-й уникальной метки и добавление в таблицу меток
        {
            bool flag = true;
            String curLabel = "";
            int numOfLabel = 0;
            while (flag)
            {
                numOfLabel = TableOfLabels.GenerateNumOfLabel();
                curLabel = "m" + numOfLabel;
                if (tableOfLabels.CheckIndentityLabel(curLabel) == 0)
                {
                    flag = false;
                }
            }
            tableOfLabels.AddRecord(new StrTableOfLabels(curLabel, 0, false, false));
            curWorkLabels.Push(curLabel); // текущие сгенерированные метки
            return curLabel;
        }

        public void PushDo(StrPrioritetOperator operDo) 
        {
            String curLabel = "";
            this.cycle.SetCycle(true);
            stack.Push(operDo);
            int prioritetLabel = tableOfPrioritets.GetPrioritet("label");
            for (int i = 0; i < 3; i++)
            {
                curLabel = GenerateLabel();
                stack.Push(new StrPrioritetOperator(curLabel, prioritetLabel));
            }
        }

        public void PushTo(StrPrioritetOperator operTo)
        {
            wasTo = true;
            PopAllThatLess(operTo);
            String curRLabel = cycle.GenerateRLabel();
            poliz.Add(curRLabel);
            tableOfId.AddRecord(new StrTableOfId(curRLabel, TableOfId.GenerateNumOfId(), 0.0));
            poliz.Add("1");
            poliz.Add("=");
            poliz.Add(curWorkLabels.ElementAt(2) + ":"); // m i
            tableOfLabels.SetNumInPoliz(curWorkLabels.ElementAt(2), poliz.Count - 1);
            poliz.Add(curRLabel);
            poliz.Add("0");
            poliz.Add("==");
            poliz.Add(curWorkLabels.ElementAt(1)); // m i+1
            poliz.Add("УПЛ");
            poliz.Add(cycle.GetCycleParam());
            poliz.Add(cycle.GetCycleParam());
            poliz.Add("1");
            poliz.Add("+");
            poliz.Add("=");
            poliz.Add(curWorkLabels.ElementAt(1) + ":"); // m i+1
            tableOfLabels.SetNumInPoliz(curWorkLabels.ElementAt(1), poliz.Count - 1);
            poliz.Add(curRLabel);
            poliz.Add("0");
            poliz.Add("=");
            poliz.Add(cycle.GetCycleParam());
        }

        public void PushEnter(StrPrioritetOperator enter) // enter выталкивает все из стека
        {
            if (stack.Count == 0)  // enter выталкивает все что выше его по приоритету
            {
                return;
            }
            int length = stack.Count;
            for (int i = 0; i < length; i++)
            {
                StrPrioritetOperator curHead = stack.Peek();

                if (wasTo == true)
                {
                    poliz.Add("-");
                    poliz.Add("0");
                    poliz.Add("<=");
                    poliz.Add(curWorkLabels.ElementAt(0));
                    poliz.Add("УПЛ");
                    wasTo = false;
                }

                if (curHead.oper == "goto")  // когда enter конец if
                {
                    poliz.Add("БП");
                    String label = curWorkLabels.Pop();
                    poliz.Add(label + ":");
                    tableOfLabels.SetNumInPoliz(label, poliz.Count - 1);
                    stack.Pop(); //выталкиваем goto из стека
                    stack.Pop(); //выталкиваем метку из стека
                    stack.Pop(); //выталкиваем if из стека
                    break;
                }
                if (enter.prioritet < curHead.prioritet)
                {
                    String headOper = stack.Pop().oper;
                    poliz.Add(headOper);
                }
                else
                {
                    break;
                }
            }
            if (poliz.Count != 0)
            {
                poliz.Add("\n");
            }
        }

        private void PushEnterEndOfIf(StrPrioritetOperator curHead)
        {
            if (curHead.oper == "goto")
            {
                poliz.Add("БП");
                String label = curWorkLabels.Pop();
                poliz.Add(label);
                tableOfLabels.SetNumInPoliz(label, poliz.Count - 1);
            }
        }

        private void SearchBegin() // начинаем строить ПОЛИЗ с begin
        {
            while (true)
            {
                String lex = CurLexem.GetNextSubstring();
                if (lex == "begin")
                {
                    return;
                }
            }
        }

        private void SearchLabelsAndSetPozition(List<String> labels)
        {
            foreach (String curLabel in labels)
            {
                for (int i = 0; i < poliz.Count; i++)
                {
                    if (poliz[i].IndexOf(':') != -1)
                    {
                        if (poliz[i].TrimEnd(':') == curLabel)
                        {
                            tableOfLabels.SetNumInPoliz(curLabel, i);
                        }
                    }
                }
            }
        }

        public Poliz(OutputTable outputTable, TableOfLabels tableOfLabels, TableOfId tableOfId) 
        {
            this.tableOfLabels = tableOfLabels;
            this.tableOfId = tableOfId;
            this.cycle = new Cycle();
            this.end = outputTable.GetOutputTable().Count - 1;
            tableOfLabels.SetAllNullNumOfLabel();
            CurLexem.SetOutputTable(outputTable);
            CurLexem.SetNullCurLexem();
            SearchBegin();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lexAnalizator21
{
    class PerfomancePoliz
    {
        private List<String> poliz;
        private Stack<String> stack = new Stack<String>();
        private TableOfId tableOfId;
        private TableOfLabels tableOfLabels;
        private TableOfConstant tableOfConstant;
        
        public void DoPerfomance()
        {

            String curLabel = "";

            for (int i = 0; i < poliz.Count; i++)
            {
                if(tableOfId.CheckIdentityId(poliz[i]) != 0) //если это идентификатор
                {
                    stack.Push(poliz[i]);
                    continue;
                }

                if(tableOfConstant.CheckIndentityConstant(poliz[i]) != 0 || CheckIsConstant(poliz[i])) //если это консанта
                {
                    stack.Push(poliz[i]);
                    continue;
                }

                if (tableOfLabels.CheckIndentityLabel(poliz[i]) != 0) //если это метка
                {
                    curLabel = poliz[i];
                    if (curLabel == "m2")
                    {
                        curLabel.Trim();
                    }
                    continue;
                }

                if (poliz[i] == "УПЛ")
                {
                    bool flag = Boolean.Parse(stack.Pop());

                    if (flag == false)
                    {
                        i = tableOfLabels.GetNumOfLabel(curLabel);
                        continue;
                    }
                    else
                    {
                        continue;
                    }
                }

                if (poliz[i] == "БП")
                {
                    i = tableOfLabels.GetNumOfLabel(curLabel);
                    continue;
                }

                if(poliz[i] == "write")
                {
                    Stack<String> elements = new Stack<string>();
                    elements = WriteElements(elements);
                    foreach (String curElem in elements)
                    {
                        (Application.OpenForms[0] as Form1).richTextConsole.Text += curElem + "\n";
                        
                    }
                    //(Application.OpenForms[0] as Form1).richTextConsole.Text += "\n";
                    continue;
                }

                if (poliz[i] == "read")
                {
                    FormInput formInput = new FormInput();
                    List<TextBox> textBoxes = new List<TextBox>();
                    List<Label> labels = new List<Label>();
                    formInput.StartPosition = FormStartPosition.CenterScreen; 
                    formInput.Height = 10;
                    formInput.AutoSize = true;
                    int x = formInput.Width / 2 - 100;
                    int y = 10;
                    List<String> elements = stack.Reverse().ToList();
                    stack.Clear();
                    for (int j = 0; j < elements.Count; j++)
                    {
                        textBoxes.Add(new TextBox());
                        labels.Add(new Label());
                        textBoxes[j].Name = "textBox" + elements[j];
                        labels[j].Name = "label" + elements[j];
                        textBoxes[j].Width = 150;
                        textBoxes[j].Height = 10;
                        labels[j].Width = 10;
                        labels[j].AutoSize = true;
                        labels[j].Text = (elements[j] + " = ");
                        labels[j].Location = new System.Drawing.Point(x, y);
                        textBoxes[j].Location = new System.Drawing.Point(x + labels[j].Width + 20, y);
                        textBoxes[j].Text = "0";
                        formInput.Controls.Add(textBoxes[j]);
                        formInput.Controls.Add(labels[j]);
                        y += 30;
                    }
                  
                    Button butOK = new Button();
                    butOK.Text = "OK";
                    butOK.Location = new System.Drawing.Point(x + 50, y);
                    butOK.Click += delegate
                    {
                        bool flag = true;
                        for (int j = 0; j < textBoxes.Count; j++)
                        {
                            if (textBoxes[j].Text != "") //если текстовое поле не пусто
                            {
                                try
                                {
                                    tableOfId.SetIdValue(elements[j], Double.Parse(textBoxes[j].Text));
                                }
                                catch (Exception e)
                                {
                                    textBoxes[j].BackColor = System.Drawing.Color.Red;
                                    flag = false;
                                }
                            }
                            else
                            {
                                textBoxes[j].BackColor = System.Drawing.Color.Red;
                                flag = false;
                            }
                        }
                        if (flag)
                        {
                            formInput.Close();
                        }
                        else
                        {
                            MessageBox.Show("Корректно заполните выделенные поля!!!");
                        }
                    };
                    formInput.Controls.Add(butOK);
                    formInput.Height += 10;
                    formInput.ShowDialog();
                }


                if (poliz[i] == "+" || poliz[i] == "-" || poliz[i] == "/" || poliz[i] == "*")
                {
                    double first = 0;
                    double second = 0;

                    first = PopElem();
                    second = PopElem();

                    String sign = poliz[i];
                    String res = CalculateExpression(first, second, sign);
                    stack.Push(res);
                    continue;
                }

                if(poliz[i] == "=")
                {
                    double first = PopElem();
                    String second = stack.Pop();
                    tableOfId.SetIdValue(second, first);
                    continue;
                }

                if (poliz[i] == "==" || poliz[i] == "!=" || poliz[i] == ">" || poliz[i] == "<" || poliz[i] == ">=" || poliz[i] == "<=")
                {
                    double first = 0;
                    double second = 0;

                    first = PopElem();
                    second = PopElem();

                    String sign = poliz[i];
                    String res = CalculateComparison(first, second, sign);
                    stack.Push(res);
                    continue;
                }

                if (poliz[i] == "!")
                {
                    try
                    {
                        bool flag = Boolean.Parse(stack.Pop());
                        stack.Push((!flag).ToString());
                    }
                    catch (Exception e) { }
                }
                
                if (poliz[i] == "\n")
                {
                    continue;
                }
            }
        }

        

        public bool CheckIsConstant(String curElem)
        {
            try
            {
                Double.Parse(curElem);
                return true;
            }
            catch (Exception e) 
            {
                return false;
            }
            
        }

        public void ReadElements(KeyPressEventArgs d)
        {
            bool flag = true;
            if (stack.Count != 0)
            {
                String curElem = stack.Pop();
                while (flag)
                {
                    try
                    {
                        (Application.OpenForms[0] as Form1).richTextConsole.Text += "Please input value of " + curElem + " = ";
                        //char ch = e1.
                        if (d.KeyChar == 13)
                        {
                            int a = 5;
                        }
                    }
                    catch(Exception e)
                    {
                        flag = true;
                    }
                }
 
            }
        }

        public Stack<String> WriteElements(Stack<String> elements)
        {
            if (stack.Count != 0)
            {
                String curId = stack.Pop();
                double idValue = tableOfId.GetIdValue(curId);
                curId += " = ";
                curId += idValue;
                elements.Push(curId);
                WriteElements(elements);
            }
            return elements;         
        }

        public double PopElem()
        {
            double first = 0;

            if (tableOfId.CheckIdentityId(stack.Peek()) != 0) //если ид
            {
                first = tableOfId.GetIdValue(stack.Pop());
            }
            else //константа
            {
                first = Double.Parse(stack.Pop());
            }

            return first;
        }
       
        public String CalculateExpression(double first, double second, String sign)
        {
            double res = 0;
            switch (sign)
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

        private String CalculateComparison(double first, double second, String sign) //вычисляем знаки сравнения
        {
            bool res = false;
            switch (sign)
            {
                case "==":
                    res = second == first;
                    break;
                case "!=":
                    res = second != first;
                    break;
                case ">":
                    res = second > first;
                    break;
                case "<":
                    res = second < first;
                    break;
                case ">=":
                    res = second >= first;
                    break;
                case "<=":
                    res = second <= first;
                    break;
            }
            return res.ToString();
        }

        private void DeleteLabelsWithPoints() //удаляем все метки с двоеточиями
        {
            int length = poliz.Count();
            for (int i = 0; i < poliz.Count(); i++)
            {
                String curElem = poliz[i];
                if (curElem.IndexOf("m") != -1 && curElem.IndexOf(":") != -1)
                {
                    poliz.RemoveAt(i);
                    --i;
                }
            }
            length = 0;
        }

        public PerfomancePoliz(Poliz poliz, TableOfId tableOfId, TableOfLabels tableOfLabels, TableOfConstant tableOfConstant)
        {
            this.poliz = poliz.GetPoliz();
            this.tableOfId = tableOfId;
            this.tableOfLabels = tableOfLabels;
            this.tableOfConstant = tableOfConstant;
            //DeleteLabelsWithPoints();
        }
    }
}

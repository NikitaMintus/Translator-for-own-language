using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace lexAnalizator21
{

    public struct StrPrioritetOperator
    {
        public String oper;
        public int prioritet;

        public StrPrioritetOperator(String oper, int prioritet)
        {
            this.oper = oper;
            this.prioritet = prioritet;
        }
    }

    public struct StrOutputBottomUpTable
    {
        public int step;
        public String stack;
        public String relation;
        public String elem;
        public String poliz;

        public StrOutputBottomUpTable (int step, String stack, String relation, String elem, String poliz)
        {
            this.step = step;
            this.stack = stack;
            this.relation = relation;
            this.elem = elem;
            this.poliz = poliz;
        }
    }



    public struct StrBottomUpTable {
        public int step;
        public Stack<String> stack;
        public String relation;
        public List<StrOutputTable> inputChane;

        public StrBottomUpTable(int step, Stack<String> stack, String relation, List<StrOutputTable> inputChane)
        {
            this.step = step;
            this.stack = stack;
            this.relation = relation;
            this.inputChane = inputChane;
        }
    }

    public struct StrFirstPlus {
        public String neterminal;
        public List<String> arrayOfFirstPlus;
        public String relation;
        public String terminal;

        public StrFirstPlus(String neterminal, List<String> arrayOfFirstPlus, String relation, String terminal)
        {
            this.neterminal = neterminal;
            this.arrayOfFirstPlus = arrayOfFirstPlus;
            this.relation = relation;
            this.terminal = terminal;
        }
    }

    public struct StrSecondNeterminal {
        public String terminal;
        public String relation;
        public String neterminal;

        public StrSecondNeterminal(String terminal, String relation, String neterminal)
        {
            this.terminal = terminal;
            this.relation = relation;
            this.neterminal = neterminal;
        }
    }

    public struct StrLastPLus {
        public String neterminal;
        public List<String> arrayOfLastPlus;
        public String relation;
        public String terminal;

        public StrLastPLus(String neterminal, List<String> arrayOfLastPlus, String relation, String terminal)
        {
            this.neterminal = neterminal;
            this.arrayOfLastPlus = arrayOfLastPlus;
            this.relation = relation;
            this.terminal = terminal;
        }
    }

    public struct StrEquals
    {
        public String first;
        public String relation;
        public String second;

        public StrEquals(String first, String relation, String second)
        {
            this.first = first;
            this.relation = relation;
            this.second = second;
        }
    }

    public struct StrFirstNeterminal {
        public String neterminal;
        public String relation;
        public String terminal;

        public StrFirstNeterminal(String neterminal, String relation, String terminal)
        {
            this.neterminal = neterminal;
            this.relation = relation;
            this.terminal = terminal;
        }
    }

    public struct StrChangedGrammar {
        public String leftPart;
        public String rightPart;

        public StrChangedGrammar(String leftPart, String rightPart)
        {
            this.leftPart = leftPart;
            this.rightPart = rightPart;
        }
    }

    public struct StrAutomat {
        public int alpha;
        public int symbol;
        public int beta;
        public int stack;
        public int exit;

        public StrAutomat(int alpha, int symbol, int beta, int stack, int exit) {
            this.alpha = alpha;
            this.symbol = symbol;
            this.beta = beta;
            this.stack = stack;
            this.exit = exit;
        }
    };

    public struct StrTableOfLabels {
        public String label;
        public int numOfLabel;
        public bool usedGoto;
        public bool usedInProgram;

        public StrTableOfLabels(String label, int numOfLabel, bool usedGoto, bool usedInProgram){
            this.label = label;
            this.numOfLabel = numOfLabel;
            this.usedGoto = usedGoto;
            this.usedInProgram = usedInProgram;
        }
    };

    public struct StrTableOfConstant {
        public String cons;
        public int numOfCons;

        public StrTableOfConstant(String cons, int numOfCons) {
            this.cons = cons;
            this.numOfCons = numOfCons;
        }
    };

    public struct StrTableOfRecursiveErrors {
        public int numOfStr;
        public String error;

        public StrTableOfRecursiveErrors(int numOfStr, String error)
        {
            this.numOfStr = numOfStr;
            this.error = error;
        }
    };

    public struct StrTableOfErrors {
        public int numOfStr;
        public String error;

        public StrTableOfErrors(int numOfStr, String error) {
            this.numOfStr = numOfStr;
            this.error = error;
        }
    };

    public struct StrTableOfId {
        public String id;
        public int numOfId;
        public double value;

        public StrTableOfId(String id, int numOfId, double value){
            this.id = id;
            this.numOfId = numOfId;
            this.value = value;
        }
    };

    public struct StrTableOfLexems
    {
        public String lexem;
        public int numOfLexem;

        public StrTableOfLexems(String lexem, int numOfLexem)
        {
            this.lexem = lexem;
            this.numOfLexem = numOfLexem;
        }
    };

    public struct StrOutputTable 
    {
        public int numOfStr;
        public String substring;
        public int numOfLexem;
        public int numOfClass;

        public StrOutputTable(int numOfStr, String substring, int numOfLexem, int numOfClass) {
            this.numOfStr = numOfStr;
            this.substring = substring;
            this.numOfLexem = numOfLexem;
            this.numOfClass = numOfClass;
        }
    };

    public partial class Form1 : Form
    {
        private TableOfId tableOfId;
        private OutputTable outputTable;
        private TableOfErrors tableOfErrors;
        private TableOfConstant tableOfConstant;
        private TableOfLabels tableOfLabels;
        private RecursiveDescent recursiveDescent;
        private TableOfRecursiveErrors tableOfRecursiveErrors;
        private TableOfLexems tableOfLexems = new TableOfLexems();
        private ProgammAutomat programmAutomat;
        private TableOfPrecedenceRelation tableOfPrRelation;
        private Poliz poliz1;
        

        InputProgram inputProgram;

        public Form1()
        {
            InitializeComponent();
            FormInput formInput = new FormInput();
            inputProgram = new InputProgram();
            tableOfId = new TableOfId();
            outputTable = new OutputTable();
            tableOfErrors = new TableOfErrors();
            tableOfConstant = new TableOfConstant();
            tableOfLabels = new TableOfLabels();
            recursiveDescent = new RecursiveDescent(outputTable);
            tableOfRecursiveErrors = new TableOfRecursiveErrors();
            tableOfPrRelation = new TableOfPrecedenceRelation();

            //удаляем ненужные для курсача вкладки
            tabControlTables.TabPages.Remove(tabPage1);
            tabControlTables.TabPages.Remove(tabPagePrecedenceRelation);

            inputProgram.ReadProgramFromFile();
            inputProgram.SetProgram(inputProgram.ReadProgramFromFile());
            fillProgramm(inputProgram.GetProgram());

            tableOfLexems.SetLexems(tableOfLexems.GetLexemsFromFile());
            fillTableOfLexems(tableOfLexems.GetLexems());

            Automat automat = new Automat(inputProgram, tableOfLexems, outputTable, tableOfId, tableOfErrors, tableOfConstant, tableOfLabels);
            //automat.StatesOfAutomat("program p1 p3\n");
            automat.StartAnalizator();
            fillOutputTable();
            fillTableOfId();
            fillTableOfErrors();
            fillTableOfConstant();
            fillTableOfLabels();
            //automat.State1('(');
        }

        public void fillTableOfLexems(StrTableOfLexems [] tableOfLexems)
        {
            foreach (StrTableOfLexems str in tableOfLexems)
            {
                dataGridOfLexems.Rows.Add(str.lexem, str.numOfLexem);
            }
        }

        public void fillProgramm(String [] program) {
            foreach (String str in program) {
                richTextBoxProgram.Text += str + '\n';
            }
        }

        public void fillOutputTable() {
            foreach(StrOutputTable curRec in outputTable.GetOutputTable()){
                dataGridOfOutputTable.Rows.Add(curRec.numOfStr, curRec.substring, curRec.numOfLexem, curRec.numOfClass);
            }
        }

        public void fillTableOfId() {
            dataGridOfId.Rows.Clear();
            foreach (StrTableOfId curRec in tableOfId.GetTableOfId()) {
                dataGridOfId.Rows.Add(curRec.id, curRec.value, curRec.numOfId);
            }
        }

        public void fillTableOfErrors() {
            foreach (StrTableOfErrors curErr in tableOfErrors.GetTableOfErrors()) {
                dataGridOfLexErrors.Rows.Add(curErr.numOfStr, curErr.error);
            }
        }

        public void fillTableOfRecursiveErrors() {
            foreach (StrTableOfRecursiveErrors curErr in tableOfRecursiveErrors.GetTableOfRecursiveErrors()) {
                dataGridOfRecursDescentErrors.Rows.Add(curErr.numOfStr, curErr.error);
            }
        }

        public void fillTableOfConstant() {
            foreach (StrTableOfConstant curCons in tableOfConstant.GetTableOfConstant()) {
                dataGridOfConstants.Rows.Add(curCons.cons, curCons.numOfCons);
            }
        }

        public void fillTableOfLabels() {
            foreach (StrTableOfLabels curLabel in tableOfLabels.GetTableOfLabels()) {
                dataGridOfLabels.Rows.Add(curLabel.label, curLabel.numOfLabel, curLabel.usedGoto, curLabel.usedInProgram);
            }
        }

        public void fillTableOfPrRelation()
        {
            int size = tableOfPrRelation.GetTableOfPrRelation()[0].Count();
            dataGridOfPrRelation.ColumnCount = size;
           
            //dataGridOfPrRelation.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridOfPrRelation.ColumnHeadersHeight = 10;
            for (int i = 0; i < size; i++)
            {
                dataGridOfPrRelation.Rows.Add();
            }
            //dataGridOfPrRelation.Rows[1].HeaderCell.Value = "fdfd";
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    dataGridOfPrRelation.Rows[i].Cells[j].Value = tableOfPrRelation.GetTableOfPrRelation()[i][j];
                }
            }

        }

        public void ClearTableOfId()
        {
            dataGridOfId.Rows.Clear();
        }

        public void clearAllTables() {
            dataGridOfOutputTable.Rows.Clear();
            dataGridOfLabels.Rows.Clear();
            dataGridOfLexErrors.Rows.Clear();
            dataGridOfConstants.Rows.Clear();
           //dataGridOfLexems.Rows.Clear();
            dataGridOfId.Rows.Clear();
            dataGridOfRecursDescentErrors.Rows.Clear();
        }
        private void labelId_Click(object sender, EventArgs e)
        {

        }

        private void labelErrorsOfLexAnaliz_Click(object sender, EventArgs e)
        {
            
        }

        public void StartSyntaxAnalizator()
        {
            dataGridOfRecursDescentErrors.Rows.Clear();
            tableOfRecursiveErrors.GetTableOfRecursiveErrors().Clear();
            if (!richTextBoxProgram.Modified)
            {
                if (tableOfErrors.GetTableOfErrors().Count == 0) // если нет лексических ошибок
                {
                    recursiveDescent = new RecursiveDescent(outputTable);
                    recursiveDescent.AksiomaProgram(outputTable.GetOutputTable(), tableOfRecursiveErrors);
                    fillTableOfRecursiveErrors();
                }
                else
                {
                    MessageBox.Show("Please, the first correct lexical mistakes!!!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please, the first do lexical analiz!!!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void butStartRecursDescent_Click(object sender, EventArgs e)
        {
            StartSyntaxAnalizator();
        }

        public void StartLexAnalizator()
        {
            clearAllTables();

            //tableOfLexems = new TableOfLexems();
            inputProgram = new InputProgram();
            tableOfId = new TableOfId();
            outputTable = new OutputTable();
            tableOfErrors = new TableOfErrors();
            tableOfConstant = new TableOfConstant();
            tableOfLabels = new TableOfLabels();

            tableOfRecursiveErrors = new TableOfRecursiveErrors();
            TableOfLabels.ClearNumOfLabel();
            TableOfConstant.ClearNumOfCons();
            TableOfId.ClearNumOfId();

            String[] strProgram = richTextBoxProgram.Lines.Select(s => s.Trim()).ToArray();
            // newDeck = newDeck.Where(w => w != newDeck.Last()).ToArray();
            strProgram = strProgram.Where(w => w != "").ToArray(); //удаляем пустые строки
            inputProgram.SetProgram(strProgram);
            Automat automat = new Automat(inputProgram, tableOfLexems, outputTable, tableOfId, tableOfErrors, tableOfConstant, tableOfLabels);
            automat.StartAnalizator();
            fillOutputTable();
            fillTableOfId();
            fillTableOfErrors();
            fillTableOfConstant();
            fillTableOfLabels();

            richTextBoxProgram.Modified = false;

            if (tableOfErrors.GetTableOfErrors().Count != 0)
            {
                MessageBox.Show("Lexical error!!!", "Lexical analizator", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MessageBox.Show("Succesful lexical analiz!!!", "Lexical analizator", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void butStartLexAnalizator_Click(object sender, EventArgs e)
        {
            StartLexAnalizator();
        }

        private void butStartPrRelation_Click(object sender, EventArgs e)
        {
           
            Equals equals = new Equals();
            equals.SearchEquals();
            //equals.GetEquals();
            tableOfPrRelation.FillAllStartedEquals(equals.GetEquals());
            FirstNeterminal firstNeterminal = new FirstNeterminal();
            firstNeterminal.SearchEqual();
            firstNeterminal.GetFirstNetermEqual();
            tableOfPrRelation.FillEquals(firstNeterminal.GetFirstNetermEqual());
            LastPlus lastPlus = new LastPlus(firstNeterminal.GetFirstNetermEqual());
            lastPlus.CalculateLastPlus();
            tableOfPrRelation.FillLastPlusLarger(lastPlus.GetLastPlus());
            SecondNeterminal secondNeterminal = new SecondNeterminal();
            secondNeterminal.SearchEqual();
            FirstPlus firstPlus = new FirstPlus(secondNeterminal.GetSecondNeterminalEqual());
            firstPlus.CalculateFirstPlus();
            secondNeterminal.GetSecondNeterminalEqual();
            tableOfPrRelation.FillEqualsSecondNeterm(secondNeterminal.GetSecondNeterminalEqual());
            tableOfPrRelation.FillFirstPlusLess(firstPlus.GetFirstPlus());
            FirstNeterminalEqualNeterminal firstNetermEqualNeterm = new FirstNeterminalEqualNeterminal(firstNeterminal.GetFirstNetermEqualNeterm());
            firstNetermEqualNeterm.CalculateLastPlus();
            firstNetermEqualNeterm.GetLastPlusArr();
            firstNetermEqualNeterm.GetFirstPlusArr();
            tableOfPrRelation.FillFirstNetermEqualNeterm(firstNetermEqualNeterm.GetLastPlusArr(), firstNetermEqualNeterm.GetFirstPlusArr());
            SecondNetermEqualNeterm secondNetermEqualNeterm = new SecondNetermEqualNeterm(secondNeterminal.GetSecondNeterminalEqualNeterm());
            secondNetermEqualNeterm.CalculateFirstPlus();
            secondNetermEqualNeterm.GetArrFirst();
            tableOfPrRelation.FillFirstPlusLess(secondNetermEqualNeterm.GetArrFirst());
            tableOfPrRelation.FillGridLess();
            tableOfPrRelation.FillGridLarger();
            fillTableOfPrRelation();
            butBottomUp.Enabled = true;
        }

        
        private void butBottomUp_Click(object sender, EventArgs e) // восходящий разбор
        {
            BottomUpAnalysis bottomUpAnalysis = new BottomUpAnalysis(tableOfPrRelation, outputTable, tableOfId);
            bottomUpAnalysis.AnalyzeTable();
           // ClearTableOfId();
            fillTableOfId();
            fillBottomUpTable(bottomUpAnalysis.GetAllNotes());
            //this.labelResBottom
        }

        private void fillBottomUpTable(List<StrOutputBottomUpTable> allNotes)
        {
            dataGridViewBottomUp.Rows.Clear();
            foreach (StrOutputBottomUpTable curStr in allNotes)
            {
                dataGridViewBottomUp.Rows.Add(curStr.step, curStr.stack, curStr.relation, curStr.elem, curStr.poliz);
            }
        }

        private void dataGridViewBottomUp_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void StartPoliz()
        {
            poliz1 = new Poliz(outputTable, tableOfLabels, tableOfId);
            poliz1.DoPoliz();
            richTextBoxPoliz.Clear();
            foreach (String curStr in poliz1.GetPoliz())
            {
                richTextBoxPoliz.Text += " " + curStr;
            }
            dataGridOfLabels.Rows.Clear();
            fillTableOfLabels();
            butStartPerfomance.Enabled = true;
        }

        private void butStartPoliz_Click(object sender, EventArgs e)
        {
            StartPoliz();
        }

        private void butStartPerfomance_Click(object sender, EventArgs e)
        {
            richTextConsole.Clear();
            StartLexAnalizator();
            StartSyntaxAnalizator();
            StartPoliz();
            PerfomancePoliz perfomancePoliz = new PerfomancePoliz(poliz1, tableOfId, tableOfLabels, tableOfConstant);
            perfomancePoliz.DoPerfomance();
            
        }

        private void richTextConsole_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (!Char.IsDigit(ch) && ch != 8 && ch != 101 && ch != 69 && ch != 46) // 101 = e 69 = E 46 =.
            {
                e.Handled = true;
            }
        }

        private void butNewForm_Click(object sender, EventArgs e)
        {
            FormInput form = new FormInput();
            Button but = new Button();
            but.Location = new Point(60, 100);
            but.Text = "Привет";
            but.Click += delegate
            {
                MessageBox.Show("Вы нажали на кнопку!!!");
            };
            form.Controls.Add(but);
            form.Show();

        }

        private void toolStripMenuUser_Click(object sender, EventArgs e)
        {
            tabControlTables.Visible = false;
            this.Size = new Size(380, 712);
            butStartLexAnalizator.Visible = false;
            //this.Location = new Point
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, 10);
        }

        private void toolStripMenuProgrammer_Click(object sender, EventArgs e)
        {
            tabControlTables.Visible = true;
            this.Size = new Size(1112, 712);
            this.Location = new Point((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2, 10);
            butStartLexAnalizator.Visible = true;
            
        }
    }
}

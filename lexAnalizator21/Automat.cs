using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    class Automat
    {
        private InputProgram inputProgram;
        private TableOfLexems tableOfLexems;
        private OutputTable outputTable;
        private TableOfId tableOfId;
        private TableOfErrors tableOfErrors;
        private TableOfConstant tableOfConstant;
        private TableOfLabels tableOfLabels;

        public Automat(InputProgram inputProgram, TableOfLexems tableOfLexems, OutputTable outputTable, TableOfId tableOfId, TableOfErrors tableOfErrors, TableOfConstant tableOfConstant, TableOfLabels tableOfLabels) {
            this.inputProgram = inputProgram;
            this.tableOfLexems = tableOfLexems;
            this.outputTable = outputTable;
            this.tableOfId = tableOfId;
            this.tableOfErrors = tableOfErrors;
            this.tableOfConstant = tableOfConstant;
            this.tableOfLabels = tableOfLabels;
        }

        /*public void CheckIdOrWord(List<String> id, int numOfStr) { //передаются возможные из одной строки 
            if (id.ElementAt(0) == "programm") { }
            StrOutputTable recordOut;
            StrTableOfId recordId;
            bool isId = true;
            foreach (String curId in id) {
                foreach (StrTableOfLexems curLexem in tableOfLexems.GetLexems())
                {
                    if (curId == curLexem.lexem) {
                        recordOut = new StrOutputTable (numOfStr, curLexem.lexem, curLexem.numOfLexem, 0);
                        outputTable.AddRecord(recordOut);
                        isId = false;
                    }
                }
                if (isId)
                {
                    recordId = new StrTableOfId(curId, TableOfId.GenerateNumOfId()); //заносим в таблицу идентификаторов
                    tableOfId.AddRecord(recordId);
                    recordOut = new StrOutputTable(numOfStr, recordId.id, TableOfId.GetNumOfLexemId(), recordId.numOfId); //заносим в таблицу лексем
                    outputTable.AddRecord(recordOut);
                }
                isId = true;
            }
        }*/
        /*public void StartAnalizator() {
            List<String> lex = new List<string>();
            List<String> id = new List<string>();
            List<String> cons = new List<string>();
            for (int i = 0; i < inputProgram.GetProgram().Length; i++) {
                StatesOfAutomat(inputProgram.GetProgram()[i] + '\n', lex, id, cons);
                if (id.Count != 0) {
                    foreach (String record in id)
                    {
                        int numOfLexem = tableOfLexems.SearchLexem(record);
                        if (numOfLexem == 0) //это не лексема языка
                        {
                            int numOfId = tableOfId.CheckIdentityId(record);
                            if (numOfId == 0) //если такого идентификатора нет
                            {
                                StrTableOfId newId = new StrTableOfId(record, TableOfId.GenerateNumOfId());
                                tableOfId.AddRecord(newId);
                                StrOutputTable newRec = new StrOutputTable(i + 1, newId.id, TableOfId.GetNumOfLexemId(), newId.numOfId);
                                outputTable.AddRecord(newRec);
                            }
                            else
                            {
                                StrOutputTable newRec = new StrOutputTable(i + 1, record, TableOfId.GetNumOfLexemId(), numOfId);
                                outputTable.AddRecord(newRec);
                                //StrTableOfErrors newError = new StrTableOfErrors(i + 1, "Identificator " + record + " has already been created!!!");
                                //tableOfErrors.AddError(newError);
                                //ошибка такой идентификатор уже существует
                            }
                        }
                        else { // это лексема языка
                            StrOutputTable newRec = new StrOutputTable(i + 1, record, numOfLexem, 0);
                            outputTable.AddRecord(newRec);
                        }
                    }

                    if (lex.Count != 0)
                    {
                        foreach (String record in lex)
                        {
                            int numOfLexem = tableOfLexems.SearchLexem(record);
                            //int num = tableOfLexems.SearchLexem("\n");
                            if (numOfLexem != 0)
                            {
                                StrOutputTable newRec = new StrOutputTable(i + 1, record, numOfLexem, 0);
                                outputTable.AddRecord(newRec);
                            }
                            else {
                                StrTableOfErrors newError = new StrTableOfErrors(i + 1, "Undetified symbol '" + record + "' !!!!");
                                tableOfErrors.AddError(newError);
                            }
                        }
                    }
                }
                lex.Clear();
                id.Clear();
                cons.Clear();
            }
        }*/

        public void StartAnalizator() {
            List<String> lex = new List<string>();
            List<String> id = new List<string>();
            List<String> cons = new List<string>();
            for (int i = 0; i < inputProgram.GetProgram().Length; i++) {
                int res = StatesOfAutomat(inputProgram.GetProgram()[i] + '\n', i+1, lex, id, cons);
                if (res == -1) {
                    return;
                }
            }
        }

        public int StatesOfAutomat(String str, int numOfStr, List<String> lex, List<String> id, List<String> cons)
        {
            String caseSwitch = "1";
            int startIndex = 0;
           
            for (int i = 0; i < str.Length; i++)
            {
                switch (caseSwitch)
                {
                    case "1":
                        caseSwitch = State1(str[i]);
                        if (caseSwitch == "g") {

                            if (str[i] != ' ') // пробел не заносим в выходную таблицу
                            {
                                String newLex = tableOfLexems.ChangeEnter(str[i].ToString());
                                int numOfLexem = tableOfLexems.SearchLexem(newLex);
                                StrOutputTable newRec = new StrOutputTable(numOfStr, newLex, numOfLexem, 0);
                                outputTable.AddRecord(newRec);
                                //startIndex = i;
                            }
                            startIndex = i;
                            caseSwitch = "1";
                            // break;
                        }
                        if (caseSwitch == "err") {
                            StrTableOfErrors newErr = new StrTableOfErrors(numOfStr, "Undefined symbol '" + str[i] + "' !!!!!!!!");
                            tableOfErrors.AddError(newErr);
                            return -1;
                            //caseSwitch = "1";
                        }
                        
                        break;
                    case "2":
                        caseSwitch = State2(str[i]);
                        if (caseSwitch == "id") {
                            if (startIndex == 0)  // для пропуска разделителя
                            {
                                String curId = str.Substring(startIndex, i - startIndex);

                                int num = tableOfId.CheckIdentityIdInDeclaration(curId, tableOfId, outputTable); //проверка на одинаковые id в объявлении
                                if (num != 0) {
                                    StrTableOfErrors newErr = new StrTableOfErrors(numOfStr, "Identificator '" + curId + "' has been already declarated with number " + num);
                                    tableOfErrors.AddError(newErr);
                                    return -1;
                                }

                                if (!AddIdOrLexem(curId, numOfStr, tableOfId, outputTable)) { // если идентификатор необъявлен
                                    if (tableOfLabels.CheckIsLabelAfterGoto(curId, outputTable, numOfStr) == 0) // если это не метка после goto (если да - то меняем флажок UsedGoto на true)
                                    {
                                        if (tableOfLabels.CheckIsLabelUsedInProgram(curId, str[i], numOfStr, outputTable) == 0) //если это переход на метку
                                        { 
                                            StrTableOfErrors newErr = new StrTableOfErrors(numOfStr, "Undefined identificator '" + curId + "' !!!!!!!!");
                                            tableOfErrors.AddError(newErr);
                                            return -1;
                                        }
                                    }
                                } //проверяет id или лексема и добавляет в выходную таблицу
                            }
                            else {
                                String curId = str.Substring(++startIndex, i - startIndex);  // тут ошибка со скобкой

                                int num = tableOfId.CheckIdentityIdInDeclaration(curId, tableOfId, outputTable); //проверка на одинаковые id в объявлении
                                if (num != 0)
                                {
                                    StrTableOfErrors newErr = new StrTableOfErrors(numOfStr, "Identificator '" + curId + "' has been already declarated\n with number " + num);
                                    tableOfErrors.AddError(newErr);
                                    return -1;
                                }

                                if (!AddIdOrLexem(curId, numOfStr, tableOfId, outputTable))
                                { // если идентификатор необъявлен
                                    if (tableOfLabels.CheckIsLabelAfterGoto(curId, outputTable, numOfStr) == 0) // если это не метка после goto (если да - то меняем флажок UsedGoto на true)
                                    {
                                        if (tableOfLabels.CheckIsLabelUsedInProgram(curId, str[i], numOfStr, outputTable) == 0) //если это переход на метку
                                        {
                                            StrTableOfErrors newErr = new StrTableOfErrors(numOfStr, "Undefined identificator '" + curId + "' !!!!!!!!");
                                            tableOfErrors.AddError(newErr);
                                            return -1;
                                        }
                                    }
                                } //проверяет id или лексема и добавляет в выходную таблицу
                            }
                            startIndex = i;
                            caseSwitch = "1";
                            --i; //возвращаемся к разделителю
                        }
                        break;
                    case "3":
                        caseSwitch = State3(str[i]);
                        if (caseSwitch == "cons") {
                            String curCons = str.Substring(startIndex + 1, i - startIndex -1);
                            tableOfConstant.AddConstant(curCons, numOfStr, tableOfConstant, outputTable);
                            startIndex = i;
                            --i;
                            caseSwitch = "1";
                        }
                        break;
                    case "4":
                        caseSwitch = State4(str[i]);
                        if (caseSwitch == "err") {
                            StrTableOfErrors newErr = new StrTableOfErrors(numOfStr, "Error!!! Waitin digit after point.");
                            tableOfErrors.AddError(newErr);
                            return -1;
                        }
                        break;
                    case "5":
                        caseSwitch = State5(str[i]);
                        if (caseSwitch == "cons")
                        {
                            String curCons = str.Substring(startIndex + 1, i - startIndex - 1);
                            tableOfConstant.AddConstant(curCons, numOfStr, tableOfConstant, outputTable);
                            startIndex = i;
                            --i;
                            caseSwitch = "1";
                        }
                        break;
                    case "6":
                        caseSwitch = State6(str[i]);
                        break;
                    case "7":
                        caseSwitch = State7(str[i]);
                        if (caseSwitch == "err") {
                            StrTableOfErrors newErr = new StrTableOfErrors(numOfStr, "Error!!! Waitin digit after '+'");
                            tableOfErrors.AddError(newErr);
                            return -1;
                        }
                        break;
                    case "8":
                        caseSwitch = State8(str[i]);
                        if (caseSwitch == "cons")
                        {
                            String curCons = str.Substring(startIndex + 1, i - startIndex - 1);
                            tableOfConstant.AddConstant(curCons, numOfStr, tableOfConstant, outputTable);
                            startIndex = i;
                            --i;
                            caseSwitch = "1";
                        }
                        break;
                    case "9":
                        caseSwitch = State9(str[i]);
                        outputTable.AddSimpleLexem(caseSwitch, numOfStr, tableOfLexems);
                        if (caseSwitch == "=")
                        {
                            caseSwitch = "1";
                            --i;
                        }
                        else
                        {
                            caseSwitch = "1";
                            ++startIndex;
                        }
                        
                        break;
                    case "10":
                        caseSwitch = State10(str[i]);
                        outputTable.AddSimpleLexem(caseSwitch, numOfStr, tableOfLexems);
                        if (caseSwitch == "<")
                        {
                            caseSwitch = "1";
                            --i;
                        }
                        else
                        {
                            caseSwitch = "1";
                            ++startIndex;
                        }
                        break;
                    case "11":
                        caseSwitch = State11(str[i]);
                        outputTable.AddSimpleLexem(caseSwitch, numOfStr, tableOfLexems);
                        if (caseSwitch == ">")
                        {
                            caseSwitch = "1";
                            --i;
                        }
                        else
                        {
                            caseSwitch = "1";
                            ++startIndex;
                        }
                        break;
                    case "12":
                        caseSwitch = State12(str[i]);
                        outputTable.AddSimpleLexem(caseSwitch, numOfStr, tableOfLexems);
                        if (caseSwitch == "!")
                        {
                            caseSwitch = "1";
                            --i;
                        }
                        else
                        {
                            caseSwitch = "1";
                            ++startIndex;
                        }
                        break;

                }
            }

           // CheckIdOrWord(id, 1);
            return 1;
        }

        public String State1(Char symbol){ // i номер проверяемого символа
            String ret = "err";
            int caseSwitch = 1;
            while (true)
            {
                switch (caseSwitch)
                {
                    case 1:
                        {
                            if (Char.IsLetter(symbol))
                            {
                                return "2";
                            }
                            else
                            {
                                caseSwitch = 2;
                            }
                            break;
                        }

                    case 2:
                        {
                            if (Char.IsDigit(symbol))
                            {
                                return "3";
                            }
                            else
                            {
                                caseSwitch = 3;
                            }
                            break;
                        }
                    case 3: 
                        {
                            if (symbol == '.')
                            {
                                return "4";
                            }
                            else
                            {
                                caseSwitch = 4;
                            }
                            break;
                        }
                    case 4:
                        {
                            if (IsSeparatorOper(symbol))
                            {
                                return "g";
                            }
                            else {
                                caseSwitch = 5;
                            }
                            break;
                        }
                    case 5:
                        {
                            if (symbol == ':')
                            {
                                return "g";
                            }
                            else {
                                caseSwitch = 6;
                            }
                            break;
                        }
                    case 6:
                        {
                            if (symbol == '=')
                            {
                                return "9";
                            }
                            else {
                                caseSwitch = 7;
                            }
                            break;
                        }
                    case 7:
                        {
                            if (symbol == '<')
                            {
                                return "10";
                            }
                            else {
                                caseSwitch = 8;
                            }
                            break;
                        }
                    case 8:
                        {
                            if (symbol == '>')
                            {
                                return "11";
                            }
                            else {
                                caseSwitch = 9;
                            }
                            break;
                        }
                    case 9:
                        {
                            if (symbol == '!')
                            {
                                return "12";
                            }
                            else {
                                return ret;
                            }
                        }

                }
            }
        }

        private String State2(char symbol) {
            if (Char.IsLetterOrDigit(symbol))
            {
                return "2";
            }
            else
            {
                return "id";
            }    
        }

        private String State3(char symbol) {
            int caseSwitch = 1;
            while(true)
            {
                switch (caseSwitch) 
                {
                    case 1:
                        {
                            if(Char.IsDigit(symbol)){
                                return "3";
                            }
                            else {
                                caseSwitch = 2;
                            }
                            break;
                        }
                    case 2:
                        {
                            if (symbol == '.'){
                                return "5";
                            }
                            else {
                                caseSwitch = 3;
                            }
                            break;
                        }
                    case 3:
                        {
                            if (symbol == 'E' || symbol == 'e'){
                                return "6";
                            }
                            else {
                                return "cons";
                            }
                        }
                }
            }
            //return "const";
        }

        private String State4(char symbol) {
            if (Char.IsDigit(symbol)) {
                return "5";
            }
            return "err";
        }

        private String State5(char symbol) {
            if (Char.IsDigit(symbol)) {
                return "5";
            }
            if (symbol == 'E' || symbol == 'e') {
                return "6";
            }
            return "cons";
        }

        private String State6(char symbol) {
            if (symbol == '+') {
                return "7";
            }
            if (Char.IsDigit(symbol)) {
                return "8";
            }
            return "err"; // тут под вопросом
        }

        private String State7(char symbol) {
            if (Char.IsDigit(symbol)) {
                return "8";
            }
            return "err";
        }

        private String State8(char symbol) {
            if (Char.IsDigit(symbol)) {
                return "8";
            }
            return "cons";
        }

        private String State9(char symbol) {
            if(symbol == '='){
                return "==";
            }
            return "="; 
        }

        private String State10(char symbol) {
            if (symbol == '=') {
                return "<=";
            }
            return "<";
        }

        private String State11(char symbol) {
            if (symbol == '=') {
                return ">=";
            }
            return ">";
        }

        private String State12(char symbol){
            if(symbol == '='){
                return "!=";
            }
            return "!";
        }

        private bool IsSeparatorOper(char symbol) { // проверка принадлежит ли классу ОРО
            char [] separatorsOper = {'\n', '+', ',',  '-', '*', '/', '(', ')', '[', ']', ' '};
            for (int i = 0; i < separatorsOper.Length; i++)
            {
                if (symbol == separatorsOper[i])
                {
                    return true;
                }
            }
            return false;
        }

        private bool AddIdOrLexem(String curId, int numOfStr, TableOfId tableOfId, OutputTable outputTable) {
            StrOutputTable newOutRec;
            int numOfLexem = tableOfLexems.SearchLexem(curId);
            if (numOfLexem == 0) //это идентификатор или метка
            {  
                if(tableOfLabels.CheckIsLabelDef(outputTable)) //это раздел меток 
                {
                    int numOfLabel = tableOfLabels.CheckIndentityLabel(curId);
                    if (numOfLabel == 0) // если такой метки нет
                    {
                        StrTableOfLabels newLabel = new StrTableOfLabels(curId, TableOfLabels.GenerateNumOfLabel(), false, false);
                        tableOfLabels.AddRecord(newLabel);
                        newOutRec = new StrOutputTable(numOfStr, curId, TableOfLabels.GetNumOfLexemLabel(), newLabel.numOfLabel);
                        outputTable.AddRecord(newOutRec);
                    }
                    else { // если есть то добавляем только в выходную таблицу
                        newOutRec = new StrOutputTable(numOfStr, curId, TableOfLabels.GetNumOfLexemLabel(), numOfLabel);
                        outputTable.AddRecord(newOutRec);
                    }
                    return true;
                }

                if (tableOfId.CheckIsDeclarationId(curId, tableOfId, outputTable)) //идентификатор
                {
                    int numOfId = tableOfId.CheckIdentityId(curId);
                    if (numOfId == 0) // если такого еще нет
                    {
                        StrTableOfId newIdRec = new StrTableOfId(curId, TableOfId.GenerateNumOfId(), 0.0);
                        tableOfId.AddRecord(newIdRec);
                        newOutRec = new StrOutputTable(numOfStr, curId, TableOfId.GetNumOfLexemId(), newIdRec.numOfId);
                        outputTable.AddRecord(newOutRec);
                    }
                    else
                    {
                        newOutRec = new StrOutputTable(numOfStr, curId, TableOfId.GetNumOfLexemId(), numOfId);
                        outputTable.AddRecord(newOutRec);
                    }
                }
                else {
                    return false;
                }
            }
            else //лексема языка
            {
                newOutRec = new StrOutputTable(numOfStr, curId, numOfLexem, 0);
                outputTable.AddRecord(newOutRec);
            }
            return true;
        }
    }
}

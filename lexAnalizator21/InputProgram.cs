using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace lexAnalizator21
{
    class InputProgram 
    {
        private String [] program;

        public void SetProgram(String [] program){
            this.program = program;
        }

        public void ClearProgram() {
            
        }

        public String[] GetProgram() {
            return this.program;
        }

        public String [] ReadProgramFromFile() {
            String[] programStr = File.ReadAllLines("program.txt").Select(s => s.Trim()).ToArray();
            return programStr;
        }
    }
}

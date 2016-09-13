using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    class TableOfErrors
    {
        List<StrTableOfErrors> tableOfErrors;

        public List<StrTableOfErrors> GetTableOfErrors() {
            return this.tableOfErrors;
        }

        public void AddError(StrTableOfErrors newError) {
            tableOfErrors.Add(newError);
        }

        public TableOfErrors() {
            tableOfErrors = new List<StrTableOfErrors>();
        }
    }
}

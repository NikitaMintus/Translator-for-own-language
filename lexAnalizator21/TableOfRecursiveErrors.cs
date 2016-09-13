using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    class TableOfRecursiveErrors
    {
        private List<StrTableOfRecursiveErrors> tableOfRecursiveErrors;

        public List<StrTableOfRecursiveErrors> GetTableOfRecursiveErrors()
        {
            return this.tableOfRecursiveErrors;
        }

        public void AddError(StrTableOfRecursiveErrors newError)
        {
            tableOfRecursiveErrors.Add(newError);
        }

        public TableOfRecursiveErrors()
        {
            tableOfRecursiveErrors = new List<StrTableOfRecursiveErrors>();
        }
    }
}

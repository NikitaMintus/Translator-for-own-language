using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    static class StackMagazine
    {
        private static Stack<int> stackMagazine = new Stack<int>();

        public static void Push(int state)
        {
            stackMagazine.Push(state);
        }

        public static int Pop()
        {
            return stackMagazine.Pop();
        }

        public static int Count()
        {
            return stackMagazine.Count();
        }

        public static void ClearStack()
        {
            stackMagazine.Clear();
        }
    }
}

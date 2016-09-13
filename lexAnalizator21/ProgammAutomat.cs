using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace lexAnalizator21
{
    class ProgammAutomat
    {
        //private List<StrAutomat> automat;
        private Dictionary<int, StrAutomat> automat;
        public ProgammAutomat()
        {
            automat = new Dictionary<int, StrAutomat>()
            {
              {1, new StrAutomat(1, 1, 2, 0, -1)},
              {2, new StrAutomat(2, 35, 3, 0, -1)},
              {3, new StrAutomat(3, 33, 4, 0, -1)},
              {4, new StrAutomat(4, 2, 5, 0, -1)},
              {5, new StrAutomat(5, 33, 6, 0, -1)},
              {6, new StrAutomat(6, 35, 7, 0, -1)},
              {7, new StrAutomat(7, 34, 8, 0, 71)},
              {71, new StrAutomat(71, 32, 6, 0, -1)},
              {8, new StrAutomat(8, 7, 9, 0, -1)},
              {9, new StrAutomat(9, 33, 10, 0, -1)},
              {10, new StrAutomat(10, 3, 11, 0, 6)},
              {11, new StrAutomat(11, 33, 12, 0, -1)},
              {12, new StrAutomat(12, 3, 13, 0, -1)},
              {13, new StrAutomat(13, 33, 14, 0, 131)},
              {131, new StrAutomat(131, 32, 12, 0, -1)},
              {14, new StrAutomat(14, 4, 15, 0, -1)},
              {15, new StrAutomat(15, 33, 21, 16, -1)},
              {16, new StrAutomat(16, 33, 17, 0, -1)},
              {17, new StrAutomat(17, 5, 0, 0, 171)},
              {171, new StrAutomat(171, 0, 21, 16, 0)},
              {21, new StrAutomat(21, 3, 22, 0, 211)},
              {211, new StrAutomat(211, 0, 31, 24, 0)},
              {22, new StrAutomat(22, 34, 23, 0, -1)},
              {23, new StrAutomat(23, 33, 31, 24, -1)},
              {24, new StrAutomat(24, 0, 0, 0, 0)},
              {31, new StrAutomat(31, 35, 32, 0, 311)},
              {311, new StrAutomat(311, 8, 34, 0, 312)},
              {312, new StrAutomat(312, 9, 34, 0, 313)},
              {313, new StrAutomat(313, 10, 37, 0, 314)},
              {314, new StrAutomat(314, 13, 51, 43, 315)},
              {315, new StrAutomat(315, 0, 0, 0, 0)},  // проверить
              {32, new StrAutomat(32, 37, 61, 33, -1)},
              {33, new StrAutomat(33, 0, 0, 0, 0)},
              {34, new StrAutomat(34, 28, 35, 0, -1)},
              {35, new StrAutomat(35, 35, 36, 0, -1)},
              {36, new StrAutomat(36, 29, 0, 0, 361)},
              {361, new StrAutomat(361, 32, 35, 0, -1)},
              {37, new StrAutomat(37, 35, 38, 0, -1)},
              {38, new StrAutomat(38, 37, 61, 39, -1)},
              {39, new StrAutomat(39, 11, 61, 40, -1)},
              {40, new StrAutomat(40, 33, 21, 41, -1)},
              {41, new StrAutomat(41, 33, 42, 0, -1)},
              {42, new StrAutomat(42, 12, 0, 0, 421)},
              {421, new StrAutomat(421, 0, 21, 41, 0)},
              {43, new StrAutomat(43, 14, 44, 0, -1)},
              {44, new StrAutomat(44, 15, 45, 0, -1)},
              {45, new StrAutomat(45, 3, 0, 0, -1)},
              {51, new StrAutomat(51, 23, 51, 0, 511)},
              {511, new StrAutomat(511, 30, 51, 52, 512)},
              {512, new StrAutomat(512, 0, 61, 54, 0)},
              {52, new StrAutomat(52, 31, 53, 0, -1)},
              {53, new StrAutomat(53, 16, 51, 0, 531)},
              {531, new StrAutomat(531, 17, 51, 0, 532)},
              {532, new StrAutomat(532, 0, 0, 0, 0)},
              {54, new StrAutomat(54, 18, 61, 53, 541)},
              {541, new StrAutomat(541, 19, 61, 53, 542)},
              {542, new StrAutomat(542, 20, 61, 53, 543)},
              {543, new StrAutomat(543, 21, 61, 53, 544)},
              {544, new StrAutomat(544, 22, 61, 53, 545)},
              {545, new StrAutomat(545, 38, 61, 53, -1)},
              {61, new StrAutomat(61, 35, 62, 0, 611)}, 
              {611, new StrAutomat(611, 36, 62, 0, 612)},
              {612, new StrAutomat(612, 28, 61, 63, -1)},
              {62, new StrAutomat(62, 24, 61, 0, 621)},
              {621, new StrAutomat(621, 25, 61, 0, 622)},
              {622, new StrAutomat(622, 26, 61, 0, 623)},
              {623, new StrAutomat(623, 27, 61, 0, 624)},
              {624, new StrAutomat(624, 0, 0, 0, 0)},
              {63, new StrAutomat(63, 29, 62, 0, -1)}
            };

            StackMagazine.Push(0); // ложим признак завершения
        }

        public int startAutomat(OutputTable outputTable)
        {
            int curLexem = CurLexem.GetCurLexem();
            int curState = 1;
            while (true)
            {
                if (curState == 0 && StackMagazine.Count() == 0)
                {
                    return 1;
                }

                if (curState == 0 && StackMagazine.Count() != 0)
                {
                    curState = StackMagazine.Pop();
                }

                if (curLexem == automat[curState].symbol) // если равно символу
                {
                    Push(curState);
                    curState = automat[curState].beta;
                    curLexem = CurLexem.TakeNextLexem();
                }
                else
                {
                    if (automat[curState].exit == -1) // ошибка
                    {
                        return -1;
                    }

                    if (automat[curState].exit == 0) //был использован символ несравнения
                    {
                        if (automat[curState].beta != 0) // если не выход
                        {
                            Push(curState);
                            curState = automat[curState].beta;
                        }
                        else // 0 - выход, берем состояние из стека
                        {
                            curState = StackMagazine.Pop();
                            if (curState == 0) // конец программы
                            {
                                return 1;
                            }
                        }
                    }
                    else // переход на другое состояние
                    {
                        curState = automat[curState].exit;
                    }
                }
            }
           
           
        }

        private void Push(int curState) 
        {
            if (automat[curState].stack != 0) // если не записан 0
            {
                StackMagazine.Push(automat[curState].stack);
            }
        }
    }
}

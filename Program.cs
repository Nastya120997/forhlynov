using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace proga1
{
    class Program
    {
        static void Main(string[] args)
        {

            double[,] passive = {   {20049, 1 },
                                    {51405, 3 },
                                    {3318637, 2},
                                    {1892362, 2},
                                    {1170183, 1},
                                    {144913, 2},
                                    {35854, 3},
                                    {1749333, 1},
                                    {9015220, 2},
                                    {5323, 3},
                                    {2900, 3},
                                    {2367473, 4},
                                    {57129, 3},
                                    {3826641, 4},
                                    {8962014, 2},
                                    {267781, 4},
                                    {241056, 4},
                                    {591241, 1},
                                    {378, 1}};

            double[,] active = {    {258294, 1},
                                    {834505, 2},
                                    {3854940, 1},
                                    {3011079, 2},
                                    {4395442, 3},
                                    {1799887, 3},
                                    {672313, 3},
                                    {316172, 4},
                                    {4739200, 2},
                                    {467792, 1},
                                    {997854, 1},
                                    {861835, 5},
                                    {131856, 1},
                                    {1052942, 2},
                                    {119974, 4},
                                    {6630, 3},
                                    {9040343, 2},
                                    {1140747, 1},
                                    {18087, 1}};


            double sum1 = 0;
            for (int i = 0; i < passive.GetLength(0); i++)
            {
                sum1 += passive[i, 0];

            }
            double sum2 = 0;
            for (int i = 0; i < active.GetLength(0); i++)
            {
                sum2 += active[i, 0];

            }
            if ((sum2 - sum1) == 0)
            {
                Console.WriteLine("Суммы по пассивам и активам сходятся");
            }
            else
            {
                Console.WriteLine("Суммы по пассивам и активам НЕ сходятся");
            }



            int[,] x = { {1,0},          // заполняем пересечения
                          {3,0},
                {7,0 },
                {11,0 },
                {3,1 },
                {8,1 },
                {11,1 },
                {2,2 },
                {4,2 },
                {7,2 },
                {11,2 },
                {5,3 },
                {8,3 },
                {11,3 },
                {8,4 },
                {9,4 },
                {11,4 },
                {13,4 },
                {6,5 },
                {9,5 },
                {11,5 },
                {13,5 },
                {6,6 },
                {9,6 },
                {11,6 },
                {13,6 },
                {5,7 },
                {10,7 },
                {11,7 },
                {1,8 },
                {2,8 },
                {3,8 },
                {8,8 },
                {11,8 },
                {1,9 },
                {2,9 },
                {3,9 },
                {0,10 },
                {2,10 },
                {13,11 },
                {15,11 },
                {16,11 },
                {7,12 },
                {7,13 },
                {11,13 },
                {12,13 },
                {15,14 },
                {18,14 },
                {4,15 },
                {5,15 },
                {6,15 },
                {11,15 },
                {13,16 },
                {14,16 },
                {11,17 },
                {15,17 },
                {17,17 },
                {1,18 },
                {2,18 }};

            int rezerv = 7; //число резервных строк
            int nomrez = 11; //номер строки резерва

            int kol_R = passive.GetLength(0) + active.GetLength(0) + rezerv;
            int kol_S = rezerv;

            double M = 100000000;

            double[,] otvet = new double[kol_R, 2];

            double sumrezerv = 0;

            for (int i = 0; i < 9; i++)
            {
                sumrezerv += active[i, 0]; //общая сумма резервов

            }

            double[,] matr = new double[1 + kol_R, x.GetLength(0) + kol_S + kol_R + 1];
            for (int i = 0; i < matr.GetLength(0); i++)
            {
                for (int j = 0; j < matr.GetLength(1); j++) //создали матрицу и заполнили нулями
                {
                    matr[i, j] = 0;
                }

            }

            int k = 0;

            for (int i = 0; i < x.GetLength(0); i++) //заполняем z-строку для x
            {
                matr[0, i] = (passive[x[i, 0], 1] - active[x[i, 1], 1]);

                if (matr[0, i] != 0)
                {
                    matr[0, i] = -matr[0, i];
                }

            }

            for (int i = x.GetLength(0) + kol_S; i < matr.GetLength(1) - 1; i++) //заполняем z-строку для R
            {
                matr[0, i] = -M;
            }


            for (int i = 0; i < x.GetLength(0); i++) //заполняем матрицу единицами

            {
                //заполняем ограничения по активам и пассивам
                matr[x[i, 0] + 1, i] = 1;
                matr[x[i, 1] + 1 + passive.GetLength(0), i] = 1;

                matr[x[i, 0] + 1, x.GetLength(0) + kol_S + x[i, 0]] = 1;
                matr[x[i, 1] + 1 + passive.GetLength(0), x.GetLength(0) + kol_S + passive.GetLength(0) + x[i, 1]] = 1;

                matr[x[i, 0] + 1, matr.GetLength(1) - 1] = passive[x[i, 0], 0];
                matr[x[i, 1] + 1 + passive.GetLength(0), matr.GetLength(1) - 1] = active[x[i, 1], 0];


                if ((x[i, 0] == nomrez) && (x[i, 1] < rezerv)) //заполняем ограничения резервов
                {
                    matr[passive.GetLength(0) + active.GetLength(0) + 1 + k, i] = 1;
                    matr[passive.GetLength(0) + active.GetLength(0) + 1 + k, x.GetLength(0) + k] = -1;
                    matr[passive.GetLength(0) + active.GetLength(0) + 1 + k, x.GetLength(0) + kol_S + passive.GetLength(0) + active.GetLength(0) + k] = 1;
                    matr[passive.GetLength(0) + active.GetLength(0) + 1 + k, matr.GetLength(1) - 1] = active[x[i, 1], 0] / sumrezerv * passive[nomrez, 0];

                    k += 1;

                }

            }

            //убираем М из z-строки
            for (int j = 0; j < matr.GetLength(1); j++)
            {
                for (int i = 1; i < matr.GetLength(0); i++)
                {
                    matr[0, j] += M * matr[i, j];
                }
            }

            /*for (int i = 0; i < matr.GetLength(0); i++)
            {
                for (int j = 0; j < matr.GetLength(1); j++) //вывод начальной симплекс-таблицы
                    Console.Write("{0} ", matr[i, j]);
                Console.WriteLine();
            }*/


            Console.Write("\n Начальное значение функции ");
            Console.Write(matr[0, matr.GetLength(1) - 1]);

            int q = 1;

            while (q != 0) //реализация симплекс-метода
            {

                double max = 0;
                double min = 100000000;
                int bazstolb = 0;
                int bazstroka = 0;
                double delim = 0;

                for (int i = 0; i < (matr.GetLength(1) - 1); i++) //находим базовый столбец
                {
                    if (matr[0, i] > max)
                    {
                        max = matr[0, i];
                        bazstolb = i;
                    }
                }




                for (int i = 1; i < matr.GetLength(0); i++) //находим базовую строку
                {
                    if (matr[i, bazstolb] != 0)
                    {
                        delim = matr[i, matr.GetLength(1) - 1] / matr[i, bazstolb];
                        if ((delim < min) && (delim > 0))
                        {
                            min = delim;
                            bazstroka = i;
                        }
                    }
                }

                otvet[bazstroka - 1, 0] = bazstolb; //вывод из базиса небазовой переменной и добавление базовой

                double bazelem;
                bazelem = matr[bazstroka, bazstolb]; //выделели базовый элемент

                for (int i = 0; i < matr.GetLength(1); i++)
                {
                    matr[bazstroka, i] = matr[bazstroka, i] / bazelem; //пересчитываем базовую строку
                }

                double[] stolb = new double[matr.GetLength(0)];

                for (int i = 0; i < matr.GetLength(0); i++)
                {
                    stolb[i] = matr[i, bazstolb];
                }

                for (int i = 0; i < matr.GetLength(0); i++) //пересчитываем матрицу
                {
                    for (int j = 0; j < matr.GetLength(1); j++)
                    {
                        if (i != bazstroka)
                        {
                            matr[i, j] = matr[i, j] - (stolb[i] * matr[bazstroka, j]);

                        }

                    }
                }


                q = 0; //проверка на оптимальность
                for (int i = 0; i < matr.GetLength(1) - 1; i++)
                {
                    if (matr[0, i] > 0)
                    {
                        q += 1;

                    }
                }

            }

            double[,] itog = new double[x.GetLength(0), 2]; //матрица для итоговых переменных


            for (int i = 0; i < otvet.GetLength(0); i++)
            {
                otvet[i, 1] = matr[i + 1, matr.GetLength(1) - 1];
            }

            for (int i = 0; i < x.GetLength(0); i++)
            {
                itog[i, 0] = i;
                for (int j = 0; j < otvet.GetLength(0); j++)
                {
                    if (otvet[j, 0] == i)
                    {
                        itog[i, 1] = otvet[j, 1];
                    }
                }


            }
            
            Console.Write("\n Удалось минимизировать значение функции до ");
            Console.Write(matr[0, matr.GetLength(1) - 1]);

            Console.Write("\n Получившиеся значения переменных: \n");
            for (int i = 0; i < itog.GetLength(0); i++)
            {
                Console.Write("x");
                for (int j = 0; j < itog.GetLength(1); j++)
                    Console.Write("{0} ", itog[i, j]);
                Console.WriteLine();
            }




            

        }
    }
}

           

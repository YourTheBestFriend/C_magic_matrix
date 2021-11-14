using System;
using static System.Math;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace magic_matrix
{
    class Program
    {

        static void printArray(int[,] array, int row, int col) 
        {
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    Console.Write($"{array[i, j],4:d1}");
                }
                Console.WriteLine();
            }
        }

        static int[,] chetn_n_kratno_4(int[,] array)
        {
            int[,] ar1 = new int[array.GetLength(0), array.GetLength(1)];
            int[,] ar2 = new int[array.GetLength(0), array.GetLength(1)];

            int k = 1;
            int k_reverse = array.GetLength(0) - 1;
            int flag_polov = (array.GetLength(0) + 1) / 2; // +1 
            bool flag_go_vtoraia_polovina = false;

            // Заполняю 1 квадрат
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (flag_go_vtoraia_polovina == false)
                    {
                        if (flag_polov == 0)
                        {
                            flag_go_vtoraia_polovina = true;
                            j--; // Чтобы в середине исправить баг а то там начиналось с нуля и пошло все не по плану
                            continue;
                        }
                        if (i % 2 != 0 && i != 0)
                        {
                            ar1[i, j] = k_reverse--;
                        }
                        else
                        {
                            ar1[i, j] = k++;
                        }
                    }
                    else
                    {
                        if (i % 2 == 0)
                        {
                            ar1[i, j] = k_reverse--;
                        }
                        else
                        {
                            ar1[i, j] = k++;
                        }
                    }
                }
                if (flag_polov != 0)
                {
                    flag_polov--;
                }
               
                k = 1;
                k_reverse = array.GetLength(0); 
            }

            // используею по новой (все зануляю)
            k_reverse = array.GetLength(0) - 1; // n для четной
            flag_go_vtoraia_polovina = false;
            flag_polov = (array.GetLength(0) + 1) / 2;
            int num_multiplication_N = 1;
            k = array.GetLength(0);  // n для не четной

            // Заполняю 2 квадрат
            for (int j = 0; j < array.GetLength(0); j++)
            {
                for (int i = 0; i < array.GetLength(1); i++)
                {
                    // Первая половина
                    if (flag_go_vtoraia_polovina == false)
                    {
                        if (i == 0 && j % 2 == 0)
                        {
                            ar2[i, j] = 0;
                        }
                        else
                        {
                            if (j % 2 == 0)
                            {
                                ar2[i, j] = num_multiplication_N++ * k; //  array.GetLength(0) = n = k
                            }
                            else
                            {
                                ar2[i, j] = k_reverse-- * k;
                            }
                        }
                        if (flag_polov == 0)
                        {
                            i--; // Чтобы в середине исправить баг а то там начиналось с нуля и пошло все не по плану
                            flag_go_vtoraia_polovina = true;
                            if (i == 0)
                            {
                                ar2[i, j] = 0;
                            }
                        }
                    }
                    // вторая половина
                    else
                    {
                        if (i == 0 && j % 2 != 0)
                        {
                            ar2[i, j] = 0;
                        }
                        else
                        {
                            if (j % 2 != 0)
                            {
                                ar2[i, j] = num_multiplication_N++ * k; //  array.GetLength(0) = n = k
                            }
                            else
                            {
                                ar2[i, j] = k_reverse-- * k;
                            }
                        }
                    } 
                }
                flag_polov--;
                k = array.GetLength(0);
                k_reverse = array.GetLength(0) - 1;
                num_multiplication_N = 1;
            }
            Console.WriteLine("FUNCTION chetn_n_kratno_4");

            //___________________
            // check 2 array
            //Console.WriteLine("array 1:");
            //printArray(ar1, ar1.GetLength(0), ar1.GetLength(1));
            //Console.WriteLine("array 2:");
            //printArray(ar2, ar2.GetLength(0), ar2.GetLength(1));
            //___________________

            // create
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    array[i, j] = ar1[i, j] + ar2[i, j];
                }
            }
            return array;
        }

        static int[,] chetn_n_ne_kratno_4(int[,] array)
        {
            // Воспользуюсь своей замечательной функицей из 2 пункта
            array = chetn_n_kratno_4(array);

            // уточнить про причем вторая половина квадрата заполняется аналогично первой

            // А теперь манипуляции
            int[] help_arr = new int[array.GetLength(0)]; // Вспомогательный 
            //int[] help_arr2 = new int[array.GetLength(0)]; // Вспомогательный 2


            // Зеркально меняю столбец (диагональ не трогаю)
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (i == j) continue;
                    if (j == 0) help_arr[i] = array[i, j];
                }
            }

            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (i == j) continue;
                    if (j == 0) help_arr[i] = array[i, j];
                }
            }

            // Зеркально меняю строку (диагональ не трогаю)
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    if (i == j) continue;
                    if (i == 0) help_arr[i] = array[i, j];
                }
            }

            return array;
        }

        static void Main(string[] args)
        {
            int[,] array1 = new int[8, 8];

            array1 = chetn_n_kratno_4(array1);
            // output
            Console.WriteLine("Your array: ");
            printArray(array1, array1.GetLength(0), array1.GetLength(1));

        }
    }
}

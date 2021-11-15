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
        // Returns true if mat[][] is magic
        // square, else returns false.
        static bool isMagicSquare(int[,] mat)
        {

            // sumd1 and sumd2 are the sum of the two diagonals
            int sumd1 = 0, sumd2 = 0;

            for (int i = 0; i < mat.GetLength(0); i++)
            {
                // (i, i) is the diagonal from top-left -> bottom-right
                // (i, N - i - 1) is the diagonal from top-right -> bottom-left
                sumd1 = sumd1 + mat[i, i];
                sumd2 = sumd2 + mat[i, mat.GetLength(0) - 1 - i];
            }
            // if the two diagonal sums are unequal then it is not a magic square
            if (sumd1 != sumd2)
                return false;

            // For sums of Rows
            for (int i = 0; i < mat.GetLength(0); i++)
            {

                int rowSum = 0, colSum = 0;
                for (int j = 0; j < mat.GetLength(0); j++)
                {
                    rowSum += mat[i, j];
                    colSum += mat[j, i];
                }
                if (rowSum != colSum || colSum != sumd1)
                    return false;
            }

            return true;
        }





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
            printArray(array, array.GetLength(0), array.GetLength(1));
            Console.WriteLine(" =11===============================");
            
            // Выделяю половину в другой массив
            int [,] help_arr = new int[array.GetLength(0), array.GetLength(1) / 2];
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1) / 2; j++)
                {
                    help_arr[i, j] = array[i, j];
                }
            }
            for (int i = (array.GetLength(0) / 2) - 1;  i < array.GetLength(0);  i++)
            {
                for (int j = array.GetLength(1)-1; j < array.GetLength(1) / 2; j++)
                {
                    array[i, j] = help_arr[i, j];
                }
            }
            Console.WriteLine(" =11===============================");
            printArray(array, array.GetLength(0), array.GetLength(1));
            Console.WriteLine(" =11===============================");


            // А теперь манипуляции
            //___________________________________________________A
            // Зеркально меняю столбец (диагональ не трогаю)
            for (int i = 1; i < array.GetLength(0); i++)
            {
                int j = array[i, 0];
                array[i, 0] = array[i, array.GetLength(0) - 1];
                array[i, array.GetLength(0) - 1] = j;
            }
            // Зеркально меняю строку (диагональ не трогаю)
            for (int i = 1; i < array.GetLength(0); i++)
            {
                int j = array[0, i];
                array[0, i] = array[array.GetLength(0) - 1, i];
                array[array.GetLength(0) - 1, i] = j;
            }
            //printArray(array, array.GetLength(0), array.GetLength(1));
            //Console.WriteLine(" ================================");

            //___________________________________________________B
            // two
            int vtoroy_sredn_in_strok = array[1, 0]; // Вторая строка первый элемент
            int index_vtoroy_sredn_in_strok = 0;

            int vtoroy_sredn_in_stolbec = array[0, 1]; // Второй столбец первый элемент
            int index_vtoroy_sredn_in_stolbec = 1;

            // last
            int posledn_sredn_in_strok = array[array.GetLength(0) - 1, 0];
            int index_posledn_sredn_in_strok = 0;

            int posledn_sredn_in_stolbec = array[0, array.GetLength(1) - 1];
            int index_posledn_sredn_in_stolbec = 0;

            // Нахожу средние 
            for (int i = 0; i < array.GetLength(0); i++)
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    // По строке
                    {
                        // Вторая строка 
                        if (i == 1)
                        {
                            if (vtoroy_sredn_in_strok < array[i, j])
                            {
                                vtoroy_sredn_in_strok = array[i, j];
                                index_vtoroy_sredn_in_strok = i;
                            }
                        }
                        // Последняя строка
                        if (i == array.GetLength(0) - 1)
                        {
                            if (posledn_sredn_in_strok < array[i, j])
                            {
                                posledn_sredn_in_strok = array[i, j];
                                index_posledn_sredn_in_strok = i;
                            }
                        }
                    }
                    // По столбцу 
                    {
                        // Второй столбец
                        if (j == 1)
                        {
                            if (vtoroy_sredn_in_stolbec < array[i, j])
                            {
                                vtoroy_sredn_in_stolbec = array[i, j];
                                index_vtoroy_sredn_in_stolbec = i;
                            }
                        }
                        // Последний столбец
                        if (j == array.GetLength(1) - 1)
                        {
                            if (posledn_sredn_in_stolbec < array[i, j])
                            {
                                posledn_sredn_in_stolbec = array[i, j];
                                index_posledn_sredn_in_stolbec = i;
                            }
                        }
                    }
                }
            }
            // switch 

            //Console.WriteLine(" ================================");
            // Для проверки что меняю
            //Console.WriteLine($" ======== {vtoroy_sredn_in_strok} ================ {posledn_sredn_in_strok}");
            //Console.WriteLine($" ======== {vtoroy_sredn_in_stolbec} ================ {posledn_sredn_in_stolbec}");
            
            // row
            array[array.GetLength(0)-1, index_posledn_sredn_in_strok] = vtoroy_sredn_in_strok;
            array[1, index_vtoroy_sredn_in_strok] = posledn_sredn_in_strok;
            
            // col
            array[index_posledn_sredn_in_stolbec, array.GetLength(1)-1] = vtoroy_sredn_in_stolbec;
            array[index_vtoroy_sredn_in_stolbec, 1] = posledn_sredn_in_stolbec;

            //Console.WriteLine(" ================================");
            //printArray(array, array.GetLength(0), array.GetLength(1));

            //___________________________________________________C
            int sredn = (array.GetLength(0) / 2) - 1;

            // stolbec
            int stolbec_k_reverse = array[0, sredn];
            array[0, sredn] = array[array.GetLength(0) - 1, sredn];
            array[array.GetLength(0) - 1, sredn] = stolbec_k_reverse;

            // stroka
            int stroka_k_reverse = array[sredn, 0];
            array[sredn, 0] = array[sredn, array.GetLength(0) - 1];
            array[sredn, array.GetLength(0) - 1] = stroka_k_reverse;

            //Console.WriteLine(" ================================");
            //printArray(array, array.GetLength(0), array.GetLength(1));

            return array;
        }

        static void Main(string[] args)
        {
            int[,] array1 = new int[7, 7];
            int[,] array2 = new int[8, 8];
            int[,] array3 = new int[6, 6];

            // output 2
            Console.WriteLine("Your array **2**: ");
            array2 = chetn_n_kratno_4(array2);
            printArray(array2, array2.GetLength(0), array2.GetLength(1));
            Console.WriteLine(" ============= " + isMagicSquare(array2) + " =============");
            // output 3
            Console.WriteLine("Your array ***3***: ");
            array3 = chetn_n_ne_kratno_4(array3);
            printArray(array3, array3.GetLength(0), array3.GetLength(1));
            Console.WriteLine(" ============= " + isMagicSquare(array3) + " =============");
        }
    }
}

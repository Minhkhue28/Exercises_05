using System;
using System.ComponentModel.Design;
using System.Linq.Expressions;
using System.Security.Cryptography;

namespace Exercises_05
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int[] array1 = new int[10];
            for (int i = 0; i < array1.Length; i++)
            {
                array1[i] = random.Next(1, 101);
            }
            Console.Write($"Random array is: ");
            foreach (int num in array1)
            {
                Console.Write(num + " ");
            }

            // Tính trung bình 1 mảng random  
            Cal_average(array1);

            // Kiểm tra xem mảng đó có giá trị mình muốn không 
            Console.WriteLine("====================================");
            Console.Write("Value you want to check appear or not: ");
            int human_value = Convert.ToInt32(Console.ReadLine());
            Console.Write($"{human_value} is appear in array: " + Check_have_value(array1, human_value));

            // Tìm chỉ số của phần tử trong mảng 
            Console.WriteLine("\n====================================");
            Console.Write("Value you want to find index: ");
            int human_num = Convert.ToInt32(Console.ReadLine());
            Find_index(array1, human_num);

            // Xóa 1 giá trị
            Console.WriteLine("\n====================================");
            Console.Write("Input value you want to remove: ");
            int remove_value = Convert.ToInt32(Console.ReadLine());
            Remove_value(array1, remove_value);

            // Tìm min, max
            Console.WriteLine("\n====================================");
            Find_max_min(array1);
            // Đảo array 
            Console.WriteLine("\n====================================");
            Reverse(array1);
            // Tìm giá trị lặp 
            Console.WriteLine("\n====================================");
            Find_duplicate_val(array1);

            // Xóa giá trị lặp 
            Console.WriteLine("====================================");
            int[] dup_num = Find_duplicate_val(array1);
            if (dup_num.Length == 0)
                Console.WriteLine("There is no dup number to remove!");
            else
            {
                int[] removed = Remove_dup_num(array1, dup_num);
                Console.Write("Array after removing duplicates: ");
                foreach (int num in removed)
                {
                    Console.Write(num + " ");
                }
            }
            Console.WriteLine("\n=== END EX01 ===");



            Console.WriteLine("\n=== EX_02 ===");
            Random rnd  = new Random();
            int[] array2 = new int[10];

            for (int i = 0; i < array2.Length; i++)
            {
                Console.WriteLine($"{i} number is : ");
                array2[i] = Convert.ToInt32(Console.ReadLine());
            }
            Console.Write($"User array is: ");
            foreach (int num in array2)
            {
                Console.Write(num + " ");
            }

            // In mảng sau khi bubble sort 
            Console.WriteLine($"\nArray after bubble sort: {string.Join(" ", BubbleSort( array2))}");
            // Cho người dùng điền 1 câu sau đó 1 từ, xem từ đó có trong câu không 
            Console.Write("Input a sentence: ");
            string[] sentence = Console.ReadLine().Split(' ');
            Console.Write("input a word: ");
            string search_word = Console.ReadLine();
            for (int i = 0; i < sentence.Length; i++)
            {
                if (sentence[i] == search_word)
                {
                    Console.WriteLine($"Position of the word: {i}");
                    return;
                }
            }
            Console.WriteLine("Not found!");
            return; 
        }

        static void Cal_average(int[] a)
        {
            int sum = 0;
            foreach (int num in a)
            {
                sum += num;
            }
            double average = sum / a.Length;
            Console.WriteLine($"\nAverage of array is: {average}");
        }

        static bool Check_have_value(int[] value, int value_tofind)
        {
            foreach (int num in value)
            {
                if (num == value_tofind)
                    return true;
            }
             return false;
            
        }

        static int Find_index(int[] random_num, int human_num)
        {
            bool found = false;
            for (int i = 0; i < random_num.Length; i++)
            {
                if (random_num[i] == human_num)
                {

                    Console.Write(human_num + $" have index at: {i} ");
                    found = true;
                    return i;
                }
            }
            if (!found)
                Console.WriteLine("Your number don't exist in array!");
            return 0;
        }

        static int[] Remove_value(int[] b, int remove)
        {
            int[] newArray = new int[b.Length - 1]; // mảng mới với đôj dài giảm 1 
            int new_index = 0;
            bool found = false;
            bool already_Remove = false; // theo doĩ đã xóa chưa (chỉ xóa giá trị đầu tiên)
            foreach (int num in b)
            {
                if (num == remove)
                {
                    found = true;
                    break;
                }
            }
            if (!found)
            {
                Console.WriteLine("Your value dont appear in array.");
                return b;
            }
            for (int i = 0; i < b.Length; i++)
            {
                if (!already_Remove && b[i] == remove)
                {
                    already_Remove = true;
                    found = true;
                    continue; // ko sao chép giá trị đó 
                }
                newArray[new_index] = b[i];
                new_index++;
            }

            Console.Write("New array is: ");
            foreach (int num in newArray)
                Console.Write(num + " ");
            return newArray;
        }

        static void Find_max_min(int[] array)
        {
            int max = 0;
            int min = 0;
            foreach (int num in array)
            {
                if (num > max)
                    max = num;
                else
                    min = num;
            }
            Console.WriteLine($"\nMax value is: {max}");
            Console.WriteLine($"Min value is: {min}");
        }

        static int[] Reverse(int[] array)
        {
            int length = array.Length;
            int[] newArray = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                newArray[i] = array[array.Length - 1 - i];
            }

            Console.WriteLine("Before reverse:");
            foreach (int num in array)
                Console.Write(num + " ");
            Console.WriteLine("\nAfer reverse:");
            foreach (int num in newArray)
                Console.Write(num + " ");
            return newArray;
        }

        static int[] Find_duplicate_val(int[] array)
        {
            Array.Sort(array);
            int index = 0; 
            int dup_count = 0; // dem gia tri trung
            // dem co mấy giá trị trùng 
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] == array[i + 1] && (i == 0 || array[i] != array[i - 1])) // nếu tìm thấy trùng và là đầu gặp 
                    dup_count++; 
            }
            if (dup_count == 0)
            {
                Console.WriteLine("\nThere is no duplicate value!");
                return new int[0];
            }

            int[] duplicates = new int[dup_count];
            for (int i = 0; i < array.Length - 1; i++)
            {
                if (array[i] == array[i + 1] && (i == 0 || array[i] != array[i - 1]))
                {
                    Console.WriteLine($"\nDuplicate value is: {array[i]}");
                    duplicates[index] = array[i];
                    index++;
                }
            }
            return duplicates;



        }
        static int[] Remove_dup_num(int[] array, int[] remove_arr)
        {
            if (remove_arr.Length == 0)
                return array;
            int _count = 0; // đếm phần tử được giữ lại 
            for (int i = 0;i <array.Length;i++)
            {
                bool should_remove = false;
                for (int j = 0;j < remove_arr.Length;j++)
                {
                    if (array[i] == remove_arr[j])
                    {
                        should_remove = true;
                        break; 
                    }
                }
                if (!should_remove)
                    _count++;
            }

            int[] result = new int[_count];
            int index = 0;
            // Thêm các phần tử không bị xóa vào mảng 
            for (int i = 0;i < array.Length; i++)
            {
                bool should_Remove = false;
                for (int j = 0; j < remove_arr.Length;j++)
                {
                    if (array[i] == remove_arr[j])
                    {
                        should_Remove = true;
                        break; 
                    }
                }
                if (!should_Remove)
                {
                    result[index] = array [i];
                    index ++;
                }
            }
            return result;
        }

        static int[] BubbleSort(int[] array)
        {
            int n = array.Length;
            for (int i = 0; i < n -1; i++)
                for (int j = 0;j < n - i -1; j++)
                {
                    if (array[j] > array[j +1])
                    {
                        int temp = array [j];
                        array[j] = array [j + 1];
                        array [j + 1] = temp;
                    }
                }
            return array;

        }
    }
}

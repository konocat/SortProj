using System.Collections.Generic;
using System.Linq;

namespace SortProj.MVVM.Model
{
    public class SortingModel
    {
        public int[] SortArray(int[] array, string sortingMethod)
        {
            switch (sortingMethod)
            {
                case "Bubble Sort":
                    return BubbleSort(array);
                case "Selection Sort":
                    return SelectionSort(array);
                case "Insertion Sort":
                    return InsertionSort(array);
                case "Quick Sort":
                    return QuickSort(array);
                case "Merge Sort":
                    return MergeSort(array);
                case "Counting Sort":
                    return CountingSort(array);
                case "Shell Sort":
                    return ShellSort(array);
                case "Radix Sort":
                    return RadixSort(array);
                case "Heap Sort":
                    return HeapSort(array);
                default:
                    return array;
            }
        }

        private int[] BubbleSort(int[] array)
        {
            int arraySize = array.Length;
            for (int i = 0; i < arraySize - 1; i++)
            {
                for (int j = 0; j < arraySize - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        int temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                    }
                }
            }
            return array;
        }

        private int[] SelectionSort(int[] array)
        {
            int arraySize = array.Length;
            for (int i = 0; i < arraySize - 1; i++)
            {
                int smallestNum = i;
                for (int j = i + 1; j < arraySize; j++)
                {
                    if (array[j] < array[smallestNum])
                    {
                        smallestNum = j;
                    }
                }

                int temp = array[smallestNum];
                array[smallestNum] = array[i];
                array[i] = temp;
            }
            return array;
        }
        private int[] InsertionSort(int[] array)
        {
            int arraySize = array.Length;
            for (int i = 1; i < arraySize; i++)
            {
                int key = array[i];
                int j = i - 1;

                while (j >= 0 && array[j] > key)
                {
                    array[j + 1] = array[j];
                    j = j - 1;
                }
                array[j + 1] = key;
            }
            return array;
        }
        private int[] QuickSort(int[] array)
        {
            if (array.Length <= 1)
            {
                return array;
            }

            int pivot = array[array.Length / 2];
            List<int> left = new List<int>();
            List<int> middle = new List<int>();
            List<int> right = new List<int>();

            foreach (int item in array)
            {
                if (item < pivot)
                {
                    left.Add(item);
                }
                else if (item == pivot)
                {
                    middle.Add(item);
                }
                else
                {
                    right.Add(item);
                }
            }

            return QuickSort(left.ToArray()).Concat(middle).Concat(QuickSort(right.ToArray())).ToArray();
        }
        private int[] MergeSort(int[] array)
        {
            if (array.Length <= 1)
            {
                return array;
            }

            int mid = array.Length / 2;
            int[] left = array.Take(mid).ToArray();
            int[] right = array.Skip(mid).ToArray();

            left = MergeSort(left);
            right = MergeSort(right);

            int[] result = new int[array.Length];
            int i = 0, j = 0, k = 0;

            while (i < left.Length && j < right.Length)
            {
                if (left[i] <= right[j])
                {
                    result[k++] = left[i++];
                }
                else
                {
                    result[k++] = right[j++];
                }
            }

            while (i < left.Length)
            {
                result[k++] = left[i++];
            }

            while (j < right.Length)
            {
                result[k++] = right[j++];
            }

            return result;
        }
        private int[] CountingSort(int[] array)
        {
            if (array == null || array.Length == 0)
            {
                return array;
            }

            int max = array.Max();
            int min = array.Min();
            int range = max - min + 1;

            int[] count = new int[range];
            int[] output = new int[array.Length];

            // Подсчет количества вхождений каждого элемента
            foreach (int num in array)
            {
                count[num - min]++;
            }

            // Накопление количества элементов
            for (int i = 1; i < range; i++)
            {
                count[i] += count[i - 1];
            }

            // Построение отсортированного массива
            for (int i = array.Length - 1; i >= 0; i--)
            {
                output[count[array[i] - min] - 1] = array[i];
                count[array[i] - min]--;
            }

            return output;
        }
        private int[] ShellSort(int[] array)
        {
            int n = array.Length;
            int gap = n / 2;

            while (gap > 0)
            {
                for (int i = gap; i < n; i++)
                {
                    int temp = array[i];
                    int j = i;

                    while (j >= gap && array[j - gap] > temp)
                    {
                        array[j] = array[j - gap];
                        j -= gap;
                    }

                    array[j] = temp;
                }

                gap /= 2;
            }

            return array;
        }
        private int[] RadixSort(int[] array)
        {
            if (array == null || array.Length == 0)
            {
                return array;
            }

            int max = array.Max();
            int exp = 1;

            while (max / exp > 0)
            {
                int[] output = new int[array.Length];
                int[] count = new int[10];

                // Инициализация массива подсчетов
                for (int i = 0; i < 10; i++)
                {
                    count[i] = 0;
                }

                // Подсчет количества вхождений каждого разряда
                for (int i = 0; i < array.Length; i++)
                {
                    int index = (array[i] / exp) % 10;
                    count[index]++;
                }

                // Накопление количества элементов
                for (int i = 1; i < 10; i++)
                {
                    count[i] += count[i - 1];
                }

                // Построение отсортированного массива
                for (int i = array.Length - 1; i >= 0; i--)
                {
                    int index = (array[i] / exp) % 10;
                    output[count[index] - 1] = array[i];
                    count[index]--;
                }

                // Копирование отсортированного массива обратно в исходный массив
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = output[i];
                }

                exp *= 10;
            }

            return array;
        }
        private int[] HeapSort(int[] array)
        {
            int n = array.Length;

            // Построение максимальной кучи
            for (int i = n / 2 - 1; i >= 0; i--)
            {
                Heapify(array, n, i);
            }

            // Извлечение элементов из кучи
            for (int i = n - 1; i > 0; i--)
            {
                // Перемещение текущего корня в конец
                int temp = array[0];
                array[0] = array[i];
                array[i] = temp;

                // Вызов Heapify для уменьшенной кучи
                Heapify(array, i, 0);
            }
            return array;
        }

        private void Heapify(int[] array, int n, int i)
        {
            int largest = i; // Инициализация наибольшего элемента как корня
            int left = 2 * i + 1; // Левый дочерний элемент
            int right = 2 * i + 2; // Правый дочерний элемент

            // Если левый дочерний элемент больше корня
            if (left < n && array[left] > array[largest])
            {
                largest = left;
            }

            // Если правый дочерний элемент больше самого большого элемента до сих пор
            if (right < n && array[right] > array[largest])
            {
                largest = right;
            }

            // Если наибольший элемент не корень
            if (largest != i)
            {
                int swap = array[i];
                array[i] = array[largest];
                array[largest] = swap;

                // Рекурсивно преобразуем поддерево
                Heapify(array, n, largest);
            }
        }

        // Добавьте другие методы сортировки
    }
}
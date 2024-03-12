 class Program
    {
        static double CalculateAverage(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
            {
                throw new ArgumentException("Array is null or empty", nameof(numbers));
            }

            double sum = 0;
            foreach (var number in numbers)
            {
                sum += number;
            }

            return sum / numbers.Length;
        }
        static int FindMaximumValue(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
            {
                throw new ArgumentException("Array is null or empty", nameof(numbers));
            }

            int max = numbers[0];
            foreach (var number in numbers)
            {
                if (number > max)
                {
                    max = number;
                }
            }

            return max;
        }

        static void Main(String[] args)
        {
            int[] arr = new[] { 1, 23, 4, 2 };
            double avg = CalculateAverage(arr);
            int max = FindMaximumValue(arr);
            Console.WriteLine(avg);
            Console.WriteLine(max);
            Console.WriteLine("lalalalal");
        }
 }

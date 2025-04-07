using System;
using System.Diagnostics;

class Program
{
    static long GCD_Euclidean(long a, long b)
    {
        if (b == 0)
            return a;
        return GCD_Euclidean(b, a % b);
    }

    static long GCD_Iterative(long a, long b)
    {
        while (b != 0)
        {
            long temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }

    static long GCD_Stein(long a, long b)
    {
        if (a == 0) return b;
        if (b == 0) return a;
        int shift;
        for (shift = 0; ((a | b) & 1) == 0; ++shift)
        {
            a >>= 1;
            b >>= 1;
        }
        while ((a & 1) == 0)
            a >>= 1;
        do
        {
            while ((b & 1) == 0)
                b >>= 1;
            if (a > b)
            {
                long temp = a;
                a = b;
                b = temp;
            }
            b -= a;
        } while (b != 0);
        return a << shift;
    }

    static void Main()
    {
        Random rand = new Random();
        const int count = 1000;
        long[] numbers1 = new long[count];
        long[] numbers2 = new long[count];

        for (int i = 0; i < count; i++)
        {
            numbers1[i] = rand.Next(1000000000, 2000000000); // 10-значні числа
            numbers2[i] = rand.Next(1000000000, 2000000000);
        }

        Stopwatch sw = new Stopwatch();

        sw.Start();
        long sum1 = 0;
        for (int i = 0; i < count; i++)
            sum1 += GCD_Euclidean(numbers1[i], numbers2[i]);
        sw.Stop();
        Console.WriteLine($"Euclidean (Recursive): {sw.ElapsedMilliseconds} ms, {sw.ElapsedTicks} ticks, {sw.ElapsedTicks / (double)Stopwatch.Frequency} sec, Sum: {sum1}");

        sw.Restart();
        long sum2 = 0;
        for (int i = 0; i < count; i++)
            sum2 += GCD_Iterative(numbers1[i], numbers2[i]);
        sw.Stop();
        Console.WriteLine($"Euclidean (Iterative): {sw.ElapsedMilliseconds} ms, {sw.ElapsedTicks} ticks, {sw.ElapsedTicks / (double)Stopwatch.Frequency} sec, Sum: {sum2}");

        sw.Restart();
        long sum3 = 0;
        for (int i = 0; i < count; i++)
            sum3 += GCD_Stein(numbers1[i], numbers2[i]);
        sw.Stop();
        Console.WriteLine($"Stein (Binary GCD): {sw.ElapsedMilliseconds} ms, {sw.ElapsedTicks} ticks, {sw.ElapsedTicks / (double)Stopwatch.Frequency} sec, Sum: {sum3}");

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
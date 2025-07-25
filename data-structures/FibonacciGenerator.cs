public class FibonacciGenerator
{
    private Dictionary<ulong,ulong> fibIndexes = new Dictionary<ulong,ulong>(100);
    private ulong fib(uint n)
    {
        if (fibIndexes.TryGetValue(n, out ulong result)){
            if (result > ulong.MaxValue / 2) throw new ArgumentOutOfRangeException(); //when a value that is indexed is more than half the max value of ulong, chances are that this operation will result in overflow
            return result;
        }

        ulong nResult = n <= 1 ? n : fib(n - 1) + fib(n - 2);
        fibIndexes.Add(n,nResult);
        return nResult;
    }

    public IEnumerable<(uint, ulong)> FibSequence()
    {
        uint start = 0;
        while (true)
        {
            yield return (start, fib(start));
            start++;
        }
    }
}

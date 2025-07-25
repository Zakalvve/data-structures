using data_structures;
using System.Diagnostics;
using System.Numerics;

//MyQueue<int> myQueue = new MyQueue<int>();
//MyStack<int> myStack = new MyStack<int>();

//for (int i = 0; i < 5; i++)
//{
//    myQueue.Enqueue(i);
//    myStack.Push(i);
//}

//Console.WriteLine("Queue   Stack");
//for (int i = 0; i < 5; i++)
//{
//    Console.WriteLine($"  {myQueue.Dequeue()}       {myStack.Pop()}");
//}
//Console.ReadLine();

//HashMap<string,int> map = new HashMap<string,int>(10);

//List<string> ids = new List<string>();

//for (int i = 0; i < 10; i++)
//{
//    string id = Guid.NewGuid().ToString();
//    map.Add(id,new Random().Next(1,1000));
//    ids.Add(id);
//}

//for (int i = 0; i < 10; i++)
//{
//    Console.WriteLine(map[ids[i]]);
//}

//for (int i = 0; i < 10; i++)
//{
//    map.Remove(ids[i]);
//}

//string str1 = "lehol";
//string str2 = "llhoe";

//if (AreStringsEqual(str1,str2))
//{
//    Console.WriteLine("Anagram");
//} else
//{
//    Console.WriteLine("Not Anangram");
//}

//bool AreStringsEqual(string one, string two)
//{
//    return AreCharArraysEqual(one.ToCharArray(), two.ToCharArray());
//}

//bool AreCharArraysEqual(char[] one,char[] two)
//{
//    if (one.Length != two.Length) return false;

//    Array.Sort(one);
//    Array.Sort(two);

//    for(int i = 0; i < one.Length; i++)
//    {
//        if (one[i] != two[i]) return false;
//    }
//    return true;
//}


//bool keepGoing = true;
//var fibIterator = new FibonacciGenerator().FibSequence().GetEnumerator();
//Stopwatch watch = new Stopwatch();
//while (keepGoing)
//{    
//    try
//    {
//        int times = Convert.ToInt32(Console.ReadLine());
//        watch.Start();
//        for (int i = 0; i < times; i++)
//        {
//            if (fibIterator.MoveNext())
//            {
//                var (index,value) = fibIterator.Current;
//                Console.WriteLine($"{index}: {value}");
//            }
//        }
//        watch.Stop();
//        Console.WriteLine($"Time taken: {watch.Elapsed}");
//    }
//    catch { keepGoing = false; }
//}

int sampleSize = 10000;
int maxPreviewSize = 20;

List<int> randomInts = new List<int>();
for (int i = sampleSize; i >= 0 ; i--)
{
    randomInts.Add(i);
}
int[] myInts = randomInts.ToArray();
int[] mergeSorted;
int[] bubbleSorted;

//merge sort
Stopwatch mergeWatch = new Stopwatch();
Console.WriteLine("TESTING MERGE SORT ALGORITHM");
Console.WriteLine("Source Array, Descending");
Console.Write("[ ");
for(int i = 0; i < maxPreviewSize; i++){
    Console.Write(myInts[i] + ", ");
}
Console.WriteLine("]");
Console.WriteLine();

mergeWatch.Start();
mergeSorted = MergeSort(myInts);
mergeWatch.Stop();

Console.WriteLine("Ouput Array, Ascending");
Console.Write("[ ");
for (int i = 0; i < maxPreviewSize; i++)
{
    Console.Write(mergeSorted[i] + ", ");
}
Console.WriteLine("]");
Console.WriteLine();
Console.WriteLine($"Merge sort took: {mergeWatch.Elapsed}");
Console.WriteLine();
//bubble sort
Stopwatch bubbleWatch = new Stopwatch();
Console.WriteLine("TESTING BUBBLE SORT ALGORITHM");
Console.WriteLine("Source Array, Descending");
Console.Write("[ ");
for (int i = 0; i < maxPreviewSize; i++)
{
    Console.Write(myInts[i] + ", ");
}
Console.WriteLine("]");
Console.WriteLine();

bubbleWatch.Start();
bubbleSorted = BubbleSort(myInts);
bubbleWatch.Stop();

Console.WriteLine("Ouput Array, Ascending");
Console.Write("[ ");
for (int i = 0; i < maxPreviewSize; i++)
{
    Console.Write(bubbleSorted[i] + ", ");
}
Console.WriteLine("]");
Console.WriteLine();
Console.WriteLine($"Bubble sort took: {bubbleWatch.Elapsed}");
Console.WriteLine();
decimal timeDiff = mergeWatch.ElapsedMilliseconds - bubbleWatch.ElapsedMilliseconds;
string noun = timeDiff >= 0 ? "slower" : "faster";
Console.WriteLine($"Tests Complete. Merge sort {Math.Abs(timeDiff)}ms {noun} than bubble sort");
Console.WriteLine($"Test array was {sampleSize} integers in descending order sorted into ascending order. [3,2,1] becomes [1,2,3]");
int[] MergeSort(int[] unsorted)
{
    int length = unsorted.Length;
    if (length == 1) return unsorted;
    int mid = (int)Math.Floor(length / 2m);

    (int[] left, int[] right) = SplitArray(unsorted,mid);

    return Merge(MergeSort(left), MergeSort(right));
}
int[] Merge(int[] left,int[] right)
{
    int[] sorted = new int[left.Length + right.Length];
    int i = 0, l = 0, r = 0;
    while (l < left.Length && r < right.Length)
    {
        if (left[l] < right[r])
        {
            sorted[i] = left[l];
            l++;
        } else
        {
            sorted[i] = right[r];
            r++;
        }
        i++;
    }

    (int[] array, int index) = l < left.Length ? (left,l) : (right,r);
    Array.Copy(array,index,sorted,i,array.Length - index);

    return sorted;
}
(int[], int[]) SplitArray(int[] input, int index)
{
    return (input.Take(index).ToArray(), input.Skip(index).ToArray());
}
int[] BubbleSort(int[] unsorted)
{
    int[] sorted = unsorted;
    bool wasSwapped;
    do
    {
        wasSwapped = false;
        for (int i = 0, j = 1; j < unsorted.Length; j++, i++)
        {
            if (sorted[i] > sorted[j])
            {
                (sorted[i], sorted[j]) = (sorted[j], sorted[i]);
                wasSwapped = true;
            }
        }
    } while (wasSwapped);

    return sorted;
}

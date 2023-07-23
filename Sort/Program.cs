using System.Diagnostics;

foreach (var listSize in new[] { 100, 1000, 10_000, 100_000, 1_000_000 })
    SortAndMeasure(listSize);

// Check CPU utilization
// Debug vs Release (msbuild .\Sort.sln /property:Configuration=Release)


static void SortAndMeasure(int listSize)
{
    // Fill
    var list1 = new int[listSize];
    for (var i = 0; i < listSize; i++)
        list1[i] = Random.Shared.Next(listSize * 10);

    var list2 = list1.ToArray();

    // Bubble sort
    var bubbleTimer = Stopwatch.StartNew();
    BubbleSort(listSize, list1);
    bubbleTimer.Stop();

    // Quick sort
    var quickTimer = Stopwatch.StartNew();
    QuickSort(list2);
    quickTimer.Stop();

    Console.WriteLine($"LIst size: {listSize}  Bubble sort: {bubbleTimer.Elapsed}  Quick sort: {quickTimer.Elapsed}");
}


static void BubbleSort(int listSize, int[] list)
{
    for (var a = listSize - 1; a > 0; a--)
    {
        for (var b = a - 1; b >= 0; b--)
            if (list[b] > list[a])
                (list[a], list[b]) = (list[b], list[a]);
    }
}


static void QuickSort(Span<int> list)
{
    if (list.Length <= 1) return;

    var last = list.Length - 1;
    var a = 0;
    var b = list.Length - 1;

    while (a < b)
    {
        while (a < b && list[a] <= list[last]) a++;
        while (a < b && list[b] > list[last]) b--;
        if (a < b)
        {
            (list[a], list[b]) = (list[b], list[a]);
            a++;
            b--;
        }
    }

    (list[a], list[last]) = (list[last], list[a]);
    if (a > 1)
        QuickSort(list.Slice(0, a - 1));
    if (a < last)
        QuickSort(list.Slice(a + 1, last - a - 1));
}

namespace Razdor.Shared.Extensions;

public static class BinarySearchByExtension
{
    public static int BinarySearchBy<T, TSearch>(this List<T> collection, TSearch search, Func<T, TSearch> converter)
        where TSearch : IComparable<TSearch>
    {
        int leftIndex = 0;
        int rightIndex = collection.Count - 1;
        
        while (leftIndex <= rightIndex)
        {
            int middle = leftIndex + (rightIndex - leftIndex) / 2;
            
            TSearch value = converter(collection[middle]);
            int comparison = value.CompareTo(search);
            
            if (comparison > 0)
                rightIndex = middle - 1;
            else if (comparison < 0)
                leftIndex = middle;
            else
                return middle;
        }
        
        return -1;
    }
}
public static class HelperExtensions
{
    public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(action);
        foreach (T element in source)
        {
            action(element);
        }
    }
}
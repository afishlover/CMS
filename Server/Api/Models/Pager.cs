namespace Api.Models;

public class Pager<T> : List<T>
{
    public int currentPage { get; set; }
    public int totalPages { get; set; }

    public Pager(List<T> items, int count, int currentPage, int pageCapacity)
    {
        this.currentPage = currentPage;
        totalPages = (int)Math.Ceiling(count / (double)pageCapacity);
        AddRange(items);
    }
    public bool hasPreviousPage => currentPage > 1;

    public bool hasNextPage => currentPage < totalPages;

    public static Pager<T> CreatePager(
        IQueryable<T> source, int currentPage, int pageCapacity)
    {
        var count = source.Count();
        var items = source.Skip(
                (currentPage - 1) * pageCapacity)
            .Take(pageCapacity).ToList();
        return new Pager<T>(items, count, currentPage, pageCapacity);
    }
}

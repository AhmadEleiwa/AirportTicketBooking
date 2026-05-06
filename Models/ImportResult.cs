public class ImportResult<T>
{
    public List<T> ValidItems { get; set; } = new();
    public List<string> Errors { get; set; } = new();
}
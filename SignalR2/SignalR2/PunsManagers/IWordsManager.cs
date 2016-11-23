namespace SignalR2.WordManager
{
    public interface IWordsManager
    {
        string CurrentWord { get; }
        string NextWord { get; }
    }
}
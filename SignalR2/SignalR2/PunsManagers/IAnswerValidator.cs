namespace SignalR2.WordManager
{
    public interface IAnswerValidator
    {
        bool ValidateWord(string word);
        IWordsManager WordManager { get; }
    }
}
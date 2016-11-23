namespace SignalR2.WordManager
{
    public sealed class AnswerValidator : IAnswerValidator
    {
        public IWordsManager WordManager => m_words_manager;

        private readonly IWordsManager m_words_manager;
        public AnswerValidator(IWordsManager words_manager)
        {
            m_words_manager = words_manager;
        }

        public bool ValidateWord(string word)
        {
            return word.Length > 0 && word[0] == '!' && word.ToLowerInvariant() == ("!" + m_words_manager.CurrentWord);
        }
    }
}
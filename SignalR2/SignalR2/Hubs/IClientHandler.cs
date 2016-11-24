namespace SignalR2.Hubs
{
    public interface IChatClientHandler
    {
        void SendMessageToAll(string message);
        void SetLabelText(string word);
        void InformCorrectAnswer();
    }
}
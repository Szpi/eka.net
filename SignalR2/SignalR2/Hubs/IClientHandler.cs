namespace SignalR2.Hubs
{
    public interface IClientHandler
    {
        void SendMessageToAll(string message);
        void SetLabelText(string word);
    }
}
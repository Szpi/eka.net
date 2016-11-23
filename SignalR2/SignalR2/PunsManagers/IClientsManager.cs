namespace SignalR2.WordManager
{
    public interface IClientsManager
    {
        void AddUser(string id);
        string GetNextClientToDraw();
        bool IsCurrentlyDrawing(string id);
        bool IsSomeoneDrawing();
        bool RemoveUser(string id);
    }
}
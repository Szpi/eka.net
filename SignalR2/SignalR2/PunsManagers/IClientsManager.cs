namespace SignalR2.WordManager
{
    public interface IClientsManager
    {
        void AddUser(string id);
        ClientsState GetNextClientToDraw(out string next_drawer);
        bool IsCurrentlyDrawing(string id);
        bool IsSomeoneDrawing();
        bool RemoveUser(string id);
    }
}
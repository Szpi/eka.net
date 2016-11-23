using System.Collections.Generic;

namespace SignalR2.Hubs
{
    public interface IPunsClientHandler
    {
        void DrawPath(string path);
        void Clear();
        void LoadImage(List<string> image);
    }
}
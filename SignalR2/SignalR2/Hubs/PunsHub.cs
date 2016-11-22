using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalR2.WordManager;

namespace SignalR2.Hubs
{
    [HubName("puns")]
    public class PunsHub : Hub<IPunsClientHandler>
    {
        static List<string> Image = new List<string>();

        private readonly IWordsManager m_word_manager;
        public PunsHub(IWordsManager manager)
        {
            m_word_manager = manager;
        }
        public override Task OnConnected()
        {
            Clients.Caller.LoadImage(Image);
            return base.OnConnected();
        }

        public void SendPath(string path)
        {
            Image.Add(path);
            Clients.Others.DrawPath(path);
        }

        public void Clear()
        {
            Image.Clear();
            Clients.All.Clear();
        }
    }

    public interface IPunsClientHandler
    {
        void DrawPath(string path);
        void Clear();
        void LoadImage(List<string> image);
    }
}
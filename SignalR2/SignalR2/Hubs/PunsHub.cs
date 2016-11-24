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
        static readonly List<string> Image = new List<string>();
       
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
}
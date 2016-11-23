using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using SignalR2.WordManager;

namespace SignalR2.Hubs
{
    [HubName("chat")]
    public class ChatHub : Hub<IClientHandler>
    {
        private const string ANSWER_GUESSES_INFORMATION = "Correct answer! Next round";
        private const string WRONG_ANSWER = "Wrong answer. Try again!";
        private const string YOU_ARE_CURRENTLY_DRAWING = "You are currently drawing. You can't guess.";
        private const string DRAWER_DISCONNECTED_NEXT_ROUND = "Drawer was disconnected. Starting next round";

        private readonly IAnswerValidator m_answer_validator;
        private readonly IWordsManager m_word_manager;
        private readonly IClientsManager m_clients_manager;
        public ChatHub(IAnswerValidator answer_validator, IWordsManager word_manager, IClientsManager clients_manager)
        {
            m_answer_validator = answer_validator;
            m_word_manager = word_manager;
            m_clients_manager = clients_manager;
        }

        public void SendToAll(string msg)
        {
            if(m_clients_manager.IsCurrentlyDrawing(Context.ConnectionId))
            {
                Clients.Caller.SendMessageToAll(YOU_ARE_CURRENTLY_DRAWING);
                return;
            }
            if(m_answer_validator.ValidateWord(msg))
            {
                Clients.All.SendMessageToAll(ANSWER_GUESSES_INFORMATION);
                Clients.Others.SendMessageToAll(msg);
                SendNextWordToDraw();
                return;
            }

            Clients.All.SendMessageToAll(WRONG_ANSWER);
            Clients.Others.SendMessageToAll(msg);
        }

        public override Task OnConnected()
        {
            m_clients_manager.AddUser(Context.ConnectionId);
            if(m_clients_manager.IsSomeoneDrawing())
            {
                Clients.Caller.SetLabelText("Guess pun!");
            }
            else
            {
                SendNextWordToDraw();
            }

            return base.OnConnected();
        }

        private void SendNextWordToDraw()
        {
            var id = m_clients_manager.GetNextClientToDraw();
            if(!string.IsNullOrEmpty(id))
            {
                Clients.Client(id).SetLabelText($"Word to draw: {m_word_manager.NextWord}");
                Clients.AllExcept(id).SetLabelText("Guess pun!");
                return;
            }
            Clients.All.SetLabelText("Waiting for at least 2 people");
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            m_clients_manager.RemoveUser(Context.ConnectionId);
            if(m_clients_manager.IsCurrentlyDrawing(Context.ConnectionId))
            {
                Clients.All.SendMessageToAll(DRAWER_DISCONNECTED_NEXT_ROUND);
                SendNextWordToDraw();
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}
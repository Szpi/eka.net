using System;
using System.Collections.Generic;
using System.Linq;

namespace SignalR2.WordManager
{
    public sealed class ClientsManager : IClientsManager
    {
        private string m_current_drawing_client;
        private readonly List<string> m_connected_users = new List<string>();
        private readonly object _mutex = new object();
        public void AddUser(string id)
        {
            lock(_mutex)
            {
                m_connected_users.Add(id);
            }
        }

        public bool RemoveUser(string id)
        {
            lock(_mutex)
            {
                m_connected_users.Remove(id);
                return m_current_drawing_client == id;
            }
        }
        public string GetNextClientToDraw()
        {
            lock(_mutex)
            {
                if(m_connected_users.Count <= 1)
                {
                    m_current_drawing_client = null;
                    return "";
                }
                if(FirstRound())
                {
                    var random_index = new Random().Next(0, m_connected_users.Count);
                    m_current_drawing_client = m_connected_users[random_index];
                }
                else
                {
                    var index = m_connected_users.IndexOf(m_current_drawing_client);
                    m_current_drawing_client = IsLastIndex(index) ? m_connected_users.FirstOrDefault() : m_connected_users[++index];
                }
                return m_current_drawing_client;
            }
        }

        private bool FirstRound()
        {
            return m_current_drawing_client == null;
        }

        public bool IsCurrentlyDrawing(string id)
        {
            return m_current_drawing_client == id;
        }

        public bool IsSomeoneDrawing()
        {
            return m_current_drawing_client != null;
        }
        private bool IsLastIndex(int index)
        {
            return index >= m_connected_users.Count - 1;
        }
    }
}
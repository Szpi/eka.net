using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Concurrent;
using System.Linq;

namespace SignalR2.WordManager
{
    public class WordsManager : IWordsManager
    {
        private const int WORD_FETCH = 1;
        public string CurrentWord => m_current_word;

        public WordsManager()
        {
            DownloadWords();
        }
        public string NextWord
        {
            get
            {
                SetRandomWord();
                return m_current_word;
            }
        }
        private string m_current_word;
        private readonly List<string> m_words = new List<string>();
        private readonly object m_mutex = new object();
        private void SetRandomWord()
        {
            lock(m_mutex)
            {
                if(m_words.Count == WORD_FETCH)
                {
                    DownloadWords();
                }
                m_current_word = m_words.First().ToLowerInvariant();
                m_words.RemoveAt(0);
            }
        }
        private void DownloadWords()
        {
            var http = $"http://api.wordnik.com/v4/words.json/randomWords?hasDictionaryDef=true&minCorpusCount=0&minLength=5&maxLength=15&limit=100&api_key=a2a73e7b926c924fad7001ca3111acd55af2ffabf50eb4ae5";
            using(var client = new WebClient())
            {
                var json_response = client.DownloadString(http);

                var json_array = JArray.Parse(json_response);
                foreach(var element in json_array)
                {
                    m_words.Add(element["word"].Value<string>());
                }
            }
        }
    }
}
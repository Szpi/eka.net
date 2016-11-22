using System.Collections.Generic;
using System.Net;
using Newtonsoft.Json.Linq;
using System;
namespace SignalR2.WordManager
{    
    public class WordsManager : IWordsManager
    {
        private readonly List<string> m_words = new List<string>();
        private readonly Random m_random_generator = new Random();
       
        public string GetRandomWord()
        {
            if (m_words.Count == 0)
            {
                DownloadWords();
            }
            var random_index = m_random_generator.Next(0, m_words.Count);
            var random_word = m_words[random_index];
            m_words.RemoveAt(random_index);
            return random_word;
        }
        private void DownloadWords()
        {
            var http = $"http://api.wordnik.com/v4/words.json/randomWords?hasDictionaryDef=true&minCorpusCount=0&minLength=5&maxLength=15&limit=100&api_key=a2a73e7b926c924fad7001ca3111acd55af2ffabf50eb4ae5";
            using (var client = new WebClient())
            {
                var json_response = client.DownloadString(http);

                var json_array = JArray.Parse(json_response);
                foreach (var element in json_array)
                {
                    m_words.Add(element["word"].Value<string>());
                }
            }
        }
    }
}
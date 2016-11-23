using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using SignalR2.Hubs;
using SignalR2.WordManager;
[assembly: OwinStartupAttribute(typeof(SignalR2.Startup))]
namespace SignalR2
{
    public partial class Startup
    {
        private readonly IWordsManager m_words_manager = new WordsManager();
        private readonly ClientsManager m_clients_manager = new ClientsManager();
        private IAnswerValidator m_answer_validator;
        public void Configuration(IAppBuilder app)
        {
            m_answer_validator = new AnswerValidator(m_words_manager);

            GlobalHost.DependencyResolver.Register(
            typeof(ChatHub),
            () => new ChatHub(m_answer_validator, m_words_manager, m_clients_manager));

            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}

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
        IWordsManager m_words_manager = new WordsManager();
        public void Configuration(IAppBuilder app)
        {
            GlobalHost.DependencyResolver.Register(
            typeof(PunsHub),
            () => new PunsHub(m_words_manager));

            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}

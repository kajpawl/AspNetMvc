using Hangfire;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SklepInternetowy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            //Inicjalizacja Hangfire
            GlobalConfiguration.Configuration.UseSqlServerStorage("KursyContext");

            app.UseHangfireDashboard();
            app.UseHangfireServer();
        }
    }
}
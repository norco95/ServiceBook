﻿using Microsoft.Owin;
using Newtonsoft.Json;
using Owin;

[assembly: OwinStartupAttribute(typeof(ServiceBook.Startup))]
namespace ServiceBook
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            

            ConfigureAuth(app);
        }
    }
}

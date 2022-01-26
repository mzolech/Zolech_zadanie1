using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace Zolech_zadanie1
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            DBManager.logoutAll();
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["identyfikator"] = "";
        }

        protected void Session_End(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Session["identyfikator"].ToString()))
                return;
            DBManager.logout(Session["identyfikator"].ToString());
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }

    }
}
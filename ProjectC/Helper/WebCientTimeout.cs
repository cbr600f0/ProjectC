using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text;

namespace ProjectC.Helper
{
    class WebCientTimeOut : WebClient
    {
        protected override WebRequest GetWebRequest(Uri uri)
        {
            WebRequest w = base.GetWebRequest(uri);
            w.Timeout = 5000;  // Timeout 5 second
            return w;
        }
    }
}

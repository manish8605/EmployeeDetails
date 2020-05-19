using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace EmployeeDetails
{
    public class RequirehttpsAttribute:AuthorizationFilterAttribute
    {
        public override void OnAuthorization(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (actionContext.Request.RequestUri.Scheme != Uri.UriSchemeHttps)
            {
                actionContext.Response = actionContext.Request.CreateResponse(System.Net.HttpStatusCode.Found);
                actionContext.Response.Content = new StringContent("<p>Use HTTPS instead of HTTP");
                UriBuilder uribuilder = new UriBuilder(actionContext.Request.RequestUri);
                uribuilder.Scheme = Uri.UriSchemeHttps;
                uribuilder.Port = 44300;

                actionContext.Response.Headers.Location = uribuilder.Uri;


            }
            else
            {
                base.OnAuthorization(actionContext);
            }
           
        }
    }
}
using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http.Filters;

namespace DSLNG.PEAR.Api.Client.Authentication
{
    /// <summary>
    /// Can be used to decorate a web api controller or controller method. 
    /// 
    /// If HmacSecret is false or not specified it will simply check if the header contains 
    /// a SecretToken value that is the  same as what is held in the item with the name 
    /// contained in SharedSecretName in the web.config appsettings
    /// 
    /// If HmacSecret is true it takes things further by checking the header of the
    /// message contains a SecretToken value that is a HMAC of the message generated
    /// using the value in the SharedSecretName in the web.config appsettings as the key.
    /// </summary>
    public class SecretAuthenticationFilter : ActionFilterAttribute
    {
        // The name of the web.config item where the shared secret is stored
        public string SharedSecretName { get; set; }
        public bool HmacSecret { get; set; }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            // We can only validate if the action filter has had this passed in
            if (!string.IsNullOrWhiteSpace((SharedSecretName)))
            {
                // Name of meta data to appear in header of each request
                const string secretTokenName = "SecretToken";

                var goodRequest = false;

                // The request should have the secretTokenName in the header containing the shared secret
                if (actionContext.Request.Headers.Contains(secretTokenName))
                {
                    var messageSecretValue = actionContext.Request.Headers.GetValues(secretTokenName).First();
                    var sharedSecretValue = ConfigurationManager.AppSettings[SharedSecretName];

                    if (HmacSecret)
                    {
                        Stream reqStream = actionContext.Request.Content.ReadAsStreamAsync().Result;
                        if (reqStream.CanSeek)
                        {
                            reqStream.Position = 0;
                        }

                        //now try to read the content as string
                        string content = actionContext.Request.Content.ReadAsStringAsync().Result;
                        var contentMD5 = content == "" ? "" : Hashing.GetHashMD5OfString(content);
                        var datePart = "";
                        var requestDate = DateTime.Now.AddDays(-2);
                        if (actionContext.Request.Headers.Date != null)
                        {
                            requestDate = actionContext.Request.Headers.Date.Value.UtcDateTime;
                            datePart = requestDate.ToString(CultureInfo.InvariantCulture);
                        }
                        var methodName = actionContext.Request.Method.Method;
                        var fullUri = actionContext.Request.RequestUri.ToString();

                        var messageRepresentation =
                            methodName + "\n" +
                            contentMD5 + "\n" +
                            datePart + "\n" +
                            fullUri;

                        var expectedValue = Hashing.GetHashHMACSHA256OfString(messageRepresentation, sharedSecretValue);

                        // Are the hmacs the same, and have we received it within +/- 5 mins (sending and
                        // receiving servers may not have exactly the same time)
                        if (messageSecretValue == expectedValue
                            && requestDate > DateTime.UtcNow.AddMinutes(-5)
                            && requestDate < DateTime.UtcNow.AddMinutes(5))
                            goodRequest = true;
                    }
                    else
                    {
                        if (messageSecretValue == sharedSecretValue)
                            goodRequest = true;
                    }
                }

                if (!goodRequest)
                {
                    var request = actionContext.Request;
                    var actionName = actionContext.ActionDescriptor.ActionName;
                    var controllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;
                    var moduleName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;

                    var errorMessage = string.Format(
                        "Error validating request to {0}:{1}:{2}",
                        moduleName, controllerName, actionName);

                    var errorResponse = request.CreateErrorResponse(HttpStatusCode.Forbidden, errorMessage);

                    // Force a wait to make a brute force attack harder
                    Thread.Sleep(2000);

                    actionContext.Response = errorResponse;
                }
            }

            base.OnActionExecuting(actionContext);
        }
    }
}

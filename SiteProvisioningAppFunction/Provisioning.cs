using System;
using System.Linq;
using System.Security;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.SharePoint.Client;
using Newtonsoft.Json;
using SiteProvisioningAppFunction.Entities;

namespace SiteProvisioningAppFunction
{
    public static class Provisioning
    {
        [FunctionName("Provisioning")]
        public static void Run([QueueTrigger("siteprovisioningrequests", Connection = "AzureWebJobsStorage")]string message, TraceWriter log)
        {
            log.Info($"C# Queue trigger function processed: {message}");

            var messageEntity = JsonConvert.DeserializeObject<Message>(message);

            using (var context = new ClientContext(messageEntity.WebUrl))
            {
                SecureString sec_pass = new SecureString();
                Array.ForEach(messageEntity.Password.ToArray(), sec_pass.AppendChar);
                sec_pass.MakeReadOnly();

                context.Credentials = new SharePointOnlineCredentials(@messageEntity.UserName, sec_pass);

                var web = context.Web;

                context.Load(web);
                context.ExecuteQuery();

                log.Info($"Site title: {web.Title}");
            }
        }
    }
}

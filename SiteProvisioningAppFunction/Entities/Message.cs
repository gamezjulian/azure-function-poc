using System;
using System.Collections.Generic;
using System.Text;

namespace SiteProvisioningAppFunction.Entities
{
    public class Message
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string WebUrl { get; set; }
        public bool EnableExternalSharing { get; set; }
    }
}

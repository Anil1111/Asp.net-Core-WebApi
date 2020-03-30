using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public class CloudMailService : IMailService
    {
        private readonly IConfiguration _configuation;

        public CloudMailService(IConfiguration configuration)
        {
            _configuation = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public void send(string subject, string message)
        {
            //send mail - output to debug window
            Debug.WriteLine($"Mail from {_configuation["mailSettings:mailFromAddress"]} to {_configuation["mailSettings:mailToAddress"]}, with CloudMailService.");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Message: {message}");
        }
    }
}
 
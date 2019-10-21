using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Realms.Sync;

namespace RealmAzureFunction
{
    public static class RealmFunction
    {
        [FunctionName("RealmFunction")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var realmServerLink = "<realmServerURL>";
            var login = "<login>";
            var password = "<password>";

            var credentials = Credentials.UsernamePassword(login, password, createUser: false);

            try
            {
                var user = await User.LoginAsync(credentials, new Uri($"https://{realmServerLink}"));
            }
            catch (Exception e)
            {
                return new ObjectResult(e.Message)
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            return new OkResult();
        }
    }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sitecore.Framework.Runtime.Configuration;
using Sitecore.Plugin.IdentityProvider.Ids4Demo.Configuration;
using System;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;

namespace Sitecore.Plugin.IdentityProvider.Ids4Demo
{
    public class ConfigureSitecore
    {
        private readonly ILogger<ConfigureSitecore> _logger;
        private readonly AppSettings _appSettings;

        public ConfigureSitecore(ISitecoreConfiguration scConfig, ILogger<ConfigureSitecore> logger)
        {
            this._logger = logger;
            this._appSettings = new AppSettings();
            scConfig.GetSection(AppSettings.SectionName);
            scConfig.GetSection(AppSettings.SectionName).Bind((object)this._appSettings.Ids4DemoIdentityProvider);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            Ids4DemoIdentityProvider ids4DemoProvider = this._appSettings.Ids4DemoIdentityProvider;
            if (!ids4DemoProvider.Enabled)
                return;
            this._logger.LogDebug("Configure '" + ids4DemoProvider.DisplayName + "'. AuthenticationScheme = " + ids4DemoProvider.AuthenticationScheme + ", ClientId = " + ids4DemoProvider.ClientId, Array.Empty<object>());
            new AuthenticationBuilder(services).AddOpenIdConnect(ids4DemoProvider.AuthenticationScheme, ids4DemoProvider.DisplayName, (Action<OpenIdConnectOptions>)(options =>
            {
                options.SignInScheme = "idsrv.external";
                options.ClientId = ids4DemoProvider.ClientId;
                options.Authority = "https://demo.identityserver.io/";
                options.MetadataAddress = ids4DemoProvider.MetadataAddress;
                options.Events.OnRedirectToIdentityProvider += (Func<RedirectContext, Task>)(context =>
                {
                    Claim first = context.HttpContext.User.FindFirst("idp");
                    if (string.Equals(first != null ? first.Value : (string)null, ids4DemoProvider.AuthenticationScheme, StringComparison.Ordinal))
                        context.ProtocolMessage.Prompt = "select_account";
                    return Task.CompletedTask;
                });
            }));
        }
    }
}

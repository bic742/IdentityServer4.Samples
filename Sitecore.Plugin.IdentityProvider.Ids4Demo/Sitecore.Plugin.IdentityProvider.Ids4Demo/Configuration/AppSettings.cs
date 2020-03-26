namespace Sitecore.Plugin.IdentityProvider.Ids4Demo.Configuration
{
    public class AppSettings
    {
        public static readonly string SectionName = "Sitecore:ExternalIdentityProviders:IdentityProviders:Okta";

        public OktaIdentityProvider OktaIdentityProvider { get; set; } = new OktaIdentityProvider();
    }
}

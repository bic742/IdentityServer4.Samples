# IdentityServer4.Samples
Samples for IdentityServer4

Sitecore samples are located in

* IdentityServer4.Samples/Clients/src/SitecoreMvcClient/
* IdentityServer4.Samples/Clients/src/SitecoreApi/

SitecoreApi requires Sitecore JSS Server Package installation. More information [here](https://jss.sitecore.com/docs/getting-started/jss-server-install)

Also requires to create API Key in Sitecore. More information [here](https://jss.sitecore.com/docs/getting-started/app-deployment)

Replace Constants.SitecoreAuthority with the Sitecore Identity URL

## Issues

For issues, use the [consolidated IdentityServer4 issue tracker](https://github.com/IdentityServer/IdentityServer4/issues).

## Long Paths

If you find after cloning the repository that some files are checked out or marked for deletion make sure to run this command.

    git config --global core.longpaths true

Then clone the repository again.

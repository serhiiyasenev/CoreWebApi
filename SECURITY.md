# Login API JWT configuration

The authentication service requires these runtime settings:

- `JwtConfig__Secret`: a unique, cryptographically random, UTF-8 signing key with at least 32 bytes; store in Azure Key Vault or protected App Service configuration
- `JwtConfig__Issuer`: use the configured issuer from appsettings, or set an environment-specific value
- `JwtConfig__Audience`: use the configured audience from appsettings, or set an environment-specific value

Issuer, audience, lifetime, signature and signing key are validated for incoming JWTs. Newly issued JWTs include issuer and audience. Rolling out this change invalidates older tokens without these claims. Revoke the previously exposed signing key and assess authentication logs. Do not deploy without securely setting a new signing secret.

The old secret may still exist in Git history. Rotate before rewriting history with `git-filter-repo` and coordinate GitHub Support, forks and collaborators.

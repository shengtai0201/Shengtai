CREATE TABLE "__EFMigrationsHistory" (
  "MigrationId" varchar(150) COLLATE "pg_catalog"."default" NOT NULL,
  "ProductVersion" varchar(32) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);
ALTER TABLE "__EFMigrationsHistory" OWNER TO "postgres";

CREATE TABLE "ApiResourceClaims" (
  "Id" serial4,
  "ApiResourceId" int4 NOT NULL,
  "Type" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "PK_ApiResourceClaims" PRIMARY KEY ("Id")
);
ALTER TABLE "ApiResourceClaims" OWNER TO "postgres";

CREATE TABLE "ApiResourceProperties" (
  "Id" serial4,
  "ApiResourceId" int4 NOT NULL,
  "Key" varchar(250) COLLATE "pg_catalog"."default" NOT NULL,
  "Value" varchar(2000) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "PK_ApiResourceProperties" PRIMARY KEY ("Id")
);
ALTER TABLE "ApiResourceProperties" OWNER TO "postgres";

CREATE TABLE "ApiResources" (
  "Id" serial4,
  "Enabled" bool NOT NULL,
  "Name" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
  "DisplayName" varchar(200) COLLATE "pg_catalog"."default",
  "Description" varchar(1000) COLLATE "pg_catalog"."default",
  "AllowedAccessTokenSigningAlgorithms" varchar(100) COLLATE "pg_catalog"."default",
  "ShowInDiscoveryDocument" bool NOT NULL,
  "Created" timestamp(6) NOT NULL,
  "Updated" timestamp(6),
  "LastAccessed" timestamp(6),
  "NonEditable" bool NOT NULL,
  CONSTRAINT "PK_ApiResources" PRIMARY KEY ("Id")
);
ALTER TABLE "ApiResources" OWNER TO "postgres";

CREATE TABLE "ApiResourceScopes" (
  "Id" serial4,
  "Scope" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
  "ApiResourceId" int4 NOT NULL,
  CONSTRAINT "PK_ApiResourceScopes" PRIMARY KEY ("Id")
);
ALTER TABLE "ApiResourceScopes" OWNER TO "postgres";

CREATE TABLE "ApiResourceSecrets" (
  "Id" serial4,
  "ApiResourceId" int4 NOT NULL,
  "Description" varchar(1000) COLLATE "pg_catalog"."default",
  "Value" varchar(4000) COLLATE "pg_catalog"."default" NOT NULL,
  "Expiration" timestamp(6),
  "Type" varchar(250) COLLATE "pg_catalog"."default" NOT NULL,
  "Created" timestamp(6) NOT NULL,
  CONSTRAINT "PK_ApiResourceSecrets" PRIMARY KEY ("Id")
);
ALTER TABLE "ApiResourceSecrets" OWNER TO "postgres";

CREATE TABLE "ApiScopeClaims" (
  "Id" serial4,
  "ScopeId" int4 NOT NULL,
  "Type" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "PK_ApiScopeClaims" PRIMARY KEY ("Id")
);
ALTER TABLE "ApiScopeClaims" OWNER TO "postgres";

CREATE TABLE "ApiScopeProperties" (
  "Id" serial4,
  "ScopeId" int4 NOT NULL,
  "Key" varchar(250) COLLATE "pg_catalog"."default" NOT NULL,
  "Value" varchar(2000) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "PK_ApiScopeProperties" PRIMARY KEY ("Id")
);
ALTER TABLE "ApiScopeProperties" OWNER TO "postgres";

CREATE TABLE "ApiScopes" (
  "Id" serial4,
  "Enabled" bool NOT NULL,
  "Name" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
  "DisplayName" varchar(200) COLLATE "pg_catalog"."default",
  "Description" varchar(1000) COLLATE "pg_catalog"."default",
  "Required" bool NOT NULL,
  "Emphasize" bool NOT NULL,
  "ShowInDiscoveryDocument" bool NOT NULL,
  CONSTRAINT "PK_ApiScopes" PRIMARY KEY ("Id")
);
ALTER TABLE "ApiScopes" OWNER TO "postgres";

CREATE TABLE "AspNetRoleClaims" (
  "Id" serial4,
  "RoleId" varchar(36) COLLATE "pg_catalog"."default" NOT NULL,
  "ClaimType" text COLLATE "pg_catalog"."default",
  "ClaimValue" text COLLATE "pg_catalog"."default",
  CONSTRAINT "PK_AspNetRoleClaims" PRIMARY KEY ("Id")
);
ALTER TABLE "AspNetRoleClaims" OWNER TO "postgres";

CREATE TABLE "AspNetRoles" (
  "Id" varchar(36) COLLATE "pg_catalog"."default" NOT NULL,
  "Name" varchar(256) COLLATE "pg_catalog"."default",
  "NormalizedName" varchar(256) COLLATE "pg_catalog"."default",
  "ConcurrencyStamp" text COLLATE "pg_catalog"."default",
  CONSTRAINT "PK_AspNetRoles" PRIMARY KEY ("Id")
);
ALTER TABLE "AspNetRoles" OWNER TO "postgres";

CREATE TABLE "AspNetUserClaims" (
  "Id" serial4,
  "UserId" varchar(36) COLLATE "pg_catalog"."default" NOT NULL,
  "ClaimType" text COLLATE "pg_catalog"."default",
  "ClaimValue" text COLLATE "pg_catalog"."default",
  CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY ("Id")
);
ALTER TABLE "AspNetUserClaims" OWNER TO "postgres";

CREATE TABLE "AspNetUserLogins" (
  "LoginProvider" varchar(128) COLLATE "pg_catalog"."default" NOT NULL,
  "ProviderKey" varchar(128) COLLATE "pg_catalog"."default" NOT NULL,
  "UserId" varchar(36) COLLATE "pg_catalog"."default" NOT NULL,
  "ProviderDisplayName" text COLLATE "pg_catalog"."default",
  CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider", "ProviderKey")
);
ALTER TABLE "AspNetUserLogins" OWNER TO "postgres";

CREATE TABLE "AspNetUserRoles" (
  "UserId" varchar(36) COLLATE "pg_catalog"."default" NOT NULL,
  "RoleId" varchar(36) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId", "RoleId")
);
ALTER TABLE "AspNetUserRoles" OWNER TO "postgres";

CREATE TABLE "AspNetUsers" (
  "Id" varchar(36) COLLATE "pg_catalog"."default" NOT NULL,
  "Account" varchar(256) COLLATE "pg_catalog"."default",
  "UserName" varchar(256) COLLATE "pg_catalog"."default",
  "NormalizedUserName" varchar(256) COLLATE "pg_catalog"."default",
  "Email" varchar(256) COLLATE "pg_catalog"."default",
  "NormalizedEmail" varchar(256) COLLATE "pg_catalog"."default",
  "EmailConfirmed" bool NOT NULL,
  "PasswordHash" text COLLATE "pg_catalog"."default",
  "SecurityStamp" text COLLATE "pg_catalog"."default",
  "ConcurrencyStamp" text COLLATE "pg_catalog"."default",
  "PhoneNumber" text COLLATE "pg_catalog"."default",
  "PhoneNumberConfirmed" bool NOT NULL,
  "TwoFactorEnabled" bool NOT NULL,
  "LockoutEnd" timestamptz(6),
  "LockoutEnabled" bool NOT NULL,
  "AccessFailedCount" int4 NOT NULL,
  "AdminUserId" varchar(36),
  CONSTRAINT "PK_AspNetUsers" PRIMARY KEY ("Id"),
  UNIQUE ("Account")
);
ALTER TABLE "AspNetUsers" OWNER TO "postgres";

CREATE TABLE "AspNetUserTokens" (
  "UserId" varchar(36) COLLATE "pg_catalog"."default" NOT NULL,
  "LoginProvider" varchar(128) COLLATE "pg_catalog"."default" NOT NULL,
  "Name" varchar(128) COLLATE "pg_catalog"."default" NOT NULL,
  "Value" text COLLATE "pg_catalog"."default",
  CONSTRAINT "PK_AspNetUserTokens" PRIMARY KEY ("UserId", "LoginProvider", "Name")
);
ALTER TABLE "AspNetUserTokens" OWNER TO "postgres";

CREATE TABLE "ClientClaims" (
  "Id" serial4,
  "Type" varchar(250) COLLATE "pg_catalog"."default" NOT NULL,
  "Value" varchar(250) COLLATE "pg_catalog"."default" NOT NULL,
  "ClientId" int4 NOT NULL,
  CONSTRAINT "PK_ClientClaims" PRIMARY KEY ("Id")
);
ALTER TABLE "ClientClaims" OWNER TO "postgres";

CREATE TABLE "ClientCorsOrigins" (
  "Id" serial4,
  "Origin" varchar(150) COLLATE "pg_catalog"."default" NOT NULL,
  "ClientId" int4 NOT NULL,
  CONSTRAINT "PK_ClientCorsOrigins" PRIMARY KEY ("Id")
);
ALTER TABLE "ClientCorsOrigins" OWNER TO "postgres";

CREATE TABLE "ClientGrantTypes" (
  "Id" serial4,
  "GrantType" varchar(250) COLLATE "pg_catalog"."default" NOT NULL,
  "ClientId" int4 NOT NULL,
  CONSTRAINT "PK_ClientGrantTypes" PRIMARY KEY ("Id")
);
ALTER TABLE "ClientGrantTypes" OWNER TO "postgres";

CREATE TABLE "ClientIdPRestrictions" (
  "Id" serial4,
  "Provider" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
  "ClientId" int4 NOT NULL,
  CONSTRAINT "PK_ClientIdPRestrictions" PRIMARY KEY ("Id")
);
ALTER TABLE "ClientIdPRestrictions" OWNER TO "postgres";

CREATE TABLE "ClientPostLogoutRedirectUris" (
  "Id" serial4,
  "PostLogoutRedirectUri" varchar(2000) COLLATE "pg_catalog"."default" NOT NULL,
  "ClientId" int4 NOT NULL,
  CONSTRAINT "PK_ClientPostLogoutRedirectUris" PRIMARY KEY ("Id")
);
ALTER TABLE "ClientPostLogoutRedirectUris" OWNER TO "postgres";

CREATE TABLE "ClientProperties" (
  "Id" serial4,
  "ClientId" int4 NOT NULL,
  "Key" varchar(250) COLLATE "pg_catalog"."default" NOT NULL,
  "Value" varchar(2000) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "PK_ClientProperties" PRIMARY KEY ("Id")
);
ALTER TABLE "ClientProperties" OWNER TO "postgres";

CREATE TABLE "ClientRedirectUris" (
  "Id" serial4,
  "RedirectUri" varchar(2000) COLLATE "pg_catalog"."default" NOT NULL,
  "ClientId" int4 NOT NULL,
  CONSTRAINT "PK_ClientRedirectUris" PRIMARY KEY ("Id")
);
ALTER TABLE "ClientRedirectUris" OWNER TO "postgres";

CREATE TABLE "Clients" (
  "Id" serial4,
  "Enabled" bool NOT NULL,
  "ClientId" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
  "ProtocolType" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
  "RequireClientSecret" bool NOT NULL,
  "ClientName" varchar(200) COLLATE "pg_catalog"."default",
  "Description" varchar(1000) COLLATE "pg_catalog"."default",
  "ClientUri" varchar(2000) COLLATE "pg_catalog"."default",
  "LogoUri" varchar(2000) COLLATE "pg_catalog"."default",
  "RequireConsent" bool NOT NULL,
  "AllowRememberConsent" bool NOT NULL,
  "AlwaysIncludeUserClaimsInIdToken" bool NOT NULL,
  "RequirePkce" bool NOT NULL,
  "AllowPlainTextPkce" bool NOT NULL,
  "RequireRequestObject" bool NOT NULL,
  "AllowAccessTokensViaBrowser" bool NOT NULL,
  "FrontChannelLogoutUri" varchar(2000) COLLATE "pg_catalog"."default",
  "FrontChannelLogoutSessionRequired" bool NOT NULL,
  "BackChannelLogoutUri" varchar(2000) COLLATE "pg_catalog"."default",
  "BackChannelLogoutSessionRequired" bool NOT NULL,
  "AllowOfflineAccess" bool NOT NULL,
  "IdentityTokenLifetime" int4 NOT NULL,
  "AllowedIdentityTokenSigningAlgorithms" varchar(100) COLLATE "pg_catalog"."default",
  "AccessTokenLifetime" int4 NOT NULL,
  "AuthorizationCodeLifetime" int4 NOT NULL,
  "ConsentLifetime" int4,
  "AbsoluteRefreshTokenLifetime" int4 NOT NULL,
  "SlidingRefreshTokenLifetime" int4 NOT NULL,
  "RefreshTokenUsage" int4 NOT NULL,
  "UpdateAccessTokenClaimsOnRefresh" bool NOT NULL,
  "RefreshTokenExpiration" int4 NOT NULL,
  "AccessTokenType" int4 NOT NULL,
  "EnableLocalLogin" bool NOT NULL,
  "IncludeJwtId" bool NOT NULL,
  "AlwaysSendClientClaims" bool NOT NULL,
  "ClientClaimsPrefix" varchar(200) COLLATE "pg_catalog"."default",
  "PairWiseSubjectSalt" varchar(200) COLLATE "pg_catalog"."default",
  "Created" timestamp(6) NOT NULL,
  "Updated" timestamp(6),
  "LastAccessed" timestamp(6),
  "UserSsoLifetime" int4,
  "UserCodeType" varchar(100) COLLATE "pg_catalog"."default",
  "DeviceCodeLifetime" int4 NOT NULL,
  "NonEditable" bool NOT NULL,
  CONSTRAINT "PK_Clients" PRIMARY KEY ("Id")
);
ALTER TABLE "Clients" OWNER TO "postgres";

CREATE TABLE "ClientScopes" (
  "Id" serial4,
  "Scope" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
  "ClientId" int4 NOT NULL,
  CONSTRAINT "PK_ClientScopes" PRIMARY KEY ("Id")
);
ALTER TABLE "ClientScopes" OWNER TO "postgres";

CREATE TABLE "ClientSecrets" (
  "Id" serial4,
  "ClientId" int4 NOT NULL,
  "Description" varchar(2000) COLLATE "pg_catalog"."default",
  "Value" varchar(4000) COLLATE "pg_catalog"."default" NOT NULL,
  "Expiration" timestamp(6),
  "Type" varchar(250) COLLATE "pg_catalog"."default" NOT NULL,
  "Created" timestamp(6) NOT NULL,
  CONSTRAINT "PK_ClientSecrets" PRIMARY KEY ("Id")
);
ALTER TABLE "ClientSecrets" OWNER TO "postgres";

CREATE TABLE "DeviceCodes" (
  "UserCode" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
  "DeviceCode" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
  "SubjectId" varchar(200) COLLATE "pg_catalog"."default",
  "SessionId" varchar(100) COLLATE "pg_catalog"."default",
  "ClientId" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
  "Description" varchar(200) COLLATE "pg_catalog"."default",
  "CreationTime" timestamp(6) NOT NULL,
  "Expiration" timestamp(6) NOT NULL,
  "Data" varchar(50000) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "PK_DeviceCodes" PRIMARY KEY ("UserCode")
);
ALTER TABLE "DeviceCodes" OWNER TO "postgres";

CREATE TABLE "IdentityResourceClaims" (
  "Id" serial4,
  "IdentityResourceId" int4 NOT NULL,
  "Type" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "PK_IdentityResourceClaims" PRIMARY KEY ("Id")
);
ALTER TABLE "IdentityResourceClaims" OWNER TO "postgres";

CREATE TABLE "IdentityResourceProperties" (
  "Id" serial4,
  "IdentityResourceId" int4 NOT NULL,
  "Key" varchar(250) COLLATE "pg_catalog"."default" NOT NULL,
  "Value" varchar(2000) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "PK_IdentityResourceProperties" PRIMARY KEY ("Id")
);
ALTER TABLE "IdentityResourceProperties" OWNER TO "postgres";

CREATE TABLE "IdentityResources" (
  "Id" serial4,
  "Enabled" bool NOT NULL,
  "Name" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
  "DisplayName" varchar(200) COLLATE "pg_catalog"."default",
  "Description" varchar(1000) COLLATE "pg_catalog"."default",
  "Required" bool NOT NULL,
  "Emphasize" bool NOT NULL,
  "ShowInDiscoveryDocument" bool NOT NULL,
  "Created" timestamp(6) NOT NULL,
  "Updated" timestamp(6),
  "NonEditable" bool NOT NULL,
  CONSTRAINT "PK_IdentityResources" PRIMARY KEY ("Id")
);
ALTER TABLE "IdentityResources" OWNER TO "postgres";

CREATE TABLE "Menu" (
  "Id" serial4,
  "ParentId" int4,
  "Type" int2 NOT NULL,
  "Text" varchar(24) COLLATE "pg_catalog"."default",
  "Small" varchar(15) COLLATE "pg_catalog"."default",
  "Url" varchar(48) COLLATE "pg_catalog"."default",
  "Icon" varchar(32) COLLATE "pg_catalog"."default",
  CONSTRAINT "Menu_pkey" PRIMARY KEY ("Id"),
  CONSTRAINT "Menu_ParentId_Text_Small_key" UNIQUE ("ParentId", "Text", "Small")
);
ALTER TABLE "Menu" OWNER TO "postgres";

CREATE TABLE "MenuRole" (
  "MenuId" int4 NOT NULL,
  "RoleId" varchar(36) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "MenuRole_pkey" PRIMARY KEY ("MenuId", "RoleId")
);
ALTER TABLE "MenuRole" OWNER TO "postgres";

CREATE TABLE "MenuUser" (
  "MenuId" int4 NOT NULL,
  "UserId" varchar(36) COLLATE "pg_catalog"."default" NOT NULL,
  "Push" int4 NOT NULL,
  CONSTRAINT "MenuUser_pkey" PRIMARY KEY ("MenuId", "UserId")
);
ALTER TABLE "MenuUser" OWNER TO "postgres";

CREATE TABLE "PersistedGrants" (
  "Key" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
  "Type" varchar(50) COLLATE "pg_catalog"."default" NOT NULL,
  "SubjectId" varchar(200) COLLATE "pg_catalog"."default",
  "SessionId" varchar(100) COLLATE "pg_catalog"."default",
  "ClientId" varchar(200) COLLATE "pg_catalog"."default" NOT NULL,
  "Description" varchar(200) COLLATE "pg_catalog"."default",
  "CreationTime" timestamp(6) NOT NULL,
  "Expiration" timestamp(6),
  "ConsumedTime" timestamp(6),
  "Data" varchar(50000) COLLATE "pg_catalog"."default" NOT NULL,
  CONSTRAINT "PK_PersistedGrants" PRIMARY KEY ("Key")
);
ALTER TABLE "PersistedGrants" OWNER TO "postgres";

ALTER TABLE "ApiResourceClaims" ADD CONSTRAINT "FK_ApiResourceClaims_ApiResources_ApiResourceId" FOREIGN KEY ("ApiResourceId") REFERENCES "ApiResources" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "ApiResourceProperties" ADD CONSTRAINT "FK_ApiResourceProperties_ApiResources_ApiResourceId" FOREIGN KEY ("ApiResourceId") REFERENCES "ApiResources" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "ApiResourceScopes" ADD CONSTRAINT "FK_ApiResourceScopes_ApiResources_ApiResourceId" FOREIGN KEY ("ApiResourceId") REFERENCES "ApiResources" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "ApiResourceSecrets" ADD CONSTRAINT "FK_ApiResourceSecrets_ApiResources_ApiResourceId" FOREIGN KEY ("ApiResourceId") REFERENCES "ApiResources" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "ApiScopeClaims" ADD CONSTRAINT "FK_ApiScopeClaims_ApiScopes_ScopeId" FOREIGN KEY ("ScopeId") REFERENCES "ApiScopes" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "ApiScopeProperties" ADD CONSTRAINT "FK_ApiScopeProperties_ApiScopes_ScopeId" FOREIGN KEY ("ScopeId") REFERENCES "ApiScopes" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "AspNetRoleClaims" ADD CONSTRAINT "FK_AspNetRoleClaims_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "AspNetUserClaims" ADD CONSTRAINT "FK_AspNetUserClaims_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "AspNetUserLogins" ADD CONSTRAINT "FK_AspNetUserLogins_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "AspNetUserRoles" ADD CONSTRAINT "FK_AspNetUserRoles_AspNetRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "AspNetUserRoles" ADD CONSTRAINT "FK_AspNetUserRoles_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "AspNetUserTokens" ADD CONSTRAINT "FK_AspNetUserTokens_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "ClientClaims" ADD CONSTRAINT "FK_ClientClaims_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "ClientCorsOrigins" ADD CONSTRAINT "FK_ClientCorsOrigins_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "ClientGrantTypes" ADD CONSTRAINT "FK_ClientGrantTypes_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "ClientIdPRestrictions" ADD CONSTRAINT "FK_ClientIdPRestrictions_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "ClientPostLogoutRedirectUris" ADD CONSTRAINT "FK_ClientPostLogoutRedirectUris_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "ClientProperties" ADD CONSTRAINT "FK_ClientProperties_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "ClientRedirectUris" ADD CONSTRAINT "FK_ClientRedirectUris_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "ClientScopes" ADD CONSTRAINT "FK_ClientScopes_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "ClientSecrets" ADD CONSTRAINT "FK_ClientSecrets_Clients_ClientId" FOREIGN KEY ("ClientId") REFERENCES "Clients" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "IdentityResourceClaims" ADD CONSTRAINT "FK_IdentityResourceClaims_IdentityResources_IdentityResourceId" FOREIGN KEY ("IdentityResourceId") REFERENCES "IdentityResources" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "IdentityResourceProperties" ADD CONSTRAINT "FK_IdentityResourceProperties_IdentityResources_IdentityResour~" FOREIGN KEY ("IdentityResourceId") REFERENCES "IdentityResources" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "Menu" ADD CONSTRAINT "Menu_ParentId_fkey" FOREIGN KEY ("ParentId") REFERENCES "Menu" ("Id") ON DELETE SET NULL ON UPDATE NO ACTION;
ALTER TABLE "MenuRole" ADD CONSTRAINT "MenuRole_MenuId_fkey" FOREIGN KEY ("MenuId") REFERENCES "Menu" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "MenuRole" ADD CONSTRAINT "MenuRole_RoleId_fkey" FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "MenuUser" ADD CONSTRAINT "MenuUser_MenuId_fkey" FOREIGN KEY ("MenuId") REFERENCES "Menu" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;
ALTER TABLE "MenuUser" ADD CONSTRAINT "MenuUser_UserId_fkey" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE ON UPDATE NO ACTION;


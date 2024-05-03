declare @Uri nvarchar(64) = 'https://only-fraser-implemented-originally.trycloudflare.com' --Update to the specific tunnel uri

set identity_insert [auth_cfg].[ClientRedirectUris] on

merge into [auth_cfg].[ClientRedirectUris] as [Target]
using (
	values (2, @Uri + '/authentication/login-callback', 1)
) as [Source] ([Id], [RedirectUri], [ClientId]) on [Source].[Id] = [Target].[Id]
when matched then
	update set 
		[RedirectUri] = [Source].[RedirectUri], 
		[ClientId] = [Source].[ClientId]
when not matched by target then
	insert ([Id], [RedirectUri], [ClientId])
	values ([Id], [RedirectUri], [ClientId]);

set identity_insert [auth_cfg].[ClientRedirectUris] off


set identity_insert [auth_cfg].[ClientPostLogoutRedirectUris] on

merge into [auth_cfg].[ClientPostLogoutRedirectUris] as [Target]
using (
	values (2, @Uri + '/authentication/logout-callback', 1)
) as [Source] ([Id], [PostLogoutRedirectUri], [ClientId]) on [Source].[Id] = [Target].[Id]
when matched then
	update set 
		[PostLogoutRedirectUri] = [Source].[PostLogoutRedirectUri], 
		[ClientId] = [Source].[ClientId]
when not matched by target then
	insert ([Id], [PostLogoutRedirectUri], [ClientId])
	values ([Id], [PostLogoutRedirectUri], [ClientId]);

set identity_insert [auth_cfg].[ClientPostLogoutRedirectUris] off





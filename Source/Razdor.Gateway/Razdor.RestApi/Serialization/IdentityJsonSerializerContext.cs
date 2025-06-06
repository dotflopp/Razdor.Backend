﻿using System.Text.Json.Serialization;
using Razdor.Identity.Module.Services.Auth.Commands;
using Razdor.Identity.Module.Services.Auth.Commands.ViewModels;
using Razdor.Identity.Module.Services.Users.Commands;
using Razdor.Identity.Module.Services.Users.ViewModels;

namespace Razdor.RestApi.Serialization;

[JsonSerializable(typeof(LoginCommand))]
[JsonSerializable(typeof(SignupCommand))]
[JsonSerializable(typeof(AccessToken))]
[JsonSerializable(typeof(SelfUserViewModel))]
[JsonSerializable(typeof(UserPreviewModel))]
[JsonSerializable(typeof(ChangeSelectedStatusCommand))]
public partial class IdentityJsonSerializerContext : JsonSerializerContext
{
}
﻿using Coolector.Common.Commands.Users;
using Coolector.Dto.Users;
using GetAccount = Coolector.Api.Queries.GetAccount;
using GetAccoutByName = Coolector.Api.Queries.GetAccoutByName;
using ICommandDispatcher = Coolector.Api.Commands.ICommandDispatcher;
using IUserStorage = Coolector.Api.Storages.IUserStorage;

namespace Coolector.Api.Modules
{
    public class AccountModule : ModuleBase
    {
        public AccountModule(ICommandDispatcher commandDispatcher, IUserStorage userStorage)
            : base(commandDispatcher)
        {
            Get("account", async args => await Fetch<GetAccount, UserDto>
                (async x => await userStorage.GetAsync(x.UserId)).HandleAsync());

            Get("{name}/account", async args => await Fetch<GetAccoutByName, UserDto>
                (async x => await userStorage.GetByNameAsync(x.Name)).HandleAsync());

            Post("sign-in", async args => await For<SignInUser>().DispatchAsync());

            Put("account/username", async args => await For<ChangeUserName>().DispatchAsync());

            Put("account/avatar", async args => await For<ChangeAvatar>().DispatchAsync());
        }
    }
}
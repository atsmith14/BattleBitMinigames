using BattleBitAPI.Common;
using BattleBitMinigames.Api;
using BattleBitMinigames.Helpers;

namespace BattleBitMinigames.Events;

public class PlayerRoles : Event
{
    public override Task OnConnected()
    {
        foreach (var player in Server.AllPlayers)
        {
            RoleHelper.SetPlayerRoles(player);
        }
        
        return Task.CompletedTask;
    }

    public override Task OnPlayerConnected(BattleBitPlayer player)
    {
        RoleHelper.SetPlayerRoles(player);

        return Task.CompletedTask;
    }

    public override Task OnPlayerJoiningToServer(ulong steamID, PlayerJoiningArguments args)
    {
        if (RoleHelper.Admins.Contains(steamID))
        {
            args.Stats.Roles = Roles.Admin;
        }
        else if (RoleHelper.Moderators.Contains(steamID))
        {
            args.Stats.Roles = Roles.Moderator;
        }
        
        return Task.CompletedTask;
    }
}
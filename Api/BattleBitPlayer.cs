﻿using System.Collections.Concurrent;
using BattleBitAPI;
using BattleBitMinigames.Enums;

namespace BattleBitMinigames.Api;
public class BattleBitApiPlayer : Player<BattleBitApiPlayer>
{
    public bool Debug = false;
    
    public ConcurrentDictionary<string, string?> PlayerProperties { get; set; } = new ConcurrentDictionary<string, string?>();
    
    public PlayerRoles[] PlayerRoles = {
        Enums.PlayerRoles.Default
    };

    public bool AddPlayerRole(PlayerRoles playerRole)
    {
        if (PlayerRoles.Contains(playerRole)) return false;
        
        var updatedRoles = new PlayerRoles[PlayerRoles.Length + 1];

        for (var i = 0; i < PlayerRoles.Length; i++)
        {
            updatedRoles[i] = PlayerRoles[i];
        }

        updatedRoles[PlayerRoles.Length] = playerRole;
        PlayerRoles = updatedRoles;

        return true;
    }

    public PlayerRoles GetHighestRole()
    {
        return PlayerRoles.Length == 0 ? Enums.PlayerRoles.Default : PlayerRoles.Max();
    }
    
    /**
     * Get a player property
     * @param propertyName The name of the property
     * @return The value of the property
     */
    public string GetPlayerProperty(string propertyName)
    {
        // Check if the player has the property
        return (this.PlayerProperties.TryGetValue(propertyName, out var property) ? property : string.Empty) ?? string.Empty;
    }
    
    /**
     * Set a player property
     * @param propertyName The name of the property
     * @param value The value of the property
     * @return True if the property was set, false if it was not
     */
    public bool SetPlayerProperty(string propertyName, string? value)
    {
        return this.PlayerProperties.TryAdd(propertyName, value);
    }
    
    /**
     * Remove a player property
     * @param propertyName The name of the property
     */
    public void RemovePlayerProperty(string propertyName)
    {
        this.PlayerProperties.TryRemove(propertyName, out _);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class NewPlayerEvent {

    public int PlayerId;
    public string PlayerName;
    public int SpawnId;
    public int Items;
    // public List<WeaponInfo> weapons;
    // public List<Consumables> items;

    public NewPlayerEvent(int playerId, string playerName)
    {
        PlayerId = playerId;
        PlayerName = playerName;

    }

}

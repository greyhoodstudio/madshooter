using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameStartEvent {

    public List<NewPlayerEvent> PlayerList;
    public List<CommonEvent> HistoryList;
    public int MapId;
    
    public GameStartEvent (string playerName)
    {
        NewPlayerEvent nPE = new NewPlayerEvent(-1, playerName);
        PlayerList = new List<NewPlayerEvent>(1);
        PlayerList.Add(nPE);
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class GameConnect {

    public int ConnectType;
    public string PlayerName;
    public string Token;

    public GameConnect (int connectionType, string name)
    {
        ConnectType = connectionType;
        PlayerName = name;
    }

}

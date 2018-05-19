using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ConnectEvent {

    public int MapId;
    public int SpawnId;
    public List<ConnectInfo> ConnectInfos;

    public ConnectEvent (int map, int spawn, ConnectInfo cinfo)
    {
        MapId = map;
        SpawnId = spawn;
        ConnectInfos = new List<ConnectInfo>();
        ConnectInfos.Clear();
        ConnectInfos.Add(cinfo);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectEvent {

    int MapId;
    int SpawnId;
    List<ConnectInfo> ConnectInfos;

    public ConnectEvent (int map, int spawn, ConnectInfo cinfo)
    {
        MapId = map;
        SpawnId = spawn;
        ConnectInfos.Add(cinfo);
    }
}

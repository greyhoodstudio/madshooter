using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CommonEvent {

    public int EventType;
    public int PlayerId;
    public int ObjectId;

    public CommonEvent (int eventType, int playerId, int objectId)
    {
        EventType = eventType;
        PlayerId = playerId;
        ObjectId = objectId;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class CommonEvent {

    public int EventType; // 0: Ping, 1: 회피, 2: 피격, 3: 재장전, 4: 아이템 습득, 5: 아이템 버림, 6: 죽음, 7: 나감
    public int PlayerId;
    public int ObjectId;

    public CommonEvent (int eventType, int playerId, int objectId)
    {
        EventType = eventType;
        PlayerId = playerId;
        ObjectId = objectId;
    }

}

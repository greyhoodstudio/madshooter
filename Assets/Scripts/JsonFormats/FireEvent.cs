using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireEvent {

    public int PlayerId;
    public int BulletId;
    public float FirePosX;
    public float FirePosY;
    public float MousePosX;
    public float MousePosY;

    public FireEvent (int pid, int bid, Vector2 firePos, Vector2 MousePos)
    {
        PlayerId = pid;
        BulletId = bid;
        FirePosX = firePos.x;
        FirePosY = firePos.y;
        MousePosX = MousePos.x;
        MousePosY = MousePos.y;
    }

}

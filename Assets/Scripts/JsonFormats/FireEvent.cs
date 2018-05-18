using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
        FirePosX = (float)Math.Round(firePos.x, 4);
        FirePosY = (float)Math.Round(firePos.y, 4);
        MousePosX = (float)Math.Round(MousePos.x, 4);
        MousePosY = (float)Math.Round(MousePos.y, 4);
    }

}

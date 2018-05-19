using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class FireEvent {

    public int PlayerNum;
    public int BulletId;
    public float PositionX;
    public float PositionY;
    public float MouseX;
    public float MouseY;
    public int WeaponId;
    public int BulletNum;

    public FireEvent (int pid, int bid, Vector2 firePos, Vector2 MousePos, int WeaponId, int BulletNm)
    {
        PlayerNum = pid;
        BulletId = bid;
        PositionX = (float)Math.Round(firePos.x, 4);
        PositionY = (float)Math.Round(firePos.y, 4);
        MouseX = (float)Math.Round(MousePos.x, 4);
        MouseY = (float)Math.Round(MousePos.y, 4);
    }

}

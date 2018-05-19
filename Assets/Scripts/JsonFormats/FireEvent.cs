using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class FireEvent {

    public int PlayerNum;    
    public float PositionX;
    public float PositionY;
    public float MouseX;
    public float MouseY;
    public int WeaponId;
    public int BulletNum;
    public int BulletId;

    public FireEvent (int pnum, Vector2 firePos, Vector2 MousePos, int weaponId, int bulletId)
    {
        PlayerNum = pnum;
        PositionX = (float)Math.Round(firePos.x, 4);
        PositionY = (float)Math.Round(firePos.y, 4);
        MouseX = (float)Math.Round(MousePos.x, 4);
        MouseY = (float)Math.Round(MousePos.y, 4);
        WeaponId = weaponId;
        BulletId = bulletId;
        BulletNum = -1;
    }

}

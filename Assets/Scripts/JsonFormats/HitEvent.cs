using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEvent {

    public int EventType;
    public int PlayerNum;
    public int BulletNum;

    public HitEvent (int type, int pnum, int bnum)
    {
        EventType = type;
        PlayerNum = pnum;
        BulletNum = bnum;
    }

}

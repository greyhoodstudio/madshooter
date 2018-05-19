using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class HitEvent {

    public int EventType;
    public int PlayerNum;
    public int BulletNum;

    public HitEvent(int pNum, int bNum){
        EventType = 1;
        PlayerNum = pNum;
        BulletNum = bNum;
    }
}

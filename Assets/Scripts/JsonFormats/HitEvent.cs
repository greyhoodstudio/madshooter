using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEvent {

    public int EventType;
    public int PlayerNum;
    public int BulletNum;

    public HitEvent(int eType, int pNum, int bNum){
        EventType = eType;
        PlayerNum = pNum;
        BulletNum = bNum;
    }
}

using UnityEngine;
using System.Collections;

public class HitEvent : MonoBehaviour
{
    public int EventType;
    public int PlayerNum;
    public int BulletNum;

    public HitEvent(int eType, int pNum, int bNum){
        EventType = eType;
        PlayerNum = pNum;
        BulletNum = bNum;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour {

    public float weaponNum;

    public float loadSpeed { get; set; }
    public float fireSpeed { get; set; } // 발사 사이 간격 (초)
    public int durability { get; set; } // 내구도
    public float range { get; set; } // 사정거리: 총알 생성 후 파괴되기까지의 시간
    public int loadBulletCnt { get; set; }  // 장전탄환갯수
    public Sprite sprite;
    public int status { get; set; } //무기의 상태 1:get 2:drop
    
    // Use this for initialization
    void Start(){

        range = 1.5f;
        fireSpeed = 0.3f;
    }
}

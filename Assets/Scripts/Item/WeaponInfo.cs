using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour {

    public int weaponId;
    public float weaponNum;

    public float loadSpeed;
    public float fireSpeed; // 발사 사이 간격 (초)
    public int durability; // 내구도
    public float range; // 사정거리: 총알 생성 후 파괴되기까지의 시간
    public int loadBulletCnt; // 장전탄환갯수
    public Sprite sprite;
    public int status; //무기의 상태 1:get 2:drop
    public int playerId;
    public int bulletId;//장착된 총알타입
    public float x, y;
    
    

    // Use this for initialization
    void Start(){

        range = 1.5f;
        fireSpeed = 0.3f;
        x = 100.0f;
        y = 100.0f;
    }
}

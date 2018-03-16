using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour {

    public float loadSpeed { get; set; }
    public float fireSpeed { get; set; }
    public int durability { get; set; } //내구도
    public int range { get; set; } //사정거리
    public int loadBulletCnt { get; set; }//장전탄환갯수
    public Sprite sprite;

    // Use this for initialization
    void Start(){

    }
}

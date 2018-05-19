using UnityEngine;
using System.Collections;

public class ConnectEvent : MonoBehaviour{
    public int MapId;
    public int SpawnId;
    public IList ConnectInfos;
    public string PlayerNm;
    public int PlayerNum;
    public int Health;
    public int Inventory;
    public int CurrWeaponId;

    public ConnectEvent(int mapId, int spawnId, IList cInfos, string pNm, int pN, int health, int inventory, int cwid){
        MapId = mapId;
        SpawnId = spawnId;
        cInfos = ConnectInfos;
        PlayerNm = pNm;
        PlayerNum = pN;
        Health = health;
        Inventory = inventory;
        CurrWeaponId = cwid;
    }
}

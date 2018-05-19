using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectInfo {

    public string PlayerName;
    public int PlayerNum;
    public int Health;
    public List<WeaponInfo> Inventory;
    public int CurrWeaponId;

    public ConnectInfo (string pname, int pnum, int health, int weaponId)
    {
        PlayerName = pname;
        PlayerNum = pnum;
        Health = health;
        CurrWeaponId = weaponId;
    }

}

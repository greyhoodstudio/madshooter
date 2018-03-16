using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public ItemManager ItemManager;
    public List<WeaponInfo> weapons;

    private void Awake() {
        weapons.Add(ItemManager.weaponList[0]);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

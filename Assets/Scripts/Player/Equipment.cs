using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {

    public Inventory inventory;
    public EquipmentRenderer handEquip;

    public GameObject currWeapon;

    // Use this for initialization
    void Awake () {
        // Equip default weapon
        currWeapon = inventory.weapons[0];
        EquipWeapon(currWeapon);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void EquipWeapon(GameObject weapon) //장비 sprite
    {
        //equipmentRenderer = handEquip;
        currWeapon = inventory.weapons[inventory.weapons.Count-1];
        handEquip.ChangeSprite(currWeapon.GetComponent<SpriteRenderer>().sprite);
    }
}

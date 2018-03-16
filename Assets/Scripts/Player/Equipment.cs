using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {

    public Inventory inventory;
    public EquipmentPart handEquip;

    public GameObject currWeapon;

    // Use this for initialization
    void Awake () {
        // Equip default weapon
        currWeapon = inventory.weapons[0];
        EquipWeapon(currWeapon.GetComponent<WeaponActionController>().equipmentPart);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void EquipWeapon(EquipmentPart equipmentRenderer) //장비 sprite
    {        
        equipmentRenderer = handEquip;
        equipmentRenderer.ChangeSprite(equipmentRenderer.spriteRenderer.sprite);
    }
}

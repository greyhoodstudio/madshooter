using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    // Script References    

    // Variables
    public WeaponInfo currentWeapon;
    public SpriteRenderer weaponSpriteRenderer;

    public List<WeaponInfo> weapons; // List of weapons in inventory
    

    // Use this for initialization
    void Start () {

        // Initialize references
        weaponSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

    }

    public void EquipWeapon(GameObject weapon)
    {
        currentWeapon = weapon.GetComponent<WeaponInfo>();
        weaponSpriteRenderer.sprite = currentWeapon.sprite;        
    }
}

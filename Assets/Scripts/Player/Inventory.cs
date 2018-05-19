using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    // Script References    

    // Variables
    public WeaponInfo currentWeapon;
    public SpriteRenderer weaponSpriteRenderer;

    public List<WeaponInfo> weapons; // List of weapons in inventory
    public WeaponActionController weaponActionController = null;
    public WeaponInfo weaponInfo = null;

	private void Awake()
	{
        
	}

	// Use this for initialization
	void Start () {
        // Initialize references
        /* 프로토타입용 기본 무기 설정*/
        currentWeapon = new WeaponInfo(1, 1, (Sprite)Resources.Load("BasicSprites/MagnetSprite", typeof(Sprite)),0.5f);
        weaponActionController = new WeaponActionController();
        weaponSpriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }

    public void EquipWeapon(WeaponInfo weapon)
    {
        //currentWeapon = weapon;
        //weaponSpriteRenderer.sprite = currentWeapon.weaponSprite;

        weaponSpriteRenderer.sprite = currentWeapon.weaponSprite;
        weapons.Add(currentWeapon);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour {

    public Inventory inventory;
    public Equipment equipment;

    private bool isFiring;
    private WeaponActionController weapon;

    // Use this for initialization
	void Start () {
        weapon = equipment.currWeapon.GetComponent<WeaponActionController>();
	}
	
	// Update is called once per frame
	void Update () {

        isFiring = Input.GetMouseButton(0);
        if (isFiring)
        {
            weapon.Fire();
        }
        
    }
        
}

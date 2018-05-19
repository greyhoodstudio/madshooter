using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {

    // Script References
    public EquipmentRenderer equipmentRenderer;

    // Variables
    public WeaponInfo currWeapon; // Current Weapon

    // Use this for initialization
    void Awake () {
       
    }

    void Start()
    {
        // Initialize References
        equipmentRenderer = GetComponentInChildren<EquipmentRenderer>();
    }

    // Update is called once per frame
    void Update () {
        //if(currWeapon!=null){
        //    currWeapon.transform.SetPositionAndRotation(currWeapon.transform.position + new Vector3(10f, 10f, 10f),Quaternion.identity);
        //}
	}

    public void EquipWeapon(GameObject weapon)                //장비 sprite
    {
        currWeapon = weapon.GetComponent<WeaponInfo>();        
        //weapon.transform.SetParent(equipmentRenderer.transform);
        equipmentRenderer.ChangeSprite(currWeapon.GetComponent<SpriteRenderer>().sprite);
    }
}

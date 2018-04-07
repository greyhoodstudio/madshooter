using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour {

    // Script References
    public Inventory inventory;
    public EquipmentRenderer equipmentRenderer;

    // Variables
    public GameObject currWeapon; // Current Weapon

    // Use this for initialization
    void Awake () {
        
    }

    void Start()
    {
        // Initialize References
        inventory = GetComponent<Inventory>();
        equipmentRenderer = GetComponentInChildren<EquipmentRenderer>();
    }

    // Update is called once per frame
    void Update () {
        //if(currWeapon!=null){
        //    currWeapon.transform.SetPositionAndRotation(currWeapon.transform.position + new Vector3(10f, 10f, 10f),Quaternion.identity);
        //}
	}

    public void EquipWeapon(GameObject weapon) //장비 sprite
    {
        //equipmentRenderer = handEquip;
        currWeapon = weapon;
        weapon.transform.SetParent(equipmentRenderer.transform);
        //inventory.weapons[inventory.weapons.Count-1];
        equipmentRenderer.ChangeSprite(currWeapon.GetComponent<SpriteRenderer>().sprite);
    }
}

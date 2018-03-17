using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{

    public Inventory inventory;
    public Equipment equipment;

    private bool isFiring;
    private WeaponActionController weaponActionController;
    private bool isItem; //아이템에 충돌했는가.
    private GameObject item;

    // Use this for initialization
    void Start()
    {
        weaponActionController = equipment.currWeapon.GetComponent<WeaponActionController>(); //FIXED.
        weaponActionController.equipmentPart = equipment.handEquip; //FIXED
        isItem = false;
    }

    // Update is called once per frame
    void Update()
    {

        isFiring = Input.GetMouseButton(0);
        if (isFiring)
        {
            weaponActionController.Fire();
        }

        if (Input.GetKeyDown(KeyCode.F) && isItem) // get item
        {
            Debug.Log("getStartItem");
            inventory.weapons.Add(item);
            equipItem();
            item.SetActive(false);
            //Destroy(item);
        }
    }

    void equipItem(){
        equipment.EquipWeapon(item);
        weaponActionController = equipment.currWeapon.GetComponent<WeaponActionController>();
        weaponActionController.equipmentPart = equipment.handEquip;
        isItem = false;
       
    }

	private void OnTriggerStay2D(Collider2D other)
	{
        if(other.gameObject.tag=="TempItem"){
            item = other.gameObject;
            isItem = true;
        }
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        isItem = false;
	}

}

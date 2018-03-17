using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    // Script References
    private Inventory inventory;
    private Equipment equipment;
    private PlayerInfo playerInfo;
    private MovementController movementController;
    private WeaponActionController weaponActionController
 
    // Variables
    private bool isFiring;
    private bool isItem; //아이템에 충돌했는가.
    private GameObject item;
    
    private bool dodgeInput;
    public bool isDodging { get; set; }

    // Use this for initialization
	void Start () {

        // Initialize scripts
        inventory = GetComponent<Inventory>();
        equipment = GetComponent<Equipment>();
        playerInfo = GetComponent<PlayerInfo>();
        movementController = GetComponent<MovementController>();
        weaponActionController = equipment.currWeapon.GetComponent<WeaponActionController>(); //FIXED.
        
        weaponActionController.equipmentPart = equipment.handEquip; //FIXED
        
        // Initialize variables
        isDodging = false;
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
  
        // Dodge Mechanic
        dodgeInput = Input.GetMouseButton(1);
        if (dodgeInput && !isDodging)
        {
            StartCoroutine("Dodge");
        }
    }

    IEnumerator Dodge()
    {
        movementController.fixedTargetPosition = transform.position + movementController.normalizedMouseDirection * playerInfo.dodgeDistance;

        Debug.Log("Start Dodge");
        isDodging = true;
        this.tag = "Dodge";
        // 회피 애니메이션 추가 필요
        yield return new WaitForSeconds(1);
        this.tag = "Player";
        isDodging = false;
        Debug.Log("End Dodge");
    }

}

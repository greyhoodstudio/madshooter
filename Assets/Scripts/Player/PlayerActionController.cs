using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    // Script References
    public Inventory inventory;
    public Equipment equipment;
    public PlayerInfo playerInfo;
    public MovementController movementController;
    public WeaponActionController weaponActionController;
    public WeaponInfo weaponInfo;

    // Variables
    public bool isFiring;
    public bool fireLock;

    public bool isItem; //아이템에 충돌했는가.
    private GameObject item; // 주변 아이템
    
    private bool dodgeInput;
    public bool isDodging { get; set; }

    void Awake()
    {
        this.tag = "Player";
    }

    // Use this for initialization
    void Start () {

        // Initialize references
        inventory = GetComponent<Inventory>();
        equipment = GetComponent<Equipment>();
        playerInfo = GetComponent<PlayerInfo>();
        movementController = GetComponent<MovementController>();
        // weaponActionController = equipment.currWeapon.GetComponent<WeaponActionController>(); //FIXED.
        // weaponActionController.equipmentPart = equipment.equipmentRenderer; //FIXED
        // weaponInfo = equipment.currWeapon.GetComponent<WeaponInfo>();
        
        // Initialize variables
        isDodging = false;
        isItem = false;
        fireLock = false;
        
	}
	
	// Update is called once per frame
    void Update()
    {
        // Fire
        isFiring = Input.GetMouseButton(0);
        if (isFiring && !fireLock && equipment.currWeapon)
        {
            weaponActionController.Fire();
            StartCoroutine("FireLock");
        }

        // Dodge
        dodgeInput = Input.GetMouseButton(1);
        if (dodgeInput && !isDodging) {
            StartCoroutine("Dodge");
        }

        // Item Pick Up
        if (Input.GetKeyDown(KeyCode.F) && isItem)
        {
            Debug.Log("getStartItem");
            inventory.weapons.Add(item);
            equipItem();
            item.SetActive(false);
            isItem = false;
            //Destroy(item);
        }
    }

    void equipItem(){
        equipment.EquipWeapon(item);
        weaponActionController = equipment.currWeapon.GetComponent<WeaponActionController>();
        weaponActionController.equipmentRenderer = equipment.equipmentRenderer;
        weaponInfo = equipment.currWeapon.GetComponent<WeaponInfo>();
             
    }

	private void OnTriggerStay2D(Collider2D other)
	{
        if(other.gameObject.tag=="Item"){
            item = other.gameObject;
            isItem = true;
        }
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        if (isItem) isItem = false; 
    }

    IEnumerator FireLock()
    {
        fireLock = true;
        yield return new WaitForSeconds(weaponInfo.fireSpeed);
        fireLock = false;
    }

    IEnumerator Dodge()
    {
        movementController.fixedTargetPosition = transform.position + movementController.normalizedMouseDirection * playerInfo.dodgeDistance;

        Debug.Log("Start Dodge");
        isDodging = true;
        this.tag = "Dodge";
        // 회피 애니메이션 추가 필요
        yield return new WaitForSeconds(0.5f);
        this.tag = "Player";
        isDodging = false;
        Debug.Log("End Dodge");
    }

}

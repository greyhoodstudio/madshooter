﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour
{
    // Script References
    public Inventory inventory;
    public PlayerInfo playerInfo;
    public MovementController movementController;

    public WeaponActionController weaponActionController = null;
    public WeaponInfo weaponInfo = null;

    // Variables
    public bool isFiring;
    public bool isItem; //아이템에 충돌했는가.

    private GameObject weapon;

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
        playerInfo = GetComponent<PlayerInfo>();
        movementController = GetComponent<MovementController>();
       
        //init Basic weapon
        weapon = Instantiate(Resources.Load("Prefabs/TempItem")) as GameObject;
        weapon.transform.SetPositionAndRotation(transform.position, Quaternion.identity);
        equipWeapon();
        weapon.transform.SetParent(playerInfo.transform.parent);

        // Initialize variables
        isDodging = false;
        isItem = false;
	}
	
	// Update is called once per frame
    void Update()
    {
        // Update weapon transform
        weapon.transform.SetPositionAndRotation(transform.position, Quaternion.identity);

        // Dodge
        dodgeInput = Input.GetMouseButton(1);
        if (dodgeInput && !isDodging)
        {
            StartCoroutine("Dodge");
        }

    //    Item Pick Up
    //    if (Input.GetKeyDown(KeyCode.F) && isItem)
    //    {
    //        Debug.Log("getStartItem");
    //        inventory.weapons.Add(weapon);
    //        equipWeapon();
    //        //weapon.SetActive(false);
    //        isItem = false;
    //        //Destroy(item);
    //    }

    }

    public void FireWeapon(int bulletId, Vector2 firePosition, Vector2 mousePosition)
    {
        if (weaponActionController == null)
            return;

        // Calculate bullet rotation
        Vector2 direction = mousePosition - firePosition;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion fireRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Fire
        weaponActionController.Fire(bulletId, firePosition, fireRotation);
    }

    public void TriggerDodge()
    {
        if (!isDodging)
        {
            StartCoroutine("Dodge");
        }
    }
    
    public void equipWeapon(){ //아이템 장착(after server)  

        inventory.EquipWeapon(weapon);
        inventory.weapons.Add(weaponInfo);
        
        // weaponActionController.equipmentRenderer = equipment.equipmentRenderer;

        weaponInfo = weapon.GetComponent<WeaponInfo>();
        weaponActionController = weapon.GetComponent<WeaponActionController>();
    }

	private void OnTriggerStay2D(Collider2D other)
	{
        if(other.gameObject.tag=="Item"){
            isItem = true;
        }
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
        if (isItem) isItem = false; 
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

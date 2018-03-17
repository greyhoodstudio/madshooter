using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour {

    // Script References
    private Inventory inventory;
    private Equipment equipment;
    private PlayerInfo playerInfo;
    private MovementController movementController;
    private WeaponActionController weapon;
    
    // Variables
    private bool isFiring;
    
    private bool dodgeInput;
    public bool isDodging { get; set; }

    // Use this for initialization
	void Start () {

        // Initialize scripts
        inventory = GetComponent<Inventory>();
        equipment = GetComponent<Equipment>();
        playerInfo = GetComponent<PlayerInfo>();
        movementController = GetComponent<MovementController>();

        weapon = equipment.currWeapon.GetComponent<WeaponActionController>();
        
        // Initialize variables
        isDodging = false;
	}
	
	// Update is called once per frame
    void Update()
    {
        isFiring = Input.GetMouseButton(0);
        if (isFiring)
        {
            weapon.Fire();
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionController : MonoBehaviour {

    public Inventory inventory;
    public Equipment equipment;

    private bool isFiring;

    // Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {

        isFiring = Input.GetMouseButton(0);
        if (isFiring)
        {
            equipment.currWeapon.Fire();
        }
        
    }
        
}

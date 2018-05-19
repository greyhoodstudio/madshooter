using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActionController : MonoBehaviour {
    
	// Use this for initialization
	void Start () {
        // Initialize tag
        // this.tag = "Item";
  	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Get Item
    void getItem(){
        // GetComponent<WeaponActionController>().weaponInfo.status = 1; //FIXED. enum으로 변경. 
    }

    // Drop Item
    void dropItem(){
        // GetComponent<WeaponActionController>().weaponInfo.status = 2; 
    }

	private void OnTriggerEnter2D(Collider2D player)
	{
        // if(player.tag == "Player"){
        //    getItem();
        // }
	}
}

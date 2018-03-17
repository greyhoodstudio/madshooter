using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActionController : MonoBehaviour {
    //item get
    //item drop

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void getItem(){
        gameObject.GetComponent<WeaponActionController>().weaponInfo.status = 1; //FIXED. enum으로 변경. 
        
    }

    void dropItem(){
        gameObject.GetComponent<WeaponActionController>().weaponInfo.status = 2; 
    }

	private void OnTriggerEnter2D(Collider2D player)
	{
        if(player.gameObject.tag == "Player"){
            getItem();
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponActionController : MonoBehaviour {

    public GameObject bulletPrefab;

    public WeaponInfo weaponInfo { get; set; }
    public EquipmentPart equipmentPart;

	// Use this for initialization
	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Fire() {
        GameObject bullet = Instantiate(bulletPrefab, 
                                        new Vector2(equipmentPart.transform.position.x, 
                                                    equipmentPart.transform.position.y), 
                                                   equipmentPart.transform.rotation);
        Destroy(bullet, 1.0f);
    }

    public void reload(){
        
    }
}

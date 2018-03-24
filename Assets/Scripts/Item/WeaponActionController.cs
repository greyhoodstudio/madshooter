using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponActionController : MonoBehaviour {

    public GameObject bulletPrefab;
    public WeaponInfo weaponInfo;
    public EquipmentRenderer equipmentRenderer;
        
    private Vector2 firePosition;
    private Quaternion fireRotation;
    
    // Use this for initialization
    void Start()
    {
        weaponInfo = GetComponent<WeaponInfo>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Fire() {
        firePosition = new Vector2(equipmentRenderer.transform.position.x, equipmentRenderer.transform.position.y);
        fireRotation = equipmentRenderer.transform.rotation;
        GameObject bullet = Instantiate(bulletPrefab, firePosition, fireRotation);
        Destroy(bullet, weaponInfo.range);
    }

    public void reload(){
        
    }
}

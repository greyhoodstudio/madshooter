using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponActionController : MonoBehaviour {

    public GameObject bulletPrefab;
    public WeaponInfo weaponInfo;
            
    private Vector2 firePosition;
    private Quaternion fireRotation;
    
    // Use this for initialization
    void Start()
    {
        weaponInfo = GetComponent<WeaponInfo>();
        bulletPrefab = Instantiate(Resources.Load("Prefabs/BasicBullet")) as GameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Fire(int bulletId, Vector2 firePosition, Quaternion fireRotation) {
        GameObject bullet = Instantiate(bulletPrefab, firePosition, fireRotation);
        bullet.GetComponent<BulletInfo>().bulletId = bulletId;
        Debug.Log("fire");
        Destroy(bullet, weaponInfo.range);
    }

    public void reload(){
        
    }
}

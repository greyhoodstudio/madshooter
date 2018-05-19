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
        weaponInfo = GetComponentInParent<Inventory>().currentWeapon;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Fire(int bulletId, Vector2 firePosition, Quaternion fireRotation) {

        StartCoroutine(FireCoroutine(bulletId, firePosition, fireRotation));
                
    }
    
    public void reload(){
        
    }

    IEnumerator FireCoroutine (int bulletId, Vector2 firePosition, Quaternion fireRotation)
    {
        bulletPrefab = Resources.Load("Prefabs/BasicBullet") as GameObject;
        GameObject bullet = Instantiate(bulletPrefab, firePosition, fireRotation);
        ClientManager.bulletList.Add(bulletId, bullet.GetComponent<BulletInfo>());

        bullet.GetComponent<BulletInfo>().bulletNum = bulletId;        
        Debug.Log("fire :" + weaponInfo.range);

        yield return new WaitForSeconds(weaponInfo.range);

        ClientManager.bulletList.Remove(bulletId);
        Destroy(bullet, weaponInfo.range);
    }
}

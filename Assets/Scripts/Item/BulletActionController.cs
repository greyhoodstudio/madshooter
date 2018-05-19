using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletActionController : MonoBehaviour {

    public Rigidbody2D myRigidbody2D;
    public BulletInfo bulletInfo;
    
    void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        bulletInfo = GetComponent<BulletInfo>();
    }

    // Use this for initialization
    void Start () {
        myRigidbody2D.velocity = transform.right * bulletInfo.bulletSpeed;
    }
    
    // Update is called once per frame
    void Update () {
		
	}

}

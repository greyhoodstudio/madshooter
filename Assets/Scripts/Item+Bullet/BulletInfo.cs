using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfo : MonoBehaviour {

    public Rigidbody2D myRigidbody2D;
    public float bulSpeed;

    // Use this for initialization
	void Start () {
        myRigidbody2D.velocity = transform.right * Time.deltaTime * bulSpeed;
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}

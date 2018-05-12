using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInfo : MonoBehaviour {

    public int bulletId { get; set; }
    public float bulletSpeed { get; set; }
    public float bulletTime { get; set; }
    public float bulletDamage { get; set; }

    // Use this for initialization
    void Start () {
        // Initialize variables
        bulletSpeed = 18f;
        bulletTime = 4f;
        bulletDamage = 10f;
    }
	
	// Update is called once per frame
	void Update () {
        
    }
}

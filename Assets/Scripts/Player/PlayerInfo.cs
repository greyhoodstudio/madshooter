using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

    public float playerHealth { get; set; }
    public float dodgeDistance { get; set; }
    public float dodgeSpeed { get; set; }

	// Use this for initialization
	void Start () {
        
        // Initialize variables
        playerHealth = 100f;
        dodgeDistance = 10f;
        dodgeSpeed = 1.5f;
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

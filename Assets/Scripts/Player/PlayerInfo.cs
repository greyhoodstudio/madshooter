using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour {

    // Variables
    public int playerId;

    public float playerHealth { get; set; }
    public float dodgeDistance { get; set; }
    public float dodgeSpeed { get; set; }
    
	// Use this for initialization
	void Start () {
        
        // Initialize variables
        playerHealth = 100f;
        dodgeDistance = 10f;
        dodgeSpeed = 1f;
		
	}
	
	// Update is called once per frame
	void Update () {
		if (playerHealth <= 0)
        {
            Debug.Log("Player " + playerId.ToString() + "Dead");
            // TODO 죽는 애니메이션
        }
	}
}

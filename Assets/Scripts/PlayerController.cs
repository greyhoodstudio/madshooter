using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public GameObject player;
    public float movSpeed;
    public Camera mainCamera;
    public GameObject bullet;

    private float axisX;
    private float axisY;
    private Vector2 mouseLocation;

    


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Movement

        axisX = Input.GetAxis("Horizontal");
        axisY = Input.GetAxis("Vertical");

        player.GetComponent<Rigidbody2D>().velocity = new Vector2(axisX * movSpeed, axisY * movSpeed);

        //Rotation

        //player.transform.right = mainCamera.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;

        //Shoot

        if (Input.GetMouseButton(0))
        {
            GameObject newBullet = Instantiate(bullet, player.transform.position, Quaternion.identity);
            newBullet.GetComponent<Rigidbody2D>().velocity = newBullet.transform.forward * 1000;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    //public variables
    public float movSpeed;
    public float rotSpeed;

    //private variables
    private Rigidbody2D playerRigidbody;
    [SerializeField]
    private float axisX;
    [SerializeField]
    private float axisY;  

	// Use this for initialization
	void Start () {
        playerRigidbody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {

        //Movement

        axisX = Input.GetAxis("Horizontal");
        axisY = Input.GetAxis("Vertical");

        if (axisX > 0) axisX = 1;
        else if (axisX < 0) axisX = -1;

        if (axisY > 0) axisY = 1;
        else if (axisY < 0) axisY = -1;

        //Rotation

        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.deltaTime);
   
    }

    private void FixedUpdate() {

        // Update movement

        playerRigidbody.AddForce(new Vector2(axisX * movSpeed - playerRigidbody.velocity.x, axisY * movSpeed - playerRigidbody.velocity.y));
        playerRigidbody.velocity = new Vector2(axisX == 0 ? 0 : playerRigidbody.velocity.x, axisY == 0 ? 0 : playerRigidbody.velocity.y);
        
    }

}

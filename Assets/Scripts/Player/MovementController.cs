using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    // Script References
    public Rigidbody2D playerRigidbody;
    public PlayerActionController playerActionController;
    public PlayerInfo playerInfo;

    // Variables
    public float axisX;
    public float axisY;
    public float movSpeed { get; set; }
    public float rotSpeed { get; set; }

    // Variables for calculation

    public Vector3 mouseDirection { get; set; }
    public Vector3 normalizedMouseDirection { get; set; }
    public Vector3 mousePosition { get; set; }
    public Vector3 fixedTargetPosition { get; set; }

    // Use this for initialization
	void Start () {
        // Initialize references
        playerRigidbody = GetComponent<Rigidbody2D>();
        playerActionController = GetComponent<PlayerActionController>();
        playerInfo = GetComponent<PlayerInfo>();

        // Initialize variables
        movSpeed = 4f;
        rotSpeed = 10f;
	}	
	// Update is called once per frame
	void Update () {

        //Rotation
        mouseDirection = mousePosition - transform.position;
        normalizedMouseDirection = mouseDirection / Vector2.Distance(transform.position, mousePosition);

        float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotSpeed * Time.deltaTime);
   
    }

    private void FixedUpdate() {

        // Update movement

        if (playerActionController.isDodging)
        {
            // Dodge
            playerRigidbody.velocity = new Vector2(0, 0);
            transform.position = Vector2.Lerp(transform.position, fixedTargetPosition, playerInfo.dodgeSpeed * Time.deltaTime);
        }
        else
        {
            // Movement
            playerRigidbody.velocity = new Vector2(axisX*movSpeed, axisY*movSpeed);

            // Activate to enable Super Meat Boy style movement
            //playerRigidbody.AddForce(new Vector2(axisX * movSpeed - playerRigidbody.velocity.x, axisY * movSpeed - playerRigidbody.velocity.y));
            //playerRigidbody.velocity = new Vector2(axisX == 0 ? 0 : playerRigidbody.velocity.x, axisY == 0 ? 0 : playerRigidbody.velocity.y);
        }
    }

}

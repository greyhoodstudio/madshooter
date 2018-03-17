using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour {

    // References
    private Rigidbody2D playerRigidbody;
    private PlayerActionController playerActionController;
    private PlayerInfo playerInfo;

    // public variables
    public float movSpeed { get; set; }
    public float rotSpeed { get; set; }

    // variables for calculation

    private float axisX;
    private float axisY;

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
        movSpeed = 10f;
        rotSpeed = 10f;
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

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
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
            transform.position = Vector2.Lerp(transform.position, fixedTargetPosition, playerInfo.dodgeSpeed * Time.deltaTime);
        }
        else
        {
            playerRigidbody.AddForce(new Vector2(axisX * movSpeed - playerRigidbody.velocity.x, axisY * movSpeed - playerRigidbody.velocity.y));
            playerRigidbody.velocity = new Vector2(axisX == 0 ? 0 : playerRigidbody.velocity.x, axisY == 0 ? 0 : playerRigidbody.velocity.y);
        }
    }

}

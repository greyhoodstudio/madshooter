using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject target;
    public float movSpeed;

    // Use this for initialization
    void Start() {
                
    }

    // Update is called once per frame
    void Update() {

    }

    private void FixedUpdate() {
        // Follow Target
        transform.position = Vector3.Slerp(transform.position, new Vector3(target.transform.position.x, target.transform.position.y, -10), movSpeed * Time.deltaTime);
    }

    public void FollowPlayer(GameObject player)
    {
        target = player;
    }

}

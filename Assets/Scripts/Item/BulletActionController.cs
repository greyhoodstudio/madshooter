using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletActionController : MonoBehaviour {

    public Rigidbody2D myRigidbody2D;
    public BulletInfo bulletInfo;

    [SerializeField]
    private bool isShot;

    // Collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<PlayerInfo>().playerHealth -= bulletInfo.bulletDamage;
            // TODO 투사체 파괴 애니메이션 추가 필요
            // TODO 캐릭터 피격 애니메이션 추가 필요
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isShot = true;
    }

    void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        bulletInfo = GetComponent<BulletInfo>();
    }

    // Use this for initialization
    void Start () {
        isShot = false;
        myRigidbody2D.velocity = transform.right * bulletInfo.bulletSpeed;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

}

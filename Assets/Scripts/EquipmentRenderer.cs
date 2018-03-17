using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentRenderer : MonoBehaviour {

    public SpriteRenderer spriteRenderer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeSprite (Sprite sprite)
    {
        spriteRenderer.sprite = sprite;
    }
}

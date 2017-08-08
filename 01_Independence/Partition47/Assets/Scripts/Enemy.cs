using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start ()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.flipX = true;

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

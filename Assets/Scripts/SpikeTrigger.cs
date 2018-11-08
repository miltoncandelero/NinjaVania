using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrigger : MonoBehaviour {

	// Use this for initialization
	public Transform spawn;
	public SpriteRenderer spriteRenderer;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other) {

        if (!spriteRenderer.enabled) return; //only collide if visible

		if (other.CompareTag("Spike")) transform.position = spawn.position;
	}

	private void OnTriggerStay2D(Collider2D other) {
		//this only happens when you blink onto death.
		if (!spriteRenderer.enabled) return; //only collide if visible

		if (other.CompareTag("Spike")) transform.position = spawn.position;
	}
}

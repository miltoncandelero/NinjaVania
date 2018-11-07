using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrigger : MonoBehaviour {

	// Use this for initialization
	public Transform spawn;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Spike")) transform.position = spawn.position;
	}
}

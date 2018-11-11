using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyPhysicsObject : MonoBehaviour {

	public Vector2 direction;
	public float speed;
	public Rigidbody2D rb;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		direction.Normalize();
		direction *= (speed*Time.deltaTime);
		//transform.position = (Vector2)transform.position + direction;
		rb.MovePosition(rb.position + direction);
	}
}

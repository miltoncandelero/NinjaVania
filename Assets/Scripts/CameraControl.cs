using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {

    public GameObject track;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = new Vector3(5f+(Mathf.Floor(track.transform.position.x / 10f) * 10), 
                                         4.5f+(Mathf.Floor(track.transform.position.y / 9f) * 9),
                                         transform.position.z);
        //14.5
        //5.5
	}
}

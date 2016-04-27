using UnityEngine;
using System.Collections;

public class RotateObject : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		transform.RotateAround (this.transform.position, Vector3.up, 1.0f);
	
	}
}

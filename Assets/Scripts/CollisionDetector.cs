using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CollisionDetector : MonoBehaviour {

	private List<GameObject> leds;

	// Use this for initialization
	void Start () {
		leds = GameObject.Find ("EventSystem").GetComponent<VolumetricLED> ().leds;

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (this.gameObject.name == "Sphere") {
//			transform.position = new Vector3 (1, Mathf.Sin (Time.time)+3, 1);			
		}

		transform.RotateAround (this.transform.position, Vector3.up, 1.0f);
		Bounds bounds = this.GetComponent<MeshCollider> ().bounds;

		//loop through all the points in the scene
		foreach (GameObject led in leds) {
//			emitRay (led);
			if (bounds.Contains (led.transform.position)) {
				Material containmentMaterial = led.GetComponent<Renderer> ().material;
				containmentMaterial.color = new Color (0.0f, 1.0f, 0.0f, 1.0f);
//				setupPlanes (led);
				emitRay (led);
			} 	
		}
	}

//	void OnTriggerEnter (Collider collision){
//		print (collision.gameObject.name);
//		Material collisionMaterial = collision.gameObject.GetComponent<Renderer> ().material;
//		collisionMaterial.color = new Color(1,0,0);
//		collisionMaterial.SetColor ("_EmissionColor", Color.white);
//	}

//	void OnTriggerExit (Collider collision){
//		Material collisionMaterial = collision.gameObject.GetComponent<Renderer> ().material;
//		collisionMaterial.color = new Color(1,1,1);
//		collisionMaterial.SetColor ("_EmissionColor", Color.white);
//	}

	void emitRay(GameObject _led){
		Vector3 forwardVector = _led.transform.TransformVector (Vector3.forward*-1);
		RaycastHit[] hits;
		hits = Physics.RaycastAll (_led.transform.position, forwardVector, 10.0f);

//		for (int i = 0; i < hits.Length; i++) {
//			Debug.DrawLine (_led.transform.position, hits [i].point);
//		}
//
//		if (hits.Length > 1) {
//			print ("Multiple Hits: " + hits.Length);
//		}

		if (hits.Length % 2 == 0) {
			Material containmentMaterial = _led.GetComponent<Renderer> ().material;
			containmentMaterial.color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
		} else {
			Material containmentMaterial = _led.GetComponent<Renderer> ().material;
			containmentMaterial.color = new Color (1.0f, 0.0f, 0.0f, 1.0f);
		}
	}


	//PLANE INTERSECTION APPROACH

	void setupPlanes(GameObject _led){
		Mesh mesh = this.GetComponent<MeshFilter> ().mesh;
		int[] indices = mesh.GetIndices (0);
		Vector3[] vertices = mesh.vertices;	

		//iterate over vertices every 3 index point
		for (int i = 0; i < indices.Length; i+=3) {
			if (rayIntersectsTriangle (this.transform.position, Vector3.forward, vertices [indices [i]], vertices [indices [i + 1]], vertices [indices [i + 2]])) {
				Material containmentMaterial = _led.GetComponent<Renderer> ().material;
				containmentMaterial.color = new Color (1.0f, 0.0f, 0.0f, 1.0f);
			} else {
				Material containmentMaterial = _led.GetComponent<Renderer> ().material;
				containmentMaterial.color = new Color (1.0f, 1.0f, 1.0f, 0.5f);
			}
		}
	}


	bool rayIntersectsTriangle(Vector3 p, Vector3 d, Vector3 v0, Vector3 v1, Vector3 v2) {

		Vector3 edge1,edge2,pvec,tvec,qvec;
		float a,f,u,v,t;
		edge1 = v1 -v0;
		edge2 = v2 - v0;
		pvec = Vector3.Cross(d,edge2);
		a = Vector3.Dot(edge1,pvec);

		if (a > -0.00001 && a < 0.00001)
			return false;

		f = 1/a;

		tvec = p -v0 ;

		u = f * Vector3.Dot(tvec,pvec);

		if (u < 0.0 || u > 1.0)
			return false;

		qvec = Vector3.Cross(tvec,edge1);
		v = f * Vector3.Dot(d,qvec);

		if (v < 0.0 || u + v > 1.0)
			return false;

		// at this stage we can compute t to find out where
		// the intersection point is on the line
		t = f * Vector3.Dot(edge2,qvec);

		if (t > 0.00001){
			return true;

		}else{
			return false;
		}

	}



}

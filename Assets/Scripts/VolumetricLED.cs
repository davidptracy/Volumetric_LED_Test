using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VolumetricLED : MonoBehaviour {

	public int rows;
	public int cols;
	public int depth;

	public float rowSpacing;
	public float colSpacing;
	public float depthSpacing;

	public float ledSize;

	public List<GameObject> leds;


	// Use this for initialization
	void Awake () {

		leds = new List<GameObject>();

//		rows = 30;
//		cols = 30;
//		depth = 30;

//		rowSpacing = .05f;
//		colSpacing = .05f;
//		depthSpacing = .05f;

//		ledSize = .0025f;

		for (float i = 0; i < rows; i++) {
			for (float j = 0; j < cols; j++) {
				for (float k = 0; k < depth; k++) {

					GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);

					Destroy (cube.GetComponent<Collider> ());

//					Rigidbody cubePhysics = cube.AddComponent<Rigidbody> ();
//					cubePhysics.useGravity = false;

//					cube.GetComponent<BoxCollider> ().isTrigger = true;
//					cube.GetComponent<SphereCollider> ().isTrigger = true;

					Material cubeMaterial = new Material (Shader.Find ("Standard"));

					cubeMaterial.SetFloat("_Mode", 2);
					cubeMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
					cubeMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
					cubeMaterial.SetInt("_ZWrite", 0);
					cubeMaterial.DisableKeyword("_ALPHATEST_ON");
					cubeMaterial.EnableKeyword("_ALPHABLEND_ON");
					cubeMaterial.DisableKeyword("_ALPHAPREMULTIPLY_ON");
					cubeMaterial.renderQueue = 3000;

					cubeMaterial.color = new Color (1.0f,1.0f,1.0f,0.5f);

					cube.GetComponent<Renderer> ().material = cubeMaterial;
					cube.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.white);

					//position and scale of leds
					cube.transform.position = new Vector3 (i*rowSpacing,j*colSpacing+2,k*depthSpacing);
					cube.transform.localScale = new Vector3(ledSize/2,ledSize/2,ledSize/2);

					//add them to the list
					leds.Add (cube);
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}

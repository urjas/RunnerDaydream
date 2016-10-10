using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CubeManager : MonoBehaviour {

	// Use this for initialization
	public GameObject cube;
	private Transform playerTransform;
	private float maxDistance = 10.0f;
	private int amnCubesOnScreen = 7;
	private List<GameObject> activeCubes;
	private float distance;
	private float spawnZ = -10.0f;
	private float xAxis, yAxis;

	void Start () {
		activeCubes = new List<GameObject> ();
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		for (int i = 0; i < amnCubesOnScreen; i++) {
			if (i < 2)
				SpawnCube ();
			else
				SpawnCube ();
		}
	
	}
	
	// Update is called once per frame
	void Update () {
		distance = Random.Range (1, maxDistance);
		if(playerTransform.position.z  > ( spawnZ - amnCubesOnScreen * distance )) {
			SpawnCube();
		}
		if(activeCubes[0].transform.position.z < playerTransform.position.z) {
			DeleteCube ();
		}
	
	}

	private void SpawnCube() {
		GameObject go;
		go = Instantiate (cube) as GameObject;
		go.transform.SetParent (transform);
		xAxis = Random.Range (-4.0f,4.0f);
		yAxis = Random.Range (3.0f,5.0f);
		Vector3 position = new Vector3 (xAxis,yAxis,spawnZ);
		go.transform.position = position;
		spawnZ += distance;
		Debug.Log (spawnZ);
		activeCubes.Add (go);
	}

	private void DeleteCube() {
		Destroy (activeCubes [0]);
		activeCubes.RemoveAt (0);
	}
}

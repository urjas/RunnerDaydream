using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CubeController : MonoBehaviour {

	// Use this for initialization
	public GameObject cube;
	private Transform playerTransform;
	private float maxDistance = 10.0f;
	private int amnCubesOnScreen = 7;
	private List<GameObject> activeCubes;
	private float distance;
	private float spawnZ = -10.0f;
	private float xAxis, yAxis;
	public GameObject controllerPivot;
	public Material cubeInactiveMaterial;
	public Material cubeHoverMaterial;
	private Renderer controllerCursorRenderer;
	private GameObject selectedObject;
	private int gameScore;

	void Start () {
		activeCubes = new List<GameObject> ();
		gameScore = 0;
		playerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		for (int i = 0; i < amnCubesOnScreen; i++) {
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
			DeleteCube (0);
		}
		if (GvrController.State != GvrConnectionState.Connected) {
			controllerPivot.SetActive(false);
		}
		controllerPivot.SetActive(true);
		controllerPivot.transform.rotation = GvrController.Orientation;

		RaycastHit hitInfo;
		Vector3 rayDirection = GvrController.Orientation * Vector3.forward;
		float startRay=GameObject.FindGameObjectWithTag ("Player").transform.position.z;
		if (Physics.Raycast(new Vector3(0,0,startRay), rayDirection, out hitInfo)) {
			if (hitInfo.collider.tag=="Target" && hitInfo.collider.gameObject) {
				SetSelectedObject(hitInfo.collider.gameObject);
			}
		} else {
			SetSelectedObject(null);
		}
		if (GvrController.TouchDown && selectedObject != null && activeCubes.Count > 0) {
			DeleteCube (activeCubes.IndexOf(selectedObject));
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
		//Debug.Log (spawnZ);
		activeCubes.Add (go);
	}

	private void DeleteCube(int index) {
		if (index!=0) {
			gameScore++;
			//Debug.Log (index);
		} 

		Destroy (activeCubes [index]);
		activeCubes.RemoveAt (index);
	}
	private void SetSelectedObject(GameObject obj) {
		if (null != selectedObject) {
			selectedObject.GetComponent<Renderer>().material = cubeInactiveMaterial;
		}
		if (null != obj) {
			obj.GetComponent<Renderer>().material = cubeHoverMaterial;
		}
		selectedObject = obj;
	}

	void OnGUI(){
		GUI.Label(new Rect(20,20,50,50),gameScore.ToString());
	}
}

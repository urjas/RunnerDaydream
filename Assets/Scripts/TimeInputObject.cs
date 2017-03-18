using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class TimeInputObject : MonoBehaviour, TimedInputHandler {

	// Use this for initialization
	void Start () {
		GetComponent<Renderer> ().material.color = Color.white;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void TimedInputStart() {
		GetComponent<Renderer> ().material.color = Color.grey;
	}

	public void HandleTimedInput() {
		GetComponent<Renderer> ().material.color = Color.blue;
		//GameObject.Destroy (GameObject.FindGameObjectWithTag("Play"));
		SceneManager.LoadScene ("runnercube", LoadSceneMode.Single);
	}
}

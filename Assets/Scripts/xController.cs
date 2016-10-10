using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocketIO;

public class xController : MonoBehaviour {

	// Use this for initialization
	private float pos; 
	private Vector3 moveVector;
	SocketIOComponent socket;
	GameObject go;
	private CharacterController player;
	public float sens;

	void Start () {
		go = GameObject.Find("SocketIO");
		socket= go.GetComponent<SocketIOComponent>();
		player = GetComponent<CharacterController> ();
		socket.On ("datarec",(SocketIOEvent obj) => {
			pos=float.Parse(obj.data.ToString().Substring(7,7));
			Debug.Log(pos);
			Vector3 nwpos=new Vector3(pos*sens,0,0);
			Vector3 posi = transform.TransformDirection(nwpos);
			player.Move(posi* Time.deltaTime);
		});
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

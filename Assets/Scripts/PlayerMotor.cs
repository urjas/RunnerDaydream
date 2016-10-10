using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SocketIO;

public class PlayerMotor : MonoBehaviour {
	private CharacterController controller;
	private Vector3 moveVector;
	private float pos; 
	SocketIOComponent socket;
	GameObject go;
	public float sens;
	public float speed =5.0f;
	private float gravity = 200.0f;
	private float animationDuration = 2.0f;
	//private Actions actions;
	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController>();
		go = GameObject.Find("SocketIO");
		socket= go.GetComponent<SocketIOComponent>();
		socket.On ("datarec",(SocketIOEvent obj) => {
			pos=float.Parse(obj.data.ToString().Substring(7,7));
			Debug.Log(pos);
			Vector3 nwpos=new Vector3(pos*sens,0,0);
			Vector3 posi = transform.TransformDirection(nwpos);
			controller.Move(posi* Time.deltaTime);
		});
		//actions = GetComponent<Actions>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time < animationDuration) {
			//controller.Move (Vector3.forward * speed * Time.deltaTime);
			return;
		}

		moveVector = Vector3.zero;
		if (controller.isGrounded) {
			//print("Grounded");
			moveVector.y = 0.0f;
		} else {
			//print ("Falling");
			moveVector.y -= gravity * Time.deltaTime;
		}
		//moveVector.x = Input.GetAxisRaw ("Horizontal") * speed;
		//print(moveVector.x);
		moveVector.z = speed;
		controller.Move (moveVector * Time.deltaTime);
		//actions.Run();

	}

}

using UnityEngine;
using UnityEngine.UI;

public class ControllerManager : MonoBehaviour {
  public GameObject controllerPivot;
  public Material cubeInactiveMaterial;
  public Material cubeHoverMaterial;
  
  private Renderer controllerCursorRenderer;

  // Currently selected GameObject.
  private GameObject selectedObject;

 
  void Update() {
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
		//Debug.Log (selectedObject);
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

  /*private void StartDragging() {
    dragging = true;
    selectedObject.GetComponent<Renderer>().material = cubeActiveMaterial;

    // Reparent the active cube so it's part of the ControllerPivot object. That will
    // make it move with the controller.
    selectedObject.transform.SetParent(controllerPivot.transform, true);
  }

  private void EndDragging() {
    dragging = false;
    selectedObject.GetComponent<Renderer>().material = cubeHoverMaterial;

    // Stop dragging the cube along.
    selectedObject.transform.SetParent(null, true);
  }*/

 
}

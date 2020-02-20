using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetector : MonoBehaviour {

    public LayerMask layer;
	public bool activated;

	private void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.GetComponent<GroundActivator> () != null) {
			col.GetComponent<GroundActivator> ().Activate ();
            activated = true;
		}
	}

	private void OnTriggerStay2D(Collider2D col)
	{
		if (col.gameObject.GetComponent<GroundActivator> () != null) {
            if (Input.GetKey (KeyCode.DownArrow)) {
                col.GetComponent<GroundActivator>().Deactivate();
                Debug.Log("Pressed");
			}
		}
	}

	private void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.GetComponent<GroundActivator> () != null) {
			col.GetComponent<GroundActivator> ().Deactivate ();
            activated = false;
		}
	}
}

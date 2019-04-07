using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControlPlatforming : MonoBehaviour {

	public Transform playerTransform;
	public float speed = 10f;

	public float yOffset, xOffset;



	void FixedUpdate () {

		Vector3 positionToMove;
		positionToMove.z = -10;//So the camera actually renders the level
		positionToMove.x = playerTransform.position.x + xOffset;
		positionToMove.y = playerTransform.position.y + yOffset;

		transform.position = Vector3.Lerp (transform.position, positionToMove, speed * Time.deltaTime);


	}


}

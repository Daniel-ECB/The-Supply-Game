using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour {

	private bool hasCompletedMovCycle = false;
	private float minDistanceToTarget = 0.001f;

	public float movSpeed = 1f;
	public Transform[] destinations;

//	[Header("References")]
	public Rigidbody2D rb2d;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (MovingPlayer ());
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(hasCompletedMovCycle)
		{
			StartCoroutine (MovingPlayer ());
		}
	}

	/// <summary>
	/// Takes the player to the specified destinations.
	/// </summary>
	/// <returns>The obstacle movement.</returns>
	IEnumerator MovingPlayer()
	{
		// Moving the player towards its next destination
		foreach (Transform destination in destinations) 
		{
			float sqrRemainingDistance = (transform.position - destination.position).sqrMagnitude;

			// I used minDistanceToTarget bc float.Epsilon is so small that will always move the position
			while (sqrRemainingDistance > minDistanceToTarget)
			{
				// Vector3.MoveTowards moves a point in a straight line from beginning to end, which is (movSpeed * Time.deltaTime) 
				// units closer to our destination
				Vector3 newPosition = Vector3.MoveTowards (transform.position, destination.position, movSpeed * Time.deltaTime);
				rb2d.MovePosition (newPosition); // Simply moves a rigidody
				// rb.MovePosition(destination); // Using this line only teleports the player between positions
				sqrRemainingDistance = (transform.position - destination.position).sqrMagnitude;
				yield return null; // This makes us wait a frame before the next loop
			}
		}

		hasCompletedMovCycle = true;
	}
}

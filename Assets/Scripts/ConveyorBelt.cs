﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ConveyorBelt : MonoBehaviour {

	// Fields
	public GameObject gearUnit;
	public float rotationSpeed = -45f;
	public int numberOfGears = 2;
	public BoxCollider2D beltCollider;

	private List<Transform> beltUnitsRef;
	private bool wasReset = true; // Just as a safety measure

	// Some deprecated vars
//	This number and the lenght of beltUnits must match, otherwise an index out of range error message in console,
//	I'll fix this later
//	public Transform[] beltUnits; // Now I'm using a list which is more efficient
//	public bool createBelt = false;

	void Awake()
	{
		beltUnitsRef = new List<Transform> ();
	}

	// Use this for initialization
	void Start () 
	{
		// Create the belt, we'll later worry about creating it not in run time
//		if(createBelt)
//		{
//			GenerateBelt (); // We are now using this function via a button
//		}
	}

	/// <summary>
	/// Generates the Conveyor Belt.
	/// </summary>
	public void GenerateBelt()
	{
		// Preventing several belts on top of each other
		if (!wasReset) 
		{
			Debug.Log ("Reset the belt before generating it again");
			return;
		}

		wasReset = false;

		// To place each unit correctly
		float xGearOffset = gearUnit.transform.localScale.x;
		Vector3 gearOffset = new Vector3 (xGearOffset, 0f, 0f);

//		Debug.Log ("Is beltUnits a fixed lenght array: " + beltUnits.IsFixedSize); // Turned out to be true

		// Creating and referencing each gear
		for (int i = 0; i < numberOfGears; i++) 
		{
			GameObject newGear = Instantiate (gearUnit, transform.position + gearOffset * i, Quaternion.identity) as GameObject;
			newGear.transform.SetParent (gameObject.transform);
//			beltUnits [i] = newGear.transform;
			beltUnitsRef.Add(newGear.transform);
		}

		// Sizing the collider
		float xColliderSize = numberOfGears * gearUnit.transform.localScale.x;
		Vector2 colliderSize = new Vector2 (xColliderSize, gearUnit.transform.localScale.y);
		beltCollider.size = colliderSize;

		// Placing the collider
		// Remember numberOfGears is an int and Vectors use floats, to avoid rounding down the int
		float soItWorks = numberOfGears - 1; 
		float xColliderOffset = soItWorks / 2 * gearUnit.transform.localScale.x;
		Vector2 colliderOffset = new Vector2 (xColliderOffset, 0f);
		beltCollider.offset = colliderOffset;

		// Get it to work after creating
		beltCollider.enabled = true;
	}

	/// <summary>
	/// Resets the Conveyor Belt to leave room for another one.
	/// </summary>
	public void ResetBelt()
	{
		// No need to eliminate empty stuff
		if(wasReset)
		{
			Debug.Log ("This belt was already reset, you may generate a new one");
			return;
		}

		wasReset = true;

		// Destroy the references
		for (int i = 0; i < beltUnitsRef.Count; i++) 
		{
//			Destroy (beltUnits[i]);	// This one dosn't work in edit mode
			DestroyImmediate (beltUnitsRef[i].gameObject, false);
		}

		beltUnitsRef.Clear ();

		// To avoid collisions with invisible belts
		beltCollider.enabled = false;
	}

	// Update is called once per frame
	void Update () 
	{
		// Rotate each unit of the conveyor belt
//		for (int i = 0; i < transform.childCount; i++) 
//		{
////			Debug.Log ("Child name: " + beltUnits[i].name);
//			beltUnits[i].Rotate(Vector3.back * Time.deltaTime * rotationSpeed);
//		}

		for (int i = 0; i < beltUnitsRef.Count; i++) 
		{
			beltUnitsRef[i].Rotate(Vector3.back * Time.deltaTime * rotationSpeed);
		}
	}
}

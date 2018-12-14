using UnityEngine;
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

	// Use this for initialization
	void Awake()
	{
		// Creating the reference
		beltUnitsRef = new List<Transform> ();
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

		// Creating and referencing each gear
		for (int i = 0; i < numberOfGears; i++) 
		{
			GameObject newGear = Instantiate (gearUnit, transform.position + gearOffset * i, Quaternion.identity) as GameObject;
			newGear.transform.SetParent (gameObject.transform);
			beltUnitsRef.Add(newGear.transform);
		}

		// Sizing the collider
		float xColliderSize = numberOfGears * gearUnit.transform.localScale.x;
		Vector2 colliderSize = new Vector2 (xColliderSize, gearUnit.transform.localScale.y);
		beltCollider.size = colliderSize;

		// Placing the collider, remember numberOfGears is an int and Vectors use floats, to avoid rounding down the int
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
		for (int i = 0; i < beltUnitsRef.Count; i++) 
		{
			beltUnitsRef[i].Rotate(Vector3.back * Time.deltaTime * rotationSpeed);
		}
	}
}

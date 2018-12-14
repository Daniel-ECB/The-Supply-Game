using UnityEngine;

public class Goal : MonoBehaviour {

	#region Fields

	public float detectRate = 0.25f;
	public float cargoGoalDelay = 1f;
	public Collider2D goalCollider;
	public GameManager gameManager;
	public Collider2D tempCargoTest;

	private Collider2D cargoCollider;
	private Cargo cargoScript;
	private bool isCargoInside = false;

	#endregion

	#region Properties

	/// <summary>
	/// Gets the reference to the cargo.
	/// </summary>
	/// <value>The Cargo.</value>
	public Cargo getCargo
	{
		get 
		{
//			Debug.Log ("Getting the reference to the cargo");
			return cargoScript;
		}
	}

	#endregion

	#region Methods

	/// <summary>
	/// Unity API. Raises the trigger enter 2d event.
	/// </summary>
	/// <param name="coll">Collider inforation.</param>
	void OnTriggerEnter2D(Collider2D coll)
	{
		// Detecting cargo inside goal
		if (coll.gameObject.CompareTag ("Cargo")) 
		{
			// Using my method
			cargoScript = coll.gameObject.GetComponent<Cargo> ();
//			InvokeRepeating ("CargoInside", 0f, detectRate);

			// Using the ColliderDistance2D info
			cargoCollider = coll;
			InvokeRepeating ("DetectCargo", 0f, detectRate);
		} 
	}

	/// <summary>
	/// Unity API. Raises the trigger exit 2d event.
	/// </summary>
	/// <param name="coll">Collider inforation.</param>
	void OnTriggerExit2D(Collider2D coll)
	{
//		if(coll.gameObject.CompareTag ("Player"))
//		{
//			Debug.Log ("Player has left the goal");
//		}

		// The cargo has left the goal
		if (coll.gameObject.CompareTag ("Cargo")) 
		{
			gameManager.CancelInvoke ("WereCargoesDelivered");
			gameManager.LevelNotEnded ();

			CancelInvoke ("DetectCargo");
			cargoScript = null;
			cargoCollider = null;
		} 
	}

	/// <summary>
	/// Detects if the cargo is inside the goal.
	/// </summary>
	void DetectCargo()
	{
//		Debug.Log ("We'll detect the cargo");

		// Ensuring the cargo is completely inside the goal, -1f bc the cargo is 1f in all axis in scale
		ColliderDistance2D _distance = goalCollider.Distance (cargoCollider);
//		Debug.Log ("_distance.distance: " + _distance.distance);

		if(_distance.distance < -cargoScript.goalThreshold)
		{
//			Debug.Log ("Cargo is inside the goal");
			gameManager.Invoke("WereCargoesDelivered", cargoGoalDelay);
		}
		else if(_distance.distance >= -cargoScript.goalThreshold)
		{
//			Debug.Log ("Cargo not completely inside goal");
			gameManager.CancelInvoke ("WereCargoesDelivered");
			gameManager.LevelNotEnded ();
		}
	}

	/// <summary>
	/// Checks if the cargo is inside the goal.
	/// </summary>
	void CargoInside()
	{
//		Debug.Log ("Checking for cargo inside goal");
		isCargoInside = cargoScript.IsCargoInsideGoal ();
	}

	#endregion
}

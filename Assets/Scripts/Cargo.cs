using UnityEngine;

public class Cargo : MonoBehaviour {

	public Rigidbody2D rb2d;

//	public float halfVerSize;
//	public float halfHorSize;

	[Header("Joint Points")]
	public Transform hookColliderPoint;
	public Transform anchorPoint;

	public float distanceFromChainEnd = 0.6f;

	[Header("Goal Checks")]
	public GameObject pointAObj; // Their positions need a little offset from the edges of the GO so they don't fall 
	public GameObject pointBObj; // outside the goal when the cargo is close to the edges of the goal
	public float goalThreshold = 1.01f;

	/// <summary>
	/// Checks if the cargo is inside the goal.
	/// </summary>
	/// <returns><c>true</c>, if the cargo was inside, <c>false</c> otherwise.</returns>
	public bool IsCargoInsideGoal()
	{
		bool pointAInside = false;
		bool pointBInside = false;
		Collider2D goalCollider;

		//Debug.Log ("PointCollide called");
//		Vector3 thePoint = new Vector3 (gameObject.transform.position.x - halfHorSize, 
//			gameObject.transform.position.y - halfVerSize);
		//Debug.Log (thePoint);

		// Checking if point A is inside the goal
		goalCollider = Physics2D.OverlapPoint (pointAObj.transform.position, 1 << LayerMask.NameToLayer("Goal"));
		//Debug.Log (goalCollider); // It collides with the cargo itself if I don't check the layer

		if(goalCollider != null && goalCollider.gameObject.CompareTag("Goal"))
		{
			//Debug.Log ("Point A colliding");
			pointAInside = true;
		}

		// Checking if point B is inside the goal
		goalCollider = Physics2D.OverlapPoint (pointBObj.transform.position, 1 << LayerMask.NameToLayer("Goal"));

		if(goalCollider != null && goalCollider.gameObject.CompareTag("Goal"))
		{
			//Debug.Log ("Point B colliding");
			pointBInside = true;
		}

		return pointAInside && pointBInside;
	}

	/// <summary>
	/// Connects the rope end to the cargo.
	/// </summary>
	/// <param name="endRB2D">Rigidbody2D of the end of the rope.</param>
	public void ConnectRopeEnd(Rigidbody2D endRB2D)
	{
		DistanceJoint2D joint = gameObject.AddComponent<DistanceJoint2D> ();

		joint.connectedBody = endRB2D;

		// So unity doesn't calculate this automatically
		joint.autoConfigureDistance = false;
		joint.autoConfigureConnectedAnchor = false;

		joint.anchor = anchorPoint.localPosition;
//		joint.connectedAnchor = new Vector2 (0f, -distanceFromChainEnd);
		joint.connectedAnchor = Vector2.zero;
		joint.distance = distanceFromChainEnd;
	}

	/// <summary>
	/// Removes the rope end.
	/// </summary>
	public void RemoveRopeEnd()
	{
		DistanceJoint2D joint = GetComponent<DistanceJoint2D> ();

		// If we don't check this we will get a null reference exception when we 
		// deliver the cargo to the goal and destroy the rope and the cargo, 
		// only when we call WereCargoesDelivered but not in LevelEnd, I don't
		// know why this only happens in one of those methods bc I use the exact
		// same lines of code. Later, somehow this bug dissapeared on its own
		if(joint != null)
		{
			Destroy (joint);
		}
	}
}

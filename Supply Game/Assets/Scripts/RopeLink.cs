using UnityEngine;

public class RopeLink : MonoBehaviour {

	public float breakRopeDelay = 0.5f;

	/// <summary>
	/// Unity API, raises the joint break 2d event. Eliminates the rope after it broke.
	/// </summary>
	/// <param name="brokenJoint">The Broken joint.</param>
	private void OnJointBreak2D (Joint2D brokenJoint)
	{
//		Debug.Log ("A joint has been broken");

		// Eliminate rope elements related to the player
		Transform playerTransform = gameObject.transform.parent;
		Player playerScript = playerTransform.gameObject.GetComponent<Player> ();
		playerScript.Invoke ("BreakRope", breakRopeDelay);

		// And to the cargo. Removed, we already do this in BreakRope()
//		playerScript.cargoScript.RemoveRopeEnd ();
	}
}

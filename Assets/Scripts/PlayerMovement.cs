using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[Header("Movement Support")]
	public float movSpeed;
	public float tilt;
	private float horMovement;
	private float verMovement;
	private Vector2 playerMov;

	public Rigidbody2D rb2d;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Getting movement input
		horMovement = Input.GetAxisRaw ("Horizontal");
		verMovement = Input.GetAxisRaw ("Vertical");
	}

	// FixedUpdate is called once per Physics update
	void FixedUpdate()
	{
		// Failed attemp to keep altitude when carrying a cargo, it will bounce from side to side
		// regardless of its mass (will remain grounded if too high) and rope lenght
//		if(rb2d.velocity.y < 0 && (horMovement == 0 && verMovement == 0))
//		{
//			Debug.Log ("We are falling");
//
//			float verticalVel = Mathf.Abs (rb2d.velocity.y);
//			Vector2 verticalStabilization = new Vector2 (0, verticalVel);
//
//			rb2d.velocity += verticalStabilization;
//		}

		// Moving the player
		if(horMovement != 0 || verMovement != 0)
		{
			playerMov = new Vector2 (horMovement, verMovement);
			rb2d.AddForce(playerMov * movSpeed * Time.deltaTime, ForceMode2D.Impulse);
		}

		// Slighly rotate the ship when moving sideways, depending on how fast we move, 
		// -tilt to regulate the rotation and show it in the correct direction. I never
		// got it working properly
//		if(horMovement != 0)
//		{
//			rb2d.AddTorque (rb2d.velocity.x * -tilt);
//		}

		// Never moved the player at all
//		rb2d.MoveRotation(rb2d.velocity.x + -tilt * Time.fixedDeltaTime);

		// I don't know why this doesn't work
//		rb2d.rotation = Mathf.Clamp (rb2d.rotation, -15f, 15f);

//		rb2d.rotation is a float, Quaternion.Euler returns a Quaternion
//		rb2d.rotation = Quaternion.Euler (0.0f, 0.0f, rb2d.velocity.x * -tilt);
	}
}

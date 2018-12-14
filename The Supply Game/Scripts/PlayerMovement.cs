using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[Header("Movement Support")]
	public float movSpeed;
	public float tilt;
	private float horMovement;
	private float verMovement;
	private Vector2 playerMov;

	public Rigidbody2D rb2d;
	
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
		// Moving the player
		if(horMovement != 0 || verMovement != 0)
		{
			playerMov = new Vector2 (horMovement, verMovement);
			rb2d.AddForce(playerMov * movSpeed * Time.deltaTime, ForceMode2D.Impulse);
		}
	}
}

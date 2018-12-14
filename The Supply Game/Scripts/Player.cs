using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {

	[Header("Rope Grapple")]
	public Rigidbody2D firstRopeLink; // reference to the previous element of the chain
	public GameObject linkPrefab;
	public int links = 7; // Number of links in the chain
	public Vector3 hookShootOffset = new Vector3(0f, -0.1f, 0f);

	private float firstLinkDistance = 0.25f;
	private GameObject[] ropeLinks;
	private bool isThereARope = false;

	[Header("Tractor Beam")]
	public bool useTractorBeam = false;
	public float hookRange = 3.5f;
	public DistanceJoint2D dJoint2D;
	public LineRenderer lineRenderer;
	public GameObject hookPosition;

	private CircleCollider2D playerArea;

	// References of the current cargo
	private Collider2D hookTargetCollider;
	[HideInInspector]
	public Cargo cargoScript;

	void Awake()
	{
		// Just as a reference
		playerArea = GetComponent<CircleCollider2D>();
	}

	// Use this for initialization
	void Start () 
	{
		// Where we fire the hook from
		dJoint2D.anchor = hookPosition.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Firing the grapple
		if(Input.GetButtonDown("Fire1"))
		{
			if(useTractorBeam && dJoint2D.connectedBody == null)
			{
				ShootGrapple();
			}

			if(!useTractorBeam && !isThereARope)
			{
				GenerateRope ();
			}
		}

		// Releasing the cargo
		if(Input.GetButtonDown("Fire2"))
		{
			// In case there's a tractor beam in use
			if(dJoint2D.connectedBody != null)
			{
				ReleaseCargo ();
			}

			// In case there's a rope in use
			if(isThereARope)
			{
				BreakRope ();
			}
		}

		// Updating the grapple
		if (lineRenderer.enabled) 
		{
			// Updating line itself
			lineRenderer.SetPosition (0, hookPosition.transform.position);
			lineRenderer.SetPosition (1, cargoScript.anchorPoint.position);
		}
	}

	/// <summary>
	/// Shoots the grapple.
	/// </summary>
	void ShootGrapple()
	{
//		Debug.Log ("Hook has been fired");

		// Making sure we have a cargo to grab
		hookTargetCollider = Physics2D.OverlapCircle(transform.position, hookRange, 1 << LayerMask.NameToLayer("Cargo"));
//		Debug.Log("Collider.name: " + hookTargetCollider.name);
//		Debug.Log ("Hook target position: " + hookTargetCollider.gameObject.transform.position);

		// Setting and grabbing the target
		if(hookTargetCollider != null)
		{
			// Making sure there is nothing between the grapple and the anchor point. I have to deactivate temporarely the
			// CircleCollider2D attatched to the player so the Linecast doesn't collide with the player itself
			cargoScript = hookTargetCollider.gameObject.GetComponent<Cargo> ();
			Vector2 _anchorPoint = cargoScript.anchorPoint.localPosition;

			playerArea.enabled = false;
			RaycastHit2D rayHit = Physics2D.Linecast (hookPosition.transform.position, cargoScript.anchorPoint.position);
			playerArea.enabled = true;

			if(rayHit.collider != null && !rayHit.transform.CompareTag("Cargo"))
			{
//				Debug.Log ("rayHit fraction: " + rayHit.fraction + ". rayHit collider: " + rayHit.collider);
//				Debug.Log ("rayHit object tag: " + rayHit.transform.tag); 				
				return;
			}

			// Connecting the target
			dJoint2D.enabled = true;
			dJoint2D.connectedBody = hookTargetCollider.attachedRigidbody;						
			dJoint2D.connectedAnchor = _anchorPoint;

			// Visual feedback of the grapple
			lineRenderer.enabled = true;
		}
	}

	/// <summary>
	/// Generates the rope.
	/// </summary>
	void GenerateRope()
	{
		// Making sure we have a cargo
		hookTargetCollider = Physics2D.OverlapCircle(transform.position, hookRange, 1 << LayerMask.NameToLayer("Cargo"));

		if (hookTargetCollider != null) 
		{
			// Making sure there is nothing between the grapple and the anchor point. I have to deactivate temporarely the
			// CircleCollider2D attatched to the player so the Linecast doesn't collide with the player itself, I added a 
			// little offset to the shooting point so that the hook also doesn't collide with the player itself.
			cargoScript = hookTargetCollider.gameObject.GetComponent<Cargo> ();

			playerArea.enabled = false;
			RaycastHit2D rayHit = Physics2D.Linecast (hookPosition.transform.position + hookShootOffset, 
				cargoScript.anchorPoint.position);
			playerArea.enabled = true;

			// Don't shoot the rope if there is something between the player and the Cargo, ignoring of
			// course cargoes and goals
			if(rayHit.collider != null && 
				(!rayHit.transform.CompareTag("Cargo") && !rayHit.transform.CompareTag("Goal")))
			{
				Debug.Log ("There is something between the player and the cargo: " + rayHit.transform.name);
				return;
			}
		} 
		else
		{
			Debug.Log ("No cargo within range");
			return;
		}

		// Reference for destroying them
		ropeLinks = new GameObject[links];

		// Connecting the rope
		Rigidbody2D previousRB2D = firstRopeLink; // The one sitting on top of the rope

		// Each element of the rope
		for (int i = 0; i < links; i++) 
		{
			GameObject link = Instantiate (linkPrefab, transform) as GameObject; // Parent each link element of the chain to the rope
			DistanceJoint2D joint = link.GetComponent<DistanceJoint2D>();
			joint.connectedBody = previousRB2D;

			if(i == 0)
			{
				joint.connectedAnchor = hookPosition.transform.localPosition;
				joint.distance = firstLinkDistance;
			}

			// Connect each element of the rope to the previous element, and connect the last element to the weight
			if (i < links - 1) 
			{
				previousRB2D = link.GetComponent<Rigidbody2D> ();				
			} 
			else 
			{
				cargoScript.ConnectRopeEnd (link.GetComponent<Rigidbody2D> ());
			}

			// Referencing
			ropeLinks.SetValue(link, i);
		}

		isThereARope = true;
	}

	/// <summary>
	/// Releases the cargo.
	/// </summary>
	void ReleaseCargo()
	{
		//Debug.Log ("Release cargo: " + hookTargetCollider.attachedRigidbody);
		lineRenderer.enabled = false;

		dJoint2D.connectedBody = null;
		dJoint2D.enabled = false;
	}

	/// <summary>
	/// Breaks the rope.
	/// </summary>
	public void BreakRope()
	{
//		Debug.Log ("Break the rope!");

		for (int i = 0; i < links; i++)
		{
			Destroy (ropeLinks [i]);			
		}

		cargoScript.RemoveRopeEnd ();

		isThereARope = false;
	}
}

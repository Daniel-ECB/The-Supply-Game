using UnityEngine;
using System.Collections;

public class Parallax : MonoBehaviour {

	public Transform[] backgrounds;
	public float ParallaxScale;
	public float ParallaxReductionFactor;
	public float Smoothing;

	private Vector3 _lastPositon;

	
	void Start()
	{
		_lastPositon = transform.position;
	}

	void FixedUpdate()
	{
		var parallax = (_lastPositon.x - transform.position.x) * ParallaxScale;

		for (var i = 0; i < backgrounds.Length; i++) {

			var backgroundTargetPosition = backgrounds [i].position.x + 1 * parallax * (i * ParallaxReductionFactor + 1);

			backgrounds [i].position = Vector3.Lerp (
				backgrounds [i].position,
				new Vector3 (backgroundTargetPosition, backgrounds [i].position.y, backgrounds [i].position.z),
				Smoothing * Time.deltaTime);
		}

		_lastPositon = transform.position;
	}

}

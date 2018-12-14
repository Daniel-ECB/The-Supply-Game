using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour {

	public Image img;
	public AnimationCurve curve;

	void Start ()
	{
		StartCoroutine (FadeIn());
	}

	/// <summary>
	/// Fades out to the specified scene.
	/// </summary>
	/// <param name="scene">The scene to be loaded after fading out.</param>
	public void FadeTo(string scene)
	{
		StartCoroutine (FadeOut(scene));
	}

	/// <summary>
	/// Fades into the scene by removing the cover image.
	/// </summary>
	/// <returns>yield return 0 for wating.</returns>
	IEnumerator FadeIn ()
	{
		float t = 1f; // T for the alpha channel change over time

		while (t > 0f)
		{
			t -= Time.deltaTime; // * speed of 2f would be form 0-1 in half a sec, 1f in a sec, 0.5f in 2 secs
			float a = curve.Evaluate(t); // This would give a different output value like a math function according to the anim curve
			img.color = new Color (0f, 0f, 0f, a); // This bc we can't just modify the alpha, we need a entire new color
			yield return 0; // This makes the game skip to the next frame, wait for a frame and then continue			
		}
	}

	/// <summary>
	/// Fades out of the scene by placing the cover image and loads a new scene.
	/// </summary>
	/// <returns>yield return 0 for wating.</returns>
	/// <param name="scene">The scene to be loaded after fading out.</param>
	IEnumerator FadeOut (string scene)
	{
		float t = 0f; // T for the alpha channel change over time

		while (t < 1f)
		{
			t += Time.deltaTime; // * speed of 2f would be form 0-1 in half a sec, 1f in a sec, 0.5f in 2 secs
			float a = curve.Evaluate(t); // This would give a different output value like a math function according to the anim curve
			img.color = new Color (0f, 0f, 0f, a); // This bc we can't just modify the alpha, we need a entire new color
			yield return 0; // This makes the game skip to the next frame, wait for a frame and then continue			
		}

		SceneManager.LoadScene (scene);
	}
}

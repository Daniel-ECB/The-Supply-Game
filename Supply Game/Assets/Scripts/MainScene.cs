using UnityEngine;

public class MainScene : MonoBehaviour {

	public string levelToLoad = "MainLevel";

	public SceneFader sceneFader;

	/// <summary>
	/// Plays the game and loads the first scene.
	/// </summary>
	public void Play()
	{
		//Debug.Log ("Play");
//		SceneManager.LoadScene(levelToLoad);
//		FindObjectOfType<SceneFader> ().FadeTo (levelToLoad); CPU Expensive
		sceneFader.FadeTo(levelToLoad);
	}

	/// <summary>
	/// Quits the game, closing the application in any platform.
	/// </summary>
	public void Quit()
	{
		Debug.Log ("Exciting..."); // This line bc Application.Quit () doesn't show anything in the editor
		Application.Quit (); // Closes the build
	}
}

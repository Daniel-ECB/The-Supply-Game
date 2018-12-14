using UnityEngine;

public class LevelSelector : MonoBehaviour {

	public SceneFader fader;

	/// <summary>
	/// Selects the specified level and tells the SceneFader to load it.
	/// </summary>
	/// <param name="levelName">Level name.</param>
	public void Select(string levelName)
	{
		fader.FadeTo (levelName);
	}

	/// <summary>
	/// Quits the game.
	/// </summary>
	public void QuitGame()
	{
		Debug.Log ("The game will be closed now");
		Application.Quit ();
	}
}

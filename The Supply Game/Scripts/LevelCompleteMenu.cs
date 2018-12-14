using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompleteMenu : MonoBehaviour {

	public GameObject ui;
	public SceneFader sceneFader;
	public string menuSceneName = "LevelSelection";

	/// <summary>
	/// Reloads the currently opened scene.
	/// </summary>
	public void RetryLevel()
	{
		sceneFader.FadeTo (SceneManager.GetActiveScene ().name);
	}

	/// <summary>
	/// Quits the current level and loads the level selection menu.
	/// </summary>
	public void LevelSelectMenu()
	{
		sceneFader.FadeTo(menuSceneName);
	}
}

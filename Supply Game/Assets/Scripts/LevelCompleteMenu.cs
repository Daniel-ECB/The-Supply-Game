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
		// So that timeScale is reset after the Level Complete UI has been activated. Forget it, we don't actually need it
		//Time.timeScale = 1f;

		// we could add an argument to the method to specify the targe scene, maybe
		sceneFader.FadeTo(menuSceneName);
	}
}

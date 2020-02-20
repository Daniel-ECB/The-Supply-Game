using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public GameObject ui;
	public SceneFader sceneFader;
	public string menuSceneName = "LevelSelection";
	public GameObject gameplayElements; // To fade out these elements
	public float gameplayElementsFade = 0.25f;

	// Update is called once per frame
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
		{
			Toggle ();			
		}
	}

	/// <summary>
	/// Toggles the pause menu.
	/// </summary>
	public void Toggle()
	{
		// TODO: If I really want to, I must remember to fade out all other UI elements
		// just like I do with the LevelCompleteMenu - DONE
		CanvasGroup gameplayElementsAlpha = gameplayElements.GetComponent<CanvasGroup>();

		ui.SetActive (!ui.activeSelf);

		// This essentially freezes the game, it prevent the loop from running, we would
		// have to change Time.fixedDeltaTime in case we want create some slow-mo 
		// functionality or speed up the game.
		if (ui.activeSelf) 
		{
			Time.timeScale = 0f;

			// Fading out
			if(gameplayElementsAlpha != null)
			{
				gameplayElementsAlpha.alpha = gameplayElementsFade;
			}
		} 
		else 
		{
			Time.timeScale = 1f;

			// Fading in
			if(gameplayElementsAlpha != null)
			{
				gameplayElementsAlpha.alpha = 1f;
			}
		}
	}

	/// <summary>
	/// Reloads the current Scene.
	/// </summary>
	public void Retry()
	{
//		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);

		Toggle (); // BC unity doesn't automatially reset Time.timeScale when loading a Scene
		sceneFader.FadeTo (SceneManager.GetActiveScene().name);
	}

	/// <summary>
	/// Quits the current level and loads the level selection menu.
	/// </summary>
	public void Menu()
	{
		// we could add an argument to the method to specify the targe scene, maybe
		Toggle();
		sceneFader.FadeTo(menuSceneName);
	}
}

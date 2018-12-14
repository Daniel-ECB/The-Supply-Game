using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public GameObject gameplayElements;
	public float gameplayElementsFade = 0.25f;
	public Text timerText;
	public Text completionTimeText;
	[HideInInspector]
	public float elapsedTime = 0f;
	private bool updatingTimer = true;

	public LevelCompleteMenu levelCompleteUI;

	[Header("")] // Just to show a blank line in the inspector
	public Goal levelGoal;
	public Player levelPlayer;

	[Header("Level Cargoes")]
	public int cargoesInLevel = 1;
	public Text cargoesText;

	private void Start()
	{
		// We set it here so that we don't have to manually set this value twice
		cargoesText.text = cargoesInLevel.ToString();
	}

	private void Update()
	{
		// Updating timer UI. Now this works
		if(updatingTimer)
		{
			elapsedTime = Mathf.Max (0, elapsedTime + Time.deltaTime);
			var timeSpan = System.TimeSpan.FromSeconds(elapsedTime);
			timerText.text = timeSpan.Hours.ToString("00") + ":" +
				timeSpan.Minutes.ToString("00") + ":" +
				timeSpan.Seconds.ToString("00") + "." +
				timeSpan.Milliseconds/100;
		}
	}

	/// <summary>
	/// Checks if all the cargoes have been delivered.
	/// </summary>
	public void WereCargoesDelivered()
	{
//		Debug.Log ("WereCargoesDelivered was called");

		// We destroy it so we avoid bugs caused by a cargo leaving the goal after it was delivered
		levelPlayer.BreakRope(); // So we don't get null reference errors after destroying the cargo
		Cargo theCargo = levelGoal.getCargo;
		Destroy (theCargo.gameObject);
		Debug.Log ("The cargo was delivered");

		// Update the quantity left and let the user know this
		cargoesInLevel--;
		cargoesText.text = cargoesInLevel.ToString();

		// If all cargoes were delivered, the level has been completed
		if(cargoesInLevel <= 0)
		{
//			Debug.Log ("All cargoes were delivered!");
			LevelEnd();
		}
	}

	/// <summary>
	/// Ends the level.
	/// </summary>
	private void LevelEnd()
	{
//		Debug.Log ("The level has been completed");

		// Just a nice effect to make it a little bit transparent
		CanvasGroup gameplayElementsAlpha = gameplayElements.GetComponent<CanvasGroup> ();

		if(gameplayElementsAlpha != null)
		{
//			Debug.Log ("We have the Canvas Group");
			gameplayElementsAlpha.alpha = gameplayElementsFade;
		}

		// Let the user know in what time the level has been completed, we don't need 1 timer running after that
		levelCompleteUI.ui.SetActive (true);
		completionTimeText.text = timerText.text;
		updatingTimer = false;
	}

	/// <summary>
	/// Cancels level completion.
	/// </summary>
	public void LevelNotEnded()
	{
		Debug.Log ("Cargo was not delivered");
	}

	/// <summary>
	/// Resets the level.
	/// </summary>
	private void ResetLevel()
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
	}
}

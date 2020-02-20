using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour {
 

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class Quit : MonoBehaviour {

    public int sceneIndex;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}

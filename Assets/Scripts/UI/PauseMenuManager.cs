using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public string pauseMenuName = "Main Menu";
    private Scene scene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            scene = SceneManager.GetSceneByName(pauseMenuName);
            if (scene.IsValid() && scene.isLoaded)
            {
                bringDownPauseMenu();
            }
            else
            {
                bringUpPauseMenu();
            }
        }
    }

    void bringUpPauseMenu()
    {
        SceneManager.LoadScene(pauseMenuName, LoadSceneMode.Additive);
        Time.timeScale = 0f;
    }

    void bringDownPauseMenu()
    {
        SceneManager.UnloadSceneAsync(pauseMenuName);
        Time.timeScale = 1f;
    }
}

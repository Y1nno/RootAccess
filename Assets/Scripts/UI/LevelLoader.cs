using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public void LoadGameScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}

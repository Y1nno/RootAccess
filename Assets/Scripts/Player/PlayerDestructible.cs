using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerRespawner : MonoBehaviour
{

    private void OnDestroy()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}

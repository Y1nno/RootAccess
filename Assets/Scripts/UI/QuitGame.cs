using UnityEngine;
using UnityEditor;

public class QuitGameScript : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Game is exiting...");
    }
}
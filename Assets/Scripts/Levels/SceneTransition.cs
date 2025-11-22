using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class ScreenTransition : MonoBehaviour
{
    [SerializeField] private Follower cameraFollower;
    [SerializeField] private string transitionTo;
    [SerializeField] private Transform startPoint;
    [SerializeField] private Vector2 exitDirection;
    [SerializeField] private float exitTime;

    private void Start()
    {
        if (transitionTo == GameManager.Instance.transitionedFromScene)
        {
            Player.Instance.transform.position = startPoint.position;
        }
    }


    private void OnTriggerEnter2D(Collider2D _other)
    {
        if (_other.CompareTag("Player"))
        {
            GameManager.Instance.transitionedFromScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(transitionTo);
            
            //StartCoroutine(LoadSceneAndRebindCamera());
        }
    }

    private IEnumerator LoadSceneAndRebindCamera()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(transitionTo);

        while (!op.isDone)
            yield return null;

        // Now that the new scene is loaded:
        var newPlayer = Player.Instance;
        cameraFollower.SetTarget(Player.Instance.transform);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public GameObject winScreen;



    //When we touch this object...
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (winScreen != null)
        {
            winScreen.SetActive(true);
        }
        else
        {
            Debug.Log("Won Game");
        }
    }
}

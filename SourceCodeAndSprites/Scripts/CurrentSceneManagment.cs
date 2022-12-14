using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentSceneManagment : MonoBehaviour
{
    public bool isPlayerPresentByDefault = false;
    public static CurrentSceneManagment instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("There is more than one instance CurrentSceneManagment in the scene.");
            return;
        }
        instance = this;
    }
}

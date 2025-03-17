using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{

    private void Update()
    {
        if (Input.anyKeyDown)
        {
            StartScene();
        }
    }
    public void StartScene()
    {
        SceneManager.LoadScene("Game"); 
    }
}

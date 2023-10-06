using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectToGame : MonoBehaviour
{
    

    
    public void ToMainGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuToGame : MonoBehaviour
{
    

    
    public void ToMainGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToturialScript : MonoBehaviour
{
    public GameObject tot;
    public float timer = 6.1f;

    public void Start()
    {
        tot.SetActive(true);
    }

    public void Update()
    {
        if(timer <= 0f)
        {
            tot.SetActive(false);
        }
    }
}

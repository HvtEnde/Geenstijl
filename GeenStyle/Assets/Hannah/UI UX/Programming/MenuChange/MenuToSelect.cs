using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuToSelect : MonoBehaviour
{
    public GameObject menu;
    public GameObject select;
    public void TransitionScreen()
    {
        menu.SetActive(false);
        select.SetActive(true);
    }
}

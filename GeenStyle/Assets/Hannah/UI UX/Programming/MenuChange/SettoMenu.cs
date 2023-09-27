using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettoMenu : MonoBehaviour
{
    public GameObject settings;
    public GameObject menu;
    public void TransitionScreen()
    {
        settings.SetActive(false);
        menu.SetActive(true);
    }
}

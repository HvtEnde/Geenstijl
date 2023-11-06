using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformationScreen : MonoBehaviour
{

    public GameObject informationUI;
    public GameObject mainMenuUI;

    public void BackToMainMenuFromInfo()
    {
        mainMenuUI.SetActive(false);
        informationUI.SetActive(true);
    }
}

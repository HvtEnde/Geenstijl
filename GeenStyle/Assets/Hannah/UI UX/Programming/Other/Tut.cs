using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tut : MonoBehaviour
{
    public Animator toturialAnim;

    public void Start()
    {
        toturialAnim = GetComponent<Animator>();

    }

   public void ChangeAnimation()
   {
        toturialAnim.SetInteger("ClickToContinue", toturialAnim.GetInteger("ClickToContinue") + 1);

   }

    public void Update()
    {
        if(Input.anyKeyDown)
        {
            ChangeAnimation();
        }
    }
}

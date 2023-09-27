using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverButton : MonoBehaviour
{
    public AudioSource sfx;
    public AudioClip hoverSfx;

    public void hoverSound()
    {
        sfx.PlayOneShot(hoverSfx);
    }
}

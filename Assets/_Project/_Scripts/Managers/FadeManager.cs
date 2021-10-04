using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    public Animator anim;

    public void FadeOut()
    {
        anim.Play("FadeOut");
    }

    public void FadeIn()
    {
        anim.Play("FadeIn");
    }
}

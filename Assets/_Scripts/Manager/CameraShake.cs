using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public Animator anm;
    private static readonly int Shake1 = Animator.StringToHash("shake");

    public void Shake()
    {
        anm.SetTrigger(Shake1);
    }
}

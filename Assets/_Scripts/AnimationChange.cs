using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChange : MonoBehaviour
{
    public SkeletonAnimation monsterAnimator; //The animator script of the monster
   
    public void ChangeAnimation(string AnimationName)  //Names are: Idle, Walk, Dead and Attack
    {
        if (monsterAnimator == null)
            return;

        bool IsLoop = true;
        if (AnimationName == "Dead")
            IsLoop = false;

        //set the animation state to the selected one
        monsterAnimator.AnimationState.SetAnimation(0, AnimationName, false);
        monsterAnimator.timeScale = 0.5f;
    }
}

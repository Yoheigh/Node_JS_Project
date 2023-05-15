using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationActions : MonoBehaviour
{
    [HideInInspector] public AnimationController animationController;

    public void Awake()
    {
        animationController = GetComponent<AnimationController>();
    }

    public void TakeAction(string action)
    {
        if (action == "FishingCast")
        {
            animationController.TriggerAnimation("FishingCast");
            animationController.ChangeCharacterState(0.4f, AnimationsState.Fishing);
            animationController.LockMovement(1f);
        }
        if (action == "FishingReel")
        {
            animationController.TriggerAnimation("FishingReel");
            animationController.ChangeCharacterState(0.4f, AnimationsState.Fishing);
            animationController.LockMovement(1f);
        }
        if (action == "FishingFinish")
        {
            animationController.TriggerAnimation("FishingFinish");
            animationController.ChangeCharacterState(0.4f, AnimationsState.Idle);
            animationController.LockMovement(1f);
        }
    }
}

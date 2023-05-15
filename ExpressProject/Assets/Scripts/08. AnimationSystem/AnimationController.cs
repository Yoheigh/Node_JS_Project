using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [HideInInspector] public Animator animator;

    [HideInInspector] public bool isMoving;
    [HideInInspector] public bool isLocked;

    public AnimationsState charState;
    private Coroutine coroutineLock = null;
    private bool allowedInput = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerAnimation(string trigger)
    {
        animator.SetInteger("Action", (int)(AnimatorTriggers)System.Enum.Parse(typeof(AnimatorTriggers), trigger));
        animator.SetTrigger("Trigger");
    }

    public void ChangeCharacterState(float waitTime, AnimationsState state)
    {
        StartCoroutine(_ChangeCharacterState(waitTime, state));
    }

    private IEnumerator _ChangeCharacterState(float waitTime, AnimationsState state)
    {
        yield return new WaitForSeconds(waitTime);
        charState = state;
    }

    public void LockMovement(float lockTime)
    {
        if (coroutineLock != null)
        {
            StopCoroutine(coroutineLock);
        }
        coroutineLock = StartCoroutine(_LockMovement(lockTime));
    }

    private IEnumerator _LockMovement(float lockTime)
    {
        allowedInput = false;
        isLocked = true;
        animator.applyRootMotion = true;
        if (lockTime != -1f)
        {
            yield return new WaitForSeconds(lockTime);
            isLocked = false;
            animator.applyRootMotion = false;
            allowedInput = true;
        }
    }
}

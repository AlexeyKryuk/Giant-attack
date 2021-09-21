using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationEvents : MonoBehaviour
{
    public UnityAction AimBlowEvent;
    public UnityAction BlowBeganEvent;
    public UnityAction HitEvent;
    public UnityAction HitEndEvent;

    private void OnAimBlowAnimation()
    {
        AimBlowEvent?.Invoke();
    }

    private void OnBlowBeganAnimation()
    {
        BlowBeganEvent?.Invoke();
    }

    private void OnHitAnimation()
    {
        HitEvent?.Invoke();
    }

    private void OnHittingEnd()
    {
        HitEndEvent?.Invoke();
    }
}

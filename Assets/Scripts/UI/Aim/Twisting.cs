using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Twisting : MonoBehaviour
{
    [SerializeField] protected Animator _animator;
    [SerializeField] protected TouchDetection _touchDetection;
    [SerializeField] protected RectTransform _crosshair;
    [SerializeField] protected float _speed;

    protected virtual void OnEnable()
    {
        _touchDetection.Touched += OnTouch;
        _crosshair.gameObject.SetActive(true);
    }

    protected virtual void OnDisable()
    {
        _touchDetection.Touched -= OnTouch;
        _crosshair.gameObject.SetActive(false);
        _animator.SetTrigger("ContinueHit");
    }

    protected abstract void OnTouch();
}

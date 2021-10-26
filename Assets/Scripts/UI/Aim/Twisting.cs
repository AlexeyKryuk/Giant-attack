using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Twisting : MonoBehaviour
{
    [SerializeField] protected Player _player;
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

        if (_player.Animator != null)
            _player.Animator.SetTrigger("ContinueHit");
    }

    protected abstract void OnTouch();
}

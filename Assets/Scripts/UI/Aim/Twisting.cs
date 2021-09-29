using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Twisting : MonoBehaviour
{
    [SerializeField] protected TouchDetection _touchDetection;
    [SerializeField] protected RectTransform _crosshair;
    [SerializeField] protected float _speed;

    protected virtual void OnEnable()
    {
        _touchDetection.Touched += OnTouch;
        _crosshair.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    protected virtual void OnDisable()
    {
        _touchDetection.Touched -= OnTouch;
        _crosshair.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    protected abstract void OnTouch();
}

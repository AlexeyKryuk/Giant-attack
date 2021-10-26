using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchImpact : MonoBehaviour
{
    [SerializeField] private Animation _animation;
    [SerializeField] private TouchDetection _touchDetection;

    private void OnEnable()
    {
        _touchDetection.Touched += OnTouch;
    }

    private void OnDisable()
    {
        _touchDetection.Touched -= OnTouch;
    }

    private void OnTouch()
    {
        _animation.Play();
    }
}

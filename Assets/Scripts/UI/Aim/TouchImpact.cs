using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeUIElement
{
    HorizontalSlider,
    VerticalSlider,
    Crosshair
}

public class TouchImpact : MonoBehaviour
{
    [SerializeField] private Animation _animation;
    [SerializeField] private TouchDetection _touchDetection;
    [SerializeField] private TypeUIElement _typeUIElement;

    private bool _isSecondTap;

    private void OnEnable()
    {
        _touchDetection.Touched += OnTouch;
        _isSecondTap = false;
    }

    private void OnDisable()
    {
        _touchDetection.Touched -= OnTouch;
        _animation.Play();
    }

    private void OnTouch()
    {
        switch (_typeUIElement)
        {
            case TypeUIElement.HorizontalSlider:
                if (!_isSecondTap) _animation.Play();
                break;

            case TypeUIElement.VerticalSlider:
                if (_isSecondTap) _animation.Play();
                break;

            case TypeUIElement.Crosshair:
                _animation.Play();
                break;

            default:
                break;
        }

        if (!_isSecondTap)
            _isSecondTap = true;
    }
}

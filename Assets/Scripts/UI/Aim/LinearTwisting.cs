using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinearTwisting : Twisting
{
    [SerializeField] private Slider _horizontal;
    [SerializeField] private Slider _vertical;

    private Slider _currentSlider;
    private Side _side = Side.Middle;

    public RectTransform Crosshair { get => _crosshair; private set => _crosshair = value; }

    protected override void OnEnable()
    {
        base.OnEnable();
        _currentSlider = _horizontal;
        _horizontal.gameObject.SetActive(true);
        _vertical.gameObject.SetActive(true);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _horizontal.gameObject.SetActive(false);
        _vertical.gameObject.SetActive(false);
    }

    protected override void OnTouch()
    {
        if (_currentSlider != _vertical)
            _currentSlider = _vertical;
        else
        {
            enabled = false;
        }
    }

    private void Update()
    {
        Twist(_currentSlider);
        FollowSlider(_crosshair);
    }

    private void Twist(Slider slider)
    {
        if (_side == Side.Middle)
        {
            slider.value += Mathf.Lerp((float)Side.Middle, (float)Side.Right, _speed * Time.unscaledDeltaTime);
            if (slider.value == (float)Side.Right)
                _side = Side.Right;
        }
        else
        {
            slider.value -= Mathf.Lerp((float)Side.Middle, (float)Side.Right, _speed * Time.unscaledDeltaTime);
            if (slider.value == (float)Side.Middle)
                _side = Side.Middle;
        }
    }

    private void FollowSlider(Transform crosshair)
    {
        crosshair.position = new Vector3(_horizontal.handleRect.transform.position.x, _vertical.handleRect.transform.position.y, crosshair.position.z);
    }
}

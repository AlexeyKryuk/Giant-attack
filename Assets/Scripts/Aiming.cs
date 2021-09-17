using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Side
{
    Left,
    Right
}

public class Aiming : MonoBehaviour
{
    [SerializeField] private PlayerAnimationEvents _animationEvents;
    [SerializeField] private Slider _horizontal;
    [SerializeField] private Slider _vertical;
    [SerializeField] private Transform _crosshair;
    [SerializeField] private float _speed;

    private Side _side = Side.Left;
    private Slider _currentSlider;
    private bool _start;

    private void OnEnable()
    {
        _animationEvents.AimBlowEvent += OnAimBlow;
    }

    private void OnDisable()
    {
        _animationEvents.AimBlowEvent -= OnAimBlow;
    }

    private void Update()
    {
        if (_start)
        {
            Twist(_currentSlider);
            FollowSlider(_crosshair);

            if (Input.GetMouseButtonDown(0))
            {
                if (_currentSlider != _vertical)
                    _currentSlider = _vertical;
                else
                {
                    Time.timeScale = 1;
                    SetActive(false);
                }
            }
        }
    }

    public void OnAimBlow()
    {
        Initialisation();
    }

    private void Initialisation()
    {
        SetActive(true);

        Time.timeScale = 0;
        _horizontal.value = 0;
        _vertical.value = 0;
        _currentSlider = _horizontal;
    }

    private void SetActive(bool value)
    {
        _start = value;
        _horizontal.gameObject.SetActive(value);
        _vertical.gameObject.SetActive(value);
        _crosshair.gameObject.SetActive(value);
    }

    private void Twist(Slider slider)
    {
        if (_side == Side.Left)
        {
            slider.value += Mathf.Lerp((float)Side.Left, (float)Side.Right, _speed * Time.unscaledDeltaTime);
            if (slider.value == (float)Side.Right)
                _side = Side.Right;
        }
        else
        {
            slider.value -= Mathf.Lerp((float)Side.Left, (float)Side.Right, _speed * Time.unscaledDeltaTime);
            if (slider.value == (float)Side.Left)
                _side = Side.Left;
        }
    }

    private void FollowSlider(Transform crosshair)
    {
        crosshair.position = new Vector3(_horizontal.handleRect.transform.position.x, _vertical.handleRect.transform.position.y, crosshair.position.z);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SwipeDetection : InputDetection
{
    [SerializeField] private float _deadZone;

    private Vector2 _tapPosition;
    private Vector2 _swipeDelta;

    private bool _isSwiping;

    private void Update()
    {
        if (!IsMobile)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isSwiping = true;
                _tapPosition = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                ResetSwipe();
            }
        }
        else
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    _isSwiping = true;
                    _tapPosition = touch.position;
                }
                else if (touch.phase == TouchPhase.Canceled)
                {
                    ResetSwipe();   
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    ResetSwipe();
                }
            }
        }

        CheckSwipe();
    }

    private void CheckSwipe()
    {
        _swipeDelta = Vector2.zero;

        if (_isSwiping)
        {
            if (!IsMobile && Input.GetMouseButton(0))
                _swipeDelta = (Vector2)Input.mousePosition - _tapPosition;
            else if (Input.touchCount > 0)
                _swipeDelta = Input.GetTouch(0).position - _tapPosition;
        }

        if (_swipeDelta.magnitude > _deadZone)
        {
            if (Mathf.Abs(_swipeDelta.x) > Mathf.Abs(_swipeDelta.y))
                Swiped?.Invoke(_swipeDelta.x > 0 ? Side.Right : Side.Left);

            ResetSwipe();
        }
    }

    private void ResetSwipe()
    {
        _isSwiping = false;

        _tapPosition = Vector2.zero;
        _swipeDelta = Vector2.zero;
    }
}

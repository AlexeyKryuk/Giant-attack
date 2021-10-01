using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetection : InputDetection
{
    private Vector2 _lastPosition;

    private void Update()
    {
        IsMobile = false;
        if (IsMobile)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
                {
                    TouchCancled?.Invoke();

                    if (Vector2.Distance(_lastPosition, touch.position) < 30f)
                    {
                        Touched?.Invoke();
                    }
                    _lastPosition = Vector2.zero;
                }

                if (_lastPosition == Vector2.zero && touch.phase == TouchPhase.Began)
                {
                    _lastPosition = touch.position;
                    TouchBegan?.Invoke();
                }

                if (touch.phase == TouchPhase.Moved)
                    TouchMoved?.Invoke();
            }
        }
        else
        {
            if (Input.GetMouseButtonUp(0))
            {
                TouchCancled?.Invoke();

                if (Vector2.Distance(_lastPosition, Input.mousePosition) < 30f)
                {
                    Touched?.Invoke();
                }
                _lastPosition = Vector2.zero;
            }

            if (_lastPosition == Vector2.zero && Input.GetMouseButtonDown(0))
            {
                TouchBegan?.Invoke();
                _lastPosition = Input.mousePosition;
            }

            if (Input.GetMouseButton(0))
                TouchMoved?.Invoke();
        }
    }
}

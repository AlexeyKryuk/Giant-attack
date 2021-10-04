using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplacementDetection : InputDetection
{
    private Vector2 _lastPosition;
    private Vector2 _centerScreen;

    private void Start()
    {
        _centerScreen = new Vector3(0, Screen.height - 500f, 0);
        Displacement = _centerScreen;
    }

    private void Update()
    {
        IsMobile = false;

        if (IsMobile)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (Vector2.Distance(touch.position, _lastPosition) > 0)
                    Displacement = touch.position - _lastPosition;

                if (touch.phase == TouchPhase.Moved)
                    _lastPosition = touch.position;
                else
                    _lastPosition = Vector2.zero;
            }
        }
        else
        {
            if (Input.GetMouseButton(0))
            {
                if (Vector2.Distance(Input.mousePosition, _lastPosition) > 0)
                    Displacement = (Vector2)Input.mousePosition - _centerScreen;

                _lastPosition = Input.mousePosition;
            }
            else
                _lastPosition = Vector2.zero;
        }
    }
}

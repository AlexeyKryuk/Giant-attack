using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplacementDetection : InputDetection
{
    private Vector2 _lastPosition;
    private Vector2 _centerScreen;

    private void Start()
    {
        _centerScreen = new Vector3(0, 0, 0);
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

                Displacement = touch.position - _centerScreen;

                if (touch.phase == TouchPhase.Moved)
                {
                    if (Vector2.Distance(touch.position, _lastPosition) > 0)
                        Displacement = touch.position - _centerScreen;

                    _lastPosition = touch.position;
                }
            }
        }
        else
        {
            Displacement = (Vector2)Input.mousePosition - _centerScreen;

            if (Input.GetMouseButton(0))
            {
                if (Vector2.Distance(Input.mousePosition, _lastPosition) > 0)
                    Displacement = (Vector2)Input.mousePosition - _centerScreen;

                _lastPosition = Input.mousePosition;
            }
        }
    }
}

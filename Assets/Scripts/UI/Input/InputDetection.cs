using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputDetection : MonoBehaviour
{
    public UnityAction<Side> Swiped;
    public UnityAction Touched;
    public UnityAction TouchCancled;
    public UnityAction TouchBegan;
    public UnityAction TouchMoved;

    public Vector2 Displacement { get; protected set; }

    protected bool IsMobile;

    private void Start()
    {
        IsMobile = Application.isMobilePlatform;
    }
}

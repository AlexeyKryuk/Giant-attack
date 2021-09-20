using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputDetection : MonoBehaviour
{
    public UnityAction<Side> Swiped;
    public UnityAction Touched;

    protected bool IsMobile;

    private void Start()
    {
        IsMobile = Application.isMobilePlatform;
    }
}

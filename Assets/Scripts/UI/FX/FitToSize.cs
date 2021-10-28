using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FitToSize : MonoBehaviour
{
    private RectTransform _parent;

    private void Awake()
    {
        _parent = GetComponentInParent<RectTransform>();
    }

    private void Update()
    {
        transform.localScale = new Vector3(_parent.rect.width / 2 - 20f, transform.localScale.y, transform.localScale.z);
    }
}

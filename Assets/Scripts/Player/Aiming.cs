using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Aiming : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private LayerMask _layerMask;

    public RaycastHit Target { get; protected set; }
    public Camera Camera => _camera;
    public LayerMask LayerMask => _layerMask;

    private void Update()
    {
        CalculateTarget();
    }

    protected abstract void CalculateTarget();
}

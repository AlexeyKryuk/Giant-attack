using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Aiming : MonoBehaviour
{
    [SerializeField] private PlayerAnimationEvents _animationEvents;
    [SerializeField] private Camera _camera;
    [SerializeField] private TwistingAim _twistingAim;
    [SerializeField] private LayerMask _layerMask;

    public Vector3 Target { get; private set; }

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
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(_twistingAim.Crosshair.position);

        if (Physics.SphereCast(ray, 1f, out hit, 1000f, _layerMask))
            Target = hit.point;
    }

    public void OnAimBlow()
    {
        _twistingAim.enabled = true;
    }
}

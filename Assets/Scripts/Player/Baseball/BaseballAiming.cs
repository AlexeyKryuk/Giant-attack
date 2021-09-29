using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BaseballAiming : Aiming
{
    [SerializeField] private PlayerAnimationEvents _animationEvents;
    [SerializeField] private Transform _crosshair;
    [SerializeField] private Twisting _twistingAim;

    private void OnEnable()
    {
        _animationEvents.AimBlowEvent += OnAimBlow;
    }

    private void OnDisable()
    {
        _animationEvents.AimBlowEvent -= OnAimBlow;
    }

    protected override void CalculateTarget()
    {
        RaycastHit hit;
        Ray ray = Camera.ScreenPointToRay(_crosshair.position);

        if (Physics.Raycast(ray, out hit, 1000f, LayerMask))
            Target = hit.point;
    }

    public void OnAimBlow()
    {
        _twistingAim.enabled = true;
    }
}

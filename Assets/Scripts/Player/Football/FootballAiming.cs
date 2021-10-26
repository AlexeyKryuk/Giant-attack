using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballAiming : Aiming
{
    [SerializeField] private Transform _crosshair;
    [SerializeField] private DisplacementDetection _input;

	protected override void CalculateTarget()
    {
        AimCrosshair();

        RaycastHit hit;
        Ray ray = Camera.ScreenPointToRay(_crosshair.position);

        if (Physics.Raycast(ray, out hit, 1000f, LayerMask))
        {
            Target = hit;
        }
    }

    private void AimCrosshair()
    {
        Vector3 displacement = new Vector3(_input.Displacement.x, _input.Displacement.y, 0);
        _crosshair.position = displacement;
    }
}

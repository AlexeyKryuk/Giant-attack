using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballAiming : Aiming
{
    [SerializeField] private Transform _crosshair;

	protected override void CalculateTarget()
    {
        AimCrosshair();

        RaycastHit hit;
        Ray ray = Camera.ScreenPointToRay(_crosshair.position);

        if (Physics.Raycast(ray, out hit, 1000f, LayerMask))
        {
            Target = hit.point;
        }
    }

    private void AimCrosshair()
    {
        if (Input.GetMouseButton(0))
        {
            _crosshair.position = Vector3.Lerp(_crosshair.position, Input.mousePosition, 100f * Time.deltaTime);
        }
    }
}

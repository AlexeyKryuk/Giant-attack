using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    [SerializeField] private RayFire.RayfireRigid _rayfire;

    private Collider[] _colliders;

    private void Awake()
    {
        _colliders = GetComponentsInChildren<Collider>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        DisableColliders();

        _rayfire.gameObject.SetActive(true);
        _rayfire.transform.SetParent(transform.parent);
        _rayfire.Demolish();

        Destroy(gameObject);
    }

    private void DisableColliders()
    {
        foreach (var collider in _colliders)
        {
            collider.enabled = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    [SerializeField] private RayFire.RayfireRigid _rayfire;

    protected override void OnEnable()
    {
        base.OnEnable();

        _rayfire.Demolish();
        Target.OnEnemyDie();
        Destroy(gameObject);
    }
}

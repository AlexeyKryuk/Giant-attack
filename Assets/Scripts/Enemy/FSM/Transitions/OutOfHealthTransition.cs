using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfHealthTransition : Transition
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Enemy.Damaged += OnDamaged;
    }

    private void OnDisable()
    {
        Enemy.Damaged -= OnDamaged;
    }

    private void OnDamaged()
    {
        if (Enemy.Health <= 0)
            NeedTransit = true;
    }
}

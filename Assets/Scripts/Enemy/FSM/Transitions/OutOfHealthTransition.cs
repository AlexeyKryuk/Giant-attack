using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutOfHealthTransition : Transition
{
    protected override void OnEnable()
    {
        base.OnEnable();
        Enemy.Died += OnDie;
    }

    private void OnDisable()
    {
        Enemy.Died -= OnDie;
    }

    private void OnDie()
    {
        NeedTransit = true;
    }
}

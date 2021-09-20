using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class TakeDamageTransition : Transition
{
    private Enemy _enemy;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _enemy.Damaged += OnTakeDamage;
    }

    private void OnDisable()
    {
        _enemy.Damaged -= OnTakeDamage;
    }

    private void OnTakeDamage()
    {
        NeedTransit = true;
    }
}

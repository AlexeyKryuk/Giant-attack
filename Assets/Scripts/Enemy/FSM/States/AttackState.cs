using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown;

    private float _currentTime;

    protected override void OnEnable()
    {
        base.OnEnable();
        _currentTime = _cooldown;
        Enemy.Damaged += OnTakeDamage;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Enemy.Damaged -= OnTakeDamage;
    }

    private void Update()
    {
        if (_currentTime >= _cooldown)
        {
            if (Target != null)
                Animator.SetTrigger("Attack");

            _currentTime = 0;
        }

        _currentTime += Time.deltaTime;
    }

    private void OnAttack()
    {
        if (Target != null)
            Target.ApplyDamage(_damage);
    }

    private void OnTakeDamage()
    {
        _currentTime = _cooldown;
    }
}

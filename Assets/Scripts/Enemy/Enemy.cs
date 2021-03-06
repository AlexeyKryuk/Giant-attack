using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Player _target;
    [SerializeField] private int _health;
    [SerializeField] private bool _isBoss;

    private Animator _animator;

    public Player Target => _target;
    public int Health => _health;
    public bool IsBoss => _isBoss;

    public UnityAction Damaged;
    public UnityAction <Enemy>Died;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ApplyDamage()
    {
        _health--;
        _animator.SetTrigger("Damage");
        Damaged?.Invoke();

        if (_health <= 0)
        {
            Died?.Invoke(this);

            if (Target != null)
                if (IsBoss)
                    Target.OnBossDie(this);
                else
                    Target.OnEnemyDie(this);
        }
    }
}

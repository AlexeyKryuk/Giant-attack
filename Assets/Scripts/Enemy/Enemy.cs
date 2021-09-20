using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private Player _target;

    private Animator _animator;

    public Player Target { get => _target; private set => _target = value; }

    public UnityAction Damaged;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ApplyDamage()
    {
        _animator.SetTrigger("Damage");
        Damaged?.Invoke();
    }
}

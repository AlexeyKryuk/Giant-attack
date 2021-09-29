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

    private Animator _animator;

    public Player Target => _target;
    public int Health => _health;

    public UnityAction Damaged;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void ApplyDamage()
    {
        _health--;
        _animator.SetTrigger("Damage");
        Damaged?.Invoke();
    }
}

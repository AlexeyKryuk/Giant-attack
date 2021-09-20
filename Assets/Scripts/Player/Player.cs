using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private int _health;

    public int Health => _health;

    public UnityAction Died;

    public void ApplyDamage(int amount)
    {
        if (_health > 0)
        {
            _health -= amount;
            if (_health <= 0)
            {
                _health = 0;
                _animator.SetTrigger("Death");

                Died?.Invoke();
            }
            else
                _animator.SetTrigger("Damage");
        }
    }
}

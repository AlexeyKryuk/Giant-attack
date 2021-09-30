using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Strafing _strafing;
    [SerializeField] private Hitting _hitting;
    [SerializeField] private int _health;

    public int Health => _health;

    public UnityAction Died;
    public UnityAction Damaged;

    public void OnEnemyDie()
    {
        _strafing.enabled = false;
        _hitting.enabled = false;
    }

    public void ApplyDamage(int amount)
    {
        if (_health > 0)
        {
            _health -= amount;
            if (_health <= 0)
            {
                _health = 0;
                _animator.SetTrigger("Death");
                _strafing.enabled = false;
                _hitting.enabled = false;

                Died?.Invoke();
            }
            else
            {
                _animator.SetTrigger("Damage");
                _strafing.enabled = false;
                _hitting.enabled = false;
                Damaged?.Invoke();
            }
        }
    }

    private void OnTakeDamageEnd()
    {
        _strafing.enabled = true;
        _hitting.enabled = true;
    }
}

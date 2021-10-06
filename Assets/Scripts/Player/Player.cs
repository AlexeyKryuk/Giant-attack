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

    private List<Enemy> _enemies;

    public int Health => _health;
    public List<Enemy> Enemies => _enemies;

    public UnityAction Damaged;
    public UnityAction Died;
    public UnityAction AllEnemyDied;
    public UnityAction EnemyDied;

    private void Awake()
    {
        _enemies = new List<Enemy>(FindObjectsOfType<Enemy>());
    }

    public void OnEnemyDie(Enemy enemy)
    {
        _enemies.Remove(enemy);
        EnemyDied?.Invoke();

        if (_enemies.Count < 1)
        {
            _strafing.enabled = false;
            _hitting.enabled = false;
            AllEnemyDied?.Invoke();
        }
    }

    public void ApplyDamage(int amount)
    {
        if (_health > 0)
        {
            _health -= amount;
            Time.timeScale = 1;

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

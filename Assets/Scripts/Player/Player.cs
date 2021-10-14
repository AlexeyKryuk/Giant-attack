using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Hitting _hitting;
    [SerializeField] private int _health;

    private List<Enemy> _enemies = new List<Enemy>();
    private List<Enemy> _bosses = new List<Enemy>();

    public int Health => _health;
    public List<Enemy> Enemies => _enemies;

    public UnityAction Damaged;
    public UnityAction Died;

    public UnityAction AllEnemyDied;
    public UnityAction EnemyDied;
    public UnityAction BossDied;

    private void Awake()
    {
        _enemies = new List<Enemy>(FindObjectsOfType<Enemy>());

        for (int i = 0; i < _enemies.Count; i++)
        {
            Enemy enemy = _enemies[i];

            if (enemy.IsBoss)
            {
                _bosses.Add(enemy);
                _enemies.Remove(enemy);
                enemy.gameObject.SetActive(false);
                i--;
            }
        }
    }

    public void OnBossDie(Enemy enemy)
    {
        BossDied?.Invoke();
        _bosses.Remove(enemy);

        if (_bosses.Count < 1)
        {
            AllEnemyDied?.Invoke();
        }
    }

    public void OnEnemyDie(Enemy enemy)
    {
        EnemyDied?.Invoke();
        _enemies.Remove(enemy);

        if (_enemies.Count < 1)
        {
            foreach (var boss in _bosses)
            {
                boss.gameObject.SetActive(true);
            }
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
                _hitting.enabled = false;

                Died?.Invoke();
            }
            else
            {
                _animator.SetTrigger("Damage");
                _hitting.enabled = false;
                Damaged?.Invoke();
            }
        }
    }

    private void OnTakeDamageEnd()
    {
        _hitting.enabled = true;
    }
}

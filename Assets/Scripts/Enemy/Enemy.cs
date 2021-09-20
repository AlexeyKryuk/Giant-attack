using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Player _target;

    public Player Target { get => _target; private set => _target = value; }

    public UnityAction Damaged;

    public void ApplyDamage()
    {
        Damaged?.Invoke();
    }
}

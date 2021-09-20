using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class TakeDamageTransition : Transition
{
    private Enemy _enemy;
    private Coroutine _coroutine;

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
        if (_coroutine == null)
            _coroutine = StartCoroutine(DelayBeforeTransit());
    }

    private IEnumerator DelayBeforeTransit()
    {
        float animationLength = Animator.GetCurrentAnimatorClipInfo(0).Length - 0.2f;
        yield return new WaitForSeconds(animationLength);
        NeedTransit = true;
    }
}

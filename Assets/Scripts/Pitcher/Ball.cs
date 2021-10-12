using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Ball : MonoBehaviour
{
    [SerializeField] private Flying _flyingBall;
    [SerializeField] private HitEffect _hitEffect;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Kick(Vector3 target)
    {
        if (_flyingBall != null)
        {
            _flyingBall.enabled = true;
            _flyingBall.SetTarget(target);
        }

        if (_hitEffect != null)
        {
            _hitEffect.enabled = true;
            _hitEffect.SetTarget(target);
        }
    }

    private void OnAnimationEnd()
    {
        _animator.enabled = false;
    }
}

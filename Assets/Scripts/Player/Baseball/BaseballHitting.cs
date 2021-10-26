using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballHitting : Hitting
{
    [SerializeField] private float _cooldown;

    private float _currentTime;
    private Animator _animator;

    protected override void Awake()
    {
        base.Awake();

        _animator = GetComponent<Animator>();
        _currentTime = _cooldown;
    }

    private void Start()
    {
        _currentTime = _cooldown;
    }

    private void Update()
    {
        if (_currentTime >= _cooldown && !_isHitting)
        {
            _isHitting = true;
            _animator.SetTrigger("Hit");
        }

        _currentTime += Time.deltaTime;
    }

    protected override void OnHit()
    {
        _isHitting = true;
        _cuurentBall = _pitcher.CurrentBall;
        _cuurentBall.Kick(_aiming.Target.point);
    }

    protected override void OnHitEnd()
    {
        _isHitting = false;
    }
}

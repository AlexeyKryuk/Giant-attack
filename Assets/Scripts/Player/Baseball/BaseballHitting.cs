using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseballHitting : Hitting
{
    [SerializeField] private float _cooldown;
    [SerializeField] private Strafing _strafing;

    private float _currentTime;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _currentTime = _cooldown;
    }

    private void Update()
    {
        if (_currentTime >= _cooldown && !_strafing.IsStrafing && !_isHitting)
        {
            if (Time.timeScale > 0)
            {
                _isHitting = true;
                _animator.SetTrigger("Hit");
            }

            _currentTime = 0;
        }

        _currentTime += Time.deltaTime;
    }

    protected override void OnHit()
    {
        _isHitting = true;
        _cuurentBall = _pitcher.CurrentBall;
        _cuurentBall.Kick(_aiming.Target);
    }

    protected override void OnHitEnd()
    {
        _isHitting = false;
    }
}

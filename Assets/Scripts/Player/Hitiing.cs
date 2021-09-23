using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Hitiing : MonoBehaviour
{
    [SerializeField] private PlayerAnimationEvents _animationEvents;
    [SerializeField] private Pitcher _pitcher;
    [SerializeField] private Aiming _aiming;
    [SerializeField] private Strafing _strafing;
    [SerializeField] private float _cooldown;

    private bool _isHitting;
    private float _currentTime;
    private Ball _cuurentBall;
    private Animator _animator;

    public bool IsHitting => _isHitting;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _currentTime = _cooldown;
        _animationEvents.HitEvent += OnHit;
        _animationEvents.HitEndEvent += OnHitEnd;
    }

    private void OnDisable()
    {
        _animationEvents.HitEvent -= OnHit;
        _animationEvents.HitEndEvent -= OnHitEnd;
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

    private void OnHitEnd()
    {
        _isHitting = false;
    }

    public void OnHit()
    {
        _cuurentBall = _pitcher.CurrentBall;
        _cuurentBall.Kick(_aiming.Target);
    }
}

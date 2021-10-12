using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Hitting : MonoBehaviour
{
    [SerializeField] protected PlayerAnimationEvents _animationEvents;
    [SerializeField] protected Pitcher _pitcher;
    [SerializeField] protected Aiming _aiming;

    private Player _player;
    protected bool _isHitting;
    protected Ball _cuurentBall;

    public bool IsHitting => _isHitting;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    protected virtual void OnEnable()
    {
        _animationEvents.HitEvent += OnHit;
        _animationEvents.HitEndEvent += OnHitEnd;
        _player.AllEnemyDied += OnLevelComplete;
    }

    protected virtual void OnDisable()
    {
        _animationEvents.HitEvent -= OnHit;
        _animationEvents.HitEndEvent -= OnHitEnd;
        _player.AllEnemyDied -= OnLevelComplete;
    }

    protected abstract void OnHitEnd();

    protected abstract void OnHit();

    private void OnLevelComplete() => enabled = false;
}

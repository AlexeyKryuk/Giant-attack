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
    [Space]
    [SerializeField] private float _angleInDegrees;
    [SerializeField] private float _kickForce;
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
        LookAtXZ(_cuurentBall.transform, _aiming.Target);

        Vector3 direction = _aiming.Target - _cuurentBall.transform.position;
        float velocity = GetVelocity(direction);

        _cuurentBall.Kick(direction.normalized, velocity, _kickForce);
    }

    private void LookAtXZ(Transform transform, Vector3 point)
    {
        transform.localEulerAngles = new Vector3(_angleInDegrees, 0f, 0f);
    }

    private float GetVelocity(Vector3 direction)
    {
        Vector3 fromToXZ = new Vector3(direction.x, 0f, direction.z);

        float x = fromToXZ.magnitude;
        float y = direction.y;

        float angleInRadians = _angleInDegrees * Mathf.PI / 180;

        float v2 = (Physics.gravity.y * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        return v;
    }
}

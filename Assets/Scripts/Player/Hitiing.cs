using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Hitiing : MonoBehaviour
{
    [SerializeField] private PlayerAnimationEvents _animationEvents;
    [SerializeField] private TouchDetection _touchDetection;
    [SerializeField] private Pitcher _pitcher;
    [SerializeField] private Aiming _aiming;
    [Space]
    [SerializeField] private float _angleInDegrees;
    [SerializeField] private float _kickForce;

    private Ball _cuurentBall;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        _animationEvents.HitEvent += OnHit;
        _touchDetection.Touched += OnTouch;
    }

    private void OnDisable()
    {
        _animationEvents.HitEvent -= OnHit;
        _touchDetection.Touched -= OnTouch;
    }

    private void OnTouch()
    {
        if (Time.timeScale > 0)
        {
            _animator.SetTrigger("Hit");
        }
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

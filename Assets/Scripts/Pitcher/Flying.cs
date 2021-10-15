using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flying : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _curvature = 0.2f;

    private Vector3 _target;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_target != null)
        {
            Vector3 from = this.transform.forward;
            Vector3 to = _target - this.transform.position;
            float time = _curvature / Vector3.Distance(_target, this.transform.position);

            this.transform.forward = Vector3.Slerp(from, to, time);
            _rigidbody.position += this.transform.forward * _moveSpeed * Time.deltaTime;
        }
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
    }
}

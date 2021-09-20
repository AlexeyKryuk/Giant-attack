using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class Ball : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;

    private Animator _animator;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Kick(Vector3 direction, float ballisticVelocity, float kickForce)
    {
        _rigidbody.velocity = direction * ballisticVelocity * kickForce;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Enemy enemy = collision.gameObject.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.ApplyDamage();
            Destroy(gameObject);
        }
    }

    private void OnAnimationEnd()
    {
        _animator.enabled = false;
    }
}

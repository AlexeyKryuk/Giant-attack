using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    [SerializeField] private ParticleSystem _deathEffect;
    [SerializeField] private RayFire.RayfireRigid _rayfire;

    private Collider[] _colliders;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _colliders = GetComponentsInChildren<Collider>();
        _rigidbody = GetComponentInChildren<Rigidbody>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        if (Enemy.IsBoss)
            BossDie();
        else
            EnemyDie();
    }

    private void DisableColliders()
    {
        foreach (var collider in _colliders)
        {
            collider.enabled = false;
        }
    }

    private void BossDie()
    {
        DisableColliders();

        _rayfire.gameObject.SetActive(true);
        _rayfire.transform.SetParent(transform.parent);
        _rayfire.Demolish();

        Destroy(gameObject);
    }

    private void EnemyDie()
    {
        Animator.SetTrigger("Death");

        for (int i = 0; i < _colliders.Length; i++)
        {
            _colliders[i].gameObject.layer = LayerMask.NameToLayer("Default");
        }

        _rigidbody.useGravity = true;
    }

    private void OnAnimationEnd()
    {
        _deathEffect.Play();
        DisableColliders();
        Destroy(gameObject, 2f);
    }
}

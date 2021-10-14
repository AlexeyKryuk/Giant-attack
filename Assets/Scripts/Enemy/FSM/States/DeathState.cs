using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    [SerializeField] private ParticleSystem _deathEffect;
    [SerializeField] private RayFire.RayfireRigid _rayfire;

    private Collider[] _colliders;

    private void Awake()
    {
        _colliders = GetComponentsInChildren<Collider>();
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

        StartCoroutine(MoveDownByTime(2.5f));
    }

    private IEnumerator MoveDownByTime(float time)
    {
        yield return new WaitForSeconds(time);

        _deathEffect.Play();
        Debug.Log(_deathEffect.gameObject.name);
        DisableColliders();
        Destroy(gameObject, 2f);
    }
}

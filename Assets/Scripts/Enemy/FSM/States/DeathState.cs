using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
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
        StartCoroutine(DestroyByTime(0f));
    }

    private void EnemyDie()
    {
        Animator.SetTrigger("Death");
        StartCoroutine(DestroyByTime(2f));
    }

    private IEnumerator DestroyByTime(float time)
    {
        yield return new WaitForSeconds(time);
        DisableColliders();

        _rayfire.gameObject.SetActive(true);
        _rayfire.transform.SetParent(transform.parent);
        _rayfire.Demolish();

        Destroy(gameObject);
    }
}

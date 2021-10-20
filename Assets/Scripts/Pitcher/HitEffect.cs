using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hitEffectSlenderPrefab;
    [SerializeField] private ParticleSystem _hitEffectBossPrefab;

    private Vector3 _target;

    public bool IsFlying { get; set; }

    private void OnCollisionEnter(Collision collision)
    {
        if (_target == null || !IsFlying)
            return;

        Enemy enemy = collision.collider.GetComponentInParent<Enemy>();
        ParticleSystem hitEffect;

        if (enemy != null)
        {
            enemy.ApplyDamage();

            if (enemy.IsBoss)
                hitEffect = _hitEffectBossPrefab;
            else
                hitEffect = _hitEffectSlenderPrefab;

            SetHitEffect(collision.contacts[0].point, collision.transform, hitEffect);
        }

        Destroy(gameObject);
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
    }

    private void SetHitEffect(Vector3 point, Transform parent, ParticleSystem effect)
    {
        var hitEffect = Instantiate(effect);
        hitEffect.transform.position = point;
        hitEffect.transform.SetParent(parent);
    }
}


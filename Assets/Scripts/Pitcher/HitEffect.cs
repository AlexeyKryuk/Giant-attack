using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hitEffectPrefab;

    private Vector3 _target;
    private bool _isHit;

    private void OnCollisionEnter(Collision collision)
    {
        if (_target == null)
            return;

        Enemy enemy = collision.collider.GetComponentInParent<Enemy>();

        if (enemy != null)
        {
            enemy.ApplyDamage();
            SetHitEffect(collision.contacts[0].point, collision.transform);
        }

        Destroy(gameObject);
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
    }

    private void SetHitEffect(Vector3 point, Transform parent)
    {
        var hitEffect = Instantiate(_hitEffectPrefab);
        hitEffect.transform.position = point;
        hitEffect.transform.SetParent(parent);
    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hitEffectPrefab;

    private Vector3 _target;

    private void Update()
    {
        if (_target != Vector3.zero)
        {
            Enemy enemy = CastEnemy(Vector3.forward);

            if (Vector3.Distance(transform.position, _target) < 1f)
            {
                if (enemy != null)
                {
                    Debug.Log("Hit");
                    enemy.ApplyDamage();
                    SetHitEffect(_target, enemy.transform);
                }

                Destroy(gameObject);
            }
        }
    }

    public void SetTarget(Vector3 target)
    {
        _target = target;
    }

    private Enemy CastEnemy(Vector3 direction)
    {
        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;

        if (Physics.SphereCast(ray, 1f, out hit, 1f))
        {
            Enemy enemy = hit.collider.GetComponentInParent<Enemy>();

            if (enemy != null)
                return enemy;
            else
                return null;
        }
        else
            return null;
    }

    private void SetHitEffect(Vector3 point, Transform parent)
    {
        var hitEffect = Instantiate(_hitEffectPrefab);
        hitEffect.transform.position = point;
        hitEffect.transform.SetParent(parent);
    }
}


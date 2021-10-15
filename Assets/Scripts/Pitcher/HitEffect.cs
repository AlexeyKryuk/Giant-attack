using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _hitEffectPrefab;
    [SerializeField] private LayerMask _layerMask;

    private Vector3 _target;

    public bool IsFlying { get; set; }

    //private void Update()
    //{
    //    Ray ray = new Ray(transform.position, transform.forward);
    //    RaycastHit hit;

    //    if (Physics.Raycast(ray, out hit, 1.5f))
    //    {
    //        Enemy enemy = hit.collider.GetComponentInParent<Enemy>();

    //        if (enemy != null)
    //        {
    //            enemy.ApplyDamage();
    //            SetHitEffect(hit.point, hit.transform);
    //            Destroy(gameObject);
    //        }

    //        Destroy(gameObject);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (_target == null || !IsFlying)
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


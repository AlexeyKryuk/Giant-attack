using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RayFire;

public class DemolishTrigger : MonoBehaviour
{
    [SerializeField] private RayfireRigid _rayfireRigid;
    [SerializeField] private ParticleSystem _effect;

    private bool _isTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (_isTrigger)
            return;

        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            _rayfireRigid.Demolish();
            _effect.Play();
            _isTrigger = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RayFire;

public class DemolishTrigger : MonoBehaviour
{
    [SerializeField] private CameraShake _camera;
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
            _camera.Shake();
            _rayfireRigid.Demolish();

            if (_effect != null)
                _effect.Play();
            
            _isTrigger = true;
        }
    }
}

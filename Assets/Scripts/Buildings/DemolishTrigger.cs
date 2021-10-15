using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RayFire;

public class DemolishTrigger : MonoBehaviour
{
    [SerializeField] private CameraShake _camera;
    [SerializeField] private List<RayfireRigid> _rayfireRigid;
    [SerializeField] private List<Tossing> _fans;
    [SerializeField] private ParticleSystem _effect;

    private bool _isTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (_isTrigger)
            return;

        Enemy enemy = other.GetComponentInParent<Enemy>();
        if (enemy != null && enemy.IsBoss)
        {
            _camera.Shake();

            foreach (var rf in _rayfireRigid)
                rf.Demolish();

            foreach (var fan in _fans)
                fan.Toss();

            if (_effect != null)
                _effect.Play();
            
            _isTrigger = true;
        }
    }
}

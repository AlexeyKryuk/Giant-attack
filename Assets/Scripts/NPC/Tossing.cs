using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Tossing : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _meshRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _force;

    private Rigidbody _rigidbody;
    private Rigidbody[] _allRigidbodies;
    private Collider[] _allColliders;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _allRigidbodies = GetComponentsInChildren<Rigidbody>();
        _allColliders = GetComponentsInChildren<Collider>();
    }

    private void OnEnable()
    {
        MakePhysical(false);
    }

    private void OnDisable()
    {
        MakePhysical(false);
    }

    public void Toss()
    {
        MakePhysical(true);
        AddForce(_force);
        //DisableCollision();
        Recolor(Color.grey);

        Destroy(gameObject, 4f);
    }

    private void MakePhysical(bool value)
    {
        _animator.enabled = !value;

        for (int i = 0; i < _allRigidbodies.Length; i++)
        {
            _allRigidbodies[i].isKinematic = !value;
        }
        for (int i = 0; i < _allColliders.Length; i++)
        {
            _allColliders[i].enabled = value;
        }

        _rigidbody.isKinematic = true;
    }

    private void AddForce(float force)
    {
        Vector3 ownDirection = (transform.forward - transform.position).normalized;
        Vector3 direction = -ownDirection / 3 + Vector3.up;

        foreach (var rigidbody in _allRigidbodies)
        {
            rigidbody.AddForce(direction * force, ForceMode.Impulse);
        }
    }

    //private void DisableCollision()
    //{
    //    gameObject.layer = LayerMask.NameToLayer("IgnoreCollision");

    //    foreach (var go in gameObject.GetAllChilds())
    //    {
    //        go.layer = LayerMask.NameToLayer("IgnoreCollision");
    //    }
    //}

    private void Recolor(Color newColor)
    {
        _meshRenderer.material.color = newColor;
    }
}

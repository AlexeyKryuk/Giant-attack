using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitiing : MonoBehaviour
{
    [SerializeField] private PlayerAnimationEvents _animationEvents;
    [SerializeField] private Pitcher _pitcher;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _crosshair;
    [SerializeField] private float _angleInDegrees;

    private Vector3 _target;
    private Transform _cuurentBall;

    private void OnEnable()
    {
        _animationEvents.HitEvent += OnHit;
    }

    private void OnDisable()
    {
        _animationEvents.HitEvent -= OnHit;
    }

    private void Update()
    {


    }

    public void OnHit()
    {
        _cuurentBall = _pitcher.CurrentBall.transform;
        ProjectTarget();
        CalculatePath(_cuurentBall.position, _target);
    }

    private void ProjectTarget()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(_crosshair.position);
        ray.direction *= 100f;

        if (Physics.Raycast(ray, out hit))
        {
            _target = hit.point;
        }
        else
        {
            _target = ray.direction;
        }
    }

    private void CalculatePath(Vector3 from, Vector3 to)
    {
        Vector3 fromTo = to - from;
        Vector3 fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);

        float x = fromToXZ.magnitude;
        float y = fromTo.y;

        float angleInRadians = _angleInDegrees * Mathf.PI / 180;

        float v2 = (Physics.gravity.y * x * x) / (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
        float v = Mathf.Sqrt(Mathf.Abs(v2));

        _cuurentBall.GetComponent<Rigidbody>().velocity = fromTo.normalized * v * 3f;
        Debug.DrawLine(from, to);
    }
}

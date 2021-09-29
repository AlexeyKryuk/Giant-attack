using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballHitting : Hitting
{
    [SerializeField] private Animator _animator;
    [SerializeField] private TrajectoryDrawing _trajectory;
    [SerializeField] private Transform _projectoryBall;

    [SerializeField] private float _firingAngle = 45.0f;
    [SerializeField] private float _gravity = 9.8f;

    private List<Vector3> _points = new List<Vector3>();
    private List<Vector3> _projectPoints = new List<Vector3>();
    private Vector3 _currentMousePos = Vector3.zero;

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(SimulateProjectile(true));
        }

        if (Input.GetMouseButton(0))
        {
            if (Vector3.Distance(_currentMousePos, Input.mousePosition) > 0)
                StartCoroutine(SimulateProjectile(true));
        }

        if (Input.GetMouseButtonUp(0))
        {
            _trajectory.ResetPoints();
        }

        if (Input.GetMouseButtonUp(0) && !_isHitting)
        {
            _animator.SetTrigger("Kick");
        }

        _currentMousePos = Input.mousePosition;
    }

    protected override void OnHit()
    {
        _isHitting = true;
        _cuurentBall = _pitcher.CurrentBall;
        _cuurentBall.Kick(_aiming.Target);

        StartCoroutine(SimulateProjectile(false));
    }

    protected override void OnHitEnd()
    {
        _isHitting = false;
        _pitcher.KickOff();
        _cuurentBall = _pitcher.CurrentBall;
    }

    private IEnumerator SimulateProjectile(bool isTrajectory)
    {
        Transform projectile;
        if (isTrajectory)
            projectile = _projectoryBall;
        else
            projectile = _cuurentBall.transform;

        _trajectory.ResetPoints();

        // Calculate distance to target
        float target_Distance = Vector3.Distance(projectile.position, _aiming.Target);

        // Calculate the velocity needed to throw the object to the target at specified angle.
        float projectile_Velocity = target_Distance / (Mathf.Sin(2 * _firingAngle * Mathf.Deg2Rad) / _gravity);

        // Extract the X  Y componenent of the velocity
        float Vx = Mathf.Sqrt(projectile_Velocity) * Mathf.Cos(_firingAngle * Mathf.Deg2Rad);
        float Vy = Mathf.Sqrt(projectile_Velocity) * Mathf.Sin(_firingAngle * Mathf.Deg2Rad);

        // Calculate flight time.
        float flightDuration = target_Distance / Vx;

        // Rotate projectile to face the target.
        projectile.rotation = Quaternion.LookRotation(_aiming.Target - projectile.position);

        float elapse_time = 0;

        if (isTrajectory)
        {
            for (float i = 0; i < flightDuration; i += 0.01f)
            {
                _points.Add(new Vector3(0, (Vy - (_gravity * i)) * 0.01f, Vx * 0.01f));
                projectile.Translate(_points[_points.Count - 1]);
                _projectPoints.Add(projectile.position);
            }
        }
        else
        {
            while (elapse_time < flightDuration)
            {
                if (projectile == null)
                {
                    yield break;
                }

                _points.Add(new Vector3(0, (Vy - (_gravity * elapse_time)) * Time.deltaTime, Vx * Time.deltaTime));
                projectile.Translate(_points[_points.Count - 1]);

                elapse_time += Time.deltaTime;

                yield return null;
            }
        }



        if (isTrajectory)
        {
            _trajectory.SetPoints(_projectPoints.ToArray());
            projectile.localPosition = Vector3.zero;
        }

        _points.Clear();
        _projectPoints.Clear();
    }
}

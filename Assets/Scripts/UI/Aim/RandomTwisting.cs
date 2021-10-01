using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomTwisting : Twisting
{
    [SerializeField] private float _radius;

    public RectTransform Crosshair { get => _crosshair; private set => _crosshair = value; }

    private Vector3 _nextPosition;
    private Vector3 _center;

    protected override void OnEnable()
    {
        base.OnEnable();
        _nextPosition = _crosshair.position;
        _center = _crosshair.position;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
    }

    protected override void OnTouch()
    {
        enabled = false;
    }

    private void Update()
    {
        RandomMove();
    }

    private void RandomMove()
    {
        if (Vector3.Distance(_crosshair.position, _nextPosition) < 20f)
        {
            _nextPosition = GetNextPosition();
        }
        _crosshair.position = Vector3.Lerp(_crosshair.position, _nextPosition, _speed * Time.unscaledDeltaTime);
    }

    private Vector3 GetNextPosition()
    {
        Vector2 pointOnCircle = GetPointOnCircle(_radius);
        float x = pointOnCircle.x;
        float y = pointOnCircle.y;

        Vector3 randomPosition = new Vector3(x, y, 0);

        return randomPosition;
    }

    private Vector2 GetPointOnCircle(float radius)
    {
        float randomAngle = UnityEngine.Random.Range(0f, 2 * Mathf.PI - float.Epsilon);
        Vector2 pointOnCircle = new Vector3(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle), 0f) * radius + _center;
        return pointOnCircle;
    }
}

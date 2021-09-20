using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strafe : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private SwipeDetection _swipeDetection;
    [SerializeField] private float _distance;
    [SerializeField] private float _smoothness;

    private Vector3 _direction;
    private Vector3 _targetPosition;
    private bool _isStrafing;
    private Side _currentSide = Side.Middle;
    private Coroutine _coroutine;

    public Side CurrentSide => _currentSide;

    private void OnEnable()
    {
        _swipeDetection.Swiped += OnSwipe;
    }

    private void OnDisable()
    {
        _swipeDetection.Swiped -= OnSwipe;
    }

    private void Start()
    {
        _targetPosition = transform.position;
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, _targetPosition) < 0.01f)
        {
            _isStrafing = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPosition, _smoothness * Time.deltaTime);
            _isStrafing = true;
        }
    }

    private void OnSwipe(Side side)
    {
        if (!_isStrafing && _animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            if (_currentSide != side)
            {
                if (side == Side.Left)
                    _currentSide--;
                else if (side == Side.Right)
                    _currentSide++;

                _animator.SetTrigger(nameof(Strafe));

                _direction = new Vector3((float)side * _distance, 0f, 0f);
                _targetPosition = transform.position + _direction;
            }
        }
    }
}

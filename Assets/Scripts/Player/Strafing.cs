using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strafing : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Hitting _hitting;
    [SerializeField] private SwipeDetection _swipeDetection;
    [SerializeField] private float _distance;
    [SerializeField] private float _smoothness;

    private Player _player;
    private Vector3 _targetPosition;
    private Side _currentSide = Side.Middle;
    private bool _isStrafing;

    public Side CurrentSide => _currentSide;
    public bool IsStrafing => _isStrafing;

    private void Awake()
    {
        _player = GetComponent<Player>();
    }

    private void OnEnable()
    {
        _swipeDetection.Swiped += Strafe;
        _player.AllEnemyDied += OnLevelComplete;
    }

    private void OnDisable()
    {
        _swipeDetection.Swiped -= Strafe;
        _player.AllEnemyDied -= OnLevelComplete;
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
        }
    }

    private void Strafe(Side side)
    {
        if (!_isStrafing && !_hitting.IsHitting)
        {
            if (_currentSide != side)
            {
                _isStrafing = true;

                if (side == Side.Left)
                    _currentSide--;
                else if (side == Side.Right)
                    _currentSide++;


                _animator.SetTrigger("Strafe");

                Vector3 direction = new Vector3((float)side * _distance, 0f, 0f);
                _targetPosition = transform.position + direction;

                StartCoroutine(GoBack(_currentSide));
            }
        }
    }

    private IEnumerator GoBack(Side sideFrom)
    {
        yield return new WaitUntil(() => !_isStrafing);

        if (sideFrom == Side.Left)
            Strafe(Side.Right);
        else if (sideFrom == Side.Right)
            Strafe(Side.Left);
        else
            yield return null;
    }

    private void OnLevelComplete() => enabled = false;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitcher : MonoBehaviour
{
    [SerializeField] private PlayerAnimationEvents _animationEvents;
    [SerializeField] private GameObject _ballPrefab;

    private GameObject _currentBall;

    private void OnEnable()
    {
        _animationEvents.BlowBeganEvent += KickOff;
    }

    private void OnDisable()
    {
        _animationEvents.BlowBeganEvent -= KickOff;
    }

    private void KickOff()
    {
        _currentBall = Instantiate(_ballPrefab, transform.position, transform.rotation, transform);
    }
}

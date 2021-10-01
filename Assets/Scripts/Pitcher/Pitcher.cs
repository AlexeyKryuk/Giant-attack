using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pitcher : MonoBehaviour
{
    [SerializeField] private PlayerAnimationEvents _animationEvents;
    [SerializeField] private Ball _ballPrefab;
    [SerializeField] private bool _needStartBall;

    public Ball CurrentBall { get; private set; }

    private void OnEnable()
    {
        if (_needStartBall)
            KickOff();

        _animationEvents.BlowBeganEvent += KickOff;
    }

    private void OnDisable()
    {
        _animationEvents.BlowBeganEvent -= KickOff;
    }

    public void KickOff()
    {
        CurrentBall = Instantiate(_ballPrefab, transform.position, transform.rotation, transform);
    }
}

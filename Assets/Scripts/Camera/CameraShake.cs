using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraShake : MonoBehaviour
{
    [SerializeField] private Player _player;
    [Space]
    [SerializeField] private float _duration;
    [SerializeField] private float _strength;
    [SerializeField] private int _vibrato;
    [SerializeField] private float _randomness;
    [SerializeField] private bool _fadeOut;

    private Camera _camera;

    private void Awake()
    {
        _camera = GetComponent<Camera>();
    }

    private void OnEnable()
    {
        _player.BossDied += Shake;
    }

    private void OnDisable()
    {
        _player.BossDied -= Shake;
    }

    public void Shake()
    {
        StartCoroutine(StartShakeByTime(0.2f));
    }

    private IEnumerator StartShakeByTime(float time)
    {
        yield return new WaitForSeconds(time);

        _camera.DOShakePosition(_duration, _strength, _vibrato, _randomness, _fadeOut);
    }
}

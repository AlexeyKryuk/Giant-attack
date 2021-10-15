using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanBehaviour : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private float _timer = 3f;
    private float _currentTime;

    private void Update()
    {
        if (_currentTime >= _timer)
        {
            float random = Random.Range(0f, 1f);
            _timer = Random.Range(0f, 3f);

            _animator.SetTrigger("Action");
            _animator.SetFloat("Variant", random);

            _currentTime = 0;
        }

        _currentTime += Time.deltaTime;
    }
}

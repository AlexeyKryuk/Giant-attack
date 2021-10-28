using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TapToStartPanel : MonoBehaviour
{
    [SerializeField] private InputDetection _input;

    public UnityAction GameBegun;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        _input.TouchBegan += OnTouch;
    }

    private void OnDisable()
    {
        _input.TouchBegan -= OnTouch;
    }

    private void OnTouch()
    {
        Time.timeScale = 1;
        GameBegun?.Invoke();
        gameObject.SetActive(false);
    }
}

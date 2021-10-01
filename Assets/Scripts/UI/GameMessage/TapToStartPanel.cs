using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapToStartPanel : MonoBehaviour
{
    [SerializeField] private InputDetection _input;

    private void Awake()
    {
        Time.timeScale = 0;
    }

    private void OnEnable()
    {
        _input.Touched += OnTouch;
    }

    private void OnDisable()
    {
        _input.Touched -= OnTouch;
    }

    private void OnTouch()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
}

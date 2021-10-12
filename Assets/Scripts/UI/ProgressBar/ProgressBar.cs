using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Text _label;

    private Slider _slider;
    private float _incrementStep;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _player.EnemyDied += OnEnemyDie;
    }

    private void OnDisable()
    {
        _player.EnemyDied -= OnEnemyDie;
    }

    private void Start()
    {
        _incrementStep = 1f / _player.Enemies.Count;
    }

    private void OnEnemyDie()
    {
        _slider.value += _incrementStep;

        if (_slider.value == 1)
        {
            _label.gameObject.SetActive(true);
            _label.rectTransform.DOShakeScale(1f, 0.5f, 5);
        }    
    }
}

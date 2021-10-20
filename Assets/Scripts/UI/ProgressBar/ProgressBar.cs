using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProgressBar : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _label;
    [Space]
    [SerializeField] private TMP_Text _currentLevel;
    [SerializeField] private TMP_Text _nextLevel;

    private Slider _slider;
    private float _incrementStep;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _currentLevel.text = Level.CurrentLevel.ToString();
        _nextLevel.text = (Level.CurrentLevel + 1).ToString();
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

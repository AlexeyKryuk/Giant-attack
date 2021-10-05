using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultViewer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Image _background;
    [Space]
    [SerializeField] private Button _loseButton;
    [SerializeField] private Button _winButton;
    [Space]
    [SerializeField] private Image _loseLabel;
    [SerializeField] private Image _winLabel;

    private Color _grey;
    private Coroutine _coroutine;

    private void OnEnable()
    {
        _grey = _background.color;
        _player.Died += OnPlayerDie;
        _player.AllEnemyDied += OnEnemyDie;
    }

    private void OnDisable()
    {
        _grey.a = 0f;
        _background.color = _grey;
        _player.Died -= OnPlayerDie;
        _player.AllEnemyDied -= OnEnemyDie;
    }

    private void OnPlayerDie()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(ShowPanelByTime(2f, _loseButton, _loseLabel));
    }

    private void OnEnemyDie()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(ShowPanelByTime(2f, _winButton, _winLabel));
    }

    private IEnumerator ShowPanelByTime(float time, Button button, Image label)
    {
        yield return new WaitForSeconds(time);

        _grey.a = 0.4f;

        _background.color = _grey;
        button.gameObject.SetActive(true);
        label.gameObject.SetActive(true);
    }
}

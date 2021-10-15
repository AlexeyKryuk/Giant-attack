using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ResultViewer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private SceneLoader _sceneLoader;
    [SerializeField] private Image _background;
    [Space]
    [SerializeField] private Button _loseButton;
    [SerializeField] private Button _winButton;
    [Space]
    [SerializeField] private Image _loseLabel;
    [SerializeField] private Image _winLabel;

    private Color _grey;
    private Coroutine _coroutine;

    public UnityAction Won;
    public UnityAction Lose;
    public UnityAction Restarted;

    private void OnEnable()
    {
        _grey = _background.color;
        _sceneLoader.CurrentSceneLoaded += OnRestartLevel;
        _player.Died += OnPlayerDie;
        _player.AllEnemyDied += OnEnemyDie;
    }

    private void OnDisable()
    {
        _grey.a = 0f;
        _background.color = _grey;
        _sceneLoader.CurrentSceneLoaded -= OnRestartLevel;
        _player.Died -= OnPlayerDie;
        _player.AllEnemyDied -= OnEnemyDie;
    }

    private void OnPlayerDie()
    {
        if (_coroutine == null)
        {
            Lose?.Invoke();
            _coroutine = StartCoroutine(ShowPanelByTime(2f, _loseButton, _loseLabel));
        }
    }

    private void OnEnemyDie()
    {
        if (_coroutine == null)
        {
            Won?.Invoke();
            _coroutine = StartCoroutine(ShowPanelByTime(2f, _winButton, _winLabel));
        }
    }

    private void OnRestartLevel()
    {
        Restarted?.Invoke();
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

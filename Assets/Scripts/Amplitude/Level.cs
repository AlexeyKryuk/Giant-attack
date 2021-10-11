using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    [SerializeField] private TapToStartPanel startPanel;
    [SerializeField] private ResultViewer resultViewer;

    public UnityAction LevelStarted;
    public UnityAction LevelWon;
    public UnityAction LevelLost;

    public int CurrentLevel { get; private set; }

    private void OnEnable()
    {
        startPanel.GameBegun += OnLevelStart;
        resultViewer.Lose += OnLose;
        resultViewer.Won += OnWon;
    }

    private void OnDisable()
    {
        startPanel.GameBegun -= OnLevelStart;
        resultViewer.Lose -= OnLose;
        resultViewer.Won -= OnWon;
    }

    private void OnLevelStart()
    {
        LevelStarted?.Invoke();
    }

    private void OnLose()
    {
        LevelLost?.Invoke();
    }

    private void OnWon()
    {
        LevelWon?.Invoke();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class StartMoveTransition : Transition
{
    [SerializeField] private TapToStartPanel _startPanel;

    private Enemy _enemy;
    private Coroutine _coroutine;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        _startPanel.GameBegun += OnGameBegun;
    }

    private void OnDisable()
    {
        _startPanel.GameBegun -= OnGameBegun;
    }

    private void OnGameBegun()
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(DelayBeforeTransit());
    }

    private IEnumerator DelayBeforeTransit()
    {
        float animationLength = Random.Range(0, 3);
        yield return new WaitForSeconds(animationLength);
        NeedTransit = true;
    }
}

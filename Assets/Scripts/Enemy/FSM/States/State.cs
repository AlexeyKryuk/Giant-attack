using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;
    [SerializeField] private Animator _animator;

    protected Player Target { get; set; }

    public List<Transition> Transitions { get => _transitions; private set => _transitions = value; }
    public Animator Animator { get => _animator; private set => _animator = value; }

    protected virtual void OnEnable()
    {
        if (Target != null)
            Target.Died += OnTargetDie;
    }

    protected virtual void OnDisable()
    {
        if (Target != null)
            Target.Died -= OnTargetDie;
    }

    public void Enter(Player target)
    {
        if (enabled == false)
        {
            Target = target;
            enabled = true;

            foreach (var transition in Transitions)
            {
                transition.enabled = true;
                transition.Init(target);
            }
        }
    }

    public void Exit()
    {
        if (enabled == true)
        {
            foreach (var transition in Transitions)
                transition.enabled = false;

            enabled = false;
        }
    }

    public State GetNextState()
    {
        foreach (var transition in Transitions)
        {
            if (transition.NeedTransit)
                return transition.TargetState;
        }

        return null;
    }

    protected void OnTargetDie()
    {
        Target.Died -= OnTargetDie;
        Target = null;
    }
}

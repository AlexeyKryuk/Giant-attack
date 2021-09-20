using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    [SerializeField] private List<Transition> _transitions;

    protected Player Target { get; set; }
    protected Animator Animator { get; private set; }
    protected Enemy Enemy { get; private set; }

    public List<Transition> Transitions { get => _transitions; private set => _transitions = value; }

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

    public void Enter(Player target, Animator animator, Enemy enemy)
    {
        if (enabled == false)
        {
            Target = target;
            Animator = animator;
            Enemy = enemy;
            enabled = true;

            foreach (var transition in Transitions)
            {
                transition.enabled = true;
                transition.Init(target, animator);
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
        Target = null;
    }
}

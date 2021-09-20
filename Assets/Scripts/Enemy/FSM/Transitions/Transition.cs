using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class Transition : MonoBehaviour
{
    [SerializeField] private State _targetState;

    public Player Target { get; private set; }
    public Animator Animator { get; private set; }

    public State TargetState => _targetState;

    public bool NeedTransit { get; protected set; }

    public void Init(Player target, Animator animator)
    {
        Target = target;
        Animator = animator;
    }

    protected virtual void OnEnable()
    {
        NeedTransit = false;
    }
}

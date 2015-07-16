using UnityEngine;
using System;
using System.Collections;

public abstract class StateModule : MonoBehaviour {

	protected DelegateStateMachine _stateMachine;

	protected void Awake()
	{
		_stateMachine = GetComponent<DelegateStateMachine> ();
	}

	public void InitModule(System.Enum value)
	{
		_stateMachine.InitState (value, OnEnter, OnExit, OnWork);
	}

	public abstract void OnEnter ();
	public abstract void OnExit ();
	public abstract IEnumerator OnWork ();
}

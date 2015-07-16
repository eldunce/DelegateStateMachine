using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class DelegateStateMachine : MonoBehaviour{

	public delegate void StateTransitionDelegate();
	public delegate IEnumerator StateWorkDelegate();
		
	Dictionary<Enum, StateTransitionDelegate> mStartStateDelegate = new Dictionary<Enum, StateTransitionDelegate>();
	Dictionary<Enum, StateWorkDelegate> mStateWorkDelegate = new Dictionary<Enum, StateWorkDelegate>();
	Dictionary<Enum, StateTransitionDelegate> mEndStateDelegate = new Dictionary<Enum, StateTransitionDelegate>();
	
	IEnumerator mCurrentWorkDelegate;
	Enum mCurrentState;
	
	public System.Enum CurrentState{
		get{
			return mCurrentState;
		}
	}
	
	public void InitState(Enum state, StateTransitionDelegate start, StateTransitionDelegate end, StateWorkDelegate work){
		mStartStateDelegate[state] = start;
		mEndStateDelegate[state] = end;
		mStateWorkDelegate[state] = work;
	}

	public void InitStateWithModule(Enum state, StateModule module)
	{
		InitState (state, module.OnEnter, module.OnExit, module.OnWork);
	}
	
	public void ChangeState(Enum newState){
		if(mCurrentWorkDelegate != null){
			StopCoroutine(mCurrentWorkDelegate);	
		}
		
		if(mCurrentState != null){
			StateTransitionDelegate exitDelegate = mEndStateDelegate[mCurrentState];
			if(exitDelegate != null){
				exitDelegate();
			}
		}
		
		StateTransitionDelegate enterDelegate = mStartStateDelegate[newState];
		if(enterDelegate != null){
			enterDelegate();
		}
		
		mCurrentState = newState;
		
		StateWorkDelegate workDelegate = mStateWorkDelegate[mCurrentState];
		if(workDelegate != null){
			mCurrentWorkDelegate = workDelegate();
			StartCoroutine(mCurrentWorkDelegate);
		}
	}
	
}

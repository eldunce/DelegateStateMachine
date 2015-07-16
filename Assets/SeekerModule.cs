using UnityEngine;
using System.Collections;

[RequireComponent(typeof(DelegateStateMachine))]
[RequireComponent(typeof(Rigidbody))]
public class SeekerModule : StateModule {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public override void OnEnter(){
		
	}

	public override void OnExit(){
		
	}

	public override IEnumerator OnWork(){
		SimpleAI ai = GetComponent<SimpleAI> ();
		while(true){
			Vector3 targetPos = ai.targetLastKnownPosition;
			Vector3 dir = targetPos - transform.position;
			dir = dir.normalized * ai.moveSpeed;
			GetComponent<Rigidbody>().MovePosition(transform.position + dir * Time.deltaTime);
			yield return null;
		}
	}

}

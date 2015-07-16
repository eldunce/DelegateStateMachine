using UnityEngine;
using System.Collections;
[RequireComponent(typeof(DelegateStateMachine))]
public class LikesDancingModule : StateModule {

	DelegateStateMachine stateMachine;
	public float danceHeight = 2f;

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
		float t = 0;
		while(true){
			// not a great dancer
			t+=Time.deltaTime;
			Vector3 pos = transform.position;
			pos.y =  Mathf.Sin(t) * danceHeight/2 + danceHeight;
			transform.position = pos;
			yield return null;
		}
	}
}

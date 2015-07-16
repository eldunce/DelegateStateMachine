// Simple AI Component; governs LOS & detection
// actual behaviors for each state are left to components
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(DelegateStateMachine))]
public class SimpleAI : MonoBehaviour {
	// detecton
	public float alertDistance;
	public float viewFOV;
	
	// interaction
	public float moveSpeed;
	public Transform [] waypoints;
	
	// state declarations
	public enum AIStates{
		Idle, // not doing anything
		Investigating, //saw something and became suspicious
		SomethingSpotted, //is currently seeing something
		ArrivedAtSomething // has arrived at something and is taking appropriate action
	}
	// the agent can see in this range in front of him
	public float fieldOfView = 60f;
	// the agent can see to this distance
	public float sightRange = 5f;
	//the distance at which we've "arrived"
	public float arrivalRange = 1f;
	
	//our target if any
	public GameObject target;
	public Vector3 targetLastKnownPosition;
	
	// references
	NavMeshAgent agent;
	DelegateStateMachine state;
	
	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		state = GetComponent<DelegateStateMachine>();
		agent.speed = moveSpeed;

		// init each state
		// todo: store in dictionary of some kind?
		SeekerModule seekModule = GetComponent<SeekerModule> ();
		if (seekModule != null)
			state.InitModule (AIStates.SomethingSpotted, seekModule);

		FollowWaypointsModule followModule = GetComponent<FollowWaypointsModule> ();
		if (followModule != null)
			state.InitModule (AIStates.Idle, followModule);

		StartCoroutine("DetectionRoutine");
	}
	
	// Update is called once per frame
	// Run checks for any targets (other entities with a different tag) within 
	// detection range and fov
	void Update () {
		
	}
	
	IEnumerator DetectionRoutine(){
		while(gameObject.activeInHierarchy){
			GameObject newTarget = null;
			foreach(GameObject ob in GameObject.FindGameObjectsWithTag("Interesting")){
				if(ob != gameObject)
				{
					// check distance
					if(Vector3.Distance(ob.transform.position, transform.position) < sightRange){
						float dot = Vector3.Dot((ob.transform.position - transform.position).normalized,
							transform.forward);
						if(dot >= 0f && dot > Mathf.Cos(fieldOfView) ){
							newTarget = ob;
						}
					}
				}
			}
			// see if we changed targets; if so, go to spotted state
			if(target != newTarget){
				target = newTarget;
				state.ChangeState(AIStates.SomethingSpotted);
			}
			// if we don't have a target anymore, we investigate it
			if(target != null && newTarget == null){
				target = null;
				state.ChangeState(AIStates.Investigating);
			}
			// if we do have a target, update its last known position
			if(target != null){
				targetLastKnownPosition = target.transform.position;
			}
			
			// don't need to run this that often
			yield return new WaitForSeconds(.5f);
		}
	}
}

using UnityEngine;
using System.Collections;

public class FollowWaypointsModule : StateModule {

	public GameObject [] waypoints;
	public float distanceTolerance = .5f;
	public float movementForce = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	override void OnEnter(){
	}

	override void OnExit(){
	}

	override IEnumerator OnWork(){
		for (int i = 0; i < waypoints.Length; ++i) {
			while(Vector3.Distance(transform.position, waypoints[i].transform.position) < distanceTolerance)
			{
				Vector3 direction = waypoints[i].transform.position - transform.position;
				direction.y = 0f;
				GetComponent<Rigidbody>().AddForce(direction.normalized * movementForce * Time.deltaTime);
				yield return null;
			}
		}
	}
}

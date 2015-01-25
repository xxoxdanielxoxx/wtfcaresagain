using UnityEngine;
using System.Collections;

public class NavMeshMovement : MonoBehaviour 
{
	NavMeshAgent agent;
	public Camera petCam;

	// Use this for initialization
	void Start () {
		agent = GetComponent<NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.mousePosition.x < Screen.width / 2 || Input.mousePosition.x > Screen.width || 
		    Input.mousePosition.y < 0 || Input.mousePosition.y > Screen.height)
			return;

		if( Input.GetMouseButtonDown(0) )
		{
			Ray ray = petCam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;

			RaycastHit[] hits = Physics.RaycastAll(ray, 10000.0f);

			Debug.DrawRay( ray.origin, ray.direction * 10000.0f, Color.red );

			if( hits.Length == 0 )
				return;

			RaycastHit firstHit = hits[hits.Length-1];

			//Debug.Log(firstHit.transform.name);

			if( firstHit.transform.gameObject.layer == LayerMask.NameToLayer ("Ground") )
			{
				agent.SetDestination (firstHit.point);
			}
		}
	}
}

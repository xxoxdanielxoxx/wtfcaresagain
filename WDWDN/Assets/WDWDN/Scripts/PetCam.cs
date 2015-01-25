using UnityEngine;
using System.Collections;

public class PetCam : MonoBehaviour 
{
	public Transform target;
	public int xMinBounds;
	public int xMaxBounds;
	public int zMaxBounds;
	public int zMinBounds;

	// Update is called once per frame
	void Update () 
	{
		transform.position = new Vector3 (target.position.x, transform.position.y, target.position.z);

		Vector3 temp = transform.position;

		temp.x = Mathf.Clamp (temp.x, xMinBounds, xMaxBounds);

		temp.z = Mathf.Clamp (temp.z, zMinBounds, zMaxBounds);

		transform.position = temp;
	}
}

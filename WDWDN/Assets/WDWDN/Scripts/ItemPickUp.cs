using UnityEngine;
using System.Collections;

public class ItemPickUp : MonoBehaviour {

	[Range(0.0f, 0.03f)]
	public float m_outlineWidth = 0.005f;

	void OnMouseOver()
	{
		renderer.material.SetFloat("_Outline", m_outlineWidth );
	}
	void OnMouseExit()
	{
		renderer.material.SetFloat("_Outline", 0.0f );
	}
	void OnMouseDown()
	{
		//do whatever you need to do to pick up the item then delete the object
	}
}

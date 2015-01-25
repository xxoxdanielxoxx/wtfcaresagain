using UnityEngine;
using System.Collections;

public class InteractiveObj : MonoBehaviour 
{
	public GameObject guiPopUp;
	public GameObject label;
	public GameObject objectUI;
	public Transform uiPos;

	void Start()
	{
		label.GetComponent<UILabel> ().text = transform.parent.name;
		objectUI.transform.position = uiPos.position;
	}

	void OnMouseOver()
	{
		guiPopUp.GetComponent<Animator> ().SetBool ("mouseOverItem", true);
	}

	void OnMouseExit()
	{
		guiPopUp.GetComponent<Animator> ().SetBool ("mouseOverItem", false);
	}
}

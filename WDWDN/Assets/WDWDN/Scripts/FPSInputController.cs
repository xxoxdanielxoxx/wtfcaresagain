using UnityEngine;
using System.Collections;

[RequireComponent (typeof (CharacterMotor))]
[AddComponentMenu("Character/FPS Input Controller")]

public class FPSInputController : MonoBehaviour 
{	
	private CharacterMotor motor;
	
	void Awake()
	{
		motor = GetComponent<CharacterMotor> ();
	}

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Get the input vector from keyboard or analog stick
		Vector3 directionVector = new Vector3(ControlerWrapper.Get().GetLeftStick (XInputDotNetPure.PlayerIndex.One).x, 0, ControlerWrapper.Get().GetLeftStick(XInputDotNetPure.PlayerIndex.One).y);
		
		directionVector.x += Input.GetAxis("Horizontal");
		directionVector.z += Input.GetAxis("Vertical");
		
		if(directionVector != Vector3.zero)
		{
			// Get the length of the directon vector and then normalize it
			// Dividing by the length is cheaper than normalizing when we already have the length anyway
			float directionLength = directionVector.magnitude;
			directionVector = directionVector / directionLength;
			
			// Make sure the length is no bigger than 1
			directionLength = Mathf.Min (1, directionLength);
			
			// Make the input vector more sensitive towards the extremes and less sensitive in the middle
			// This makes it easier to control slow speeds when using analog sticks
			directionLength = directionLength * directionLength;
			
			// Multiply the normalized direction vector by the modified length
			directionVector = directionVector * directionLength;
		}
	
		if (ControlerWrapper.Get().A_Hit(0) || Input.GetButton("Jump"))
		{
			motor.inputJump = true;
		}
		else
		{
			motor.inputJump = false;
		}
		
		if (Input.GetButton("Fire1"))
		{
			BroadcastMessage("Shoot", SendMessageOptions.DontRequireReceiver);
		}
		
		// Apply the direction to the CharacterMotor
		motor.inputMoveDirection = transform.rotation * directionVector;
	}
}
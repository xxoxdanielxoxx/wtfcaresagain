using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class ControlerLook : MonoBehaviour 
{


	/// Minimum and Maximum values can be used to constrain the possible rotation
	
	
	
	/// To make an FPS style character:
	
	/// - Create a capsule.
	
	/// - Add a rigid body to the capsule
	
	/// - Add the MouseLook script to the capsule.
	
	///   -> Set the mouse look to use LookX. (You want to only turn character but not tilt it)
	
	/// - Add FPSWalker script to the capsule
	
	
	
	/// - Create a camera. Make the camera a child of the capsule. Reset it's transform.
	
	/// - Add a MouseLook script to the camera.
	
	///   -> Set the mouse look to use LookY. (You want the camera to tilt up and down like a head. The character already turns.)
	
	//[AddComponentMenu("Camera-Control/Mouse Look")]
	
	public enum RotationAxes { MouseX = 0, MouseY = 1 }
	
	public RotationAxes axes   = RotationAxes.MouseX;
	

		
		float sensitivityX   = 15F;
	

		
		float sensitivityY   = 15F;
	

		
	public float sensitivityStandardX   = 15F;


		
	public float sensitivityStandardY   = 15F;
	

		
		float offsetY   = 0;
	

		
	float offsetX   = 0;
	

		
		float totalOffsetY   = 0;
	

		
		float totalOffsetX   = 0;
	

		
		float resetSpeed   = 1;
	

		
		float resetDelay   = 0;
	

		
		float maxKickback   = 0;
	

		
		float xDecrease   = 0;
	
	
	
	public float minimumX   = -360F;
	
	public float maximumX  = 360F;
	
	
	
	public float minimumY   = -60F;
	
	public float maximumY   = 60F;
	
	public bool smooth   = true;
	public float smoothFactor   = 2;
	//var smoothIterations = new Array();
	public int  iterations   = 10;
	
	Quaternion  tRotation;
	
	public float idleSway = 0.001f ;
	
	private int minStored ;
	private int maxStored ;
	
	//added by dw to pause camera when in store
	

	static bool freeze   = false;

	bool individualFreeze   = false;
	
	

	float rotationX   = 0F;

	float rotationY = 0F;
	
	

	Quaternion originalRotation  ;
	
	Quaternion[]  temp  ;
	Quaternion  smoothRotation  ;
	
	
	void Freeze() {
		
		freeze = true;
		
	}
	
	
	void UnFreeze() {
		
		freeze = false;
		
	}
	
	void SetRotation ( Vector3 target)
	{
		rotationX = target.y;
		//rotationY = target.x;
	}
	
	void Update ()
		
	{
		if(freeze|| !PlayerWeapons.canLook || individualFreeze) return;
		
		Quaternion xQuaternion  ;
		Quaternion yQuaternion  ;
		float offsetVal  ;
		  
		Vector2 ControllerInputAxis = ControlerWrapper.Get ().GetRightStick (PlayerIndex.One);

		if (axes == RotationAxes.MouseX)
			
		{
			
			rotationX += ControllerInputAxis.x * sensitivityX;
			
			float xDecrease;
			
			if(totalOffsetX > 0)
			{
				xDecrease = Mathf.Clamp(resetSpeed*Time.deltaTime, 0, totalOffsetX);
			} else {
				xDecrease = Mathf.Clamp(resetSpeed*-Time.deltaTime, totalOffsetX, 0);
			}
			
			if(resetDelay > 0){
				
				xDecrease = 0;
				
				resetDelay = Mathf.Clamp(resetDelay-Time.deltaTime, 0, resetDelay);
				
			}
			
			if(Random.value < .5)
				offsetX *= -1;
			
			if((totalOffsetX < maxKickback && totalOffsetX >= 0) || (totalOffsetX > -maxKickback && totalOffsetX <= 0)){
				
				totalOffsetX += offsetX;
				
			}  else {
				
				//offsetX = 0;
				resetDelay *= .5f;
				
			}
			
			rotationX = ClampAngle (rotationX, minimumX, maximumX)+ offsetX - xDecrease;
			
			if((ControllerInputAxis.x * sensitivityX) < 0){

				totalOffsetX += ControllerInputAxis.x * sensitivityX;
				
			}
			
			rotationX+=Mathf.Sin(Time.time)*idleSway;
			
			totalOffsetX -= xDecrease;
			
			if(totalOffsetX < 0) 
				
				totalOffsetX = 0;
			
			xQuaternion = Quaternion.AngleAxis (rotationX, Vector3.up);
			
			tRotation = originalRotation * xQuaternion;
			
			offsetVal = Mathf.Clamp(totalOffsetX*smoothFactor,1, smoothFactor);
			
			if(smooth){
				transform.localRotation=Quaternion.Slerp(transform.localRotation,tRotation,Time.deltaTime*25/smoothFactor*offsetVal);
			} else {
				transform.localRotation = tRotation;
			}
			
		}
		
		else
			
		{
						   
						//ControlerWrapper.Get().GetLeftStick(XInputDotNetPure.PlayerIndex).y	
							
			rotationY +=  ControllerInputAxis.y * sensitivityY;
			
			float yDecrease  = Mathf.Clamp(resetSpeed*Time.deltaTime, 0, totalOffsetY);
			
			if(resetDelay > 0)
			{
				
				yDecrease = 0;
				
				resetDelay = Mathf.Clamp(resetDelay-Time.deltaTime, 0, resetDelay);
				
			}
			
			if(totalOffsetY < maxKickback){
				
				totalOffsetY += offsetY;
				
			}  else {
				
				offsetY = 0;
				
				resetDelay *= .5f;
				
			}
			
			rotationY = ClampAngle (rotationY, minimumY, maximumY)+ offsetY - yDecrease;
			
			if((ControllerInputAxis.y* sensitivityY) < 0){
				
				totalOffsetY += ControllerInputAxis.y * sensitivityY;
				
			}
			
			rotationY+=Mathf.Sin(Time.time)*idleSway;
			
			totalOffsetY -= yDecrease;
			
			if(totalOffsetY < 0) 
				
				totalOffsetY = 0;
			
			yQuaternion = Quaternion.AngleAxis (rotationY, Vector3.left);
			
			tRotation = originalRotation * yQuaternion;
			
			offsetVal = Mathf.Clamp(totalOffsetY*smoothFactor,1, smoothFactor);

			Vector3 tempEulerAngle = transform.localEulerAngles;

			if(smooth)
			{
				tempEulerAngle.x = Quaternion.Slerp(transform.localRotation,tRotation,Time.deltaTime*25/smoothFactor*offsetVal).eulerAngles.x;

				transform.localEulerAngles = tempEulerAngle;
			} else 
			{
				tempEulerAngle.x = tRotation.x;

				transform.localEulerAngles = tempEulerAngle;
			}
			
		}
		
		offsetY = 0;
		
		offsetX = 0;
		
	}
	
	
	void Start ()
		
	{
		
		// Make the rigid body not change rotation
		
		if (rigidbody)
			
			rigidbody.freezeRotation = true;
		
		originalRotation = transform.localRotation;
		
		sensitivityX = sensitivityStandardX;
		
		sensitivityY = sensitivityStandardY;
		
		if(smoothFactor <=1){
			smoothFactor = 1;
		}
		
	}
	
	
	static float ClampAngle (float angle , float min, float max ) 
		
	{
		
		if (angle < -360F)
			
			angle += 360F;
		
		if (angle > 360F)
			
			angle -= 360F;
		
		return Mathf.Clamp (angle, min, max);
		
	}
	
	
	void Aiming(float zoom) 
	{
		
		sensitivityX = sensitivityX/zoom;
		
		sensitivityY = sensitivityY/zoom;
		
	}
	
	void StopAiming() {
		
		sensitivityX = sensitivityStandardX;
		
		sensitivityY = sensitivityStandardY;
		
	}
	
	void LockIt (int min ,  int max) 
	{
		if (axes == RotationAxes.MouseX) 
		{
			maxStored =(int)maximumX;
			minStored =(int)minimumX;
			maximumX = (int)rotationX+max;
			minimumX = (int)rotationX-min;
		} else 
		{
			maxStored = (int)maximumY;
			minStored = (int)minimumY;
			maximumY =  (int)rotationY+max;
			minimumY =  (int)rotationY-min;
		}
	}
	
	void LockItSpecific(int min, int max)
	{
		if (axes == RotationAxes.MouseX) {
			maxStored = (int)maximumX;
			minStored = (int)minimumX;
			maximumX =  (int)max;
			minimumX =  (int)min;
		} else {
			maxStored =(int) maximumY;
			minStored =(int) minimumY;
			maximumY = (int) max;
			minimumY = (int) min;
		}
	}
	
	void UnlockIt () {
		if (axes == RotationAxes.MouseX) {
			maximumX = maxStored;
			minimumX = minStored;
		} else {
			maximumY = maxStored;
			minimumY = minStored;
		}
	}
	
	void UpdateIt(){
		rotationX = transform.localEulerAngles.y - originalRotation.eulerAngles.y;
		rotationY = transform.localEulerAngles.x- originalRotation.eulerAngles.x;
		totalOffsetX = 0;
	}
}

using UnityEngine;
using System.Collections;
using XInputDotNetPure;


/// <summary>
/// /// This is the InputComponent.
/// Route all input detection through here
/// </summary>
public class ControlerWrapper : MonoBehaviour 
{
    // Statics
	static ControlerWrapper s_theInput;

	GamePadState[] padCurrent;
    GamePadState[] padLast;

    void Awake()
    {
        padCurrent = new GamePadState[4];
        padLast = new GamePadState[4];
        s_theInput = this;
    }

    /// <summary>
    /// Reads the current state of the keyboard
    /// </summary>
    public void Update()
    {
        // TODO: Add your update code here
      
        for (int i = 0; i < 4; ++i)
        {
            padLast[i] = padCurrent[i];
            padCurrent[i] = GamePad.GetState((PlayerIndex)i);
        }
	}

    /// <summary>
    /// Returns the InputComponent - use this to gain access to it from anywhere in your program
    /// "Singleton" pattern.
    /// </summary>
    static public ControlerWrapper Get()
    {
        return s_theInput;
    }
	/*
    /// <summary>
    /// Returns true if the "button" is currently being held down
    /// </summary>
    /// <param name="index">The index of the controller to check.</param>
    /// <param name="button">The button to be tested.</param>
    public bool IsButtonDown(PlayerIndex index, GamePadButtons button)
    {
         if (padCurrent[(int)index].Buttons == button)
			return true;
		else
			return false;
    }
	
    /// <summary>
    /// Returns true if the "button" has just now been hit (leading edge test).
    /// </summary>
    /// <param name="index">The index of the controller to check.</param>
    /// <param name="button">The button to be tested.</param>
    public bool IsButtonHit(PlayerIndex index, GamePadButtons button)
    {
        return padCurrent[(int)index].IsButtonDown(button) && padLast[(int)index].IsButtonUp(button);
    }

    /// <summary>
    /// Returns true if the "button" has just now been released (trailing edge test).
    /// </summary>
    /// <param name="index">The index of the controller to check.</param>
    /// <param name="button">The button to be tested.</param>
    public bool IsButtonRelease(PlayerIndex index, GamePadButtons button)
    {
        return padCurrent[(int)index].IsButtonUp(button) && padLast[(int)index].IsButtonDown(button);
    } */
    /// <summary>
    /// Returns the Left Stick
    /// </summary>
    /// <param name="index">The index of the controller to check.</param>
    public Vector2 GetLeftStick(PlayerIndex index)
    {
        Vector2 inputDirection = Vector2.zero;
		
		if(padCurrent[(int)index].ThumbSticks.Left.X != 0)
			inputDirection.x += padCurrent[(int)index].ThumbSticks.Left.X;
		if(padCurrent[(int)index].ThumbSticks.Left.Y != 0)
			inputDirection.y += padCurrent[(int)index].ThumbSticks.Left.Y;
		
		return inputDirection;	
	}

    /// <summary>
    /// Returns the Right Stick
    /// </summary>
    /// <param name="index">The index of the controller to check.</param>
    public Vector2 GetRightStick(PlayerIndex index)
    {
        Vector2 inputDirection = Vector2.zero;
		
		if(padCurrent[(int)index].ThumbSticks.Right.X != 0)
			inputDirection.x += padCurrent[(int)index].ThumbSticks.Right.X;
		if(padCurrent[(int)index].ThumbSticks.Right.Y != 0)
			inputDirection.y += padCurrent[(int)index].ThumbSticks.Right.Y;
		
		return inputDirection;
    }

    /// <summary>
    /// Returns the Left Trigger
    /// </summary>
    /// <param name="index">The index of the controller to check.</param>
    public float GetLeftTrigger(PlayerIndex index)
    {
        return (float)padCurrent[(int)index].Triggers.Left;
    }

    /// <summary>
    /// Returns the Right Trigger
    /// </summary>
    /// <param name="index">The index of the controller to check.</param>
    public float GetRightTrigger(PlayerIndex index)
    {
        return padCurrent[(int)index].Triggers.Right;
    }

    /// <summary>
    /// Returns true if the Left Trigger has just been hit
    /// </summary>
    /// <param name="index">The index of the controller to check.</param>
    public bool LeftTriggerHit(PlayerIndex index)
    {
        return (padCurrent[(int)index].Triggers.Left > 0.0f) && (padLast[(int)index].Triggers.Left == 0.0f);
    }

    /// <summary>
    /// Returns true if the Right Trigger has just been hit
    /// </summary>
    /// <param name="index">The index of the controller to check.</param>
    public bool RightTriggerHit(PlayerIndex index)
    {
        return (padCurrent[(int)index].Triggers.Right > 0.0f) && (padLast[(int)index].Triggers.Right == 0.0f);
    }
	
	public bool RightBumperHold(int index)
	{
		return(padCurrent[(int)index].Buttons.RightShoulder == 0);	
	}
	public bool LeftBumperHold(int index)
	{
		return(padCurrent[(int)index].Buttons.LeftShoulder == 0);	
	}
	
	public bool RightBumperHit(int index)
	{
		return(padCurrent[(int)index].Buttons.RightShoulder ==  ButtonState.Pressed ) && (padLast[(int)index].Buttons.RightShoulder == ButtonState.Released);	
	}
	public bool LeftBumperHit(int index)
	{
		return(padCurrent[(int)index].Buttons.LeftShoulder ==  ButtonState.Pressed ) && (padLast[(int)index].Buttons.LeftShoulder == ButtonState.Released);	
	}
	
	public bool A_Hit(int index)
	{
		return(padCurrent[(int)index].Buttons.A ==  ButtonState.Pressed ) && (padLast[(int)index].Buttons.A == ButtonState.Released);	
	}
	
	public bool A_Hold(int index)
	{
		return padCurrent[index].Buttons.A == ButtonState.Pressed;
	}
	
	public bool Start_Hit(int index)
	{
		return(padCurrent[(int)index].Buttons.Start ==  ButtonState.Pressed ) && (padLast[(int)index].Buttons.Start == ButtonState.Released);	
	}
	
	public bool Dpad_UP_Hit(int index)
	{
		return(padCurrent[(int)index].DPad.Up == ButtonState.Pressed) && (padLast[(int)index].DPad.Up == ButtonState.Released);	
	}
	
	public bool Dpad_Down_Hit(int index)
	{
		return(padCurrent[(int)index].DPad.Down == ButtonState.Pressed) && (padLast[(int)index].DPad.Down == ButtonState.Released);	
	}
}

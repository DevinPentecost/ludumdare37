using UnityEngine;
using System.Collections;

public class ControllerHelper {

	//Keyboard controls. Should be able to remap these eventually
	private static string axisJump = "Jump";
	private static string axisJump1 = "Jump1";
	private static string axisJump2 = "Jump2";
	private static string axisJump3 = "Jump3";

	private static string axisColor1 = "Color1";
	private static string axisColor2 = "Color2";
	private static string axisColor3 = "Color3";

	private static string axisRestart = "Restart";

	private static string axisCancel = "Cancel";

	//Color wheel joystick
	public static string axisJoystickColorX = "JoystickColorX";
	public static string axisJoystickColorY = "JoystickColorY";
	private static Vector3[] compass = new Vector3[] { Vector3.left, Vector3.right, Vector3.forward,
		Vector3.back, Vector3.up, Vector3.down };
	private static ArrayList compassArrayList = new ArrayList(compass);

	private static string[] keysColor1 = new string[] { "q", "a" };
	private static string[] keysColor2 = new string[] { "w", "s" };
	private static string[] keysColor3 = new string[] { "e", "d" };
	private static string[] keysJump = new string[] { "q", "w", "e", "space" };

	//Is this the joystick vector for color1?
	public static bool GetCompassIndexColor1(int compassIndex)
	{
		//Is the index the left index?
		if(compassIndex == ControllerHelper.compassArrayList.IndexOf(Vector3.left))
			return true;
		return false;
	}

	//Is this the joystick vector for color2?
	public static bool GetCompassIndexColor2(int compassIndex)
	{
		//Is the index the left index?
		if (compassIndex == ControllerHelper.compassArrayList.IndexOf(Vector3.down))
			return true;
		return false;
	}

	//Is this the joystick vector for color3?
	public static bool GetCompassIndexColor3(int compassIndex)
	{
		//Is the index the left index?
		if (compassIndex == ControllerHelper.compassArrayList.IndexOf(Vector3.right))
			return true;
		return false;
	}

	//Get the current joystick axis
	public static Vector2 GetJoystickColor()
	{
		//Get the X and Y percentages
		float X = Input.GetAxisRaw(ControllerHelper.axisJoystickColorX);
		float Y = Input.GetAxisRaw(ControllerHelper.axisJoystickColorY);

		//Return the vector
		return new Vector2(X, Y);
	}

	//Figure out which axis is being used
	public static int GetJoystickColorCompassIndex()
	{
		//Get both the X and Y
		Vector2 joystickAxis = ControllerHelper.GetJoystickColor();
		float Z = 0f;
		Vector3 vector = new Vector3(joystickAxis.x, joystickAxis.y, Z);

		//The index of the closest direction
		int compassIndex = -1;

		//Is there even a vector?
		if(Mathf.Approximately(joystickAxis.x, 0) && Mathf.Approximately(joystickAxis.y, 0))
		{
			//Don't go nowhere!
			return compassIndex;
		}

		//Get the index of direction
		float maxDotProduct = -Mathf.Infinity;
		for(int i = 0; i < ControllerHelper.compass.Length; ++i)
		{
			//Do the dot product
			float dotProduct = Vector3.Dot(vector, ControllerHelper.compass[i]);

			//Is it better?
			if(dotProduct > maxDotProduct)
			{
				//Use this one
				maxDotProduct = dotProduct;
				compassIndex = i;
			}
		}

		//Return the index it is closest to
		return compassIndex;
	}

	//Was the restart axis used?
	public static float GetRestartAxis()
	{
		//Get the highest value restart axis
		return Input.GetAxisRaw(ControllerHelper.axisRestart);
	}

	//Was jump pushed down?
	public static bool JumpKeysDown()
	{
		//Were any of the jump keys pushed down?
		return ControllerHelper.GetKeysDown(ControllerHelper.keysJump);
	}

	//Check all the jump axis
	public static float GetAllJumpAxes()
	{
		//Get the highest value of all jump axes
		float jumpAxis = GetJumpAxis();
		float jump1Axis = GetJump1Axis();
		float jump2Axis = GetJump2Axis();
		float jump3Axis = GetJump3Axis();

		//Return the max
		return Mathf.Max(new float[] 
			{ jumpAxis, jump1Axis, jump2Axis, jump3Axis }
		);
	}
	public static float GetJumpAxis()
	{
		//Get the highest value jump axis
		return Input.GetAxisRaw(ControllerHelper.axisJump);
	}

	//Check all the jump axis
	public static float GetJump1Axis()
	{
		//Get the highest value jump axis
		return Input.GetAxisRaw(ControllerHelper.axisJump1);
	}

	//Check all the jump axis
	public static float GetJump2Axis()
	{
		//Get the highest value jump axis
		return Input.GetAxisRaw(ControllerHelper.axisJump2);
	}

	//Check all the jump axis
	public static float GetJump3Axis()
	{
		//Get the highest value jump axis
		return Input.GetAxisRaw(ControllerHelper.axisJump3);
	}

	//Check all the Color1 axis
	public static float GetColor1Axis()
	{
		//Get the highest value jump axis
		return Input.GetAxisRaw(ControllerHelper.axisColor1);
	}

	//Check all the Color2 axis
	public static float GetColor2Axis()
	{
		//Get the highest value jump axis
		return Input.GetAxisRaw(ControllerHelper.axisColor2);
	}

	//Check all the Color3 axis
	public static float GetColor3Axis()
	{
		//Get the highest value jump axis
		return Input.GetAxisRaw(ControllerHelper.axisColor3);
	}

	//Was jump released??
	public static bool JumpKeysUp()
	{
		//Were any of the jump keys pushed down?
		return ControllerHelper.GetKeysUp(ControllerHelper.keysJump);
	}

	//Were any keys pressed for changing color?
	public static bool Color1KeysDown()
	{
		//Any of the valid color keys?
		return ControllerHelper.GetKeysDown(ControllerHelper.keysColor1);
	}

	//Were any keys pressed for changing color?
	public static bool Color2KeysDown()
	{
		//Any of the valid color keys?
		return ControllerHelper.GetKeysDown(ControllerHelper.keysColor2);
	}

	//Were any keys pressed for changing color?
	public static bool Color3KeysDown()
	{
		//Any of the valid color keys?
		return ControllerHelper.GetKeysDown(ControllerHelper.keysColor3);
	}

	//For checking multiple keys
	public static bool GetKeysDown(string[] keys)
	{
		//For each key
		foreach (string key in keys)
		{
			//Is it down?
			if (Input.GetKeyDown(key))
			{
				//Its good
				return true;
			}
		}
		//We made it this far, so no go
		return false;
	}

	//For checking multiple keys
	public static bool GetKeysUp(string[] keys)
	{
		//For each key
		foreach (string key in keys)
		{
			//Is it down?
			if (Input.GetKeyUp(key))
			{
				//Its good
				return true;
			}
		}
		//We made it this far, so no go
		return false;
	}

	//For checking multiple keys
	public static bool GetKeys(string[] keys)
	{
		//For each key
		foreach (string key in keys)
		{
			//Is it down?
			if (Input.GetKey(key))
			{
				//Its good
				return true;
			}
		}
		//We made it this far, so no go
		return false;
	}

	//Did the user hit escape or quit or whatever
	public static float GetExitAxis()
	{
		//Get the highest value jump axis
		return Input.GetAxisRaw(ControllerHelper.axisCancel);
	}
}
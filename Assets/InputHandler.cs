using UnityEngine;
using System.Collections;
using Types;

public class InputHandler : MonoBehaviour
{
	public static float GetAxis() {
		return Input.GetAxis ("Horizontal");
	}
	
	public static InputCollection GetCollection() {
		
		var collection = new InputCollection ();
		if (Input.GetKeyDown (KeyCode.Space))
			collection.punch = true;
		if (Input.GetKeyDown (KeyCode.C))
			collection.uppercut = true;
		if (Input.GetKeyDown (KeyCode.UpArrow))
			collection.jump = true;
		if (Input.GetKey (KeyCode.DownArrow))
			collection.down = true;
		if (Input.GetKey (KeyCode.X))
			collection.block = true;

		if (Input.GetKey (KeyCode.Joystick1Button0))
			collection.jump = true;

		return collection;
	}
	
}

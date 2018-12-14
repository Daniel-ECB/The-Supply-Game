using UnityEngine;
using UnityEditor;

// Derive from editor as we want to modify the editor and the script ConveyorBelt more specifically
[CustomEditor(typeof(ConveyorBelt))]
public class ConveyorBeltEditor : Editor 
{
	// Override bc we will modify an already existing function in Unity. OnInspectorGUI is the
	// function that shows all the values in the inspector as we see them
	public override void OnInspectorGUI()
	{
		// We call this line to execute the original function, aditional code can be 
		// added before or after this
		base.OnInspectorGUI();

		// target is the object being inspected, in our case, the ConveyorBelt script as this is the
		// script we are modifying in the editor, target is by default an Object so we have to cast
		// the value
		ConveyorBelt belt = (ConveyorBelt)target;

		// Everything between this line and GUILayout.EndHorizontal () will be displayed horizontally
		GUILayout.BeginHorizontal ();

			// Generates the belt
			if(GUILayout.Button("Generate Belt"))
			{
//				Debug.Log ("We pressed generate belt");
				belt.GenerateBelt ();
			}

			// Resets the belt to leave room for another one
			if(GUILayout.Button("Reset Belt"))
			{
				belt.ResetBelt ();
			}

		GUILayout.EndHorizontal ();
	}
}

using UnityEngine;
using System.Collections;
using UnityEditor;


#if UNITY_EDITOR

[CustomEditor(typeof(PlayGrid))]


public class PlayGridEditor : Editor {


	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		PlayGrid myScript = (PlayGrid)target;

		if( GUILayout.Button( "Create Grid" ) )
		{
			myScript.CreateGrid();	
		}
		if( GUILayout.Button( "Errase Grid" ) )
		{
			myScript.Errase();	
		}

		if( GUILayout.Button( "Place Ships" ) )
		{
			myScript.PlaceFleet();
		}
	}
}
#endif

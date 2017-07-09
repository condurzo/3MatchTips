using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlayingObject))]
public class PlayingObjectInspector : Editor {
	PlayingObject targetPlayer;

	void OnEnable(){
		targetPlayer = (PlayingObject)target;
	}

	public override void OnInspectorGUI ()
	{
		DrawDefaultInspector ();
		EditorGUILayout.Space ();
		//if (targetPlayer.adjacentItems != null) {
			EditorGUILayout.PrefixLabel ("ADYACENTES");
			if (targetPlayer.adjacentItems.Izquierda != null)
				EditorGUILayout.ObjectField ("IZQUIERDA:", targetPlayer.adjacentItems.Izquierda, typeof(PlayingObject), false);
			if (targetPlayer.adjacentItems.Derecha != null)
				EditorGUILayout.ObjectField ("DERECHA:", targetPlayer.adjacentItems.Derecha, typeof(PlayingObject), false);
			if (targetPlayer.adjacentItems.Arriba != null)
				EditorGUILayout.ObjectField ("ARRIBA:", targetPlayer.adjacentItems.Arriba, typeof(PlayingObject), false);
			if (targetPlayer.adjacentItems.Abajo != null)
				EditorGUILayout.ObjectField ("ABAJO:", targetPlayer.adjacentItems.Abajo, typeof(PlayingObject), false);
	//	}
	}
}

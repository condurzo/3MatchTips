using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeName : MonoBehaviour {

	public int nameCont;
	public bool Activador1;
	public bool Activador2;
	// Update is called once per frame
	void Update () {
		this.gameObject.name = "Playing Object " + nameCont.ToString() +"(Clone)";
			
		if (!Activador1) {
			nameCont++;
			if (nameCont == 5) {
				Activador1 = true;
				Activador2 = true;
			}
		}
		if (Activador2) {
			nameCont--;
			if (nameCont == 1) {
				Activador1 = false;
				Activador2 = false;
			}
		}


		Debug.Log (nameCont);
	}
}

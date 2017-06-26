using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isLiveNormal : MonoBehaviour {

	void OnDestroy(){
		//Debug.Log ("ROMPIO COMUN");
		if (Tiempo.spawnSigno == false) {
			Tiempo.contadorSigno += 1;
		}

	}

}

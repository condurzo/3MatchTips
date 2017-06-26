using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isLiveSigno : MonoBehaviour {

	void OnDestroy(){
		Tiempo.spawnSigno = false;
	}

}

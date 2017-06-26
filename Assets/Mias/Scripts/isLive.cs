using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isLive : MonoBehaviour {

	void OnDestroy(){
		Debug.Log ("SUME TIEMPO");
		Tiempo.crono += 5;
	}

}

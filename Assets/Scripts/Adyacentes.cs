using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adyacentes : MonoBehaviour {
	public PlayingObject arriba;
	public PlayingObject abajo;
	public PlayingObject derecha;
	public PlayingObject izquierda;


	public PlayingObject Arriba{
	    get{
	        return this.arriba;
	    }
	    set{
	        this.arriba = value;
	    }
	}
	public PlayingObject Abajo{
	    get{
	        return this.abajo;
	    }
	    set{
	        this.abajo = value;
	    }
	}
	public PlayingObject Derecha {
		get {
			return this.derecha;
		}
		set {
	        this.derecha = value;
	    }
	}
	public PlayingObject Izquierda{
	    get{
	        return this.izquierda;
	    }
	    set{

	        this.izquierda = value;
	    }
	}

	public Adyacentes ()
	{
		
		arriba = null;
		abajo = null;
		derecha = null;
		izquierda = null;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectLevels : MonoBehaviour {
	//AL VOLVER AL MENU SETEAR SIEMPRE A 0 LOS PLAYER PREFS
	public static bool todosActivados;

	public static bool SimbolActive;
	public static int AguaLevel;

	public void PlayTimeAttack(){
		TodosCero ();
		PlayerPrefs.SetInt ("TodosActivados", 1);
		Application.LoadLevel ("LevelInfinito");
	}

	void Start (){
		TodosCero ();
	}

	void TodosCero(){
		PlayerPrefs.SetInt ("TodosActivados", 0);

		PlayerPrefs.SetInt ("Agua1", 0);
		PlayerPrefs.SetInt ("Agua2", 0);
		PlayerPrefs.SetInt ("Agua3", 0);
		PlayerPrefs.SetInt ("Agua4", 0);
		PlayerPrefs.SetInt ("Agua5", 0);
		PlayerPrefs.SetInt ("EsAgua", 0);

		PlayerPrefs.SetInt ("Fuego1", 0);
		PlayerPrefs.SetInt ("Fuego2", 0);
		PlayerPrefs.SetInt ("Fuego3", 0);
		PlayerPrefs.SetInt ("Fuego4", 0);
		PlayerPrefs.SetInt ("Fuego5", 0);
		PlayerPrefs.SetInt ("EsFuego", 0);

		PlayerPrefs.SetInt ("Tierra1", 0);
		PlayerPrefs.SetInt ("Tierra2", 0);
		PlayerPrefs.SetInt ("Tierra3", 0);
		PlayerPrefs.SetInt ("Tierra4", 0);
		PlayerPrefs.SetInt ("Tierra5", 0);
		PlayerPrefs.SetInt ("EsTierra", 0);

		PlayerPrefs.SetInt ("Aire1", 0);
		PlayerPrefs.SetInt ("Aire2", 0);
		PlayerPrefs.SetInt ("Aire3", 0);
		PlayerPrefs.SetInt ("Aire4", 0);
		PlayerPrefs.SetInt ("Aire5", 0);
		PlayerPrefs.SetInt ("EsAire", 0);
	}

	//AGUA
	public void Agua1(){
		SimbolActive = false;
		TodosCero ();
		PlayerPrefs.SetInt ("Agua1", 1);
		PlayerPrefs.SetInt ("EsAgua", 1);
		Application.LoadLevel ("LevelInfinito");

	}

	public void Agua2(){
		if (PlayerPrefs.GetInt ("DesbAgua2") == 1) {
			SimbolActive = false;
			TodosCero ();
			PlayerPrefs.SetInt ("Agua2", 1);
			PlayerPrefs.SetInt ("EsAgua", 1);
			Application.LoadLevel ("LevelInfinito");
		}
	}

	public void Agua3(){
		if (PlayerPrefs.GetInt ("DesbAgua3") == 1) {
			SimbolActive = false;
			TodosCero ();
			PlayerPrefs.SetInt ("Agua3", 1);
			PlayerPrefs.SetInt ("EsAgua", 1);
			Application.LoadLevel ("LevelInfinito");
		}
	}

	public void Agua4(){
		if (PlayerPrefs.GetInt ("DesbAgua4") == 1) {
			SimbolActive = false;
			TodosCero ();
			PlayerPrefs.SetInt ("Agua4", 1);
			PlayerPrefs.SetInt ("EsAgua", 1);
			Application.LoadLevel ("LevelInfinito");
		}
	}

	public void Agua5(){
		if (PlayerPrefs.GetInt ("DesbAgua5") == 1) {
			SimbolActive = false;
			TodosCero ();
			PlayerPrefs.SetInt ("Agua5", 1);
			PlayerPrefs.SetInt ("EsAgua", 1);
			Application.LoadLevel ("LevelInfinito");
		}
	}

	//FUEGO
	public void Fuego1(){
		if (PlayerPrefs.GetInt ("DesbFuego1") == 1) {
			SimbolActive = false;
			TodosCero ();
			PlayerPrefs.SetInt ("Fuego1", 1);
			PlayerPrefs.SetInt ("EsFuego", 1);
			Application.LoadLevel ("LevelInfinito");
		}
	}

	public void Fuego2(){
		if (PlayerPrefs.GetInt ("DesbFuego2") == 1) {
			SimbolActive = false;
			TodosCero ();
			PlayerPrefs.SetInt ("Fuego2", 1);
			PlayerPrefs.SetInt ("EsFuego", 1);
			Application.LoadLevel ("LevelInfinito");
		}
	}

	public void Fuego3(){
		if (PlayerPrefs.GetInt ("DesbFuego3") == 1) {
			SimbolActive = false;
			TodosCero ();
			PlayerPrefs.SetInt ("Fuego3", 1);
			PlayerPrefs.SetInt ("EsFuego", 1);
			Application.LoadLevel ("LevelInfinito");
		}
	}

	public void Fuego4(){
		if (PlayerPrefs.GetInt ("DesbFuego4") == 1) {
			SimbolActive = false;
			TodosCero ();
			PlayerPrefs.SetInt ("Fuego4", 1);
			PlayerPrefs.SetInt ("EsFuego", 1);
			Application.LoadLevel ("LevelInfinito");
		}
	}

	public void Fuego5(){
		if (PlayerPrefs.GetInt ("DesbFuego5") == 1) {
			SimbolActive = false;
			TodosCero ();
			PlayerPrefs.SetInt ("Fuego5", 1);
			PlayerPrefs.SetInt ("EsFuego", 1);
			Application.LoadLevel ("LevelInfinito");
		}
	}

	//TIERRA
	public void Tierra1(){
		if (PlayerPrefs.GetInt ("DesbTierra1") == 1) {
			SimbolActive = false;
			TodosCero ();
			PlayerPrefs.SetInt ("Tierra1", 1);
			PlayerPrefs.SetInt ("EsTierra", 1);
			Application.LoadLevel ("LevelInfinito");
		}
	}

	public void Tierra2(){
		if (PlayerPrefs.GetInt ("DesbTierra2") == 1) {
			SimbolActive = false;
			TodosCero ();
			PlayerPrefs.SetInt ("Tierra2", 1);
			PlayerPrefs.SetInt ("EsTierra", 1);
			Application.LoadLevel ("LevelInfinito");
		}
	}

	public void Tierra3(){
		if (PlayerPrefs.GetInt ("DesbTierra3") == 1) {
			SimbolActive = false;
			TodosCero ();
			PlayerPrefs.SetInt ("Tierra3", 1);
			PlayerPrefs.SetInt ("EsTierra", 1);
			Application.LoadLevel ("LevelInfinito");
		}
	}

	public void Tierra4(){
		if (PlayerPrefs.GetInt ("DesbTierra4") == 1) {
			SimbolActive = false;
			TodosCero ();
			PlayerPrefs.SetInt ("Tierra4", 1);
			PlayerPrefs.SetInt ("EsTierra", 1);
			Application.LoadLevel ("LevelInfinito");
		}
	}

	public void Tierra5(){
		if (PlayerPrefs.GetInt ("DesbTierra5") == 1) {
			SimbolActive = false;
			TodosCero ();
			PlayerPrefs.SetInt ("Tierra5", 1);
			PlayerPrefs.SetInt ("EsTierra", 1);
			Application.LoadLevel ("LevelInfinito");
		}
	}

	//AIRE
	public void Aire1(){
		if (PlayerPrefs.GetInt ("DesbAire1") == 1) {
			SimbolActive = false;
			TodosCero ();
			PlayerPrefs.SetInt ("Aire1", 1);
			PlayerPrefs.SetInt ("EsAire", 1);
			Application.LoadLevel ("LevelInfinito");
		}
	}

	public void Aire2(){
		if (PlayerPrefs.GetInt ("DesbAire2") == 1) {
			SimbolActive = false;
			TodosCero ();
			PlayerPrefs.SetInt ("Aire2", 1);
			PlayerPrefs.SetInt ("EsAire", 1);
			Application.LoadLevel ("LevelInfinito");
		}
	}

	public void Aire3(){
		if (PlayerPrefs.GetInt ("DesbAire3") == 1) {
			SimbolActive = false;
			TodosCero ();
			PlayerPrefs.SetInt ("Aire3", 1);
			PlayerPrefs.SetInt ("EsAire", 1);
			Application.LoadLevel ("LevelInfinito");
		}
	}

	public void Aire4(){
		if (PlayerPrefs.GetInt ("DesbAire4") == 1) {
			SimbolActive = false;
			TodosCero ();
			PlayerPrefs.SetInt ("Aire4", 1);
			PlayerPrefs.SetInt ("EsAire", 1);
			Application.LoadLevel ("LevelInfinito");
		}
	}

	public void Aire5(){
		if (PlayerPrefs.GetInt ("DesbAire5") == 1) {
			SimbolActive = false;
			TodosCero ();
			PlayerPrefs.SetInt ("Aire5", 1);
			PlayerPrefs.SetInt ("EsAire", 1);
			Application.LoadLevel ("LevelInfinito");
		}
	}
}

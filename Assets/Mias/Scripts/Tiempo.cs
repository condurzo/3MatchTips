using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiempo : MonoBehaviour {
	public static bool Playing;
	public static float crono;
	public int minutos;
	public int segundos;
	public TextMesh TiempoText;
	public GameObject GameOverScreen;
	public GameObject GameOverScreenPlaySeccion;
	public bool changes;

	public GameObject Fondo;
	public Material Agua;
	public Material Fuego;
	public Material Aire;
	public Material Tierra;

	public AudioSource MusicaFondo;
	public AudioClip Music_Agua;
	public AudioClip Music_Fuego;
	public AudioClip Music_Aire;
	public AudioClip Music_Tierra;
	public AudioClip Music_GameOver;

	public static bool AguaBool;
	public static bool FuegoBool;
	public static bool TierraBool;
	public static bool AireBool;

	public GameObject BarrasOption;
	public GameObject OptionGO;

	public GameObject MenuGame;

	public static int contadorSigno;
	public static bool instanceSigno;
	public static bool spawnSigno;

	void Start(){
		if (PlayerPrefs.GetInt ("TodosActivados") == 1) {
			crono = 240;
		}
		if (PlayerPrefs.GetInt ("EsAgua") == 1) {
			crono = 120;
		}
		if (PlayerPrefs.GetInt ("EsFuego") == 1) {
			crono = 120;
		}
		if (PlayerPrefs.GetInt ("EsTierra") == 1) {
			crono = 120;
		}
		if (PlayerPrefs.GetInt ("EsAire") == 1) {
			crono = 120;
		}


		MusicaFondo = GetComponent<AudioSource> ();
	}

	public void sumartiempo(){
		crono += 5;
	}

	public void ReloadScene(){
		Application.LoadLevel ("LevelInfinito");
	}

	public void GoToMenu(){
		Application.LoadLevel ("Menu");
	}

	public void OpenMenuGO(){
		MenuGame.SetActive (true);
	}

	public void CloseMenuGO(){
		MenuGame.SetActive (false);
	}

	public void SoundMenuOpen(){
		OptionGO.SetActive (true);
		BarrasOption.GetComponent<CanvasGroup> ().alpha = 1;
		BarrasOption.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		BarrasOption.GetComponent<CanvasGroup> ().interactable = true;
	}
	public void SoundMenuClose(){
		OptionGO.SetActive (false);
		BarrasOption.GetComponent<CanvasGroup> ().alpha = 0;
		BarrasOption.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		BarrasOption.GetComponent<CanvasGroup> ().interactable = false;
	}

	void Update () {
			if (contadorSigno >= 50) {
				instanceSigno = true;
				contadorSigno = 0;
			}

		//Debug.Log ("SIGNO: " + contadorSigno);
			Cronometro ();

		if (PlayerPrefs.GetInt ("TodosActivados") == 1) {
			
			if (!changes) {
				if ((crono >= 180) && (crono <= 240)) {
				
					Fondo.GetComponent<MeshRenderer> ().material = Agua;
					MusicaFondo.clip = Music_Agua;
					if (!MusicaFondo.isPlaying) {
						//Debug.Log ("ES AGUA");
						MusicaFondo.Play ();
						MusicaFondo.loop = true;
						AguaBool = true;
						FuegoBool = false;
						TierraBool = false;
						AireBool = false;
					}
				}
				if ((crono >= 120) && (crono <= 180)) {
					//Debug.Log ("ES Fuego");
					Fondo.GetComponent<MeshRenderer> ().material = Fuego;
					MusicaFondo.clip = Music_Fuego;
					if (!MusicaFondo.isPlaying) {
						//Debug.Log ("ENTRE");
						MusicaFondo.Play ();
						MusicaFondo.loop = true;
						AguaBool = false;
						FuegoBool = true;
						TierraBool = false;
						AireBool = false;
					}
				}
				if ((crono >= 60) && (crono <= 120)) {
					//Debug.Log ("ES Aire");
					Fondo.GetComponent<MeshRenderer> ().material = Aire;
					MusicaFondo.clip = Music_Aire;
					if (!MusicaFondo.isPlaying) {
						MusicaFondo.Play ();
						MusicaFondo.loop = true;
						AguaBool = false;
						FuegoBool = false;
						AireBool = true;
						TierraBool = false;
					}
				}
				if (crono <= 60) {
					//Debug.Log ("ES TIERRA");
					Fondo.GetComponent<MeshRenderer> ().material = Tierra;
					MusicaFondo.clip = Music_Tierra;
					if (!MusicaFondo.isPlaying) {
						MusicaFondo.Play ();
						MusicaFondo.loop = true;
						AguaBool = false;
						FuegoBool = false;
						AireBool = false;
						TierraBool = true;
					}
				}
			}
		}

		if (PlayerPrefs.GetInt ("EsAgua") == 1) {
			if (!changes) {
				//Debug.Log ("ESTOY");
				Fondo.GetComponent<MeshRenderer> ().material = Agua;
				MusicaFondo.clip = Music_Agua;
				if (!MusicaFondo.isPlaying) {
					//Debug.Log ("ES AGUA");
					MusicaFondo.Play ();
					MusicaFondo.loop = true;
					AguaBool = true;
					FuegoBool = false;
					TierraBool = false;
					AireBool = false;
				}
			}
		}

		if (PlayerPrefs.GetInt ("EsFuego") == 1) {
			if (!changes) {
				//Debug.Log ("ESTOY");
				Fondo.GetComponent<MeshRenderer> ().material = Fuego;
				MusicaFondo.clip = Music_Fuego;
				if (!MusicaFondo.isPlaying) {
					//Debug.Log ("ENTRE");
					MusicaFondo.Play ();
					MusicaFondo.loop = true;
					AguaBool = false;
					FuegoBool = true;
					TierraBool = false;
					AireBool = false;
				}
			}
		}

		if (PlayerPrefs.GetInt ("EsAire") == 1) {
			if (!changes) {
				//Debug.Log ("ESTOY");
				Fondo.GetComponent<MeshRenderer> ().material = Aire;
				MusicaFondo.clip = Music_Aire;
				if (!MusicaFondo.isPlaying) {
					MusicaFondo.Play ();
					MusicaFondo.loop = true;
					AguaBool = false;
					FuegoBool = false;
					AireBool = true;
					TierraBool = false;
				}
			}
		}

		if (PlayerPrefs.GetInt ("EsTierra") == 1) {
			if (!changes) {
				//Debug.Log ("ESTOY");
				Fondo.GetComponent<MeshRenderer> ().material = Tierra;
				MusicaFondo.clip = Music_Tierra;
				if (!MusicaFondo.isPlaying) {
					MusicaFondo.Play ();
					MusicaFondo.loop = true;
					AguaBool = false;
					FuegoBool = false;
					AireBool = false;
					TierraBool = true;
				}
			}
		}

	}
	

	void Cronometro(){
		crono -= Time.deltaTime*1;
		segundos = (int)crono % 60;
		if (segundos >= 60) {
			segundos = 0;
		}
		minutos = (int)crono / 60;
		TiempoText.text = minutos.ToString () + " : " + segundos.ToString ();

		if ((segundos == 0) && (minutos == 0)) {
			if (PlayerPrefs.GetInt ("TodosActivados") == 1) {
				GameOverScreen.SetActive (true);
				Playing = false;
				changes = true;
				MusicaFondo.clip = Music_GameOver;
				if (!MusicaFondo.isPlaying) {
					MusicaFondo.Play ();
					MusicaFondo.loop = true;
				}
			}

			//AGUA
			if (PlayerPrefs.GetInt ("Agua1") == 1) {
				GameOverScreenPlaySeccion.SetActive (true);
				Playing = false;
				changes = true;
				MusicaFondo.clip = Music_GameOver;
				if (!MusicaFondo.isPlaying) {
					MusicaFondo.Play ();
					MusicaFondo.loop = true;
				}
				PlayerPrefs.SetInt ("DesbAgua2", 1);
			}
			if (PlayerPrefs.GetInt ("Agua2") == 1) {
				GameOverScreenPlaySeccion.SetActive (true);
				Playing = false;
				changes = true;
				MusicaFondo.clip = Music_GameOver;
				if (!MusicaFondo.isPlaying) {
					MusicaFondo.Play ();
					MusicaFondo.loop = true;
				}
				PlayerPrefs.SetInt ("DesbAgua3", 1);
			}
			if (PlayerPrefs.GetInt ("Agua3") == 1) {
				GameOverScreenPlaySeccion.SetActive (true);
				Playing = false;
				changes = true;
				MusicaFondo.clip = Music_GameOver;
				if (!MusicaFondo.isPlaying) {
					MusicaFondo.Play ();
					MusicaFondo.loop = true;
				}
				PlayerPrefs.SetInt ("DesbAgua4", 1);
			}
			if (PlayerPrefs.GetInt ("Agua4") == 1) {
				GameOverScreenPlaySeccion.SetActive (true);
				Playing = false;
				changes = true;
				MusicaFondo.clip = Music_GameOver;
				if (!MusicaFondo.isPlaying) {
					MusicaFondo.Play ();
					MusicaFondo.loop = true;
				}
				PlayerPrefs.SetInt ("DesbFuego1", 1);
				PlayerPrefs.SetInt ("DesbAgua5", 1);
			}

			//FUEGO
			if (PlayerPrefs.GetInt ("Fuego1") == 1) {
				GameOverScreenPlaySeccion.SetActive (true);
				Playing = false;
				changes = true;
				MusicaFondo.clip = Music_GameOver;
				if (!MusicaFondo.isPlaying) {
					MusicaFondo.Play ();
					MusicaFondo.loop = true;
				}
				PlayerPrefs.SetInt ("DesbFuego2", 1);
			}
			if (PlayerPrefs.GetInt ("Fuego2") == 1) {
				GameOverScreenPlaySeccion.SetActive (true);
				Playing = false;
				changes = true;
				MusicaFondo.clip = Music_GameOver;
				if (!MusicaFondo.isPlaying) {
					MusicaFondo.Play ();
					MusicaFondo.loop = true;
				}
				PlayerPrefs.SetInt ("DesbFuego3", 1);
			}
			if (PlayerPrefs.GetInt ("Fuego3") == 1) {
				GameOverScreenPlaySeccion.SetActive (true);
				Playing = false;
				changes = true;
				MusicaFondo.clip = Music_GameOver;
				if (!MusicaFondo.isPlaying) {
					MusicaFondo.Play ();
					MusicaFondo.loop = true;
				}
				PlayerPrefs.SetInt ("DesbFuego4", 1);
			}
			if (PlayerPrefs.GetInt ("Fuego4") == 1) {
				GameOverScreenPlaySeccion.SetActive (true);
				Playing = false;
				changes = true;
				MusicaFondo.clip = Music_GameOver;
				if (!MusicaFondo.isPlaying) {
					MusicaFondo.Play ();
					MusicaFondo.loop = true;
				}
				PlayerPrefs.SetInt ("DesbAire1", 1);
				PlayerPrefs.SetInt ("DesbFuego5", 1);
			}

			//AIRE
			if (PlayerPrefs.GetInt ("Aire1") == 1) {
				GameOverScreenPlaySeccion.SetActive (true);
				Playing = false;
				changes = true;
				MusicaFondo.clip = Music_GameOver;
				if (!MusicaFondo.isPlaying) {
					MusicaFondo.Play ();
					MusicaFondo.loop = true;
				}
				PlayerPrefs.SetInt ("DesbAire2", 1);
			}
			if (PlayerPrefs.GetInt ("Aire2") == 1) {
				GameOverScreenPlaySeccion.SetActive (true);
				Playing = false;
				changes = true;
				MusicaFondo.clip = Music_GameOver;
				if (!MusicaFondo.isPlaying) {
					MusicaFondo.Play ();
					MusicaFondo.loop = true;
				}
				PlayerPrefs.SetInt ("DesbAire3", 1);
			}
			if (PlayerPrefs.GetInt ("Aire3") == 1) {
				GameOverScreenPlaySeccion.SetActive (true);
				Playing = false;
				changes = true;
				MusicaFondo.clip = Music_GameOver;
				if (!MusicaFondo.isPlaying) {
					MusicaFondo.Play ();
					MusicaFondo.loop = true;
				}
				PlayerPrefs.SetInt ("DesbAire4", 1);
			}
			if (PlayerPrefs.GetInt ("Aire4") == 1) {
				GameOverScreenPlaySeccion.SetActive (true);
				Playing = false;
				changes = true;
				MusicaFondo.clip = Music_GameOver;
				if (!MusicaFondo.isPlaying) {
					MusicaFondo.Play ();
					MusicaFondo.loop = true;
				}
				PlayerPrefs.SetInt ("DesbTierra1", 1);
				PlayerPrefs.SetInt ("DesbAire5", 1);
			}

			//TIERRA
			if (PlayerPrefs.GetInt ("Tierra1") == 1) {
				GameOverScreenPlaySeccion.SetActive (true);
				Playing = false;
				changes = true;
				MusicaFondo.clip = Music_GameOver;
				if (!MusicaFondo.isPlaying) {
					MusicaFondo.Play ();
					MusicaFondo.loop = true;
				}
				PlayerPrefs.SetInt ("DesbTierra2", 1);
			}
			if (PlayerPrefs.GetInt ("Tierra2") == 1) {
				GameOverScreenPlaySeccion.SetActive (true);
				Playing = false;
				changes = true;
				MusicaFondo.clip = Music_GameOver;
				if (!MusicaFondo.isPlaying) {
					MusicaFondo.Play ();
					MusicaFondo.loop = true;
				}
				PlayerPrefs.SetInt ("DesbTierra3", 1);
			}
			if (PlayerPrefs.GetInt ("Tierra3") == 1) {
				GameOverScreenPlaySeccion.SetActive (true);
				Playing = false;
				changes = true;
				MusicaFondo.clip = Music_GameOver;
				if (!MusicaFondo.isPlaying) {
					MusicaFondo.Play ();
					MusicaFondo.loop = true;
				}
				PlayerPrefs.SetInt ("DesbTierra4", 1);
			}
			if (PlayerPrefs.GetInt ("Tierra4") == 1) {
				GameOverScreenPlaySeccion.SetActive (true);
				Playing = false;
				changes = true;
				MusicaFondo.clip = Music_GameOver;
				if (!MusicaFondo.isPlaying) {
					MusicaFondo.Play ();
					MusicaFondo.loop = true;
				}
				PlayerPrefs.SetInt ("DesbTierra5", 1);
			}
		} else {
			Playing = true;
		}
	}
}

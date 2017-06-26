using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu : MonoBehaviour {
	public GameObject BarrasOption;
	public GameObject OptionGO;
	public GameObject OpcionesMenu;
	public GameObject PlaySelection;

	public void ReloadScene(){
		PlayerPrefs.SetInt ("TodosActivados", 1);
		Application.LoadLevel ("LevelInfinito");
	}

	public void ExtiGame(){
		Application.Quit ();
	}

	public void PlayBtn(){
		OpcionesMenu.SetActive(false);
		PlaySelection.SetActive(true);
	}

	public void CerrarBtn(){
		OpcionesMenu.SetActive(true);
		PlaySelection.SetActive(false);
	}

	public void OptionsBtn(){
		OptionGO.SetActive (true);
		BarrasOption.GetComponent<CanvasGroup> ().alpha = 1;
		BarrasOption.GetComponent<CanvasGroup> ().blocksRaycasts = true;
		BarrasOption.GetComponent<CanvasGroup> ().interactable = true;

	}
	public void CloseOptionsBtn(){
		OptionGO.SetActive (false);
		BarrasOption.GetComponent<CanvasGroup> ().alpha = 0;
		BarrasOption.GetComponent<CanvasGroup> ().blocksRaycasts = false;
		BarrasOption.GetComponent<CanvasGroup> ().interactable = false;

	}
}

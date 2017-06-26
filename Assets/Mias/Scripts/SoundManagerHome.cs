using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManagerHome :  MonoBehaviour {

	public Scrollbar SonidoScroll;
	public AudioSource volumeMusica;

	public Scrollbar FXScroll;
//	public AudioSource volumeFX1;
//	public AudioSource volumeFX2;
//	public AudioSource volumeFX3;
//	public AudioSource volumeFX4;
//	public AudioSource volumeFX5;
//	public AudioSource volumeFX6;
//	public AudioSource volumeFX7;
//	public AudioSource volumeFX8;
//	public AudioSource volumeFX9;


	void Awake(){
		SonidoScroll.value = PlayerPrefs.GetFloat ("VolumenMuscia");
		FXScroll.value = PlayerPrefs.GetFloat ("VolumesFX");
	}

	public void OnValueMusica (){
		volumeMusica.volume = SonidoScroll.value;
		PlayerPrefs.SetFloat ("VolumenMuscia", SonidoScroll.value);
	}

	public void OnValueFX (){
//		volumeFX1.volume = FXScroll.value;
//		volumeFX2.volume = FXScroll.value;
//		volumeFX3.volume = FXScroll.value;
//		volumeFX4.volume = FXScroll.value;
//		volumeFX5.volume = FXScroll.value;
//		volumeFX6.volume = FXScroll.value;
//		volumeFX7.volume = FXScroll.value;
//		volumeFX8.volume = FXScroll.value;
//		volumeFX9.volume = FXScroll.value;
		PlayerPrefs.SetFloat ("VolumesFX", FXScroll.value);
	}

}

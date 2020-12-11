using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdentPonto : MonoBehaviour {

	[SerializeField]
	private AudioSource audioS;
	[SerializeField]
	private AudioClip clip;
	[SerializeField]
	private GameObject pontosImg;

	void OnTriggerExit2D(Collider2D col)
	{
		if(col.gameObject.CompareTag("bola") || col.gameObject.CompareTag("bolaclone"))
		{
			GAMEMANAGER.instance.pontos++;

			GAMEMANAGER.instance.moedasIntSave += GAMEMANAGER.instance.pontos * 50;

			UIMANAGER.instance.moedasUI.text = (GAMEMANAGER.instance.moedasIntSave).ToString ("c");

			ShootScript.fezponto = true;
			TocaAudio.TocadordeAudio (audioS,clip);
			Instantiate (pontosImg, gameObject.transform.position, Quaternion.identity);
		}
	}

}

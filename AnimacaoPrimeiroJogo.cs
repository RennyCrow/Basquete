using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimacaoPrimeiroJogo : MonoBehaviour {



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MostraEsferas()
	{

	}

	IEnumerator Esferas()
	{
		int pontinhos = 0;

		while(pontinhos < 6)
		{
			yield return new WaitForSeconds (1);

		}

	}
}

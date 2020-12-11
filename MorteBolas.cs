using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MorteBolas : MonoBehaviour {

	//Variaveis Morte

	[SerializeField]
	private float vidaBola = 1f;
	[SerializeField]
	private Color cor;
	[SerializeField]
	private Renderer bolaRender;
	[SerializeField]
	public bool tocouChao = false;



	// Use this for initialization
	void Start () {
		bolaRender = gameObject.GetComponent<Renderer> ();
		cor = bolaRender.material.GetColor ("_Color");


	}
	
	// Update is called once per frame
	void Update()
	{
		if(tocouChao == true)
		{
			MataBola ();
            
		}

		if(transform.position.x < GAMEMANAGER.instance.posE.position.x || transform.position.x > GAMEMANAGER.instance.posD.position.x)
		{
			MataBola ();
		}

	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("chao"))
		{
			tocouChao = true;
		}

	}

	void MataBola()
	{
		if(vidaBola > 0)
		{
			vidaBola -= Time.deltaTime;
			bolaRender.material.SetColor ("_Color", new Color (cor.r, cor.g, cor.b, vidaBola));
		}

		if(vidaBola <= 0)
		{   
			GAMEMANAGER.instance.bolaEmCena = false;
            Destroy(gameObject);



			if(gameObject.CompareTag("bola"))
			{
				GAMEMANAGER.instance.numJogadas--;
				UIMANAGER.instance.numBolas.text = GAMEMANAGER.instance.numJogadas.ToString ();

				if(GAMEMANAGER.instance != null && GAMEMANAGER.instance.numJogadas > 0)
				{
					GAMEMANAGER.instance.NascBolas ();
				}
			}


                                    
        }

	}


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class ShootScript : MonoBehaviour {


	private float forca = 2.0f;
	private Vector2 startPos;
	[SerializeField]
	private bool tiro = false;
	[SerializeField]
	private bool mirando = false;


	[SerializeField]
	private GameObject dotsGO;
	private List<GameObject> caminho;

	[SerializeField]
	private Rigidbody2D myRBody;
	[SerializeField]
	private Collider2D myCollider;

	private Vector2 vel;

	//public int x = 0;

	//Variaveis aux
	[SerializeField]
	private float valorX, valorY;

	//Tipo de jogada
	[SerializeField]
	private bool bateuAro = false,bateuTabela = false;


	//Marcou ponto

	public static bool fezponto;

	[SerializeField]
	private bool liberaSky;

	[SerializeField]
	private Animator anim;


    // Use this for initialization
    void Start () {

       



        anim = GameObject.FindWithTag ("RimTxt").GetComponent<Animator> (); 
			
		liberaSky = false;
		fezponto = false;
		dotsGO = GameObject.FindWithTag ("dots");
		myRBody.isKinematic = true;
		myCollider.enabled = false;
		startPos = transform.position;
		caminho = dotsGO.transform.Cast<Transform> ().ToList ().ConvertAll (t => t.gameObject);
		for(int x=0; x < caminho.Count;x++)
		{
			caminho [x].GetComponent<Renderer> ().enabled = false;
		}

	}


		
	void FixedUpdate () {

		if(GAMEMANAGER.instance.jogoExecutando == true)
		{
			Vector2 wp = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (wp, Vector2.zero);

			if(hit.collider == null)
			{
				if (!myRBody.gameObject.CompareTag("bolaclone"))
				{
					Mirando();
				} 
			}

		}

     



	}

	void Update()
	{
		if(GAMEMANAGER.instance.jogoExecutando == true)
		{
			if(!myRBody.isKinematic)
			{            

				if(bateuTabela == false)
				{
					//RimShot
					if (bateuAro == true && fezponto == true && liberaSky == false) {
						GAMEMANAGER.instance.rimShot = true;

						fezponto = false;

						GAMEMANAGER.instance.desafioNum1RimShot--;
						GAMEMANAGER.instance.DesafioDeFase (OndeEstou.instance.fase);

						anim.Play ("RimShotAnim");
					} 
					//SwishShot
					else if(fezponto == true && liberaSky == false)
					{
						GAMEMANAGER.instance.swishShot = true;

						fezponto = false;

                        GAMEMANAGER.instance.desafioNum2SwishShot--;
                        GAMEMANAGER.instance.DesafioDeFase(OndeEstou.instance.fase);
                    }
				}

				//SkyHook

				if(liberaSky == true && fezponto == true)
				{
					GAMEMANAGER.instance.skyHook = true;

					fezponto = false;
                    GAMEMANAGER.instance.desafioNum3SkyHook--;
                    GAMEMANAGER.instance.DesafioDeFase(OndeEstou.instance.fase);
                }

			}
		}

    }




	//Metodos

	void MostraCaminho()
	{
		for(int x=0; x < caminho.Count;x++)
		{
			caminho [x].GetComponent<Renderer> ().enabled = true;
		}
	}

	void EscondeCaminho()
	{
		for(int x=0; x < caminho.Count;x++)
		{
			caminho [x].GetComponent<Renderer> ().enabled = false;
		}
	}


	Vector2 PegaForca(Vector3 mouse)
	{
		
		return (new Vector2 (startPos.x + valorX ,startPos.y + valorY) - new Vector2 (mouse.x, mouse.y)) * forca;

		
	}


	Vector2 CaminhoPonto(Vector2 posInicial,Vector2 velInicial,float tempo)
	{
		return posInicial + velInicial * tempo + 0.5f * Physics2D.gravity * tempo * tempo;
	}

	void CalculoCaminho()
	{

			vel = PegaForca (Input.mousePosition) * Time.fixedDeltaTime / myRBody.mass;




		for(int x=0; x < caminho.Count; x++)
		{
			caminho [x].GetComponent<Renderer> ().enabled = true;
			float t = x / 20f;
			Vector3 point = CaminhoPonto (transform.position, vel, t);
			point.z = 1.0f;
			caminho [x].transform.position = point;
		}
	}



	void Mirando()
	{
		if (tiro == true)			
			return;
		



		if(Input.GetMouseButton(0) /*&& VERIFICA_AREA_RESTRITA.restrita == false*/)
		{
			if(GAMEMANAGER.instance.primeiraVez == 0)
			{
				GAMEMANAGER.instance.DesligaTuto ();
			}

			if (mirando == false) {				
				mirando = true;
				startPos = Input.mousePosition;
				CalculoCaminho ();
				MostraCaminho ();
			} else {
				
				CalculoCaminho ();
			}
			
		}else if(mirando == true && tiro == false){

			myRBody.isKinematic = false;
			myCollider.enabled = true;
			tiro = true;
			mirando = false;
			myRBody.AddForce(PegaForca(Input.mousePosition));
			EscondeCaminho();
		}
	}


	void OnBecameInvisible()
	{	
		SegueBola.alvoInvisivel = true;
	}

	void OnBecameVisible()
	{	
		SegueBola.alvoInvisivel = false;
	}


	void OnCollisionEnter2D(Collision2D col)
	{
		if(col.gameObject.CompareTag("Aro"))
		{
			bateuAro = true;
		}

		if(col.gameObject.CompareTag("Tabela"))
		{
			bateuTabela = true;
		}
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if(col.gameObject.CompareTag("Sky"))
		{
			liberaSky = true;
		}

	}


}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GAMEMANAGER : MonoBehaviour {


	[System.Serializable]
	public class DesafiosTxt
	{
		public int ondeEstou;
		public string desafioRim, desafioSwish, desafioSky;
		public int desafioInt1RimShot = 0, desafioInt2SwishShot = 0, desafioInt3SkyHook = 0;
		public int numeroJogadas;
	}

	public List<DesafiosTxt> desafiosList;

	void ListaAdd()
	{
		foreach(DesafiosTxt desaf in desafiosList)
		{
			if(desaf.ondeEstou == OndeEstou.instance.fase)
			{
				UIMANAGER.instance.desafio1.text = desaf.desafioRim;
				UIMANAGER.instance.desafio2.text = desaf.desafioSwish;
				UIMANAGER.instance.desafio3.text = desaf.desafioSky;

				desafioNum1RimShot = desaf.desafioInt1RimShot;
				desafioNum2SwishShot = desaf.desafioInt2SwishShot;
				desafioNum3SkyHook = desaf.desafioInt3SkyHook;

				numJogadas = desaf.numeroJogadas;

				UIMANAGER.instance.desafio1Ap.text = desaf.desafioRim;
				UIMANAGER.instance.desafio2Ap.text = desaf.desafioSwish;
				UIMANAGER.instance.desafio3Ap.text = desaf.desafioSky;


				break;
			}
		}
	}


	//limites

	public Transform posE, posD;
	//

	public static GAMEMANAGER instance;

	public int desafioNum1RimShot, desafioNum2SwishShot, desafioNum3SkyHook;

	public bool bolaEmCena;
	public int numJogadas;
    //public GameObject bolaPrefab;
    public GameObject []bolaPrefab;

	[SerializeField]
	private Transform posDireita, posEsquerda, posCima, posBaixo;

	public bool jogoExecutando = true, win = false, lose = false;

	//Mão Bolinhas

	public GameObject mao, bolinhas;
	public int primeiraVez = 0;

	//Identifica_ponto
	public int pontos = 0;
	public bool rimShot = false, swishShot = false,skyHook = false;

	public int moedasIntSave;

	[SerializeField]
	private GameObject fundo, tela,telaWL;
	[SerializeField]
	private Animator animCont;
    //***********************************************************
    private Animator maoAnim, bolinhasAnim;


	public void LiberaContagem()
	{
		fundo.gameObject.SetActive (false);
		tela.gameObject.SetActive (false);
        telaWL.SetActive(false);
		animCont.Play ("ContadorAnim");
	}


	void Awake()
	{


		if(instance == null)
		{
			instance = this;
			DontDestroyOnLoad (this.gameObject);
		}
		else
		{
			Destroy (gameObject);
		}

        if (PlayerPrefs.HasKey("PrimeiraVez") == false)
        {
            PlayerPrefs.SetInt("PrimeiraVez", 0);
            primeiraVez = PlayerPrefs.GetInt("PrimeiraVez");
        }
 

        SceneManager.sceneLoaded += Carrega;


    }

    void Carrega(Scene cena, LoadSceneMode modo)
	{
		StartGame();
		ListaAdd ();

		posE = GameObject.FindWithTag ("ME").GetComponent<Transform> ();
		posD = GameObject.FindWithTag ("MD").GetComponent<Transform> ();

		posDireita = GameObject.FindWithTag ("DIREITA_POS").GetComponent<Transform> ();
		posEsquerda = GameObject.FindWithTag ("ESQUERDA_POS").GetComponent<Transform> ();
		posCima = GameObject.FindWithTag ("CIMA_POS").GetComponent<Transform> ();
		posBaixo = GameObject.FindWithTag ("BAIXO_POS").GetComponent<Transform> ();

		fundo = GameObject.FindWithTag ("fundoC");
		tela = GameObject.FindWithTag ("telaDesafio");
		animCont = GameObject.FindWithTag ("contador").GetComponent<Animator> ();

        
        telaWL = GameObject.FindWithTag("telaWL");

        //***************************************************************************************
        maoAnim = GameObject.FindWithTag("mao").GetComponent<Animator>();
        bolinhasAnim = GameObject.FindWithTag("bolinhas").GetComponent<Animator>();

        primeiraVez = PlayerPrefs.GetInt("PrimeiraVez");
        if (primeiraVez == 0 || primeiraVez == 1)
		{
			PegaSpritesTutorial ();

			if(primeiraVez == 1 )
			{
				Matador (mao.gameObject,bolinhas.gameObject);
			}
		}




    }

	// Use this for initialization
	void Start () {

        //PlayerPrefs.DeleteAll();


        StartGame();
		ListaAdd ();

		bolaEmCena = true;
    }

    // Update is called once per frame
    void Update () {

		if(Input.GetKeyDown(KeyCode.Space))
		{
			OndeEstou.instance.fase++;
			SceneManager.LoadScene (OndeEstou.instance.fase);
		}

		if(Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene (OndeEstou.instance.fase);
		}


		//Vencer ou perder

		if(numJogadas <= 0 )
		{
            if(desafioNum1RimShot > 0 || desafioNum2SwishShot > 0 || desafioNum3SkyHook > 0)
            {
                YouLose();
            }
			
		}
		else if(numJogadas > 0 && desafioNum1RimShot <= 0 && desafioNum2SwishShot <= 0 && desafioNum3SkyHook <= 0)
		{
			YouWin ();
		}


  


    }



	public void NascBolas()
	{
        //Codigo ajustado para achar a bola, forma mais complexa
		GameObject bolaClone = Instantiate (bolaPrefab[UIMANAGER.instance.aux], new Vector2 (Random.Range (posEsquerda.position.x, posDireita.position.x), Random.Range (posCima.position.y, posBaixo.position.y)), Quaternion.identity) as GameObject;
		bolaEmCena = true;

        SegueBola.alvo = bolaClone.transform;



    }

	public void DesligaTuto()
	{
		Matador (mao.gameObject,bolinhas.gameObject);
		PlayerPrefs.SetInt ("PrimeiraVez",1);
       
	}

	void Matador(GameObject obj, GameObject obj2)
	{
		Destroy (obj);
		Destroy (obj2);
	}

	void PegaSpritesTutorial()
	{
		mao = GameObject.FindWithTag ("mao");
		bolinhas = GameObject.FindWithTag ("bolinhas");
	}


	void Novamente()
	{
		SceneManager.LoadScene (OndeEstou.instance.fase);
	}

	void Avancar()
	{
		OndeEstou.instance.fase++;
		SceneManager.LoadScene (OndeEstou.instance.fase);
	}

	void Voltar()
	{
		SceneManager.LoadScene ("Menu_Fases");
	}


	void StartGame()
	{

		//novo
		UIMANAGER.instance.novamenteBtn.onClick.AddListener(Novamente);
		UIMANAGER.instance.avancarBtn.onClick.AddListener(Avancar);
		UIMANAGER.instance.voltarBtn.onClick.AddListener(Voltar);
		//
		UIMANAGER.instance.entendiBtn.onClick.AddListener (LiberaContagem);
		jogoExecutando = false;
		pontos = 0;
		win = false;
		lose = false;
		moedasIntSave = SCOREMANAGER.instance.LoadDados ();
		UIMANAGER.instance.moedasUI.text = moedasIntSave.ToString("c");




    }



    public void DesafioDeFase(int fase)
	{
        

        if (OndeEstou.instance.fase == fase)
		{
			if(desafioNum1RimShot == 0)
			{
				UIMANAGER.instance.desafio1T.isOn = true;
				print ("RimShot Completo");
			}
		}

        if (OndeEstou.instance.fase == fase)
        {
            if (desafioNum2SwishShot == 0)
            {
                UIMANAGER.instance.desafio2T.isOn = true;
                print("SwishShot Completo");
            }
        }

        if (OndeEstou.instance.fase == fase)
        {
            if (desafioNum3SkyHook == 0)
            {
                UIMANAGER.instance.desafio3T.isOn = true;
                print("SkyHook Completo");
            }
        }
    }



	void YouWin()
	{

		if(jogoExecutando == true)
		{
			win = true;
			jogoExecutando = false;
			print ("WIN");
			SCOREMANAGER.instance.SalvarDados (moedasIntSave);
			telaWL.SetActive (true);
			UIMANAGER.instance.txtWL.text = "VOCÊ GANHOU!";
            PLACAR.instance.AddToLeaderboard();
		}

	}

	void YouLose()
	{
		if(jogoExecutando == true)
		{
			lose = true;
			jogoExecutando = false;
			print ("LOSE");            
			telaWL.SetActive (true);
			UIMANAGER.instance.txtWL.text = "VOCÊ PERDEU!";
			UIMANAGER.instance.avancarBtn.gameObject.SetActive (false);
			AppLoveEx.instance.ShowAd ();
        }
	}


    //***********************************************

    public void PrimeiraJogada()
    {
        //*********************************************************************************************
        if (jogoExecutando == true && primeiraVez == 0)
        {
            if (mao != null && bolinhas != null)
            {
                maoAnim.Play("iconehand");
                bolinhasAnim.Play("bolinhas");
                print("animando");
            }
            print(primeiraVez);

        }
    }

}

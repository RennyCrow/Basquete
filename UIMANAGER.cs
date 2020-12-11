using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIMANAGER : MonoBehaviour {

	public static UIMANAGER instance;

	public Text desafio1, desafio2, desafio3;

	public Text numBolas;

	public Toggle desafio1T, desafio2T, desafio3T;

	public Text moedasUI;

	public Button entendiBtn;

	public Text desafio1Ap, desafio2Ap, desafio3Ap;


	public Text txtWL;
	public Button avancarBtn;


	public Button voltarBtn,novamenteBtn;

	//Loja

	public List<int> bolas;
	public Image menuIMG;
	public Sprite []imagemSp;//
	public int aux = 0;

	public Button[] compraBtn;

	//

	private Button sobe, desce;


	void Awake()
	{

		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			Destroy(gameObject);
		}


		//
		bolas = new List<int>();
		bolas.Add(0);



		if (!PlayerPrefs.HasKey("Bola0"))
		{
			PlayerPrefs.SetInt("Bola0", bolas[0]);
			PlayerPrefs.SetInt("list_Count",1);//novo
			print("salvo");
		}

		for (int i = 1; i < PlayerPrefs.GetInt("list_Count"); i++)
		{
			bolas.Add(PlayerPrefs.GetInt("Bola" + i));
		}

		//loja
		if (OndeEstou.instance.fase == 2)//
		{
			menuIMG = GameObject.FindWithTag("imgBolasLoja").GetComponent<Image>();//            

			//moedasUI = GameObject.FindWithTag("coinUI").GetComponent<Text>();

			desafio1T = GameObject.FindWithTag("togg1").GetComponent<Toggle>();
			desafio2T = GameObject.FindWithTag("togg2").GetComponent<Toggle>();
			desafio3T = GameObject.FindWithTag("togg3").GetComponent<Toggle>();


			desafio1 = GameObject.FindWithTag("d1").GetComponent<Text>();
			desafio2 = GameObject.FindWithTag("d2").GetComponent<Text>();
			desafio3 = GameObject.FindWithTag("d3").GetComponent<Text>();





		}
		///




		SceneManager.sceneLoaded += Carrega;


	}

	void Carrega(Scene cena, LoadSceneMode modo)
	{

		if (OndeEstou.instance.fase == 2)//
		{
			txtWL = GameObject.FindWithTag("txtWL").GetComponent<Text>();
			avancarBtn = GameObject.FindWithTag("btnAvancar").GetComponent<Button>();


			voltarBtn = GameObject.FindWithTag("btnVoltar").GetComponent<Button>();
			novamenteBtn = GameObject.FindWithTag("btnNovamente").GetComponent<Button>();



			entendiBtn = GameObject.FindWithTag("btnEntendi").GetComponent<Button>();

			//moedasUI = GameObject.FindWithTag("coinUI").GetComponent<Text>();

			numBolas = GameObject.FindWithTag("numBolas").GetComponent<Text>();
			numBolas.text = GAMEMANAGER.instance.numJogadas.ToString();

			desafio1T = GameObject.FindWithTag("togg1").GetComponent<Toggle>();
			desafio2T = GameObject.FindWithTag("togg2").GetComponent<Toggle>();
			desafio3T = GameObject.FindWithTag("togg3").GetComponent<Toggle>();


			desafio1 = GameObject.FindWithTag("d1").GetComponent<Text>();
			desafio2 = GameObject.FindWithTag("d2").GetComponent<Text>();
			desafio3 = GameObject.FindWithTag("d3").GetComponent<Text>();

			desafio1Ap = GameObject.FindWithTag("desafio1Ap").GetComponent<Text>();
			desafio2Ap = GameObject.FindWithTag("desafio2Ap").GetComponent<Text>();
			desafio3Ap = GameObject.FindWithTag("desafio3Ap").GetComponent<Text>();

			//NOVO LOJA

			menuIMG = GameObject.FindWithTag("imgBolasLoja").GetComponent<Image>();//
			menuIMG.sprite = imagemSp[PlayerPrefs.GetInt("Bola" + bolas[0])];//

			//Sobe desce btn

			sobe = GameObject.FindWithTag ("btnCima").GetComponent<Button> ();
			desce = GameObject.FindWithTag ("btnBaixo").GetComponent<Button> ();

			sobe.onClick.AddListener (CimaBolas);
			desce.onClick.AddListener (BaixoBolas);
			aux = 0;
		}

		//novo******************************************************************************************************************
		moedasUI = GameObject.FindWithTag("coinUI").GetComponent<Text>();
		moedasUI.text = SCOREMANAGER.instance.LoadDados ().ToString("c");

		AtualizaBTNBola ();


	}

	// Use this for initialization
	void Start () {


		if (OndeEstou.instance.fase == 2)//
		{
			numBolas.text = GAMEMANAGER.instance.numJogadas.ToString();

			menuIMG.sprite = imagemSp[PlayerPrefs.GetInt("Bola" + bolas[0])];//
		}

		//SCOREMANAGER.instance.SalvarDados (1000);

		// PlayerPrefs.DeleteAll();


	}



	//*******************************************************************************************
	public void Compra(int id)
	{
		if (id == 1) {
			
			if (SCOREMANAGER.instance.LoadDados () >= 1000) {			
				ChamaCompra (1);
				SCOREMANAGER.instance.PerdeMoedas (1000);
				moedasUI.text = SCOREMANAGER.instance.LoadDados ().ToString("c");
			} else {

				print ("Sem dinheiro");
			}

		} else if (id == 2) {
			
			if (SCOREMANAGER.instance.LoadDados () >= 3000) {
				ChamaCompra (2);
				SCOREMANAGER.instance.PerdeMoedas (3000);
				moedasUI.text = SCOREMANAGER.instance.LoadDados ().ToString("c");
			} else {

				print ("Sem dinheiro");
			}
		}

	}

	//**********************************************************************************
	void ChamaCompra(int id)
	{
		bolas.Add (id);

		PlayerPrefs.SetInt ("list_Count", bolas.Count);
		PlayerPrefs.SetInt("Bola"+id,id);
		compraBtn[id - 1].interactable = false;
		if(id != 2)
		{
			compraBtn[id].interactable = true;
		}


		if (bolas.Contains(id))
		{			
			compraBtn[id - 1].GetComponentInChildren<Text>().text = "Comprado";
			compraBtn[id - 1].GetComponentInChildren<Text>().color = new Color(0, 1, 0, 1);

		}
	}

	void AjustaBolasBtn(int x)
	{
		
		compraBtn [x].interactable = false;
		compraBtn[x].GetComponentInChildren<Text>().text = "Comprado";
		compraBtn [x].GetComponentInChildren<Text> ().color = new Color (0,1,0,1);

	}

	void BaixoBolas()
	{
		if(aux < bolas.Count - 1)
		{
			aux++;
			menuIMG.sprite = imagemSp [PlayerPrefs.GetInt ("Bola" + aux)];
		}
	}

	void CimaBolas()
	{
		if(aux >= 1)
		{
			aux--;
			menuIMG.sprite = imagemSp [PlayerPrefs.GetInt ("Bola" + aux)];
		}
	}


	void AtualizaBTNBola()
	{
		if(OndeEstou.instance.fase == 3)//ajustado
		{
			compraBtn = new Button[2];

			compraBtn [0] = GameObject.FindWithTag ("btnCompra1").GetComponent<Button> ();
			compraBtn [1] = GameObject.FindWithTag ("btnCompra2").GetComponent<Button> ();


			compraBtn [0].onClick.AddListener (() => Compra (1));
			compraBtn [1].onClick.AddListener (() => Compra (2));
		

			if(bolas.Contains(1))
			{
				AjustaBolasBtn (0);

				if(!bolas.Contains(2))
				{
					compraBtn [1].interactable = true;
				}
			}

			if(bolas.Contains(2))
			{
				AjustaBolasBtn (1);
			}

		}
	}

	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.R))
		{
			SceneManager.LoadScene (OndeEstou.instance.fase);
		}

	}



}

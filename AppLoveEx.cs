using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AppLoveEx : MonoBehaviour {

	public static AppLoveEx instance;

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
	}

	// Use this for initialization
	void Start () {

		AppLovin.InitializeSdk ();
		AppLovin.PreloadInterstitial ();
	}


	public void ShowAd()
	{
		if(AppLovin.HasPreloadedInterstitial())
		{
			AppLovin.ShowInterstitial ();
		}
	}

}

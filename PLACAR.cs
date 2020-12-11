using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PLACAR : MonoBehaviour {

    public static PLACAR instance;

    private void Awake()
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
    }

    // Use this for initialization
    void Start () {
        GooglePlayGames.PlayGamesPlatform.Activate();
        Login();
    }
	
	// Update is called once per frame
	void Update () {

        

	}

    public void Login()
    {
        Social.localUser.Authenticate((bool success) => { });
    }

    public void AddToLeaderboard()
    {
        Social.ReportScore(SCOREMANAGER.instance.LoadDados(), PlacarStatic.placarBest, (bool success) => { });

    }

    public void ShowLeaderboard()
    {
        if(Social.localUser.authenticated)
        {
            GooglePlayGames.PlayGamesPlatform.Instance.ShowLeaderboardUI(PlacarStatic.placarBest);
        }
        else
        {
            Login();
        }
       
    }

    
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager :  Singleton<GameManager> {

   [SerializeField]
    protected string m_PlayerName;

    // How many players are there
    public int numberOfPlayers = 2;

    // Dictionary that will save the "Jugador n: X" selected in the
    // Select your characters screen
    public Dictionary<string, GameObject> players = new Dictionary<string, GameObject>();

    public GameObject parent;
 
    protected virtual void Start () {
       
        //players.Add("Jugador2", GameObject.Find("Riceman"));
        //players.Add("Jugador3", GameObject.Find("Samurai"));
        //players.Add("Jugador4", GameObject.Find("Undead"));

        SceneManager.LoadScene ( "Main Scene" );
        SceneManager.LoadScene ( "Dice", LoadSceneMode.Additive);
        SceneManager.LoadScene ( "Level 1", LoadSceneMode.Additive );
        SceneManager.LoadScene ( "Level 2", LoadSceneMode.Additive );
        SceneManager.LoadScene ( "Level 3", LoadSceneMode.Additive );
        SceneManager.LoadScene ( "Level 4", LoadSceneMode.Additive );
        SceneManager.LoadScene ( "Continent", LoadSceneMode.Additive );
        SceneManager.LoadScene ( "Main Characters", LoadSceneMode.Additive );
        SceneManager.LoadScene ( "Navigation Mesh", LoadSceneMode.Additive );

        // players.Add("Jugador1", GameObject.Find("Witch"));
        // Debug.Log(players["Jugador1"]);


        

        // foreach (GameObject player in GameObject.Find("Characters").transform){
        //     player.SetActive(true);
        // }

    }
 
    public string GetPlayerName () {
        return m_PlayerName;
    }

    public PlayerController Player;
    
}

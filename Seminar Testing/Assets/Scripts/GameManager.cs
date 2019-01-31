using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;
    public BoardManager boardScript;
    [HideInInspector] public bool playersTurn = true;
    public int playerScore = 0;
    public int playerLife = 5;

    private int level = 3;

	// Use this for initialization
	void Awake () {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
        boardScript = GetComponent<BoardManager>();
        InitGame();
	}
	
	// Update is called once per frame
	void InitGame () {
        boardScript.SetupScene(level);
	}

    public void GameOver()
    {
        enabled = false;
    }

    private void Update()
    {
        
    }
}

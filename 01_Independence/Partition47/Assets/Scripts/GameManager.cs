﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public BoardManager boardScript;
    int level = 8;

	// Use this for initialization
	void Awake ()
    {
        boardScript = GetComponent<BoardManager>();
        InitGame();
	}
	
    void InitGame()
    {
        boardScript.SetupScene(level);
    }
	// Update is called once per frame
	void Update () {
		
	}
}

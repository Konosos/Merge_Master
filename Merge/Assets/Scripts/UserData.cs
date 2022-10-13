using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[System.Serializable]
public class UserData 
{
    public int playerMoney;
    public string playerName;
    public int playerRanking;
    public bool firstInGame;

    //---Chess_Data---
    public string[] chessNames;
    public int[] chess_XBoard;
    public int[] chess_YBoard;

    //---Shop_Data---


    //Game Data
    public int numGameWin;
    public int gameIndex;
    public bool soundOn;
    public bool vibrateOn;

    public ulong lastSpinTime;

    public UserData()
    {
        playerName = "You";
        playerMoney = 0;
        playerRanking = 15015;
        firstInGame = true;

        numGameWin = 0;
        gameIndex = 1;
        DateTime dateTime = new DateTime(2001, 1, 1);
        lastSpinTime = (ulong)dateTime.Ticks;

        chessNames = new string[2] {GameConfigs.SPIDERMAN_NAME, GameConfigs.CAPTAIN_NAME };
        chess_XBoard = new int[2] { 2, 2 };
        chess_YBoard = new int[2] { 0, 2 };

        soundOn = true;
        vibrateOn = true;
    }
}

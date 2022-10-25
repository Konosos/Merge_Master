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
    public string[] purchasedHero;

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
        playerMoney = 600;
        playerRanking = 15015;
        firstInGame = true;
        purchasedHero = new string[3] { GameConfigs.SPIDERMAN_NAME, GameConfigs.BLACK_PANTHER_NAME, GameConfigs.BATMAN_NAME };

        numGameWin = 0;
        gameIndex = 1;
        DateTime dateTime = new DateTime(2001, 1, 1);
        lastSpinTime = (ulong)dateTime.Ticks;

        chessNames = new string[1] { GameConfigs.BLACK_PANTHER_NAME };
        chess_XBoard = new int[1] {  2 };
        chess_YBoard = new int[1] {  2 };

        soundOn = true;
        vibrateOn = true;
    }
}

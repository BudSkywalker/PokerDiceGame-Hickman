using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//////////////////////////////////////////////
//Assignment/Lab/Project: Poker Dice Game
//Name: Logan Hickman
//Section: 2021SP.SGD.213.2101
//Instructor: Aurore Wold
//Date: 01/17/2022
/////////////////////////////////////////////
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// Array containing all 5 player dice
    /// </summary>
    [SerializeField]
    private Dice[] playerDice;
    /// <summary>
    /// How many times has the player rolled?
    /// </summary>
    private int rolls = 0;
    [SerializeField]
    private Button rollAgainButton;
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button endTurnButton;
    [SerializeField]
    private TMP_Text playerResult;
    [SerializeField]
    private TMP_Text endGameText;
    
    /// <summary>
    /// Resets all variables and starts the game with a fresh roll
    /// </summary>
    public void RestartGame()
    {
        foreach(Dice dice in playerDice)
        {
            dice.value = 0;
            if(dice.locked)
            {
                dice.ToggleLock();
            }
        }
        rolls = 0;
        rollAgainButton.interactable = true;
        endTurnButton.interactable = true;
        playButton.interactable = false;
        endGameText.text = "";
        RollPlayerDice();
    }

    /// <summary>
    /// Rolls all unlocked player dice
    /// </summary>
    public void RollPlayerDice()
    {
        if(rolls < 3)
        {
            foreach (Dice dice in playerDice)
            {
                if (!dice.locked)
                {
                    dice.value = Random.Range(1, 7);
                }
            }
            rolls++;
        }
        if (rolls >= 3)
        {
            rollAgainButton.interactable = false;
        }

        playerResult.text = CalculateScore(playerDice);
    }

    /// <summary>
    /// Calculates the AI's roll and displays who won
    /// </summary>
    public void EndGame()
    {
        int[] aiDice = RollAIDice();
        string gameResult = CompareWinner(playerDice, aiDice);
        
        string aiResult = "";
        foreach (int i in aiDice)
        {
            aiResult += i + ", ";
        }

        endGameText.text = "Computer rolled " + aiResult + "resulting in " + CalculateScore(aiDice) + ". " + gameResult;
        playButton.interactable = true;
        endTurnButton.interactable = false;
        rollAgainButton.interactable = false;
    }

    /// <summary>
    /// Generates a roll for the AI
    /// </summary>
    /// <returns>Array containing the values of 5 dice rolls</returns>
    private int[] RollAIDice()
    {
        int[] aiDice = new int[5];

        for(int i = 0; i < 5; i++)
        {
            aiDice[i] = Random.Range(1, 7);
        }

        return aiDice;
    }

    /// <summary>
    /// Compares the winners between the player and the AI, including limited tie-breakers
    /// </summary>
    /// <param name="player">Player dice</param>
    /// <param name="ai">AI dice</param>
    /// <returns>Did the player win, lose, or tie?</returns>
    private string CompareWinner(Dice[] player, int[] ai)
    {
        //Setup
        bool pair;
        bool trips;
        
        //Convert player array to use-able int array
        int[] playerScoreArray = new int[player.Length];
        for (int i = 0; i < playerScoreArray.Length; i++)
        {
            playerScoreArray[i] = player[i].value;
        }

        //Find Player Values
        int[] playerValues = new int[6];
        foreach (int i in playerScoreArray)
        {
            switch (i)
            {
                case 1:
                    playerValues[0]++;
                    break;
                case 2:
                    playerValues[1]++;
                    break;
                case 3:
                    playerValues[2]++;
                    break;
                case 4:
                    playerValues[3]++;
                    break;
                case 5:
                    playerValues[4]++;
                    break;
                case 6:
                    playerValues[5]++;
                    break;
                default:
                    break;
            }
        }

        //Calculate Player Score
        int playerScore = 0;
        int playerValue1 = -1;
        int playerValue2 = -1;

        pair = false;
        trips = false;
        for(int i = 0; i < playerValues.Length; i++)
        {
            switch (playerValues[i])
            {
                case 5:
                    playerScore = 6;
                    playerValue1 = i;
                    break;
                case 4:
                    playerScore = 5;
                    playerValue1 = i;
                    break;
                case 3:
                    trips = true;
                    if (pair)
                    {
                        playerScore = 4;
                        playerValue2 = playerValue1;
                        playerValue1 = i;
                    }
                    else
                    {
                        playerScore = 3;
                        playerValue1 = i;
                    }
                    break;
                case 2:
                    if (trips)
                    {
                        playerScore = 4;
                        playerValue2 = i;
                    }
                    else if (pair)
                    {
                        playerScore = 2;
                        playerValue2 = playerValue1;
                        playerValue1 = i;
                    }
                    else
                    {
                        playerScore = 1;
                        playerValue1 = i;
                    }
                    pair = true;
                    break;
                case 1:
                    if (playerValue2 < i && !pair && !trips)
                    {
                        playerValue2 = i;
                    }
                    break;
                case 0:
                    break;
                default:
                    Debug.Log("Unknown or invalid value " + i);
                    break;
            }
        }

        //Find AI Values
        int[] aiValues = new int[6];
        foreach (int i in ai)
        {
            switch (i)
            {
                case 1:
                    aiValues[0]++;
                    break;
                case 2:
                    aiValues[1]++;
                    break;
                case 3:
                    aiValues[2]++;
                    break;
                case 4:
                    aiValues[3]++;
                    break;
                case 5:
                    aiValues[4]++;
                    break;
                case 6:
                    aiValues[5]++;
                    break;
                default:
                    break;
            }
        }

        //Calculate AI Score
        int aiScore = 0;
        int aiValue1 = -1;
        int aiValue2 = -1;

        pair = false;
        trips = false;
        for(int i = 0; i < aiValues.Length; i++)
        {
            switch (aiValues[i])
            {
                case 5:
                    aiScore = 6;
                    aiValue1 = i;
                    break;
                case 4:
                    aiScore = 5;
                    aiValue1 = i;
                    break;
                case 3:
                    trips = true;
                    if (pair)
                    {
                        aiScore = 4;
                        aiValue2 = aiValue1;
                        aiValue1 = i;
                    }
                    else
                    {
                        aiScore = 3;
                        aiValue1 = i;
                    }
                    break;
                case 2:
                    if (trips)
                    {
                        aiScore = 4;
                        aiValue2 = i;
                    }
                    else if (pair)
                    {
                        aiScore = 2;
                        aiValue2 = aiValue1;
                        aiValue1 = i;
                    }
                    else
                    {
                        aiScore = 1;
                        aiValue1 = i;
                    }
                    pair = true;
                    break;
                case 1:
                    if (aiValue2 < i && !pair && !trips)
                    {
                        aiValue2 = i;
                    }
                    break;
                case 0:
                    break;
                default:
                    Debug.Log("Unknown or invalid value " + i);
                    break;
            }
        }
        
        //Compare Scores
        if (playerScore > aiScore)
        {
            return "Player Wins!";
        }
        else if(playerScore < aiScore)
        {
            return "Player Loses!";
        }
        else if(playerValue1 > aiValue1)
        {
            return "Player Wins!";
        }
        else if (playerValue1 < aiValue1)
        {
            return "Player Loses!";
        }
        else if(playerValue2 > aiValue2)
        {
            return "Player Wins!";
        }
        else if (playerValue2 < aiValue2)
        {
            return "Player Loses!";
        }
        else
        {
            return "Its a tie!";
        }
    }

    /// <summary>
    /// Converts the <paramref name="dice"/> into int array and passes it through <see cref="CalculateScore(int[])"/>
    /// </summary>
    /// <param name="dice">Dice</param>
    /// <returns>Result of the combination of dice</returns>
    private string CalculateScore(Dice[] dice)
    {
        int[] score = new int[dice.Length];
        for(int i = 0; i < score.Length; i++)
        {
            score[i] = dice[i].value;
        }
        return CalculateScore(score);
    }

    /// <summary>
    /// Calculates what the result of the dice are
    /// </summary>
    /// <param name="score">Dice</param>
    /// <returns>Result of the combination of dice</returns>
    private string CalculateScore(int[] score)
    {
        //Setup
        string result = "Nothing Special";
        
        //Collect values
        int[] values = new int[6];
        foreach (int i in score)
        {
            switch(i)
            {
                case 1:
                    values[0]++;
                    break;
                case 2:
                    values[1]++;
                    break;
                case 3:
                    values[2]++;
                    break;
                case 4:
                    values[3]++;
                    break;
                case 5:
                    values[4]++;
                    break;
                case 6:
                    values[5]++;
                    break;
                default:
                    break;
            }
        }

        //Compare Values
        bool pair = false;
        bool trips = false;
        foreach(int i in values)
        {
            switch(i)
            {
                case 5:
                    result = "Five of a Kind";
                    break;
                case 4:
                    result = "Four of a Kind";
                    break;
                case 3:
                    trips = true;
                    if(pair)
                    {
                        result = "Full House";
                    }
                    else
                    {
                        result = "Three of a Kind";
                    }
                    break;
                case 2:
                    if(trips)
                    {
                        result = "Full House";
                    }
                    else if(pair)
                    {
                        result = "Two Pairs";
                    }
                    else
                    {
                        result = "One Pair";
                    }
                    pair = true;
                    break;
                case 1:
                case 0:
                    break;
                default:
                    Debug.Log("Unknown or invalid value " + i);
                    break;
            }
        }

        return result;
    }
}

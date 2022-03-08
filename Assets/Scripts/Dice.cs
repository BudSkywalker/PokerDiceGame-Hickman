using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//////////////////////////////////////////////
//Assignment/Lab/Project: Poker Dice Game
//Name: Logan Hickman
//Section: 2021SP.SGD.213.2101
//Instructor: Aurore Wold
//Date: 01/17/2022
/////////////////////////////////////////////
public class Dice : MonoBehaviour
{
    /// <summary>
    /// What number does this dice hold?
    /// </summary>
    public int value;
    /// <summary>
    /// Is this dice locked and avoids being rolled?
    /// </summary>
    public bool locked;
    /// <summary>
    /// Dice Button
    /// </summary>
    [SerializeField]
    private Button button;
    /// <summary>
    /// Dice Text Field
    /// </summary>
    [SerializeField]
    private TMP_Text text;

    void Update()
    {
        text.text = value.ToString();
    }

    /// <summary>
    /// Toggle the lock state of the dice
    /// </summary>
    public void ToggleLock()
    {
        locked = !locked;
        if(locked)
        {
            GetComponent<Button>().image.color = Color.red;
        }
        else
        {
            GetComponent<Button>().image.color = Color.white;
        }

    }
}

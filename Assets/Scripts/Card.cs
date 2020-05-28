using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Card
{
    public string cardName;
    public int attack;
    public int defense;
    public ImageTargetBehaviour it;

    public Card(string CardName, int Attack, int Defense, ImageTargetBehaviour It)
    {
        cardName = CardName;
        attack = Attack;
        defense = Defense;
        it = It;
    }
}
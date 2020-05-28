using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;
using static Vuforia.TrackableBehaviour;

public class GameBehaviour : MonoBehaviour
{
    Player player1;
    Player player2;

    int turn;

    public TextMesh lifepoints1;
    public TextMesh lifepoints2;

    bool played;
    bool attacked;

    AudioSource audioData;

    private ImageTargetBehaviour mImageTargetBehaviour;

    public static List<Card> cardList;
    public static List<Card> CardsonField;
    public static List<Card> CardsonField1;
    public static List<Card> CardsonField2;

    public ImageTargetBehaviour FieryStone;
    public ImageTargetBehaviour RedDragon;
    public ImageTargetBehaviour SoulEaterDragon;
    public ImageTargetBehaviour AngryMinotaur;
    public ImageTargetBehaviour RedBoar;
    public ImageTargetBehaviour FierceBee;
    public ImageTargetBehaviour BloodBat;
    public ImageTargetBehaviour WhirlwindEagle;
    public ImageTargetBehaviour TravellingCrusader;
    public ImageTargetBehaviour KingoftheJungle;


    private void Start()
    {
        player1 = new Player(10000);
        player2 = new Player(10000);

        turn = 1;
        audioData = GetComponent<AudioSource>();

        lifepoints1.text = player1.lifepoints.ToString();
        lifepoints2.text = player2.lifepoints.ToString();

        cardList = new List<Card>();
        CardsonField = new List<Card>();
        CardsonField1 = new List<Card>();
        CardsonField2 = new List<Card>();

        Card RedBoarCard = new Card("Red Boar Card", 500, 2500, RedBoar);
        Card RedDragonCard = new Card("Red Dragon Card", 2000, 2000, RedDragon);
        Card FieryStoneCard = new Card("Fiery Stone Card", 1500, 2000, FieryStone);
        Card SoulEaterDragonCard = new Card("Soul Eater Dragon Card", 2500, 2000, SoulEaterDragon);
        Card AngryMinotaurCard = new Card("Angry Minotaur Card", 1000, 2500, AngryMinotaur);

        Card FierceBeeCard = new Card("Fierce Bee Card", 500, 500, FierceBee);
        Card BloodBatCard = new Card("Blood Bat Card", 1000, 500, BloodBat);
        Card WhirlwindEagleCard = new Card("Whirlwind Eagle Card", 1500, 1000, WhirlwindEagle);
        Card TravellingCrusaderCard = new Card("Travelling Crusader Card", 2000, 2000, TravellingCrusader);
        Card KingoftheJungleCard = new Card("King of the Jungle Card", 3000, 3000, KingoftheJungle);

        cardList.Add(AngryMinotaurCard);
        cardList.Add(FieryStoneCard);
        cardList.Add(RedBoarCard);
        cardList.Add(SoulEaterDragonCard);
        cardList.Add(RedDragonCard);
        cardList.Add(FierceBeeCard);
        cardList.Add(BloodBatCard);
        cardList.Add(WhirlwindEagleCard);
        cardList.Add(TravellingCrusaderCard);
        cardList.Add(KingoftheJungleCard);
    }

    public void GameMechanics()
    {
        if (mImageTargetBehaviour)
        {

            if (turn == 1)
            {
                if (!played)
                {
                    Play(turn);
                }

                if (!attacked && CountAmountInList(CardsonField1) > 0)
                {
                    Attack(turn);
                }
                if (CheckIfRoundPlayed() && !gameOver())
                {
                    NextTurn();
                }
                if(gameOver())
                {
                    StartCoroutine(WaitForEnd());
                }
            }
        }
        if (mImageTargetBehaviour)
        { 

            if (turn == 2)
            {
                if (!played)
                {
                    Play(turn);
                }

                if (!attacked && CountAmountInList(CardsonField2) > 0)
                {
                    Attack(turn);
                }
                if (CheckIfRoundPlayed() && !gameOver())
                {
                    NextTurn();
                }
                if (gameOver())
                {
                    StartCoroutine(WaitForEnd());
                }

            }
                
        }
    }

    public bool gameOver()
    {
        if(player2.lifepoints <= 0)
        {
            lifepoints1.text = "PLAYER 1 WINS";
            lifepoints2.text = "PLAYER 1 WINS";
            return true;

        }
        if (player1.lifepoints <= 0)
        {
            lifepoints1.text = "PLAYER 2 WINS";
            lifepoints2.text = "PLAYER 2 WINS";
            return true;

        }
        return false;
    }

    public void NextTurn()
    {

        if (turn == 1)
        {
            turn = 2;
            lifepoints2.fontSize = 15;
            lifepoints1.fontSize = 10;
            played = false;
            attacked = false;
            mImageTargetBehaviour = null;
        }
        else
        {
            turn = 1;
            lifepoints1.fontSize = 15;
            lifepoints2.fontSize = 10;
            played = false;
            attacked = false;
            mImageTargetBehaviour = null;
        }
    }


    public bool CheckIfRoundPlayed()
    {
        if (played && attacked)
        {
            return true;
        }
        else
        {
            GameMechanics();
            return false;
        }
    }

    public void OnTrackableStateChanged(StatusChangeResult obj)
    {

        if (obj.NewStatus == Status.DETECTED ||

            obj.NewStatus == Status.TRACKED ||

            obj.NewStatus == Status.EXTENDED_TRACKED)
        {
            GameMechanics();
        }
    }

    public void Play(int turn)
    {
        if (IsCardITBAndITBEqual(mImageTargetBehaviour.Trackable.Name))
        {
            if (turn == 1)
            {
                played = true;

            }
            if (turn == 2)
            {

                played = true;

            }
        }
        else
        {
            GameMechanics();
        }
            
        

    }

    public void Attack(int turn)
    {
        if (turn == 1)
        {
            foreach(Card card1 in CardsonField1)
            {
                if (CountAmountInList(CardsonField2) > 0)
                {
                    foreach(Card card2 in CardsonField2)
                    {
                        if (card2.defense >= card1.attack)
                        {
                            player2.lifepoints = player2.lifepoints - (card1.attack/2);
                            lifepoints2.text = player2.lifepoints.ToString();
                            audioData.Play();
                            attacked = true;
                        }
                        else
                        {
                           
                            player2.lifepoints = player2.lifepoints - card1.attack;
                            lifepoints2.text = player2.lifepoints.ToString();
                            audioData.Play();
                            attacked = true;
                        }
                    }
                }
                else
                {
                    player2.lifepoints = player2.lifepoints - card1.attack;
                    lifepoints2.text = player2.lifepoints.ToString();
                    audioData.Play();
                    attacked = true;
                }
            }

        }

        if (turn == 2)
        {
            foreach (Card card2 in CardsonField2)
            {
                if (CountAmountInList(CardsonField1) > 0)
                {

                    foreach (Card card1 in CardsonField1)
                    {
                        if (card1.defense >= card2.attack)
                        {
                            player1.lifepoints = player1.lifepoints - (card2.attack/2);
                            lifepoints1.text = player1.lifepoints.ToString();
                            audioData.Play();
                            attacked = true;
                        }
                        else
                        {
                            player1.lifepoints = player1.lifepoints - card2.attack;
                            lifepoints1.text = player1.lifepoints.ToString();
                            audioData.Play();
                            attacked = true;
                        }
                    }
                }
                else
                {
                    player1.lifepoints = player1.lifepoints - card2.attack;
                    lifepoints1.text = player1.lifepoints.ToString();
                    audioData.Play();
                    attacked = true;
                }
            }
        }
    }

    public bool IsCardITBAndITBEqual(string TrackedName)
    {

            foreach (Card card in cardList)
            {
                if (CountAmountInList(CardsonField) > 0)
                {
                    if (card.cardName == TrackedName)
                    {
                        {
                            CardsonField.Add(card);
                            if (turn == 1)
                            {
                                CardsonField1.Add(card);
                                cardList.Remove(card);
                                return true;
                            }
                            if (turn == 2)
                            {
                                CardsonField2.Add(card);
                                cardList.Remove(card);
                                return true;
                            }
                        }
                    }
                }
                else
                {
                    if (card.cardName == TrackedName)
                    {
                        {
                            CardsonField.Add(card);
                            if (turn == 1)
                            {
                                CardsonField1.Add(card);
                                cardList.Remove(card);
                                return true;
                            }
                            else
                            {
                                CardsonField2.Add(card);
                                cardList.Remove(card);
                                return true;
                            }
                        }
                    }
                }


            }

        return false;
    }

    public void changeTrackableBehaviour(ImageTargetBehaviour itb)
    {
        if (AllCardsInField(CardsonField) != itb)
        {
            mImageTargetBehaviour = itb;
            mImageTargetBehaviour.RegisterOnTrackableStatusChanged(OnTrackableStateChanged);
        }
        else
        {
            GameMechanics();
        }
        
    }

    public int CountAmountInList(List<Card> list)
    {
        int Count = 0;

        foreach(Card card in list)
        {
            Count++;
        }

        return Count;
    }

    public ImageTargetBehaviour AllCardsInField(List<Card> list)
    {

        foreach (Card card in list) 
        { 
            return card.it;
        }
        return null;

    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private IEnumerator WaitForEnd()
    {
        yield return new WaitForSeconds(5);
        ReturnToMainMenu();

    }
    
}
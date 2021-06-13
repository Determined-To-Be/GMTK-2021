using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMTKMMXXI;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{ 
    public Queue<Card> deck;

    CardPickup[] inPlay;
    Text inPlayUI;

    // Singleton pattern
    static GameController _Instance;
    public static GameController Instance
    {
        get
        {
            if (_Instance != null)
            {
                return _Instance;
            }

            GameObject obj = new GameObject();
            _Instance = obj.AddComponent<GameController>();
            return _Instance;
        }
    }

    void Awake()
    {
        // Singleton pattern
        if (_Instance == null)
        {
            _Instance = GetComponent<GameController>();
        }
        else if (_Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        deck = new Queue<Card>();
        for (Faces f = Faces.DEUCE; f <= Faces.ACE; f++)
        {
            for (Suits s = Suits.DIAMONDS; s <= Suits.SPADES; s++)
            {
                deck.Enqueue(new Card(f, s));
            }
        }

        inPlay = GetComponentsInChildren<CardPickup>();
        foreach (CardPickup cp in inPlay)
        {
            cp.Deal();
        }

        inPlayUI = GameObject.Find("In Play").GetComponent<Text>();
    }

    void Update()
    {
        if (deck.Count > 0)
        {
            foreach (CardPickup cp in inPlay)
            {
                if (cp.card == null)
                {
                    cp.Deal();
                    break;
                }
            }
        }

        inPlayUI.text = "";
        foreach (CardPickup cp in inPlay)
        {
            if (cp.card != null)
            {
                inPlayUI.text += cp.card.face.ToString() + " | " + cp.card.suit.ToString() + "\n";
            }
        }
    }

    public Card Deal()
    {
        return deck.Dequeue();
    }

    public void Discard(Card c)
    {
        deck.Enqueue(c);
    }
}

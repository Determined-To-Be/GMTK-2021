using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMTKMMXXI;

public class GameController : MonoBehaviour
{
    [SerializeField] int cooldown;

    CardPickup[] inPlay;
    Queue<Card> deck;

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

        deck = new Queue<Card>();
        inPlay = GetComponentsInChildren<CardPickup>();
    }

    void Start()
    {
        for (Faces f = Faces.DEUCE; f <= Faces.ACE; f++)
        {
            for (Suits s = Suits.DIAMONDS; s <= Suits.SPADES; s++)
            {
                deck.Enqueue(new Card(f, s));
            }
        }

        foreach (CardPickup cp in inPlay)
        {
            cp.Deal(deck.Dequeue());
        }

        Shuffle();
    }

    IEnumerator Shuffle()
    {
        while (true)
        {
            yield return new WaitForSeconds(cooldown);

            foreach (CardPickup cp in inPlay)
            {
                if (deck.Count == 0)
                {
                    break;
                }

                if (cp.card == null)
                {
                    cp.Deal(deck.Dequeue());
                }
            }
        }
    }

    public void Discard(Card c)
    {
        deck.Enqueue(c);
    }
}

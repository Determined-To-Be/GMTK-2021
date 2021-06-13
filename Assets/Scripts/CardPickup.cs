using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMTKMMXXI;

namespace GMTKMMXXI
{
    public enum Faces
    {
        DEUCE,
        THREE,
        FOUR,
        FIVE,
        SIX,
        SEVEN,
        EIGHT,
        NINE,
        TEN,
        JACK,
        QUEEN,
        KING,
        ACE
    }
    public enum Suits
    {
        DIAMONDS,
        CLUBS,
        HEARTS,
        SPADES
    }
}

public class CardPickup : MonoBehaviour
{
    public Card card;

    [SerializeField] float rotSpeed;
    [SerializeField] float bounceSpeed;
    [SerializeField] float bounceHeight;

    Vector3 initPos;
    Vector3 goalPos;
    bool isDirectionUp;

    void Start()
    {
        initPos = transform.position;
        goalPos = transform.position + (Vector3.up * bounceHeight);
        isDirectionUp = true;
    }

    void FixedUpdate()
    {
        transform.Rotate(Vector3.forward * rotSpeed);
        transform.position = transform.position + ((isDirectionUp ? Vector3.up : Vector3.down) * bounceSpeed);
        isDirectionUp = transform.position.y > goalPos.y ? false : (transform.position.y < initPos.y ? true : isDirectionUp);
    }

    public void Deal()
    {
        gameObject.SetActive(true);
        card = GameController.Instance.Deal();
    }
}

public class Card
{
    public Faces face;
    public Suits suit;

    public Card(Faces face, Suits suit)
    {
        this.face = face;
        this.suit = suit;
    }
} 

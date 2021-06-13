using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMTKMMXXI;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torque;
    [SerializeField] float accel;
    [SerializeField] float reverseAccel;

    Rigidbody rb;
    Text handUI;
    Stack<Card> hand;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        hand = new Stack<Card>();
        handUI = GameObject.Find("Hand").GetComponent<Text>();
    }

    void FixedUpdate()
    {
        rb.AddTorque(transform.up * torque * InputManager.Instance.GetSteering());
        rb.AddForce(transform.forward * (InputManager.Instance.GetButton(Buttons.BRAKE) ? -reverseAccel : accel * (InputManager.Instance.GetButton(Buttons.GAS) ? 1 : 0)));
    }

    private void Update()
    {
        if (InputManager.Instance.GetButtonDown(Buttons.DROP))
        {
            GameController.Instance.Discard(hand.Pop());
            //UpdateHandUI();
        }

        UpdateHandUI();
    }

    private void OnCollisionEnter(Collision collision)
    {
        CardPickup cp = collision.gameObject.GetComponent<CardPickup>();
        Debug.Log(cp);
        if (cp != null)
        {
            if (hand.Count < 2)
            {
                hand.Push(cp.card);
                cp.card = null;
                cp.gameObject.SetActive(false);
                //UpdateHandUI();
            }
        }
    }

    void UpdateHandUI() {
        handUI.text = "";

        /*
        if (hand.Count == 0)
        {
            handUI.text = "[EMPTY]";
            return;
        }
        */

        foreach (Card c in hand)
        {
            handUI.text += c.face.ToString() + " | " + c.suit.ToString() + "\n";
        }

        handUI.text += "\n";
        foreach(Card c in GameController.Instance.deck)
        {
            handUI.text += c.face.ToString() + " | " + c.suit.ToString() + "\n";
        }
    }
}

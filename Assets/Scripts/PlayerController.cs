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

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        handUI = GameObject.Find("Hand").GetComponent<Text>();
        hand = new Stack<Card>();
    }

    void FixedUpdate()
    {
        rb.AddTorque(transform.up * torque * InputManager.Instance.GetSteering());
        rb.AddForce(transform.forward * (InputManager.Instance.GetButton(Buttons.BRAKE) ? -reverseAccel : accel * (InputManager.Instance.GetButton(Buttons.GAS) ? 1 : 0)));
    }

    private void Update()
    {
        if (InputManager.Instance.GetButtonDown(Buttons.DROP) && hand.Count > 0)
        {
            GameController.Instance.Discard(hand.Pop());
        }

        UpdateHandUI();
    }

    private void OnCollisionEnter(Collision collision)
    {
        CardPickup cp = collision.gameObject.GetComponent<CardPickup>();
        if (cp != null)
        {
            if (hand.Count < 2)
            {
                hand.Push(cp.Draw());
            }
        }
    }

    void UpdateHandUI() {
        handUI.text = "";
        if (hand.Count > 0)
        {
            foreach (Card c in hand)
            {
                handUI.text += c.face.ToString() + " | " + c.suit.ToString() + "\n";
            }
        }
    }
}

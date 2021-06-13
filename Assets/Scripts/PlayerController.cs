using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMTKMMXXI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torque;
    [SerializeField] float accel;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddTorque(transform.up * torque * InputManager.Instance.GetSteering());
        rb.AddForce(transform.up * accel * (InputManager.Instance.GetButton(Buttons.GAS) ? 1 : 0));
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMTKMMXXI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float torque;
    [SerializeField] float accel;
    [SerializeField] float reverseAccel;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rb.AddTorque(transform.up * torque * InputManager.Instance.GetSteering());
        rb.AddForce(transform.forward * (InputManager.Instance.GetButton(Buttons.BRAKE) ? -reverseAccel : accel * (InputManager.Instance.GetButton(Buttons.GAS) ? 1 : 0)));
    }
}

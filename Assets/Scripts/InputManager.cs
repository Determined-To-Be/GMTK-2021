using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GMTKMMXXI;

namespace GMTKMMXXI
{
    public enum Buttons
    {
        GAS = 0x01,
        BRAKE = 0x02,
        HANDBRAKE = 0x04,
        USE = 0x08,
        SWAP = 0x10,
        DROP = 0x20,
        SUBMIT = 0x40,
        CANCEL = 0x80
    }
}

public class InputManager : MonoBehaviour
{
    InputFrame[] buffer = new InputFrame[2];

    // Singleton pattern
    static InputManager _Instance;
    public static InputManager Instance
    {
        get
        {
            if (_Instance != null)
            {
                return _Instance;
            }

            GameObject obj = new GameObject();
            _Instance = obj.AddComponent<InputManager>();
            return _Instance;
        }
    }

    void Awake()
    {
        // Singleton pattern
        if (_Instance == null)
        {
            _Instance = GetComponent<InputManager>();
        }
        else if (_Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        buffer[1] = buffer[0];
        buffer[0] = new InputFrame();
    }

    public float GetSteering()
    {
        return buffer[0].GetSteering();
    }

    public float GetSteeringDelta()
    {
        return buffer[1].GetSteering() - buffer[0].GetSteering();
    }

    public bool GetButton(int mask)
    {
        return buffer[0].GetButton(mask);
    }

    public bool GetButtonDown(int mask)
    {
        return buffer[0].GetButton(mask) && !buffer[1].GetButton(mask);
    }

    public bool GetButtonUp(int mask)
    {
        return !buffer[0].GetButton(mask) && buffer[1].GetButton(mask);
    }

    public bool GetButton(Buttons mask)
    {
        return GetButton((int)mask);
    }

    public bool GetButtonDown(Buttons mask)
    {
        return GetButtonDown((int)mask);
    }

    public bool GetButtonUp(Buttons mask)
    {
        return GetButtonUp((int)mask);
    }

    class InputFrame
    {
        float steering;
        int buttons;

        public InputFrame()
        {
            steering = Input.GetAxisRaw("Horizontal");

            buttons = 0x00;
            buttons = buttons | ToInt(Input.GetButton("Vertical"));
            buttons = buttons | ToInt(Input.GetButton("Brake")) << 1;
            buttons = buttons | ToInt(Input.GetButton("Handbrake")) << 2;
            buttons = buttons | ToInt(Input.GetButton("Use")) << 3;
            buttons = buttons | ToInt(Input.GetButton("Swap")) << 4;
            buttons = buttons | ToInt(Input.GetButton("Drop")) << 5;
            buttons = buttons | ToInt(Input.GetButton("Submit")) << 6;
            buttons = buttons | ToInt(Input.GetButton("Cancel")) << 7;
        }

        int ToInt(bool b)
        {
            return b ? 0x01 : 0x00;
        }

        public float GetSteering()
        {
            return steering;
        }

        public bool GetButton(int mask)
        {
            return (mask & buttons) != 0;
        }
    }
}

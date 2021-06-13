using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Button
{
    BRAKE = 0x01,
    HANDBRAKE = 0x02,
    USE = 0x04,
    SWAP = 0x08,
    DROP = 0x10,
    PAUSE = 0x20,
    HONK = 0x40
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

    float GetSteering()
    {
        return buffer[0].GetSteering();
    }

    float GetSteeringDelta()
    {
        return buffer[1].GetSteering() - buffer[0].GetSteering();
    }

    bool GetButton(byte mask)
    {
        return buffer[0].GetButton(mask);
    }

    bool GetButtonDown(byte mask)
    {
        return buffer[0].GetButton(mask) && !buffer[1].GetButton(mask);
    }

    bool GetButtonUp(byte mask)
    {
        return !buffer[0].GetButton(mask) && buffer[1].GetButton(mask);
    }

    class InputFrame
    {
        float steering;
        byte buttons;

        public InputFrame()
        {
            steering = Input.GetAxisRaw("Steer");

            buttons = 0x00;
            // TODO fix cast error on these
            buttons |= ToByte(Input.GetButton("Brake"));
            buttons |= ToByte(Input.GetButton("Handbrake")) << 1;
            buttons |= ToByte(Input.GetButton("Use")) << 2;
            buttons |= ToByte(Input.GetButton("Swap")) << 3;
            buttons |= ToByte(Input.GetButton("Drop")) << 4;
            buttons |= ToByte(Input.GetButton("Pause")) << 5;
            buttons |= ToByte(Input.GetButton("Honk")) << 6;
        }

        byte ToByte(bool b)
        {
            return b ? 0x01 : 0x00;
        }

        public float GetSteering()
        {
            return steering;
        }

        public bool GetButton(byte mask)
        {
            return (mask & buttons) != 0;
        }
    }
}

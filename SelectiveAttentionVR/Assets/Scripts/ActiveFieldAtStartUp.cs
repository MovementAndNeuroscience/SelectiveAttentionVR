
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ActiveFieldAtStartUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var inputfield = GetComponent<TMP_InputField>();
        inputfield.ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {
        var inputfield = GetComponent<TMP_InputField>();
        if (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Keypad0))
        {
            inputfield.text += "0";
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            inputfield.text += "1"; 
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            inputfield.text += "2";
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            inputfield.text += "3";
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {
            inputfield.text += "4";
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Keypad5))
        {
            inputfield.text += "5";
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Keypad6))
        {
            inputfield.text += "6";
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) || Input.GetKeyDown(KeyCode.Keypad7))
        {
            inputfield.text += "7";
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) || Input.GetKeyDown(KeyCode.Keypad8))
        {
            inputfield.text += "8";
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) || Input.GetKeyDown(KeyCode.Keypad9))
        {
            inputfield.text += "9";
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            inputfield.text += "a";
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            inputfield.text += "b";
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            inputfield.text += "c";
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            inputfield.text += "d";
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            inputfield.text += "e";
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            inputfield.text += "f";
        }
        if (Input.GetKeyDown(KeyCode.G))
        {
            inputfield.text += "g";
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            inputfield.text += "h";
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            inputfield.text += "i";
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            inputfield.text += "j";
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            inputfield.text += "k";
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            inputfield.text += "l";
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            inputfield.text += "m";
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            inputfield.text += "n";
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            inputfield.text += "o";
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            inputfield.text += "p";
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            inputfield.text += "q";
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            inputfield.text += "r";
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            inputfield.text += "s";
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            inputfield.text += "t";
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            inputfield.text += "u";
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            inputfield.text += "v";
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            inputfield.text += "w";
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            inputfield.text += "x";
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            inputfield.text += "y";
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            inputfield.text += "z";
        }
        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            var length = inputfield.text.Length;
            inputfield.text = inputfield.text.Remove(length - 1, 1); 
        }
    }
}

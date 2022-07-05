using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActiveFieldAtStartUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var inputfield = GetComponent<TMPro.TMP_InputField>();
        inputfield.ActivateInputField();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

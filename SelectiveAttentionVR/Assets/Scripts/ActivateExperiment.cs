using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateExperiment : MonoBehaviour
{

    public GameObject stimuliController; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            stimuliController.SetActive(true); 
            this.gameObject.SetActive(false);
        }
        
    }
}

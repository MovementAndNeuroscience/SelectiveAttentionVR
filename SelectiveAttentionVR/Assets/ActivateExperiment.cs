using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateExperiment : MonoBehaviour
{

    public GameObject stimuliController; 
    public float onScreenTime = 4.0f; 

    private float time = 0.0f; 

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.fixedDeltaTime; 
        if (time > onScreenTime)
        {
            stimuliController.SetActive(true); 
            this.gameObject.SetActive(false);
        }
        
    }
}

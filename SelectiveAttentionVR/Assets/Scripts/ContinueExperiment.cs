using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueExperiment : MonoBehaviour
{
    public GameObject AVstimuliController;
    public GameObject AstimuliController;

    private bool enableAVStim = true;
    private bool enableAStim = false; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && enableAVStim)
        {
            AVstimuliController.SetActive(true);
            this.gameObject.SetActive(false);
            enableAVStim = false;
            enableAStim = true; 
        }
        else if (Input.GetKeyDown(KeyCode.Return) && enableAStim)
        {
            AstimuliController.SetActive(true);
            this.gameObject.SetActive(false);
            enableAStim = false;
        }
    }
}

using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public GameObject audVisStim;

    public static int maxReactiontimes = 36; 

    private bool allReactionTimesFound = false;
    private bool enableAudVisStim = true;
    private bool startofBlock = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (startofBlock)
        {
            audVisStim.SetActive(true);
            startofBlock = false;   
        }

        else if (enableAudVisStim && audVisStim.GetComponent<AudioVisualStimuliController>().AllReactionTimesFound())
        {
            audVisStim.SetActive(false);
            enableAudVisStim = false;
        }

        if (audVisStim.GetComponent<AudioVisualStimuliController>().AllReactionTimesFound())
        {
            allReactionTimesFound = true; 
        }
    }
    public bool AllReactionTimesFound()
    { return allReactionTimesFound; }
    public List<string> GetAnswers()
    {
        var aVStimAnswers = audVisStim.GetComponent<AudioVisualStimuliController>().GetAnswers();
        List<string> answers = new List<string>();
        answers.AddRange(aVStimAnswers);

        return answers; 
    }
}

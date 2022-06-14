using UnityEngine;

public class StimuliController : MonoBehaviour
{
    private float timer = 0.0f;
    private float grandClock = 0.0f;
    private bool reactionTimeEnabled = true;
    private bool insertOnsetTime = true;
    private int reactionTimeCounter = 0;
    private bool allReactionTimesFound = false;
    private double[] distractorPos = new double[] { 0.0, 400.0 };

    private double[] filler_up_left_pos = new double[] { -300.0, 200.0 };
    private double[] filler_low_left_pos = new double[] { -300.0, -200.0 };
    private double[] filler_low_right_pos = new double[] { 300.0, -200.0 };
    private double[] filler_up_right_pos = new double[] { 300.0, 200,0 };
    private int allowedBNeutral = 2; 
    private int allowedBIncongruence = 2;
    private int allowedBCongruence = 2;
    private int allowedPNeutral = 2;
    private int allowedPIncongruence = 2;
    private int allowedPCongruence = 2; 
    private char targetletter = 'g';

    public GameObject p_target;
    public GameObject b_target;
    public GameObject p_distractor;
    public GameObject b_distractor;
    public GameObject g_distractor;
    public GameObject h_filler;
    public GameObject l_filler;
    public GameObject y_filler;
    public GameObject x_fixation;
    public GameObject faster;
    public GameObject happyFace;
    public GameObject sadFace;

    public static int maxReactiontimes = 12;
    public float[] reactionTimes = new float[maxReactiontimes];   
    public float[] stimuliOnsetTimes = new float[maxReactiontimes];
    public float[] stimuliOffsetTimes = new float[maxReactiontimes];
    public float[] stimuliTimes = new float[maxReactiontimes];
    public string[] presentedConditions = new string[maxReactiontimes];
    public string[] answers = new string[maxReactiontimes];

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (allReactionTimesFound)
        {
            timer = 6.5f; 
        }


        timer += Time.fixedDeltaTime;
        grandClock += Time.fixedDeltaTime;

        if (timer > 0.0f && timer < 1.0f)
        {
            x_fixation.SetActive(true); 
        }

        else if (timer > 1.0f && timer < 1.2f)
        {
            x_fixation.SetActive(false);
            // show some stimuli
           
            //gameObject.GetComponent<MeshRenderer>().material = fixationMaterial;
            //if( insertOnsetTime)
            //{
            //    stimuliOnsetTimes[reactionTimeCounter] = grandClock;
            //    insertOnsetTime = false; 
            //}

            //if (Input.GetMouseButtonDown(0) && reactionTimeEnabled)
            //{
            //    reactionTimes[reactionTimeCounter] = grandClock - stimuliOnsetTimes[reactionTimeCounter];
            //    reactionTimeCounter += 1;
            //    reactionTimeEnabled = false;
            //}

        }
        else if (timer > 1.2f && timer < 1.7f)
        {
            // Record input right or left click from mouse 
            
            
            //reactionTimeEnabled = true;
            //insertOnsetTime = true; 

            //gameObject.GetComponent<MeshRenderer>().material = pausematerial;
            //if (stimuliOffsetTimes[reactionTimeCounter -1] == 0 && stimuliTimes[reactionTimeCounter -1] == 0)
            //{
            //    stimuliOffsetTimes[reactionTimeCounter -1] = grandClock;
            //    stimuliTimes[reactionTimeCounter -1] = stimuliOffsetTimes[reactionTimeCounter -1] - stimuliOnsetTimes[reactionTimeCounter -1];
                
            //}
            //if(reactionTimeCounter == maxReactiontimes)
            //{
            //    allReactionTimesFound = true;
            //}

        }
        else if (timer > 1.7f && timer < 2.3f)
        {
            // provide feedback based on reaction 



        }
        else if (timer > 2.3f)
        {
            if(!allReactionTimesFound)
            timer = 0.00f;
        }

    }

    public bool AllReactionTimesFound()
    {
        return allReactionTimesFound; 
    }
    public float[] GetRTs()
    {
        return reactionTimes; 
    }
    public float[] GetOnSetTimes()
    {
        return stimuliOnsetTimes;
    }
    public float[] GetOffSetTimes()
    {
        return stimuliOffsetTimes;
    }
    public float[] GetStimuliScreenTimes()
    {
        return stimuliTimes;
    }
}

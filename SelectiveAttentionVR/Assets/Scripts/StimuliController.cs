using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StimuliController : MonoBehaviour
{
    private float timer = 0.0f;
    private float grandClock = 0.0f;
    private bool reactionTimeEnabled = true;
    private bool insertOnsetTime = true;
    private int stimuliCounter = 0;
    private bool allReactionTimesFound = false;
    private Vector2 distractorPos = new Vector2(0.0f, 400.0f);
    private Vector2 filler_up_left_pos = new Vector2(-300.0f, 200.0f);
    private Vector2 filler_low_left_pos = new Vector2(-300.0f, -200.0f);
    private Vector2 filler_low_right_pos = new Vector2(300.0f, -200.0f);
    private Vector2 filler_up_right_pos = new Vector2(300.0f, 200.0f);
    private int allowedBNeutral = 2;
    private int allowedBIncongruence = 2;
    private int allowedBCongruence = 2;
    private int allowedPNeutral = 2;
    private int allowedPIncongruence = 2;
    private int allowedPCongruence = 2;
    private string targetLetter = "g";
    private string distractorLetter = "g";

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
    public int[] answer_codes = new int[maxReactiontimes];

    private List<string> fillerposes = new List<string>();
    private List<string> fillers = new List<string>();
    private List<string> distractors = new List<string>();
    private List<string> targets = new List<string>();

    public struct Condition_Controller
        {
        public string target;
        public string distractor; 
        public int allowedBNeutral;
        public int allowedBIncongruence;
        public int allowedBCongruence;
        public int allowedPNeutral;
        public int allowedPIncongruence;
        public int allowedPCongruence;
        public string[] presentedConditions; 
        }

    // Start is called before the first frame update
    void Start()
    {
        fillers.Add("l");
        fillers.Add("h");
        fillers.Add("y");

        distractors.Add("p");
        distractors.Add("b");
        distractors.Add("g");

        targets.Add("b");
        targets.Add("p");
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
            fillerposes.Add("UpLeft");
            fillerposes.Add("DownLeft");
            fillerposes.Add("UpRight");
            fillerposes.Add("DownRight");

            x_fixation.SetActive(true); 
        }

        else if (timer > 1.0f && timer < 1.5f)
        {
            x_fixation.SetActive(false);

            var randpos = Random.Range(0, fillerposes.Count - 1);
            var targetpos = fillerposes[randpos];
            var targetPostiionVector = getPositionTarget(targetpos);
            fillerposes.Remove(targetpos);

            var condition_control = FindTargetAndDistractor(allowedPCongruence, allowedPIncongruence, allowedPNeutral, allowedBCongruence,
                allowedBIncongruence, allowedBNeutral, presentedConditions, targets, distractors, stimuliCounter);

            allowedPCongruence = condition_control.allowedPCongruence;
            allowedPIncongruence = condition_control.allowedPIncongruence;
            allowedPNeutral = condition_control.allowedPNeutral;
            allowedBCongruence = condition_control.allowedBCongruence;
            allowedBIncongruence = condition_control.allowedBIncongruence;
            allowedBNeutral = condition_control.allowedBNeutral;
            presentedConditions = condition_control.presentedConditions;
            targetLetter = condition_control.target;
            distractorLetter = condition_control.distractor;

            PositionningTargetAndDistractor(targetPostiionVector);
            PositioningFillers();

        }
        else if (timer > 1.5f && timer < 5.5f)
        {

            //reactionTimeEnabled = true;
            //insertOnsetTime = true; 

            //gameObject.GetComponent<MeshRenderer>().material = pausematerial;
            //if (stimuliOffsetTimes[reactionTimeCounter -1] == 0 && stimuliTimes[reactionTimeCounter -1] == 0)
            //{
            //    stimuliOffsetTimes[reactionTimeCounter -1] = gr&&Clock;
            //    stimuliTimes[reactionTimeCounter -1] = stimuliOffsetTimes[reactionTimeCounter -1] - stimuliOnsetTimes[reactionTimeCounter -1];
                
            //}
            //if(reactionTimeCounter == maxReactiontimes)
            //{
            //    allReactionTimesFound = true;
            //}

        }
        if(timer > 1.0f && timer < 5.5f)
        {
            // Record input right or left click from mouse 
        }

        else if (timer > 5.5f && timer < 6.0f)
        {
            // provide feedback based on reaction 



        }
        else if (timer > 6.0f)
        {
            stimuliCounter ++;
            if(maxReactiontimes == stimuliCounter)
            {
                allReactionTimesFound = true;  
            }

            if (!allReactionTimesFound)
            timer = 0.00f;
        }

    }

    private void PositioningFillers()
    {
        var randomPosition = new System.Random();
        fillerposes = fillerposes.OrderBy(a => randomPosition.Next()).ToList();

        var randomFiller = new System.Random();
        fillers = fillers.OrderBy(a => randomPosition.Next()).ToList();

        for (int i = 0; i <= 2; i++)
        {
            if (fillers[i] == "h" && fillerposes[i] == "UpLeft")
            {
                h_filler.transform.position = new Vector3(filler_up_left_pos.x, filler_up_left_pos.y);
            }
            else if (fillers[i] == "h" && fillerposes[i] == "DownLeft")
            {
                h_filler.transform.position = new Vector3(filler_low_left_pos.x, filler_low_left_pos.y);
            }
            else if (fillers[i] == "h" && fillerposes[i] == "UpRight")
            {
                h_filler.transform.position = new Vector3(filler_up_right_pos.x, filler_up_right_pos.y);
            }
            else if (fillers[i] == "h" && fillerposes[i] == "DownRight")
            {
                h_filler.transform.position = new Vector3(filler_low_right_pos.x, filler_low_right_pos.y);
            }
            else if (fillers[i] == "l" && fillerposes[i] == "UpLeft")
            {
                l_filler.transform.position = new Vector3(filler_up_left_pos.x, filler_up_left_pos.y);
            }
            else if (fillers[i] == "l" && fillerposes[i] == "DownLeft")
            {
                l_filler.transform.position = new Vector3(filler_low_left_pos.x, filler_low_left_pos.y);
            }
            else if (fillers[i] == "l" && fillerposes[i] == "UpRight")
            {
                l_filler.transform.position = new Vector3(filler_up_right_pos.x, filler_up_right_pos.y);
            }
            else if (fillers[i] == "l" && fillerposes[i] == "DownRight")
            {
                l_filler.transform.position = new Vector3(filler_low_right_pos.x, filler_low_right_pos.y);
            }
            else if (fillers[i] == "y" && fillerposes[i] == "UpLeft")
            {
                l_filler.transform.position = new Vector3(filler_up_left_pos.x, filler_up_left_pos.y);
            }
            else if (fillers[i] == "y" && fillerposes[i] == "DownLeft")
            {
                l_filler.transform.position = new Vector3(filler_low_left_pos.x, filler_low_left_pos.y);
            }
            else if (fillers[i] == "y" && fillerposes[i] == "UpRight")
            {
                l_filler.transform.position = new Vector3(filler_up_right_pos.x, filler_up_right_pos.y);
            }
            else if (fillers[i] == "y" && fillerposes[i] == "DownRight")
            {
                l_filler.transform.position = new Vector3(filler_low_right_pos.x, filler_low_right_pos.y);
            }
        }
    }

    private void PositionningTargetAndDistractor(Vector2 targetPostiionVector)
    {
        if (targetLetter == "p")
            p_target.transform.position = new Vector3(targetPostiionVector.x, targetPostiionVector.y);
        else if (targetLetter == "b")
            b_target.transform.position = new Vector3(targetPostiionVector.x, targetPostiionVector.y);

        if (distractorLetter == "p")
            p_distractor.transform.position = new Vector3(distractorPos.x, distractorPos.y);
        else if (distractorLetter == "b")
            b_distractor.transform.position = new Vector3(distractorPos.x, distractorPos.y);
        else if (distractorLetter == "g")
            g_distractor.transform.position = new Vector3(distractorPos.x, distractorPos.y);
    }

    private Vector2 getPositionTarget(string targetpos)
    {
        switch (targetpos)
        {
            case "UpLeft":
                return filler_up_left_pos;
            case "DownLeft":
                return filler_low_left_pos;
            case "UpRight":
                return filler_up_right_pos;
            case "DownRight":
                return filler_low_right_pos; 
            default: return new Vector2(0.0f,0.0f);
        }
    }
    private Condition_Controller FindTargetAndDistractor(int allowedPCongruence, int allowedPIncongruence, int allowedPNeutral, int allowedBCongruence, int allowedBIncongruence, int allowedBNeutral, string[] presentedConditions, List<string> targets, List<string> distractors, int stimuliTimeCounter)
    {
        var targetIndex = Random.Range(0, targets.Count - 1);
        var tar = targets[targetIndex];
        Condition_Controller tarAndDist = FindDistractor( tar, allowedPCongruence,  allowedPIncongruence, allowedPNeutral, allowedBCongruence, allowedBIncongruence, allowedBNeutral, presentedConditions, distractors, stimuliTimeCounter);
        return tarAndDist;
    }

    private Condition_Controller FindDistractor(string target, int allowedPCongruence, int allowedPIncongruence, int allowedPNeutral, int allowedBCongruence, int allowedBIncongruence, int allowedBNeutral, string[] presentedConditions, List<string> distractors, int stimuliTimeCounter)
        {
        var distractorIndex = Random.Range(0, distractors.Count - 1);
        var dist = distractors[distractorIndex];

        if (target == "p" && dist == "p" && allowedPCongruence > 0)
        {
            allowedPCongruence = allowedPCongruence - 1;
            presentedConditions[stimuliTimeCounter] = "Congruent";
            return new Condition_Controller
            {
                target = target,
                distractor = dist,
                allowedPCongruence = allowedPCongruence,
                allowedPIncongruence = allowedPIncongruence,
                allowedPNeutral = allowedPNeutral,
                allowedBCongruence = allowedBCongruence,
                allowedBIncongruence = allowedBIncongruence,
                allowedBNeutral = allowedBNeutral,
                presentedConditions = presentedConditions,
            };

        }

        else if (target == "p" && dist == "b" && allowedPIncongruence > 0)
        {
            allowedPIncongruence = allowedPIncongruence - 1;
            presentedConditions[stimuliTimeCounter] = "Incongruent";
            return new Condition_Controller
            {
                target = target,
                distractor = dist,
                allowedPCongruence = allowedPCongruence,
                allowedPIncongruence = allowedPIncongruence,
                allowedPNeutral = allowedPNeutral,
                allowedBCongruence = allowedBCongruence,
                allowedBIncongruence = allowedBIncongruence,
                allowedBNeutral = allowedBNeutral,
                presentedConditions = presentedConditions,
            };
        }
        else if (target == "p" && dist == "g" && allowedPNeutral > 0)
        {
            allowedPNeutral = allowedPNeutral - 1;
            presentedConditions[stimuliTimeCounter] = "Neutral";
            return new Condition_Controller
            {
                target = target,
                distractor = dist,
                allowedPCongruence = allowedPCongruence,
                allowedPIncongruence = allowedPIncongruence,
                allowedPNeutral = allowedPNeutral,
                allowedBCongruence = allowedBCongruence,
                allowedBIncongruence = allowedBIncongruence,
                allowedBNeutral = allowedBNeutral,
                presentedConditions = presentedConditions,
            };
        }
        else if (target == "b" && dist == "p" && allowedBIncongruence > 0)
        {

            allowedBIncongruence = allowedBIncongruence - 1;
            presentedConditions[stimuliTimeCounter] = "Incongruent";
            return new Condition_Controller
            {
                target = target,
                distractor = dist,
                allowedPCongruence = allowedPCongruence,
                allowedPIncongruence = allowedPIncongruence,
                allowedPNeutral = allowedPNeutral,
                allowedBCongruence = allowedBCongruence,
                allowedBIncongruence = allowedBIncongruence,
                allowedBNeutral = allowedBNeutral,
                presentedConditions = presentedConditions,
            };
        }
        else if (target == "b" && dist == "b" && allowedBCongruence > 0)
        {
            allowedBCongruence = allowedBCongruence - 1;
            presentedConditions[stimuliTimeCounter] = "Congruent";
            return new Condition_Controller
            {
                target = target,
                distractor = dist,
                allowedPCongruence = allowedPCongruence,
                allowedPIncongruence = allowedPIncongruence,
                allowedPNeutral = allowedPNeutral,
                allowedBCongruence = allowedBCongruence,
                allowedBIncongruence = allowedBIncongruence,
                allowedBNeutral = allowedBNeutral,
                presentedConditions = presentedConditions,
            };
        }
        else if (target == "b" && dist == "g" && allowedBNeutral > 0)
        {
            allowedBNeutral = allowedBNeutral - 1;
            presentedConditions[stimuliTimeCounter] = "Neutral";
            return new Condition_Controller
            {
                target = target,
                distractor = dist,
                allowedPCongruence = allowedPCongruence,
                allowedPIncongruence = allowedPIncongruence,
                allowedPNeutral = allowedPNeutral,
                allowedBCongruence = allowedBCongruence,
                allowedBIncongruence = allowedBIncongruence,
                allowedBNeutral = allowedBNeutral,
                presentedConditions = presentedConditions,
            };
        }
        else
        {
            var targetIndex = Random.Range(0, targets.Count - 1);
            var tar = targets[targetIndex];
            return FindDistractor(tar, allowedPCongruence, allowedPIncongruence, allowedPNeutral, allowedBCongruence, allowedBIncongruence, allowedBNeutral, presentedConditions, distractors, stimuliTimeCounter);
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

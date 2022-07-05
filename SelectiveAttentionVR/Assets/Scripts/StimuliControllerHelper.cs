using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StimuliControllerHelper : MonoBehaviour
{
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PositioningFillers(List<string> fillerposes, List<string> fillers, GameObject h_filler, GameObject y_filler, GameObject l_filler, Vector2 filler_up_left_pos, Vector2 filler_low_left_pos, Vector2 filler_up_right_pos, Vector2 filler_low_right_pos)
    {
        var randomPosition = new System.Random();
        fillerposes = fillerposes.OrderBy(a => randomPosition.Next()).ToList();

        var randomFiller = new System.Random();
        fillers = fillers.OrderBy(a => randomPosition.Next()).ToList();

        h_filler.SetActive(true);
        y_filler.SetActive(true);
        l_filler.SetActive(true);

        for (int i = 0; i <= 2; i++)
        {
            if (fillers[i] == "h" && fillerposes[i] == "UpLeft")
            {
                h_filler.GetComponent<RectTransform>().anchoredPosition = filler_up_left_pos;
            }
            else if (fillers[i] == "h" && fillerposes[i] == "DownLeft")
            {
                h_filler.GetComponent<RectTransform>().anchoredPosition = filler_low_left_pos;
            }
            else if (fillers[i] == "h" && fillerposes[i] == "UpRight")
            {
                h_filler.GetComponent<RectTransform>().anchoredPosition = filler_up_right_pos;
            }
            else if (fillers[i] == "h" && fillerposes[i] == "DownRight")
            {
                h_filler.GetComponent<RectTransform>().anchoredPosition = filler_low_right_pos;
            }
            else if (fillers[i] == "l" && fillerposes[i] == "UpLeft")
            {
                l_filler.GetComponent<RectTransform>().anchoredPosition = filler_up_left_pos;
            }
            else if (fillers[i] == "l" && fillerposes[i] == "DownLeft")
            {
                l_filler.GetComponent<RectTransform>().anchoredPosition = filler_low_left_pos;
            }
            else if (fillers[i] == "l" && fillerposes[i] == "UpRight")
            {
                l_filler.GetComponent<RectTransform>().anchoredPosition = filler_up_right_pos;
            }
            else if (fillers[i] == "l" && fillerposes[i] == "DownRight")
            {
                l_filler.GetComponent<RectTransform>().anchoredPosition = filler_low_right_pos;
            }
            else if (fillers[i] == "y" && fillerposes[i] == "UpLeft")
            {
                y_filler.GetComponent<RectTransform>().anchoredPosition = filler_up_left_pos;
            }
            else if (fillers[i] == "y" && fillerposes[i] == "DownLeft")
            {
                y_filler.GetComponent<RectTransform>().anchoredPosition = filler_low_left_pos;
            }
            else if (fillers[i] == "y" && fillerposes[i] == "UpRight")
            {
                y_filler.GetComponent<RectTransform>().anchoredPosition = filler_up_right_pos;
            }
            else if (fillers[i] == "y" && fillerposes[i] == "DownRight")
            {
                y_filler.GetComponent<RectTransform>().anchoredPosition = filler_low_right_pos;
            }
        }
    }

    public (float[], float[], bool) ShowBlankScreen(GameObject p_target, GameObject b_target, GameObject p_distractor, 
        GameObject b_distractor, GameObject g_distractor, GameObject h_filler, GameObject l_filler, GameObject y_filler, float[] stimuliOffsetTimes,
        float[] stimuliOnsetTimes, float[] stimuliTimes, int stimuliCounter, float grandClock)
    {
        p_target.SetActive(false);
        b_target.SetActive(false);
        p_distractor.SetActive(false);
        b_distractor.SetActive(false);
        g_distractor.SetActive(false);
        h_filler.SetActive(false);
        l_filler.SetActive(false);
        y_filler.SetActive(false);

        if (stimuliOffsetTimes[stimuliCounter] == 0 && stimuliTimes[stimuliCounter] == 0)
        {
            stimuliOffsetTimes[stimuliCounter] = grandClock;
            stimuliTimes[stimuliCounter] = stimuliOffsetTimes[stimuliCounter] - stimuliOnsetTimes[stimuliCounter];
        }
        return(stimuliOffsetTimes, stimuliTimes, false);
    }

    public (bool,bool, float[], string[], int[], bool) RecordReaction(string targetLetter, bool enableHappy, bool enableSad, float[] reactionTimes, float[] stimuliOnsetTimes, int stimuliCounter, float grandClock, string[] answers, int[] answer_codes, bool reactionTimeEnabled)
    {
        if (Input.GetMouseButtonDown(0) && targetLetter == "p")
        {
            enableHappy = true;
            reactionTimes[stimuliCounter] = grandClock - stimuliOnsetTimes[stimuliCounter];
            answers[stimuliCounter] = "Correct";
            answer_codes[stimuliCounter] = 1;
            reactionTimeEnabled = false;
            return (enableHappy,enableSad, reactionTimes, answers, answer_codes, reactionTimeEnabled); 
        }

        else if (Input.GetMouseButtonDown(0) && targetLetter == "b")
        {
            enableSad = true;
            reactionTimes[stimuliCounter] = grandClock - stimuliOnsetTimes[stimuliCounter];
            answers[stimuliCounter] = "Incorrect";
            answer_codes[stimuliCounter] = 2;
            reactionTimeEnabled = false;
            return (enableHappy, enableSad, reactionTimes, answers, answer_codes, reactionTimeEnabled);
        }
        else if (Input.GetMouseButtonDown(1) && targetLetter == "b")
        {
            enableHappy = true;
            reactionTimes[stimuliCounter] = grandClock - stimuliOnsetTimes[stimuliCounter];
            answers[stimuliCounter] = "Correct";
            answer_codes[stimuliCounter] = 1;
            reactionTimeEnabled = false;
            return (enableHappy, enableSad, reactionTimes, answers, answer_codes, reactionTimeEnabled);
        }

        else if (Input.GetMouseButtonDown(1) && targetLetter == "p")
        {
            enableSad = true;
            reactionTimes[stimuliCounter] = grandClock - stimuliOnsetTimes[stimuliCounter];
            answers[stimuliCounter] = "Incorrect";
            answer_codes[stimuliCounter] = 2;
            reactionTimeEnabled = false;
            return (enableHappy, enableSad, reactionTimes, answers, answer_codes, reactionTimeEnabled);
        }
        return (enableHappy, enableSad, reactionTimes, answers, answer_codes, reactionTimeEnabled);
    }

    public Condition_Controller FindTargetAndDistractor(int allowedPCongruence, int allowedPIncongruence, int allowedPNeutral, 
        int allowedBCongruence, int allowedBIncongruence, int allowedBNeutral, string[] presentedConditions, List<string> targets, List<string> distractors, int stimuliTimeCounter)
    {
        var targetIndex = Random.Range(0, targets.Count);
        var tar = targets[targetIndex];
        Condition_Controller tarAndDist = FindDistractor(tar, allowedPCongruence, allowedPIncongruence, allowedPNeutral, 
            allowedBCongruence, allowedBIncongruence, allowedBNeutral, presentedConditions, distractors, stimuliTimeCounter, targets);
        return tarAndDist;
    }

    private Condition_Controller FindDistractor(string target, int allowedPCongruence, int allowedPIncongruence, int allowedPNeutral, 
        int allowedBCongruence, int allowedBIncongruence, int allowedBNeutral, string[] presentedConditions, List<string> distractors, int stimuliTimeCounter, List<string> targets)
    {
        var distractorIndex = Random.Range(0, distractors.Count);
        var dist = distractors[distractorIndex];

        if (target == "p" && dist == "p" && allowedPCongruence > 0)
        {
            allowedPCongruence = allowedPCongruence - 1;
            presentedConditions[stimuliTimeCounter] = "Congruent";
            return CreateConditionController(target, allowedPCongruence, allowedPIncongruence, allowedPNeutral, allowedBCongruence, allowedBIncongruence, allowedBNeutral, presentedConditions, dist);

        }

        else if (target == "p" && dist == "b" && allowedPIncongruence > 0)
        {
            allowedPIncongruence = allowedPIncongruence - 1;
            presentedConditions[stimuliTimeCounter] = "Incongruent";
            return CreateConditionController(target, allowedPCongruence, allowedPIncongruence, allowedPNeutral, allowedBCongruence, allowedBIncongruence, allowedBNeutral, presentedConditions, dist);
        }
        else if (target == "p" && dist == "g" && allowedPNeutral > 0)
        {
            allowedPNeutral = allowedPNeutral - 1;
            presentedConditions[stimuliTimeCounter] = "Neutral";
            return CreateConditionController(target, allowedPCongruence, allowedPIncongruence, allowedPNeutral, allowedBCongruence, allowedBIncongruence, allowedBNeutral, presentedConditions, dist);
        }
        else if (target == "b" && dist == "p" && allowedBIncongruence > 0)
        {

            allowedBIncongruence = allowedBIncongruence - 1;
            presentedConditions[stimuliTimeCounter] = "Incongruent";
            return CreateConditionController(target, allowedPCongruence, allowedPIncongruence, allowedPNeutral, allowedBCongruence, allowedBIncongruence, allowedBNeutral, presentedConditions, dist);
        }
        else if (target == "b" && dist == "b" && allowedBCongruence > 0)
        {
            allowedBCongruence = allowedBCongruence - 1;
            presentedConditions[stimuliTimeCounter] = "Congruent";
            return CreateConditionController(target, allowedPCongruence, allowedPIncongruence, allowedPNeutral, allowedBCongruence, allowedBIncongruence, allowedBNeutral, presentedConditions, dist);
        }
        else if (target == "b" && dist == "g" && allowedBNeutral > 0)
        {
            allowedBNeutral = allowedBNeutral - 1;
            presentedConditions[stimuliTimeCounter] = "Neutral";
            return CreateConditionController(target, allowedPCongruence, allowedPIncongruence, allowedPNeutral, allowedBCongruence, allowedBIncongruence, allowedBNeutral, presentedConditions, dist);
        }
        else
        {
            var targetIndex = Random.Range(0, targets.Count);
            var tar = targets[targetIndex];
            return FindDistractor(tar, allowedPCongruence, allowedPIncongruence, allowedPNeutral, allowedBCongruence, 
                allowedBIncongruence, allowedBNeutral, presentedConditions, distractors, stimuliTimeCounter, targets);
        }
    }

    private static Condition_Controller CreateConditionController(string target, int allowedPCongruence, int allowedPIncongruence, int allowedPNeutral, int allowedBCongruence, int allowedBIncongruence, int allowedBNeutral, string[] presentedConditions, string dist)
    {
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
}

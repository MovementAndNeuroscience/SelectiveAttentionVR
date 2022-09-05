using System.Collections.Generic;
using UnityEngine;

public class AudioVisualStimuliController : MonoBehaviour
{
    private float timer = 0.0f;
    private float grandClock = 0.0f;
    private bool reactionTimeEnabled = true;
    private bool enableStimuli = true;
    private bool enableFeedback = true;
    private bool enableBlankScreen = true;
    private bool enableFixation = true;
    private int stimuliCounter = 0;
    private bool allReactionTimesFound = false;
    private Vector2 distractorPos = new Vector2(0.0f, 400.0f);
    private Vector2 filler_up_left_pos = new Vector2(-300.0f, 200.0f);
    private Vector2 filler_low_left_pos = new Vector2(-300.0f, -200.0f);
    private Vector2 filler_low_right_pos = new Vector2(300.0f, -200.0f);
    private Vector2 filler_up_right_pos = new Vector2(300.0f, 200.0f);
    private int allowedBNeutral = 6;
    private int allowedBIncongruence = 6;
    private int allowedBCongruence = 6;
    private int allowedPNeutral = 6;
    private int allowedPIncongruence = 6;
    private int allowedPCongruence = 6;
    private int visCong = 4;
    private int visIncong = 4;
    private int visNeu = 4;
    private int audVisCong = 4;
    private int audVisIncong = 4;
    private int audVisNeu = 4;
    private int audCong = 4;
    private int audIncong = 4;
    private int audNeu = 4;
    private string targetLetter = "g";
    private string distractorLetter = "g";

    private bool enableHappy = false;
    private bool enableSad = false;

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
    public GameObject canvas; 

    public GameObject p_audio;
    public GameObject b_audio;
    public GameObject g_audio; 

    public static int maxReactiontimes = 36;
    public float[] reactionTimes = new float[maxReactiontimes];
    public float[] fixationCrossOnsetTimes = new float[maxReactiontimes];
    public float[] fixationCrossOffsetTimes = new float[maxReactiontimes];
    public float[] stimuliOnsetTimes = new float[maxReactiontimes];
    public float[] stimuliOffsetTimes = new float[maxReactiontimes];
    public float[] blankScreenOnsetTimes = new float[maxReactiontimes];
    public float[] blankScreenOffsetTimes = new float[maxReactiontimes];
    public float[] feedbackOnsetTimes = new float[maxReactiontimes];
    public float[] feedbackOffsetTimes = new float[maxReactiontimes];
    public float[] stimuliTimes = new float[maxReactiontimes];
    public string[] presentedConditions = new string[maxReactiontimes];
    public string[] presentedDistractorModality = new string[maxReactiontimes];
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

        timer += Time.fixedDeltaTime;
        grandClock += Time.fixedDeltaTime;

        if (timer > 0.0f && timer < 1.0f && enableFixation)
        {
            fillerposes.Clear();
            fillerposes.Add("UpLeft");
            fillerposes.Add("DownLeft");
            fillerposes.Add("UpRight");
            fillerposes.Add("DownRight");

            x_fixation.SetActive(true);
            fixationCrossOnsetTimes[stimuliCounter] = grandClock;
            enableFixation = false;
        }

        else if (timer > 1.0f && timer < 1.2f && enableStimuli)
        {
            x_fixation.SetActive(false);
            fixationCrossOffsetTimes[stimuliCounter] = grandClock;

            var randpos = Random.Range(0, fillerposes.Count);
            var targetpos = fillerposes[randpos];
            var targetPostiionVector = getPositionTarget(targetpos);
            fillerposes.Remove(targetpos);

            var condition_control = GetComponent<StimuliControllerHelper>().FindTargetAndDistractor(allowedPCongruence, allowedPIncongruence, allowedPNeutral, allowedBCongruence,
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
            stimuliOnsetTimes[stimuliCounter] = grandClock;
            enableStimuli = false;

        }
        else if (timer > 1.2f && timer < 6.2f && enableBlankScreen)
        {
            ShowBlankScreen();
            blankScreenOnsetTimes[stimuliCounter] = grandClock;
        }

        if (timer > 1.0f && timer < 6.2f && reactionTimeEnabled)
        {
            RecordReaction();
        }

        else if (timer > 6.2f && timer < 6.7f && enableFeedback)
        {
            blankScreenOffsetTimes[stimuliCounter] = grandClock;
            ProvideFeedback();
            feedbackOnsetTimes[stimuliCounter] = grandClock;
        }
        else if (timer > 6.7f && !allReactionTimesFound)
        {
            faster.SetActive(false);
            happyFace.SetActive(false);
            sadFace.SetActive(false);
            feedbackOffsetTimes[stimuliCounter] = grandClock;

            stimuliCounter++;
            if (maxReactiontimes == stimuliCounter)
            {
                allReactionTimesFound = true;
            }

            if (!allReactionTimesFound)
            {
                timer = 0.00f;
                reactionTimeEnabled = true;
                enableFeedback = true;
                enableBlankScreen = true;
                enableStimuli = true;
                enableFixation = true;
                enableSad = false;
                enableHappy = false;
            }

            if (allReactionTimesFound)
            {
                timer = 7.5f;
            }
        }
    }

    private void ProvideFeedback()
    {
        canvas.SetActive(true); 
        if (!enableHappy && !enableSad)
        {
            answer_codes[stimuliCounter] = 0;
            answers[stimuliCounter] = "None";
            faster.SetActive(true);
        }
        else if (enableHappy)
        {
            happyFace.SetActive(true);
        }
        else if (enableSad)
        {
            sadFace.SetActive(true);
        }

        enableFeedback = false;
    }

    private void RecordReaction()
    {
        (enableHappy, enableSad, reactionTimes, answers, answer_codes, reactionTimeEnabled) =
            GetComponent<StimuliControllerHelper>().RecordReaction(targetLetter, enableHappy, enableSad, reactionTimes,
            stimuliOnsetTimes, stimuliCounter, grandClock, answers, answer_codes, reactionTimeEnabled);
    }

    private void ShowBlankScreen()
    {
        canvas.SetActive(false);
        (stimuliOffsetTimes, stimuliTimes, enableBlankScreen) = GetComponent<StimuliControllerHelper>().ShowBlankScreen(p_target, b_target, p_distractor,
        b_distractor, g_distractor, h_filler, l_filler, y_filler, stimuliOffsetTimes, stimuliOnsetTimes, stimuliTimes, stimuliCounter, grandClock);
        p_audio.SetActive(false);
        b_audio.SetActive(false);
        g_audio.SetActive(false);
    }

    private void PositioningFillers()
    {
        GetComponent<StimuliControllerHelper>().PositioningFillers(fillerposes, fillers, h_filler, y_filler, l_filler,
            filler_up_left_pos, filler_low_left_pos, filler_up_right_pos, filler_low_right_pos);
    }

    private void PositionningTargetAndDistractor(Vector2 targetPostiionVector)
    {
        if (targetLetter == "p")
        {
            p_target.SetActive(true);
            p_target.GetComponent<RectTransform>().anchoredPosition = targetPostiionVector;
        }
        else if (targetLetter == "b")
        {
            b_target.SetActive(true);
            b_target.GetComponent<RectTransform>().anchoredPosition = targetPostiionVector;
        }

        ChooseDistractorType();
    }
    private void ChooseDistractorType()
    {
        int distType = Random.Range(1, 4);
        switch (distType)
        {
            case 1:
                if (distractorLetter == "p" && targetLetter == "p" && visCong >= 1)
                {
                    p_distractor.SetActive(true);
                    p_distractor.GetComponent<RectTransform>().anchoredPosition = distractorPos;
                    visCong -= 1;
                    InsertModalityVisual();

                }
                else if (distractorLetter == "b" && targetLetter == "b" && visCong >= 1)
                {
                    b_distractor.SetActive(true);
                    b_distractor.GetComponent<RectTransform>().anchoredPosition = distractorPos;
                    visCong -= 1;
                    InsertModalityVisual();
                }
                else if (distractorLetter == "b" && targetLetter == "p" && visIncong >= 1)
                {
                    b_distractor.SetActive(true);
                    b_distractor.GetComponent<RectTransform>().anchoredPosition = distractorPos;
                    visIncong -= 1;
                    InsertModalityVisual();
                }
                else if (distractorLetter == "p" && targetLetter == "b" && visIncong >= 1)
                {
                    p_distractor.SetActive(true);
                    p_distractor.GetComponent<RectTransform>().anchoredPosition = distractorPos;
                    visIncong -= 1;
                    InsertModalityVisual();
                }
                else if (distractorLetter == "g" && visNeu >= 1)
                {
                    g_distractor.SetActive(true);
                    g_distractor.GetComponent<RectTransform>().anchoredPosition = distractorPos;
                    visNeu -= 1;
                    InsertModalityVisual();
                }
                else
                {
                    ChooseDistractorType();
                }
                break;

            case 2:
                if (distractorLetter == "p" && targetLetter == "p" && audVisCong >= 1)
                {
                    p_distractor.SetActive(true);
                    p_distractor.GetComponent<RectTransform>().anchoredPosition = distractorPos;
                    p_audio.SetActive(true);
                    p_audio.GetComponent<AudioSource>().Play();
                    audVisCong -= 1;
                    InsertModalityAudioVisual();
                }
                else if (distractorLetter == "b" && targetLetter == "b" && audVisCong >= 1)
                {
                    b_distractor.SetActive(true);
                    b_distractor.GetComponent<RectTransform>().anchoredPosition = distractorPos;
                    b_audio.SetActive(true);
                    b_audio.GetComponent<AudioSource>().Play();
                    audVisCong -= 1; 
                    InsertModalityAudioVisual();
                }
                else if (distractorLetter == "b" && targetLetter == "p" && audVisIncong >= 1)
                {
                    b_distractor.SetActive(true);
                    b_distractor.GetComponent<RectTransform>().anchoredPosition = distractorPos;
                    b_audio.SetActive(true);
                    b_audio.GetComponent<AudioSource>().Play();
                    audVisIncong -= 1;
                    InsertModalityAudioVisual();
                }
                else if (distractorLetter == "p" && targetLetter == "b" && audVisIncong >= 1)
                {
                    p_distractor.SetActive(true);
                    p_distractor.GetComponent<RectTransform>().anchoredPosition = distractorPos;
                    p_audio.SetActive(true);
                    p_audio.GetComponent<AudioSource>().Play();
                    audVisIncong -= 1;
                    InsertModalityAudioVisual();
                }
                else if (distractorLetter == "g" && audVisNeu >= 1)
                {
                    g_distractor.SetActive(true);
                    g_distractor.GetComponent<RectTransform>().anchoredPosition = distractorPos;
                    g_audio.SetActive(true);
                    g_audio.GetComponent<AudioSource>().Play();
                    audVisNeu -= 1;
                    InsertModalityAudioVisual();
                }
                else
                {
                    ChooseDistractorType();
                }
                break;

            case 3:
                if (distractorLetter == "p" && targetLetter == "p" && audCong >= 1)
                {
                    p_audio.SetActive(true);
                    p_audio.GetComponent<AudioSource>().Play();
                    audCong -= 1;
                    InsertModalityAudio();
                }
                else if (distractorLetter == "b" && targetLetter == "b" && audCong >= 1)
                {
                    b_audio.SetActive(true);
                    b_audio.GetComponent<AudioSource>().Play();
                    audCong -= 1;
                    InsertModalityAudio();
                }
                else if (distractorLetter == "b" && targetLetter == "p" && audIncong >= 1)
                {
                    b_audio.SetActive(true);
                    b_audio.GetComponent<AudioSource>().Play();
                    audIncong -= 1;
                    InsertModalityAudio();
                }
                else if (distractorLetter == "p" && targetLetter == "b" && audIncong >= 1)
                {
                    p_audio.SetActive(true);
                    p_audio.GetComponent<AudioSource>().Play();
                    audIncong -= 1;
                    InsertModalityAudio();
                }
                else if (distractorLetter == "g" && audNeu >= 1)
                {
                    g_audio.SetActive(true);
                    g_audio.GetComponent<AudioSource>().Play();
                    audNeu -= 1;
                    InsertModalityAudio();
                }
                else
                {
                    ChooseDistractorType();
                }
                break;

        }
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
            default: return new Vector2(0.0f, 0.0f);
        }
    }

    private void InsertModalityVisual()
    {
        presentedDistractorModality[stimuliCounter] = "visual"; 
    }
    private void InsertModalityAudio()
    {
        presentedDistractorModality[stimuliCounter] = "audio";
    }
    private void InsertModalityAudioVisual()
    {
        presentedDistractorModality[stimuliCounter] = "audio visual";
    }
    public bool AllReactionTimesFound()
    { return allReactionTimesFound; }
    public float[] GetRTs()
    { return reactionTimes; }
    public float[] GetOnSetTimes()
    { return stimuliOnsetTimes; }
    public float[] GetOffSetTimes()
    { return stimuliOffsetTimes; }
    public float[] GetFixationOnSetTimes()
    { return fixationCrossOnsetTimes; }
    public float[] GetFixationOffSetTimes()
    { return fixationCrossOffsetTimes; }
    public float[] GetBlankOnSetTimes()
    { return blankScreenOnsetTimes; }
    public float[] GetBlankOffSetTimes()
    { return blankScreenOffsetTimes; }
    public float[] GetFeedbackOnSetTimes()
    { return feedbackOnsetTimes; }
    public float[] GetFeedbackOffSetTimes()
    { return feedbackOffsetTimes; }
    public float[] GetStimuliScreenTimes()
    { return stimuliTimes; }
    public string[] GetPresentedConditions()
    { return presentedConditions; }
    public string[] GetPresentedModalities()
    { return presentedDistractorModality; }
    public string[] GetAnswers()
    { return answers; }
    public int[] GetAnswerCodes()
    { return answer_codes; }
}

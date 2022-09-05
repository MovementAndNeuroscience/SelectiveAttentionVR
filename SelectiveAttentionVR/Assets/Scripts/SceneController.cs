using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SceneController : MonoBehaviour
{
    public GameObject subjectIdTextField;
    public GameObject InsertSubIdText;
    public GameObject slowTrainingSession;
    public GameObject normalTrainingSession;
    public GameObject introtext;
    public GameObject vejledningstext;
    public GameObject pause; 
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
    public GameObject p_audio;
    public GameObject b_audio;
    public GameObject g_audio;
    public GameObject block1;
    public GameObject block2;
    public GameObject block3;
    public GameObject block4;


    public GameObject audVisStimBlock1;
    public GameObject audVisStimBlock2;
    public GameObject audVisStimBlock3;
    public GameObject audVisStimBlock4;


    public string subjectId;

    private bool enableIntroText = true;
    private bool enablePauseAfterSlowTraining = true;
    private bool enablePauseAfterNormTraining = true;
    private bool savefile = true;
    private bool enablePauseAfterBlock1 = true;
    private bool enablePauseAfterBlock2 = true;
    private bool enablePauseAfterBlock3 = true;
    private bool saveBlock1 = true;
    private bool saveBlock2 = true;
    private bool saveBlock3 = true;
    private bool saveBlock4 = true;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        block1.SetActive(false);
        block2.SetActive(false); 
        block3.SetActive(false); 
        block4.SetActive(false);
        audVisStimBlock1.SetActive(false);
        audVisStimBlock2.SetActive(false);
        audVisStimBlock3.SetActive(false);
        audVisStimBlock4.SetActive(false);
        slowTrainingSession.SetActive(false); 
        normalTrainingSession.SetActive(false); 
        vejledningstext.SetActive(false);
        subjectIdTextField.SetActive(true);
        InsertSubIdText.SetActive(true);
        introtext.SetActive(false);
        pause.SetActive(false); 
        p_target.SetActive(false);
        b_target.SetActive(false);
        p_distractor.SetActive(false);
        b_distractor.SetActive(false);
        g_distractor.SetActive(false);
        h_filler.SetActive(false);
        l_filler.SetActive(false);
        y_filler.SetActive(false);
        x_fixation.SetActive(false);   
        faster.SetActive(false);
        happyFace.SetActive(false);
        sadFace.SetActive(false);
        b_audio.SetActive(false);
        p_audio.SetActive(false);
        g_audio.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return) && enableIntroText)
        {
            var textField = subjectIdTextField.GetComponent<TMPro.TMP_InputField>();
            subjectId = textField.text;

            subjectIdTextField.SetActive(false);
            InsertSubIdText.SetActive(false);
            introtext.SetActive(true);
            enableIntroText = false; 
        }
        
        if (slowTrainingSession.GetComponent<SlowTrainingController>().AllReactionTimesFound() && enablePauseAfterSlowTraining)
        {
            pause.SetActive(true);
            enablePauseAfterSlowTraining = false;
            slowTrainingSession.SetActive(false);
        }

        if (normalTrainingSession.GetComponent<SlowTrainingController>().AllReactionTimesFound() && enablePauseAfterNormTraining)
        {
            pause.SetActive(true);
            enablePauseAfterNormTraining = false;
            normalTrainingSession.SetActive(false);
        }
        
        if(block1.GetComponent<BlockController>().AllReactionTimesFound() && enablePauseAfterBlock1)
        {
            pause.SetActive(true);
            enablePauseAfterBlock1 = false;
            var blockno = 1;

            if(saveBlock1)
            {
                SaveBlock(audVisStimBlock1, blockno);
                saveBlock1 = false; 
            }
            block1.SetActive(false);
        }

        if (block2.GetComponent<BlockController>().AllReactionTimesFound() && enablePauseAfterBlock2)
        {
            pause.SetActive(true);
            enablePauseAfterBlock2 = false;
            var blockno = 2;

            if (saveBlock2)
            {
                SaveBlock(audVisStimBlock2, blockno);
                saveBlock2 = false;
            }
            block2.SetActive(false);
        }
        if (block3.GetComponent<BlockController>().AllReactionTimesFound() && enablePauseAfterBlock3)
        {
            pause.SetActive(true);
            enablePauseAfterBlock3 = false;
            var blockno = 3;

            if (saveBlock3)
            {
                SaveBlock(audVisStimBlock3, blockno);
                saveBlock3 = false;
            }
            block3.SetActive(false);
        }
        

        if (block1.GetComponent<BlockController>().AllReactionTimesFound() && block2.GetComponent<BlockController>().AllReactionTimesFound() && block3.GetComponent<BlockController>().AllReactionTimesFound() && block4.GetComponent<BlockController>().AllReactionTimesFound())
        {
            pause.SetActive(true);
            var blockno = 4;
            if (saveBlock4)
            {
                SaveBlock(audVisStimBlock4, blockno);
                saveBlock4 = false;
            }
        }
    }
    public void SaveBlock(GameObject block, int blockNo)
    {
        var avb1_RTs = block.GetComponent<AudioVisualStimuliController>().GetRTs();
        var avb1_OnTimes = block.GetComponent<AudioVisualStimuliController>().GetOnSetTimes();
        var avb1_OffTimes = block.GetComponent<AudioVisualStimuliController>().GetOffSetTimes();
        var avb1_FixOnTimes = block.GetComponent<AudioVisualStimuliController>().GetFixationOnSetTimes();
        var avb1_FixOffTimes = block.GetComponent<AudioVisualStimuliController>().GetFixationOffSetTimes();
        var avb1_BlankOnTimes = block.GetComponent<AudioVisualStimuliController>().GetBlankOnSetTimes();
        var avb1_BlankOffTimes = block.GetComponent<AudioVisualStimuliController>().GetBlankOffSetTimes();
        var avb1_FeedOnTimes = block.GetComponent<AudioVisualStimuliController>().GetFeedbackOnSetTimes();
        var avb1_FeedOffTimes = block.GetComponent<AudioVisualStimuliController>().GetFeedbackOffSetTimes();
        var avb1_stimOnScreenTimes = block.GetComponent<AudioVisualStimuliController>().GetStimuliScreenTimes();
        var avb1_presentedCond = block.GetComponent<AudioVisualStimuliController>().GetPresentedConditions();
        var avb1_Answers = block.GetComponent<AudioVisualStimuliController>().GetAnswers();
        var avb1_AnswerCodes = block.GetComponent<AudioVisualStimuliController>().GetAnswerCodes();
        var avb1_presentedModalities = block.GetComponent<AudioVisualStimuliController>().GetPresentedModalities();

        GetComponent<FileSaver>().saveFile(subjectId, blockNo, 
                        avb1_RTs, avb1_OnTimes, avb1_OffTimes, avb1_FixOnTimes, avb1_FixOffTimes, avb1_BlankOnTimes, 
                        avb1_BlankOffTimes, avb1_FeedOnTimes, avb1_FeedOffTimes, avb1_stimOnScreenTimes, avb1_presentedCond, 
                        avb1_Answers, avb1_AnswerCodes, avb1_presentedModalities);

    }
}

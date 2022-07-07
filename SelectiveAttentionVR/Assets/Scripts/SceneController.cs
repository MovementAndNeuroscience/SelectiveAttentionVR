using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SceneController : MonoBehaviour
{
    public GameObject subjectIdTextField;
    public GameObject InsertSubIdText;
    public GameObject StimuliController;
    public GameObject audioStimuliController;
    public GameObject visualaudioStimuliController;
    public GameObject introtext;
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

    public string subjectId;

    private bool enableIntroText = true;
    private bool enableFirstPause = true;
    private bool enableSecondPause = true; 

    // Start is called before the first frame update
    void Start()
    {
        StimuliController.SetActive(false);
        audioStimuliController.SetActive(false);
        visualaudioStimuliController.SetActive(false);
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
        else if(StimuliController.GetComponent<StimuliController>().AllReactionTimesFound() && enableFirstPause)
        {
           pause.SetActive(true);
           enableFirstPause = false;
            StimuliController.SetActive(false);  
        }
        else if (visualaudioStimuliController.GetComponent<AudioVisualStimuliController>().AllReactionTimesFound() && enableSecondPause)
        {
            pause.SetActive(true);
            enableSecondPause = false;
            visualaudioStimuliController.SetActive(false);
        }

        if (StimuliController.GetComponent<StimuliController>().AllReactionTimesFound() && visualaudioStimuliController.GetComponent<AudioVisualStimuliController>().AllReactionTimesFound() && audioStimuliController.GetComponent<AudioStimuliController>().AllReactionTimesFound())
        {
            var visRTs = StimuliController.GetComponent<StimuliController>().GetRTs();
            var visOnsetTimes = StimuliController.GetComponent<StimuliController>().GetOnSetTimes();
            var visOffsetTimes = StimuliController.GetComponent<StimuliController>().GetOffSetTimes();
            var visStimuliOnScreenTimes = StimuliController.GetComponent<StimuliController>().GetStimuliScreenTimes();
            var visPresentedConditions = StimuliController.GetComponent<StimuliController>().GetPresentedConditions();
            var visAnswers = StimuliController.GetComponent<StimuliController>().GetAnswers();
            var visAnswerCodes = StimuliController.GetComponent<StimuliController>().GetAnswerCodes();

            var audVisRTs = visualaudioStimuliController.GetComponent<AudioVisualStimuliController>().GetRTs();
            var audVisOnsetTimes = visualaudioStimuliController.GetComponent<AudioVisualStimuliController>().GetOnSetTimes();
            var audVisOffsetTimes = visualaudioStimuliController.GetComponent<AudioVisualStimuliController>().GetOffSetTimes();
            var audVisStimuliOnScreenTimes = visualaudioStimuliController.GetComponent<AudioVisualStimuliController>().GetStimuliScreenTimes();
            var audVisPresentedConditions = visualaudioStimuliController.GetComponent<AudioVisualStimuliController>().GetPresentedConditions();
            var audVisAnswers = visualaudioStimuliController.GetComponent<AudioVisualStimuliController>().GetAnswers();
            var audVisAnswerCodes = visualaudioStimuliController.GetComponent<AudioVisualStimuliController>().GetAnswerCodes();

            var audRTs = audioStimuliController.GetComponent<AudioStimuliController>().GetRTs();
            var audOnsetTimes = audioStimuliController.GetComponent<AudioStimuliController>().GetOnSetTimes();
            var audOffsetTimes = audioStimuliController.GetComponent<AudioStimuliController>().GetOffSetTimes();
            var audStimuliOnScreenTimes = audioStimuliController.GetComponent<AudioStimuliController>().GetStimuliScreenTimes();
            var audPresentedConditions = audioStimuliController.GetComponent<AudioStimuliController>().GetPresentedConditions();
            var audAnswers = audioStimuliController.GetComponent<AudioStimuliController>().GetAnswers();
            var audanswerCodes = audioStimuliController.GetComponent<AudioStimuliController>().GetAnswerCodes();

            GetComponent<FileSaver>().saveFile(subjectId, visRTs, visOnsetTimes, visOffsetTimes, visStimuliOnScreenTimes, visPresentedConditions, visAnswers, visAnswerCodes,
                audVisRTs, audVisOnsetTimes, audVisOffsetTimes, audVisStimuliOnScreenTimes, audVisPresentedConditions, audVisAnswers, audVisAnswerCodes,
                audRTs, audOnsetTimes, audOffsetTimes, audStimuliOnScreenTimes, audPresentedConditions, audAnswers, audanswerCodes);

#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }


    }
}

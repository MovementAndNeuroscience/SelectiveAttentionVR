using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class SceneController : MonoBehaviour
{
    public GameObject subjectIdTextField;
    public GameObject InsertSubIdText;
    public GameObject StimuliController;
    public GameObject introtext;
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

    // Start is called before the first frame update
    void Start()
    {
        StimuliController.SetActive(false);
        subjectIdTextField.SetActive(true);
        InsertSubIdText.SetActive(true);
        introtext.SetActive(false);

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

        if (Input.GetKeyDown(KeyCode.Return))
        {
            var textField = subjectIdTextField.GetComponent<InputField>();
            subjectId = textField.text;

            subjectIdTextField.SetActive(false);
            InsertSubIdText.SetActive(false);
            introtext.SetActive(true);
        }

        if (StimuliController.GetComponent<StimuliController>().AllReactionTimesFound())
        {
            var rTs = StimuliController.GetComponent<StimuliController>().GetRTs();
            var onsetTimes = StimuliController.GetComponent<StimuliController>().GetOnSetTimes();
            var offsetTimes = StimuliController.GetComponent<StimuliController>().GetOffSetTimes();
            var stimuliOnScreenTimes = StimuliController.GetComponent<StimuliController>().GetStimuliScreenTimes();

            GetComponent<FileSaver>().saveFile(subjectId, rTs, onsetTimes, offsetTimes, stimuliOnScreenTimes);

            // Application.Quit() does not work in the editor so
            // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }


    }
}

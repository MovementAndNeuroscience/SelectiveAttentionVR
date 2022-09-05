using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;

public class ContinueExperiment : MonoBehaviour
{
    public GameObject slowTrainingSession; 
    public GameObject NormalTrainingSession; 

    public GameObject block1;
    public GameObject block2;
    public GameObject block3;
    public GameObject block4;

    private bool enableSlowTraining = true;
    private bool enableTraining = false;
    private bool enableBlock1 = false;
    private bool enableBlock2 = false;
    private bool enableBlock3 = false;
    private bool enableBlock4 = false;

    private TMP_Text textMP;

    private List<string> block1Answers;
    private List<string> block2Answers;
    private List<string> block3Answers;
    private List<string> block4Answers;

    // Start is called before the first frame update
    void Start()
    {
        textMP = GetComponent<TMP_Text>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
        if (slowTrainingSession.GetComponent<SlowTrainingController>().AllReactionTimesFound() && enableSlowTraining == true)
        {
            var answers = slowTrainingSession.GetComponent<SlowTrainingController>().GetAnswers();
            CalculateAndInsertAccuracySlowTraining(answers);
            enableTraining = true;

            if (Input.GetKeyDown(KeyCode.Return) && enableTraining)
            {
                NormalTrainingSession.SetActive(true);
                gameObject.SetActive(false);
                enableSlowTraining = false;
            }
        }

        else if (NormalTrainingSession.GetComponent<SlowTrainingController>().AllReactionTimesFound() && enableTraining == true)
        {
            var answers = NormalTrainingSession.GetComponent<SlowTrainingController>().GetAnswers();
            CalculateAndInsertAccuracyTraining(answers);
            enableBlock1 = true;

            if (Input.GetKeyDown(KeyCode.Return) && enableBlock1)
            {
                block1.SetActive(true);
                gameObject.SetActive(false);
                enableTraining = false;
            }
        }
        else if (block1.GetComponent<BlockController>().AllReactionTimesFound() && enableBlock1 == true)
        {
            block1Answers = block1.GetComponent<BlockController>().GetAnswers();
            CalculateAndInsertAccuracy(block1Answers);
            enableBlock2 = true;

            if (Input.GetKeyDown(KeyCode.Return) && enableBlock2)
            {
                block2.SetActive(true);
                gameObject.SetActive(false);
                enableBlock1 = false;
            }
        }

        else if (block2.GetComponent<BlockController>().AllReactionTimesFound() && enableBlock2 == true)
        {
            block2Answers = block2.GetComponent<BlockController>().GetAnswers();
            CalculateAndInsertAccuracy(block2Answers);
            enableBlock3 = true;

            if (Input.GetKeyDown(KeyCode.Return) && enableBlock3)
            {
                block3.SetActive(true);
                gameObject.SetActive(false);
                enableBlock2 = false;
            }
        }

        else if (block3.GetComponent<BlockController>().AllReactionTimesFound() && enableBlock3 == true)
        {
            block3Answers = block3.GetComponent<BlockController>().GetAnswers();
            CalculateAndInsertAccuracy(block3Answers);
            enableBlock4 = true;

            if (Input.GetKeyDown(KeyCode.Return) && enableBlock4)
            {
                block4.SetActive(true);
                gameObject.SetActive(false);
                enableBlock3 = false;
            }
        }

        else if (block4.GetComponent<BlockController>().AllReactionTimesFound() && enableBlock4 == true)
        {


            block4Answers = block4.GetComponent<BlockController>().GetAnswers();
            List<string> answers = new List<string>();
            answers.AddRange(block1Answers); 
            answers.AddRange(block2Answers);
            answers.AddRange(block3Answers);
            answers.AddRange(block4Answers);

            double totanswers = answers.Count;
            double correctAnswerCount = 0;
            foreach (var answer in answers)
            {
                if (answer == "Correct")
                {
                    correctAnswerCount++;
                }
            }
            double procentCorrect = (correctAnswerCount / totanswers) * 100;
            var roundedCorrect = ((int)procentCorrect);
            textMP.text = "Du har reddet julen! \nTillykke du fangede \n" + roundedCorrect.ToString() + "% af alle b'erne og p'erne \nJulemanden kan nu skrive navne på alle julegaverne \nTryk på enter for at afslutte";


            if (Input.GetKeyDown(KeyCode.Return))
            {
                gameObject.SetActive(false);
                enableBlock4 = false;
#if UNITY_EDITOR
                EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
            }
        }
    }

    private void CalculateAndInsertAccuracy(string[] answers)
    {
        double totanswers = answers.Length;
        double correctAnswerCount = 0;
        foreach (var answer in answers)
        {
            if (answer == "Correct")
            {
                correctAnswerCount++;
            }
        }
        double procentCorrect = (correctAnswerCount / totanswers) * 100;
        int roundedCorrect = (int)Mathf.Round((float)procentCorrect);  
        textMP.text = "Pause \nTillykke du fangede \n" + roundedCorrect.ToString() + "% af b'erne og p'erne \nTryk på enter for at fortsætte";
    }

    private void CalculateAndInsertAccuracySlowTraining(string[] answers)
    {
        double totanswers = answers.Length;
        double correctAnswerCount = 0;
        foreach (var answer in answers)
        {
            if (answer == "Correct")
            {
                correctAnswerCount++;
            }
        }
        double procentCorrect = (correctAnswerCount / totanswers) * 100;
        int roundedCorrect = (int)Mathf.Round((float)procentCorrect);
        textMP.text = "Pause \nTillykke du fangede \n" + roundedCorrect.ToString() + "% af b'erne og p'erne \nTryk på enter for at fortsætte træningen";
    }

    private void CalculateAndInsertAccuracyTraining(string[] answers)
    {
        double totanswers = answers.Length;
        double correctAnswerCount = 0;
        foreach (var answer in answers)
        {
            if (answer == "Correct")
            {
                correctAnswerCount++;
            }
        }
        double procentCorrect = (correctAnswerCount / totanswers) * 100;
        int roundedCorrect = (int)Mathf.Round((float)procentCorrect);
        textMP.text = "Pause \nTillykke du har gennemført træningen du fangede \n" + roundedCorrect.ToString() + "% af b'erne og p'erne \nTryk på enter for at fortsætte";
    }
    private void CalculateAndInsertAccuracy(List<string> answers)
    {
        double totanswers = answers.Count;
        double correctAnswerCount = 0;
        foreach (var answer in answers)
        {
            if (answer == "Correct")
            {
                correctAnswerCount++;
            }
        }
        double procentCorrect = (correctAnswerCount / totanswers) * 100;
        int roundedCorrect = (int)Mathf.Round((float)procentCorrect);
        textMP.text = "Pause \nTillykke du fangede \n" + roundedCorrect.ToString() + "% af b'erne og p'erne \nTryk på enter for at fortsætte";
    }
}

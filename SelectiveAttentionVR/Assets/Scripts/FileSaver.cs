using UnityEngine;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class FileSaver : MonoBehaviour
{

    private List<string[]> rowData = new List<string[]>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void saveFile(string subjectID,float[] visRTs, float[] visOnsetTimes, float[] visOffsetTimes, float[] visStimuliOnScreenTime, string[] visPresentedConditions, string[] visAnswers, int[] visAnswerCodes,
                float[] audVisRTs, float[] audVisOnsetTimes, float[] audVisOffsetTimes, float[] audVisStimuliOnScreenTimes, string[] audVisPresentedConditions, string[] audVisAnswers, int[] audVisAnswerCodes,
                float[] audRTs, float[] audOnsetTimes, float[] audOffsetTimes, float[] audStimuliOnScreenTimes, string[] audPresentedConditions, string[] audAnswers, int[] audanswerCodes)
    {
        // Creating First row of titles manually..
        string[] rowDataTemp = new string[21];
        rowDataTemp[0] = "Visual ReactionTime";
        rowDataTemp[1] = "Visual Stimuli Onset Time";
        rowDataTemp[2] = "Visual Stimuli Offset Time";
        rowDataTemp[3] = "Visual Stimuli On Screen Time";
        rowDataTemp[4] = "Visual Presented Condition";
        rowDataTemp[5] = "Visual Answers";
        rowDataTemp[6] = "Visual Answer Codes";
        rowDataTemp[7] = "Audio Visual ReactionTime";
        rowDataTemp[8] = "Audio Visual Stimuli Onset Time";
        rowDataTemp[9] = "Audio Visual Stimuli Offset Time";
        rowDataTemp[10] = "Audio Visual Stimuli On Screen Time";
        rowDataTemp[11] = "Audio Visual Presented Condition";
        rowDataTemp[12] = "Audio Visual Answers";
        rowDataTemp[13] = "Audio Visual Answer Codes";
        rowDataTemp[14] = "Audio ReactionTime";
        rowDataTemp[15] = "Audio Stimuli Onset Time";
        rowDataTemp[16] = "Audio Stimuli Offset Time";
        rowDataTemp[17] = "Audio Stimuli On Screen Time";
        rowDataTemp[18] = "Audio Presented Condition";
        rowDataTemp[19] = "Audio Answers";
        rowDataTemp[20] = "Audio Answer Codes";
        rowData.Add(rowDataTemp);

        // You can add up the values in as many cells as you want.
        for (int i = 0; i < visRTs.Length; i++)
        {
            rowDataTemp = new string[21];
            rowDataTemp[0] = visRTs[i].ToString();
            rowDataTemp[1] = visOnsetTimes[i].ToString();
            rowDataTemp[2] = visOffsetTimes[i].ToString();
            rowDataTemp[3] = visStimuliOnScreenTime[i].ToString();
            rowDataTemp[4] = visPresentedConditions[i].ToString();
            rowDataTemp[5] = visAnswers[i].ToString();
            rowDataTemp[6] = visAnswerCodes[i].ToString();
            rowDataTemp[7] = audVisRTs[i].ToString();
            rowDataTemp[8] = audVisOnsetTimes[i].ToString();
            rowDataTemp[9] = audVisOffsetTimes[i].ToString();
            rowDataTemp[10] = audVisStimuliOnScreenTimes[i].ToString();
            rowDataTemp[11] = audVisPresentedConditions[i].ToString();
            rowDataTemp[12] = audVisAnswers[i].ToString();
            rowDataTemp[13] = audVisAnswerCodes[i].ToString();
            rowDataTemp[14] = audRTs[i].ToString();
            rowDataTemp[15] = audOnsetTimes[i].ToString();
            rowDataTemp[16] = audOffsetTimes[i].ToString();
            rowDataTemp[17] = audStimuliOnScreenTimes[i].ToString();
            rowDataTemp[18] = audPresentedConditions[i].ToString();
            rowDataTemp[19] = audAnswers[i].ToString();
            rowDataTemp[20] = audanswerCodes[i].ToString();
            rowData.Add(rowDataTemp);
        }

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ";";

        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < length; index++)
            sb.AppendLine(string.Join(delimiter, output[index]));


        string filePath = getPath(subjectID);

        StreamWriter outStream = File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    // Following method is used to retrive the relative path as device platform
    private string getPath(string SubjectID)
    {
        var fileName = SubjectID + "_SelectAttention_Data.csv";
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + fileName;
#elif UNITY_ANDROID
        return Application.persistentDataPath+fileName;
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+fileName;
#else
        return Application.dataPath +"/"+fileName;
#endif
    }
}

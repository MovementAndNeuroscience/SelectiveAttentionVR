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
    public void saveFile(string subjectId, int blockNo, 
        float[] avb1_RTs, float[] avb1_OnTimes, float[] avb1_OffTimes, float[] avb1_FixOnTimes, float[] avb1_FixOffTimes, 
        float[] avb1_BlankOnTimes, float[] avb1_BlankOffTimes, float[] avb1_FeedOnTimes, float[] avb1_FeedOffTimes, 
        float[] avb1_stimOnScreenTimes, string[] avb1_presentedCond, string[] avb1_Answers, int[] avb1_AnswerCodes, string[] avb1_presentedModalities)
    {
        // Creating First row of titles manually..
        string[] rowDataTemp = new string[14];
        rowDataTemp[0] = "ReactionTime Block" + blockNo;
        rowDataTemp[1] = "Stimuli Onset Time Block" + blockNo;
        rowDataTemp[2] = "Stimuli Offset Time Block" + blockNo;
        rowDataTemp[3] = "Fixation Cross Onset Time Block" + blockNo;
        rowDataTemp[4] = "Fixation Cross Offset Time Block" + blockNo;
        rowDataTemp[5] = "Blank Screen Onset Time Block" + blockNo;
        rowDataTemp[6] = "Blank Screen Offset Time Block" + blockNo;
        rowDataTemp[7] = "Feedback Onset Time Block" + blockNo;
        rowDataTemp[8] = "Feedback Offset Time Block" + blockNo;
        rowDataTemp[9] = "Stimuli On Screen Time Block" + blockNo;
        rowDataTemp[10] = "Presented Condition Block" + blockNo;
        rowDataTemp[11] = "Answers Block" + blockNo;
        rowDataTemp[12] = "Answer Codes Block" + blockNo;
        rowDataTemp[13] = "Distractor Modality Block" + blockNo;
        rowData.Add(rowDataTemp);

        // You can add up the values in as many cells as you want.
        for (int i = 0; i < avb1_RTs.Length; i++)
        {
            rowDataTemp = new string[14];
            rowDataTemp[0] = avb1_RTs[i].ToString();
            rowDataTemp[1] = avb1_OnTimes[i].ToString();
            rowDataTemp[2] = avb1_OffTimes[i].ToString();
            rowDataTemp[3] = avb1_FixOnTimes[i].ToString();
            rowDataTemp[4] = avb1_FixOffTimes[i].ToString();
            rowDataTemp[5] = avb1_BlankOnTimes[i].ToString();
            rowDataTemp[6] = avb1_BlankOffTimes[i].ToString();
            rowDataTemp[7] = avb1_FeedOnTimes[i].ToString();
            rowDataTemp[8] = avb1_FeedOffTimes[i].ToString();
            rowDataTemp[9] = avb1_stimOnScreenTimes[i].ToString();
            rowDataTemp[10] = avb1_presentedCond[i].ToString();
            rowDataTemp[11] = avb1_Answers[i].ToString();
            rowDataTemp[12] = avb1_AnswerCodes[i].ToString();
            rowDataTemp[13] = avb1_presentedModalities[i].ToString();
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


        string filePath = getPath(subjectId);

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

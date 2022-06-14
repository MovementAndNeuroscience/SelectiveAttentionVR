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
   public void saveFile(string subjectID,float[] rTs, float[] onsetTimes, float[] offsetTimes, float[] stimuliOnScreenTime)
    {
        // Creating First row of titles manually..
        string[] rowDataTemp = new string[4];
        rowDataTemp[0] = "ReactionTime";
        rowDataTemp[1] = "Stimuli Onset Time";
        rowDataTemp[2] = "Stimuli Offset Time";
        rowDataTemp[3] = "Stimuli On Screen Time";
        rowData.Add(rowDataTemp);

        // You can add up the values in as many cells as you want.
        for (int i = 0; i < rTs.Length; i++)
        {
            rowDataTemp = new string[4];
            rowDataTemp[0] = rTs[i].ToString();
            rowDataTemp[1] = onsetTimes[i].ToString();
            rowDataTemp[2] = offsetTimes[i].ToString();
            rowDataTemp[3] = stimuliOnScreenTime[i].ToString();
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
        var fileName = SubjectID + "_RT_Data.csv";
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

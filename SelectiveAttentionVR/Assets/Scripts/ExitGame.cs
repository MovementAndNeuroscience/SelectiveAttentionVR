using UnityEditor;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    public GameObject stimControlBlock1;
    public GameObject stimControlBlock2;
    public GameObject stimControlBlock3;
    public GameObject stimControlBlock4;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(stimControlBlock1.activeSelf)
            {
                GetComponent<SceneController>().SaveBlock(stimControlBlock1, 1);
            }
            if (stimControlBlock2.activeSelf)
            {
                GetComponent<SceneController>().SaveBlock(stimControlBlock2, 2);
            }
            if (stimControlBlock3.activeSelf)
            {
                GetComponent<SceneController>().SaveBlock(stimControlBlock3, 3);
            }
            if (stimControlBlock4.activeSelf)
            {
                GetComponent<SceneController>().SaveBlock(stimControlBlock4, 4);
            }

#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}

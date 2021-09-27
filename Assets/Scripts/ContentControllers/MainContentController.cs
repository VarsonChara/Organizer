using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class MainContentController : MonoBehaviour
{

    [SerializeField]
    private ContentScriptable[] contentTypes;

    /// <summary>
    /// The target where content will be stored, first instance of this has VerticalGroup in it.
    /// </summary>
    [SerializeField]
    private Transform contentContainer;

    /// <summary>
    /// A list of contents for the later use.
    /// </summary>
    [SerializeField]
    private List<ContentSaveInfo> contents = new List<ContentSaveInfo>();

    
    [Header("Save File parameters")]
    [SerializeField]
    private string saveFileName = "SaveFile";

    private string saveFileCompletePath;


    [Space]

    [Header("Testing")]
    // TODO: REMOVE OR REPURPOSE
    public GameObject testContent;

    private void Awake()
    {
        if (contentContainer == null)
        {
            Debug.LogError("The contentContainer is empty!");
            return;
        }


        //Temporary gameobject and content for instantiating
        GameObject tmpGo;
        Content tmpContent;

        //TODO: Repurpose it to be loading from file, also create a function that create new content while playing the app
        //TODO: Change it to coroutine, so the app can load and stay responsive, regardless of the amount of content loaded
        //TODO: A loading bar or circle implementation would be nice

        saveFileCompletePath = saveFileName + ".json";

        if (!File.Exists(saveFileCompletePath))
        {
            File.Create(saveFileCompletePath);
        }

        


        // TODO: Remove completly
        for (int i = 0; i < 10; i++)
        {
            int generatedFlair = UnityEngine.Random.Range(0, contentTypes.Length - 1);

            ContentSaveInfo generatedTmpInfo = new ContentSaveInfo(i.ToString(), (FlairType)generatedFlair);

            tmpGo = Instantiate(testContent, contentContainer);
            tmpContent = tmpGo.GetComponent<Content>();
            contents.Add(generatedTmpInfo);


            tmpContent.contentScriptable = DetermineContent((FlairType)generatedFlair);
            tmpContent.SetContent(i.ToString());

        }
        
    }


    public ContentScriptable DetermineContent(FlairType flair)
    {

        foreach (var cType in contentTypes)
        {
            if (cType.flairType == flair) return cType;
        }

        Debug.LogError("Content not found! Critical error!");

        Debug.LogError((int)flair);

#if UNITY_EDITOR
        Debug.Break();
#else
        // TODO: Needs to implement feedback on app crash
        Application.Quit();
#endif
        return null;
    }


    private void OnApplicationQuit()
    {

        string save = JsonHelper.ToJson(contents.ToArray(), true);
        //string save = JsonUtility.ToJson(contents, true);

        Debug.Log(save);
        File.WriteAllText(saveFileCompletePath, save);
    }

}

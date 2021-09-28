using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class MainContentController : MonoBehaviour
{
    // A lazy but very optimal implementation of Singleton, modifying blocked and instead of doing stuff with every call, we do it only when instance is about to change
    // and we make sure everything is correct at that point to remove any additional operations when calling from outside
    public static MainContentController Instance
    {
        get
        {
            return _instance;
        }
    }

    private static MainContentController _instance;

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

    /// <summary>
    /// Prefab to create a new content
    /// </summary>
    [SerializeField]
    private GameObject contentTemplate;
    
    [Header("Save File parameters")]
    [SerializeField]
    private string saveFileName = "SaveFile";

    private string saveFileCompletePath;

    bool error = false;



    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(this);
            return;
        }
        else
        {
            DontDestroyOnLoad(this);
            _instance = this;
        }

        if (error = ErrorHandler()) return;

        //TODO: Repurpose it to be loading from file, also create a function that create new content while playing the app
        //TODO: Change it to coroutine, so the app can load and stay responsive, regardless of the amount of content loaded
        //TODO: A loading bar or circle implementation would be nice

        saveFileCompletePath = saveFileName + ".json";

        if (!File.Exists(saveFileCompletePath))
        {
            File.Create(saveFileCompletePath);
            File.WriteAllText(saveFileCompletePath, "{}");
        }
        else
        {
            StreamReader sr = File.OpenText(saveFileCompletePath);

            //Read from Json
            string jsonSave = sr.ReadToEnd();

            //Convert to array
            ContentSaveInfo[] tmpSaveInfo = JsonHelper.FromJson<ContentSaveInfo>(jsonSave);

            foreach (var csi in tmpSaveInfo)
            {
                CreateContent(csi);
            }

        }
    }

    #region CONTENT_MANIPULATION

    ContentScriptable DetermineContent(FlairType flair)
    {

        foreach (var cType in contentTypes)
        {
            if (cType.flairType == flair) return cType;
        }

        Debug.LogError("Content type not found! Critical error! Flair not found: " + flair);


#if UNITY_EDITOR
        Debug.Break();
#else
        // TODO: Needs to implement feedback on app crash
        Application.Quit();
#endif
        return null;
    }

    public void CreateContent(ContentSaveInfo csi)
    {
        contents.Add(csi);
        GameObject go = Instantiate(contentTemplate, contentContainer);
        Content tmpContent = go.GetComponent<Content>();

        tmpContent.contentScriptable = DetermineContent(csi.flair);
        tmpContent.SetContent(csi.description);
    }

    #endregion


    private void OnApplicationQuit()
    {
        //If error, do not save data!
        if (error) return;

        // As the app quits, save the state!
        string save = JsonHelper.ToJson(contents.ToArray(), true);
        File.WriteAllText(saveFileCompletePath, save);
    }

    /// <summary>
    /// Primitive error handler function
    /// </summary>
    bool ErrorHandler()
    {
        //Sanity checks, if sth happens they can be usefull
        if (contentContainer == null)
        {
            Debug.LogError("The contentContainer is empty!");
            return true;
        }

        if (contentTemplate == null)
        {
            Debug.LogError("The contentTemplate is empty!");
            return true;
        }


        return false;
    }

}

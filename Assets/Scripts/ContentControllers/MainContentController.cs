using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

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
    /// A save class, that holds all the data needed for save
    /// </summary>
    private SaveContainer saveContainer;

    private Dictionary<string, GameObject> spawnedContents = new Dictionary<string, GameObject>();

    /// <summary>
    /// Prefab to create a new content
    /// </summary>
    [SerializeField]
    private GameObject contentTemplate;

    [Header("Button Mode")]
    private MainPanelButtonToggleMode toggleMode = MainPanelButtonToggleMode.None;

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

        //TODO: Change it to coroutine, so the app can load and stay responsive, regardless of the amount of content loaded
        //TODO: A loading bar or circle implementation would be nice

        saveFileCompletePath = saveFileName + ".json";

        saveContainer = new SaveContainer();

        if (!File.Exists(saveFileCompletePath))
        {
            File.Create(saveFileCompletePath);
            File.WriteAllText(saveFileCompletePath, "{}");
        }
        else
        {
            StreamReader sr = File.OpenText(saveFileCompletePath);

            //Read from Json
            //TODO: If savefile grows too big, needs to chop that into smaller pieces so it wont be a massive string
            string jsonSave = sr.ReadToEnd();

            //Convert to array
            SaveContainer tmpSave = JsonUtility.FromJson<SaveContainer>(jsonSave);

            foreach (var csi in tmpSave.GetActiveContents())
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
        if (!saveContainer.AddContnet(csi)) return;
        GameObject go = Instantiate(contentTemplate, contentContainer);
        Content tmpContent = go.GetComponent<Content>();

        spawnedContents.Add(csi.description, go);
        tmpContent.contentScriptable = DetermineContent(csi.flair);
        tmpContent.SetContent(csi);
    }

    public void RemoveContent(ContentSaveInfo csi)
    {
        if (!saveContainer.RemoveContent(csi)) return;

        Destroy(spawnedContents[csi.description]);
    }

    public void CompleteContent(ContentSaveInfo csi)
    {
        if (!saveContainer.CompleteContent(csi)) return;

        Destroy(spawnedContents[csi.description]);
    }
    /// <summary>
    /// Function called on button click
    /// </summary>
    /// <param name="mode">Set app mode</param>
    public bool ChangeToggleButtons(MainPanelButtonToggleMode mode)
    {
        if (mode == toggleMode)
        {
            toggleMode = MainPanelButtonToggleMode.None;
            return false;
        }
        else
        {
            toggleMode = mode;
            return true;
        }
    }

    public void ModeAction(ContentSaveInfo csi)
    {
        switch (toggleMode)
        {
            case MainPanelButtonToggleMode.None:
                break;
            case MainPanelButtonToggleMode.Complete:
                CompleteContent(csi);
                break;
            case MainPanelButtonToggleMode.Delete:
                RemoveContent(csi);
                break;
        }
    }

    #endregion


    private void OnApplicationQuit()
    {
        //TODO: Not really best solution, might want to save every operation
        //If error, do not save data!
        if (error) return;

        // As the app quits, save the state!
        string save = JsonUtility.ToJson(saveContainer, true);
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

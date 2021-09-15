using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainContentController : MonoBehaviour
{
    /// <summary>
    /// The target where content will be stored, first instance of this has VerticalGroup in it.
    /// </summary>
    [SerializeField]
    private Transform contentContainer;

    /// <summary>
    /// A list of contents for the later use.
    /// </summary>
    private List<Content> contents = new List<Content>();

    [SerializeField]
    private ScrollRect scrollRect;

    // TODO: REMOVE OR REPURPOSE
    public ContentScriptable testScriptable;
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
        for (int i = 0; i < 10; i++)
        {
            tmpGo = Instantiate(testContent, contentContainer);
            tmpContent = tmpGo.GetComponent<Content>();
            contents.Add(tmpContent);

            tmpContent.contentScriptable = testScriptable;
            tmpContent.SetContent(i.ToString());

        }
        
    }
}

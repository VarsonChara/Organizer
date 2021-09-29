using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveContainer
{
    //To not do: I wish i could make them properties >.<

    /// <summary>
    /// List of active, non finished contents
    /// </summary>
    [SerializeField]
    private List<ContentSaveInfo> activeContents = new List<ContentSaveInfo>();

    /// <summary>
    /// List of completed contents, stored only for now
    /// </summary>
    [SerializeField]
    private List<ContentSaveInfo> completedContents = new List<ContentSaveInfo>();



    public bool CompleteContent(ContentSaveInfo csi)
    {
        if (!activeContents.Contains(csi))
        {
            //TODO: Another Error popup here >.>
            Debug.LogError("No matching content found!");
            return false;
        }
        else
        {
            activeContents.Remove(csi);
            completedContents.Add(csi);
            return true;
        }
    }

    public bool AddContnet(ContentSaveInfo csi)
    {
        if (activeContents.Contains(csi))
        {
            //TODO: Another Error popup here >.>
            Debug.LogError("Item Already exists!");
            return false;
        }
        else
        {
            activeContents.Add(csi);
            return true;
        }

    }

    public bool RemoveContent(ContentSaveInfo csi)
    {
        if (!activeContents.Contains(csi))
        {
            //TODO: Another Error popup here >.>
            Debug.LogError("No matching content found!");
            return false;
        }
        else
        {
            activeContents.Remove(csi);
            return true;
        }
    }

    public ContentSaveInfo[] GetActiveContents()
    {
        return ListHandler<ContentSaveInfo>.CopyList(activeContents);
    }
}



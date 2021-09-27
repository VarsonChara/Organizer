using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ContentSaveInfo
{
    public ContentSaveInfo(string desc, FlairType givenFlair)
    {
        description = desc;
        flair = givenFlair;
    }

    public string description;
    public FlairType flair;
}

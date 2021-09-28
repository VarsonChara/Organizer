using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum FlairType
{
    Game,
    VisualNovel,
    Book,
    Anime,
    Learning,
    Work,
    ToDo,
    Misc
}

public static class Enums
{
    //Fast Get enum strings
    //TODO: Add spaces before capitals
    public static List<string> GetEnumNames(object _enum)
    {
        List<string> flairNames = new List<string>();

        int enumLen = Enum.GetValues(_enum.GetType()).Length;

        for (int i = 0; i < enumLen; i++)
        {
            string currentName = ((FlairType)i).ToString();
            flairNames.Add(currentName);
        }

        return flairNames;
    }
}

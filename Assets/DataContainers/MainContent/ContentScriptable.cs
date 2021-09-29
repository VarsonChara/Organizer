using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataContainer", menuName = "Scriptable Objects/Content Scriptable", order = 1)]
public class ContentScriptable : ScriptableObject
{
    [Header("Flair parameters")]
    public FlairType flairType = FlairType.Misc;

    public Color flairColor;
    //Hide flairName
    private string flairName = "";
    //Allow only read
    public string FlairName
    {
        get
        {
            if (flairName.Equals(""))
            {
                DefineFlairName();
            }
            return flairName;
        }
    }

    [Header("Description parameters")]
    public Color descriptionColor;
    public string description;

    [Header("Separator parameters")]
    public Color separatorColor;


    private void DefineFlairName()
    {
        flairName = "UNDEFINED";
        //TODO: Change this stuff to the Enums.GetNames() so it can be scalable, when it gets capital letter recognition
        switch (flairType)
        {
            case FlairType.Anime:
                flairName = "Anime";
                break;
            case FlairType.Book:
                flairName = "Book";
                break;
            case FlairType.Game:
                flairName = "Game";
                break;
            case FlairType.Learning:
                flairName = "Learning";
                break;
            case FlairType.Misc:
                flairName = "Misc";
                break;
            case FlairType.ToDo:
                flairName = "To do!";
                break;
            case FlairType.VisualNovel:
                flairName = "Visual Novel";
                break;
            case FlairType.Work:
                flairName = "Work";
                break;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Content : MonoBehaviour
{
    [HideInInspector]
    public ContentScriptable contentScriptable;

    //Flair
    [SerializeField]
    Image flairBackground;

    [SerializeField]
    TMP_Text flairText;

    //Separator
    [SerializeField]
    Image separator;

    //Decription
    [SerializeField]
    Image descriptionBackground;

    [SerializeField]
    TMP_Text descriptionText;

    public void SetContent(string description)
    {
        if (contentScriptable == null)
        {
            Debug.LogError("Empty ContentScriptable!");
            return;
        }
        //Flair
        flairBackground.color = contentScriptable.flairColor;
        flairText.text = contentScriptable.FlairName;

        //Separator
        separator.color = contentScriptable.separatorColor;

        //Description
        descriptionBackground.color = contentScriptable.descriptionColor;
        descriptionText.text = description;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Content : MonoBehaviour
{
    ContentSaveInfo selfCsi;

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

    public void SetContent(ContentSaveInfo csi)
    {
        if (contentScriptable == null)
        {
            Debug.LogError("Empty ContentScriptable!");
            return;
        }

        selfCsi = csi;

        //Flair
        flairBackground.color = contentScriptable.flairColor;
        flairText.text = contentScriptable.FlairName;

        //Separator
        separator.color = contentScriptable.separatorColor;

        //Description
        descriptionBackground.color = contentScriptable.descriptionColor;
        descriptionText.text = csi.description;
    }

    public void OnClick()
    {
        MainContentController.Instance.ModeAction(selfCsi);
    }
}

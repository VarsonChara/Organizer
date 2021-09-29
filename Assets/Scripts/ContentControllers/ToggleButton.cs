using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleButton : MonoBehaviour
{
    static List<ToggleButton> buttons = new List<ToggleButton>();

    /// <summary>
    /// Active color, needs to be set as default, or given a color in code here! >.> >.<
    /// </summary>
    public Color activeColor;

    /// <summary>
    /// Standard button color, automatic
    /// </summary>
    Color standardColor;

    /// <summary>
    /// Button image, needed to manipulate color
    /// </summary>
    Image image;

    public MainPanelButtonToggleMode mode = MainPanelButtonToggleMode.None;

    private void Start()
    {
        activeColor = new Color(106f/255f, 176f/255f, 76f/255f);

        //Get image reference
        image = GetComponent<Button>().image;

        // Get its standard color on start
        standardColor = image.color;

        if (mode == MainPanelButtonToggleMode.None)
        {
            //TODO: Maybe another error message? >.<
            Debug.LogError("Mode not set on button!");
        }
        else
        {
            buttons.Add(this);
        }
    }

    public void ButtonClick()
    {
        foreach (var b in buttons)
        {
            b.ClearColor();
        }
        if (MainContentController.Instance.ChangeToggleButtons(mode))
        {
            image.color = activeColor;
        }

    }

    void ClearColor()
    {
        image.color = standardColor;
    }

    private void OnDestroy()
    {
        buttons.Remove(this);
    }

    public static void ResetToggleMode()
    {
        MainContentController.Instance.ChangeToggleButtons(MainPanelButtonToggleMode.None);

        foreach (var b in buttons)
        {
            b.ClearColor();
        }
    }

}

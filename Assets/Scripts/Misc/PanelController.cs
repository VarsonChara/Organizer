using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelController : MonoBehaviour
{

    //Making sure only the correct panel is turned on, if someone (me) forgets to turn sth off
    void Start()
    {
        for (int i = 1; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        AddPanelDropdown.ClearOptions();

        List<string> optionList = new List<string>();

        FlairType flair = FlairType.ToDo;
        optionList = Enums.GetEnumNames(flair);

        AddPanelDropdown.AddOptions(optionList);
    }


    #region BUTTON_FUNCTIONS

    #region ADD_PANEL_ACTIONS
    public GameObject AddPanel
    {
        get
        {
            if (!addPanel)
            {
                //TODO: Instead of logging error, make a popup so even the user sees thats an error
                Debug.LogError("AddPanel no instance!");
                return null;
            }
            else
            {
                return addPanel;
            }
        }
        set
        {
            addPanel = value;
        }
    }

    public TMPro.TMP_InputField AddPanelInputField
    {
        get
        {
            if (!addPanelInputField)
            {
                //TODO: Instead of logging error, make a popup so even the user sees thats an error
                Debug.LogError("AddPanelDropdown no instance!");
                return null;
            }
            else
            {
                return addPanelInputField;
            }
        }
        set
        {
            addPanelInputField = value;
        }
    }

    public TMPro.TMP_Dropdown AddPanelDropdown
    {
        get
        {
            if (!addPanelDropdown)
            {
                //TODO: Instead of logging error, make a popup so even the user sees thats an error
                Debug.LogError("AddPanelDropdown no instance!");
                return null;
            }
            else
            {
                return addPanelDropdown;
            }
        }
        set
        {
            addPanelDropdown = value;
        }
    }

    [SerializeField]
    [Tooltip("AddPanel gameobject that will be activated and deactivated on button clicks")]
    [Header("AddPanel objects")]
    private GameObject addPanel;

    [SerializeField]
    private TMPro.TMP_InputField addPanelInputField;

    [SerializeField]
    private TMPro.TMP_Dropdown addPanelDropdown;

    public void AddButton()
    {
        AddPanel.SetActive(true);
    }

    public void CancelButton()
    {
        AddPanel.SetActive(false);
        AddPanelInputField.text = "";
    }

    public void SubmitButton()
    {
        if (AddPanelInputField.text.Equals(""))
        {
            //TODO, error message!
            return;
        }

        ContentSaveInfo content = new ContentSaveInfo(AddPanelInputField.text, (FlairType)AddPanelDropdown.value);
        MainContentController.Instance.CreateContent(content);

        AddPanel.SetActive(false);
        AddPanelInputField.text = "";
    }


    #endregion

    #endregion



}

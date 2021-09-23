using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ScrollRect))]
public class ScrollRectTop : MonoBehaviour
{
    void Start()
    {
        //Needs to wait a frame
        StartCoroutine(WaitAFrame());
    }

    IEnumerator WaitAFrame()
    {
        yield return null;

        //Get the rectTransform
        ScrollRect sr = GetComponent<ScrollRect>();
        RectTransform rt = sr.content;

        //Set the Y to the top
        Vector3 startPos = rt.anchoredPosition;
 
        //Divide by -2, but faster
        startPos.y = rt.sizeDelta.y * -.5f;
        rt.anchoredPosition = startPos;
    }
}

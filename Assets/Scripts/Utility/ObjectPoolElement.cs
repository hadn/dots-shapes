using UnityEngine;
using System.Collections;

public class ObjectPoolElement : MonoBehaviour
{
    [HideInInspector]
    public bool beingUsed = false;

    public void activate()
    {
        gameObject.SetActive(true);
        beingUsed = true;
    }
    public void deactivate()
    {
        gameObject.SetActive(false);
        beingUsed = false;
    }
    public void copyTransform(GameObject copyThis)
    {
        RectTransform rectT = GetComponent<RectTransform>();
        if (rectT != null)
        {
            RectTransform copyTransform = copyThis.GetComponent<RectTransform>();
            rectT.anchoredPosition = copyTransform.anchoredPosition;
            rectT.localRotation = copyTransform.localRotation;
            rectT.localScale = copyTransform.localScale;
        }
        else
        {
            transform.position = copyThis.transform.position;
            transform.localRotation = copyThis.transform.localRotation;
            transform.localScale = copyThis.transform.localScale;
        }
    }
}
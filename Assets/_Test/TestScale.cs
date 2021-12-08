using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestScale : MonoBehaviour
{
    float targetScale = 2f;

    GameObject cardObject;
    

    // Start is called before the first frame update
    void Start()
    {
        cardObject = transform.GetChild(0).gameObject;
        //Debug.Log("RectTransform:")        
        ResetSize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ResetSize()
    {
        // Get the card rect
        RectTransform cardRect = cardObject.transform as RectTransform;
        // Get the card image
        Image cardImage = cardObject.GetComponent<Image>();
        // Reset cardImage scale
        cardImage.SetNativeSize();
        float ratio = cardRect.sizeDelta.x / cardRect.sizeDelta.y;
        // Rescale accorging to the parent transform height
        //cardRect.rect.Set(cardRect.rect.x, cardRect.rect.y, cardRect.rect.width, (transform as RectTransform).rect.height);
        cardRect.sizeDelta = new Vector2((transform as RectTransform).rect.width, (transform as RectTransform).rect.width / ratio);
        
        
    }
}

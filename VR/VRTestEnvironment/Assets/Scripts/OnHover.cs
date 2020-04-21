using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class OnHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image image;
    //private Button button;
    // Start is called before the first frame update
    void Start()
    {
        //button = GetComponent<Button>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverEnter();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverExit();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick();
    }

    void OnHoverEnter()
    {
        //button.image.color = Color.gray;
        image.color = Color.gray;
    }

    void OnHoverExit()
    {
        //button.image.color = Color.white;
        image.color = Color.white;
    }

    void OnClick()
    {
        //button.image.color = Color.blue;
        image.color = Color.blue;
    }
}

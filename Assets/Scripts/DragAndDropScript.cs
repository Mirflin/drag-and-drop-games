using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndDropScript : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler,IPointerDownHandler
{
    private CanvasGroup canvasGro;
    private RectTransform rectTra;
    public ObjectScript objectScr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canvasGro = GetComponent<CanvasGroup>();   
        rectTra = GetComponent<RectTransform>();

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if((Input.GetMouseButton(0) && !Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(2))) 
        {
            Debug.Log("OnPointerDown");
            objectScr.effects.PlayOneShot(objectScr.audioCli[0]);
        }
        
    }
    public void OnBeginDrag(PointerEventData eventData)
    {

    }
    public void OnDrag(PointerEventData eventData)
    {

    }
    public void OnEndDrag(PointerEventData eventData)
    {

    }
            
    // Update is called once per frame - 2 frames - 3 frames - 4 frames - 5 frames - 6 frames
    void Update()
    {
        
    }
}

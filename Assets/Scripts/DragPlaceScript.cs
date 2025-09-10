using UnityEngine;
using UnityEngine.EventSystems;

public class DragPlaceScript : MonoBehaviour, IDropHandler
{
    private float placeZRot, vehicleZRot, rotDiff;
    private Vector3 placeSiz, vehicleSiz;
    private float xSizeDiff, ySizeDiff;
    public ObjectScript objScript;

    public void OnDrop(PointerEventData eventData)
    {
        if((eventData.pointerDrag != null) && Input.GetMouseButtonUp(0) && !Input.GetMouseButton(1) && !Input.GetMouseButton(2))
        {
            if (eventData.pointerDrag.tag.Equals(tag))
            {
                placeZRot = eventData.pointerDrag.GetComponent<RectTransform>().transform.eulerAngles.z;
                vehicleZRot = GetComponent<RectTransform>().transform.eulerAngles.z;

                rotDiff = Mathf.Abs(placeZRot - vehicleZRot);

                Debug.Log(rotDiff);

                placeSiz = eventData.pointerDrag.GetComponent<RectTransform>().localScale;

                vehicleSiz = GetComponent<RectTransform>().localScale;

                xSizeDiff = Mathf.Abs(placeSiz.x - vehicleSiz.x);
                ySizeDiff = Mathf.Abs(placeSiz.y - vehicleSiz.y);
                Debug.Log(xSizeDiff);
                Debug.Log(ySizeDiff);

                if((rotDiff <= 5) || (rotDiff >= 355 && rotDiff <= 360) && (xSizeDiff <= 0.05 && ySizeDiff <= 0.05)){
                    Debug.Log("Correct place");
                }
            }
            else
            {
                objScript.rightPlace = false;
                objScript.effects.PlayOneShot(objScript.audioCli[1]);

                switch (eventData.pointerDrag.tag)
                {
                    case "Garbage":
                        objScript.vehicles[0].GetComponent<RectTransform>().localPosition = objScript.startCoordinates[0];
                        break;
                    case "Medicine":
                        objScript.vehicles[1].GetComponent<RectTransform>().localPosition = objScript.startCoordinates[0];
                        break;
                    case "Fire":
                        objScript.vehicles[2].GetComponent<RectTransform>().localPosition = objScript.startCoordinates[0];
                        break;
                    case "Police":
                        objScript.vehicles[9].GetComponent<RectTransform>().localPosition = objScript.startCoordinates[0];
                        break;
                    case "Excavator":
                        objScript.vehicles[8].GetComponent<RectTransform>().localPosition = objScript.startCoordinates[0];
                        break;
                    case "Cement":
                        objScript.vehicles[5].GetComponent<RectTransform>().localPosition = objScript.startCoordinates[0];
                        break;
                    case "School":
                        objScript.vehicles[3].GetComponent<RectTransform>().localPosition = objScript.startCoordinates[0];
                        break;
                    case "B2":
                        objScript.vehicles[4].GetComponent<RectTransform>().localPosition = objScript.startCoordinates[0];
                        break;
                    case "e46":
                        objScript.vehicles[6].GetComponent<RectTransform>().localPosition = objScript.startCoordinates[0];
                        break;
                    case "e61":
                        objScript.vehicles[7].GetComponent<RectTransform>().localPosition = objScript.startCoordinates[0];
                        break;
                    case "Tractor1":
                        objScript.vehicles[10].GetComponent<RectTransform>().localPosition = objScript.startCoordinates[0];
                        break;
                    case "Tractor5":
                        objScript.vehicles[11].GetComponent<RectTransform>().localPosition = objScript.startCoordinates[0];
                        break;
                    default:
                        Debug.Log("Unknown tag detected!");
                        break;
                }
            }
        }
    }
    void Start()
    {

    }
}

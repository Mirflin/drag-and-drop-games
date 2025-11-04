using System.Collections.Generic;
using UnityEngine;

public class RandomPositions : MonoBehaviour
{
    [Tooltip("RectTransform that defines the allowed placement area (usually a panel inside Canvas).")]
    public RectTransform area;

    [Tooltip("Objects to place randomly (UI elements or world objects).")]
    public GameObject[] objectsToPlace;

    [Tooltip("Padding inside the area rect (in local units)")]
    public float padding = 20f;

    [Tooltip("Minimum world distance between placed objects to reduce overlap")]
    public float minDistance = 50f;

    [Tooltip("Attempts per object to find a non-overlapping position")]
    public int maxAttempts = 50;
        
    [Tooltip("Randomize automatically on Start")]
    public bool randomizeOnStart = true;

    void Start()
    {
        // fallback: if area not assigned, try to find a Canvas and use its RectTransform
        if (area == null)
        {
            Canvas parentCanvas = GetComponentInParent<Canvas>();
            if (parentCanvas == null)
                parentCanvas = FindObjectOfType<Canvas>();

            if (parentCanvas != null)
                area = parentCanvas.GetComponent<RectTransform>();
            else
                Debug.LogWarning("RandomPositions: area not assigned and no Canvas found in scene.");
        }

        if (randomizeOnStart)
            RandomizePositions();
    }

    // Expose in inspector context menu for quick testing in editor
    [ContextMenu("Randomize Positions")]
    public void RandomizePositions()
    {
        if (area == null)
        {
            Debug.LogError("RandomPositions: area (RectTransform) is not assigned.");
            return;
        }

        if (objectsToPlace == null || objectsToPlace.Length == 0)
        {
            Debug.LogWarning("RandomPositions: objectsToPlace is empty.");
            return;
        }

        Rect rect = area.rect;
        var placedWorldPositions = new List<Vector3>();
        System.Random rng = new System.Random();

        Canvas areaCanvas = area.GetComponentInParent<Canvas>();
        Camera uiCamera = areaCanvas != null ? areaCanvas.worldCamera : null;

        foreach (var obj in objectsToPlace)
        {
            if (obj == null) continue;
            RectTransform rt = obj.GetComponent<RectTransform>();
            Transform objTransform = obj.transform;

            Vector3 chosenWorld = objTransform.position;
            bool found = false;

            for (int attempt = 0; attempt < maxAttempts; attempt++)
            {
                float rx = Mathf.Lerp(rect.xMin + padding, rect.xMax - padding, (float)rng.NextDouble());
                float ry = Mathf.Lerp(rect.yMin + padding, rect.yMax - padding, (float)rng.NextDouble());
                Vector2 localPoint = new Vector2(rx, ry);

                // world position for this local point inside area
                Vector3 worldPos = area.TransformPoint(localPoint);
                worldPos.z = objTransform.position.z;

                bool tooClose = false;
                foreach (var p in placedWorldPositions)
                {
                    if (Vector3.Distance(p, worldPos) < minDistance)
                    {
                        tooClose = true;
                        break;
                    }
                }

                if (!tooClose)
                {
                    chosenWorld = worldPos;
                    placedWorldPositions.Add(worldPos);
                    found = true;
                    break;
                }
            }

            // apply chosen position
            if (rt != null && rt.parent == area) // same parent -> anchoredPosition is appropriate
            {
                Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(uiCamera, chosenWorld);
                RectTransformUtility.ScreenPointToLocalPointInRectangle(area, screenPoint, uiCamera, out Vector2 anchored);
                rt.anchoredPosition = anchored;
            }
            else
            {
                objTransform.position = chosenWorld;
            }

            if (!found)
                Debug.LogWarning($"RandomPositions: couldn't find non-overlapping pos for {obj.name} after {maxAttempts} attempts; placed anyway.");
        }

        Debug.Log($"RandomPositions: placed {placedWorldPositions.Count} / {objectsToPlace.Length} objects.");
    }
}

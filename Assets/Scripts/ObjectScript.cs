using System;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI; // Для UI.Text

public class ObjectScript : MonoBehaviour
{
    public GameObject[] vehicles;
    [HideInInspector]
    public Vector2[] startCoordinates;
    public Canvas can;
    public AudioSource effects;
    public AudioClip[] audioCli;
    [HideInInspector]
    public bool rightPlace = false;
    public static GameObject lastDragged = null;
    public static bool drag = false;
    public static int correctCount = 0;
    public static int incorrectCount = 0;
    public bool end = false;
    public GameObject endObject;
    public Text endText;
    public Text resultText;
    public Image[] stars;

    public float timer = 0f;


    void Awake()
    {
        startCoordinates = new Vector2[vehicles.Length];
        for(int i = 0; i < vehicles.Length; i++)
        {
            startCoordinates[i] = vehicles[i].GetComponent<RectTransform>().localPosition;
        }

        GameObject resultsObj = GameObject.Find("Results");
        if (resultsObj != null)
        {
            var graphic = resultsObj.GetComponent<Graphic>();
            if (graphic != null)
                graphic.enabled = false;
            else
                resultsObj.SetActive(false);
        }
    }

    void Start()
    {
        ResetParameters();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (correctCount + incorrectCount == vehicles.Length)
        {
            end = true;
            if (endObject != null && !endObject.activeSelf)
            {
                endObject.SetActive(true);

                resultText.text = "Correct: "+correctCount+" Incorrect: "+incorrectCount;
                if (endText != null)
                    endText.text = "Game ended!";

                SetStarsByTime(timer);
            }
        }
    }

    void SetStarsByTime(float time)
    {
        int starCount = 1;
        if (time < 30f) starCount = 3;
        else if (time < 60f) starCount = 2;

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].enabled = i < starCount;
        }
    }

    public void ResetParameters()
    {
        correctCount = 0;
        incorrectCount = 0;
        lastDragged = null;
        drag = false;
        end = false;
        timer = 0f;
        rightPlace = false;

        if (endObject != null)
            endObject.SetActive(false);

        if (resultText != null)
            resultText.text = "";

        if (endText != null)
            endText.text = "";

        if (stars != null)
            foreach (var star in stars)
                star.enabled = false;

        // Возврат всех vehicles на стартовые координаты
        if (vehicles != null && startCoordinates != null)
        {
            for (int i = 0; i < vehicles.Length; i++)
            {
                vehicles[i].GetComponent<RectTransform>().localPosition = startCoordinates[i];
            }
        }
    }


}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void goToCars()
    {
        SceneManager.LoadScene("CityScene");
    }

    public void goToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Gameexit()
    {
        Debug.Log("exit");
        Application.Quit();
    }
}

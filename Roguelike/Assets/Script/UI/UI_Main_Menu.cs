using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Main_Menu : MonoBehaviour
{
    [SerializeField] private string sceneName = "Game";
    [SerializeField] private UI_Fade_Screen fadeScreen;

    public void ContinueGame()
    {
        StartCoroutine(LoadGameWithFadeEffect(1.5f));
    }
    IEnumerator LoadGameWithFadeEffect(float delay)
    {
        fadeScreen.FadeOut();
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(sceneName);
    }
}

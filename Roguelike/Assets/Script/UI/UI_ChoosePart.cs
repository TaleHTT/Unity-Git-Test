using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_ChoosePart : MonoBehaviour
{
    [SerializeField] private UI_Fade_Screen fadeScreen;

    public void ChoosePartOne()
    {
        SceneManager.LoadScene("Part_1");
        //StartCoroutine(LoadPart_1WithFadeEffect(1.5f));
    }
    //IEnumerator LoadPart_1WithFadeEffect(float delay)
    //{
    //    fadeScreen.FadeOut();
    //    yield return new WaitForSeconds(delay);
    //    SceneManager.LoadScene("Part_1");
    //}
}

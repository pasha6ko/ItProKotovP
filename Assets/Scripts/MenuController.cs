using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private RectTransform background;
    [SerializeField] private GameObject settings;
    [SerializeField] private List<GameObject> parametrs;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Image loadingIcon;

    Image _backgroundImage;
    Color _backgroundColor, _backgroundTrColor;
    //[SerializeField] private Vector3 mainMenuPos, setPos;
    // Start is called before the first frame update
    void Start()
    {
        _backgroundImage = background.GetComponent<Image>();
        _backgroundColor = _backgroundImage.color;
    }

    public void OpenSettings(float time)
    {
        StartCoroutine(ToSettings(background.localPosition, time));
    }
    public void NewGame()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator ToSettings(Vector3 startPos, float speed)
    {
        mainMenu.SetActive(false);
        foreach (GameObject i in parametrs)
        {
            i.SetActive(false);
        }
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime / speed;
            background.localPosition = Vector3.Lerp(startPos, new Vector3(0, 0, 0), time);
            _backgroundImage.color = Color.Lerp(new Color(_backgroundColor.r, _backgroundColor.g, _backgroundColor.b, 0), new Color(_backgroundColor.r, _backgroundColor.g, _backgroundColor.b, 255),time);
            yield return null;
        }

        settings.SetActive(true);
        foreach (GameObject i in parametrs)
        {
            yield return new WaitForSecondsRealtime(0.15f);
            i.SetActive(true);
        }

    }
    public void CloseSettings(float time)
    {
        StartCoroutine(ToMenu(background.localPosition, time));
    }
    IEnumerator ToMenu(Vector3 startPos, float speed)
    {
        settings.SetActive(false);
        float time = 0f;
        while (time < 1f)
        {
            time += Time.deltaTime / speed;
            background.localPosition = Vector3.Lerp(startPos, new Vector3(-1147, 0, 0), time);
            _backgroundImage.color = new Color(_backgroundColor.r, _backgroundColor.g, _backgroundColor.b, Mathf.Lerp(255, _backgroundColor.a, time));
            yield return null;
        }

        
        mainMenu.SetActive(true);
    }
    IEnumerator LoadScene()
    {
        AsyncOperation loading = SceneManager.LoadSceneAsync(1);
       // loadingScreen.SetActive(true);
        while(!loading.isDone)
        {
            //loadingIcon.fillAmount = loading.progress;
            yield return null;
        }
        
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour, Interacteble
{
    [SerializeField] int sceneNumber;
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Interact(Player player)
    {
        ResourceManager.instance.SaveData();
        StartCoroutine(LoadSceneAsync(sceneNumber));
    }
    public void LoadScene(int n)
    {
        StartCoroutine(LoadSceneAsync(n));
    }
    public IEnumerator LoadSceneAsync(int sceneNumber)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneNumber);
        while(!load.isDone)
        {

            yield return null;
        }
    }

    public void UpdateInteractionIcon(TextMeshProUGUI text)
    {
        text.text = gameObject.name;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

interface Interacteble
{
    public void Interact();

    public void UpdateUI(TextMeshProUGUI text);

    public GameObject GetGameObject();
}

public class Item : MonoBehaviour, Interacteble
{
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Interact()
    {
        Destroy(gameObject);
    }

    public void UpdateUI(TextMeshProUGUI text)
    {
        text.text = gameObject.name;
    }
    
}

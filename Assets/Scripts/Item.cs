using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

interface Interacteble
{
    public void Interact(Player player);

    public void UpdateInteractionIcon(TextMeshProUGUI text);

    public GameObject GetGameObject();
}

public class Item : MonoBehaviour, Interacteble
{
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Interact(Player player)
    {
        Destroy(gameObject);
    }

    public void UpdateInteractionIcon(TextMeshProUGUI text)
    {
        text.text = "Pick up";
    }
    
}

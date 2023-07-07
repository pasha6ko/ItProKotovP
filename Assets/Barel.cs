using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class Barel : MonoBehaviour, Interacteble
{
    [SerializeField] Player player;
    [SerializeField] GameObject PouringUI;
    [SerializeField] Image beerImage;
    [SerializeField, Range(0f, 10f)] float pouringLimit, floorSpeed,pouringSpeed;
    float pouringValue;
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void Interact(Player player)
    {
        if (player.hand.childCount > 0) return;
        if (!ResourceManager.instance.SubstractBarel()) return;
        PouringUI.SetActive(true);
        StartCoroutine(PoutingBeer());
    }

    public void UpdateInteractionIcon(TextMeshProUGUI text)
    {
        text.text = "Pour beer";
    }
    public void UpdateUI()
    {
        beerImage.fillAmount = pouringValue / pouringLimit;
    }
    IEnumerator PoutingBeer()
    {
        pouringValue = 0;
        while (pouringValue < pouringLimit)
        {
            pouringValue -= floorSpeed*Time.deltaTime;
            UpdateUI();
            yield return null;
        }
        PouringUI.SetActive(false);
        player.SpawnBeer();
    }
    public void OnClick()
    {
        pouringValue += pouringSpeed;
        UpdateUI();
    }

}

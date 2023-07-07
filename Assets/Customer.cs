using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Customer : MonoBehaviour, Interacteble
{
    [SerializeField] float timeToWait;
    [SerializeField] Image waitIndex;
    public GameObject GetGameObject()
    {
        throw new System.NotImplementedException();
    }

    public void Interact(Player player)
    {
        if (player.hand.childCount == 0) return;
        if (player.hand.GetChild(0).CompareTag("Beer"))
        {
            ResourceManager.instance.Sell();
            player.RemoveItemFromHand();
            Destroy(gameObject);
        }
    }

    public void UpdateInteractionIcon(TextMeshProUGUI text)
    {
        text.text = "Give";
    }
    private void Start()
    {
        timeToWait = Random.Range(6f,10f);
        StartCoroutine(Waiting());
    }
    IEnumerator Waiting()
    {
        float time = 0;
        while (time < timeToWait)
        {
            time+= Time.deltaTime;
            waitIndex.fillAmount = 1f-time/timeToWait;
            yield return null;
        }
        Destroy(gameObject);
        print("Suck");
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour, Interacteble
{
    [SerializeField] GameObject shopUI;
    [SerializeField] Player player;


    [SerializeField] TMPro.TextMeshProUGUI beerBarelPriceText, passivePriceText, activePriceText;
    [SerializeField, Range(0, 100f)] float beerBarelPrice;

    List<Dictionary<string, float>> _passiveModificators = new List<Dictionary<string, float>>()
    {
        new Dictionary<string, float>(){["price"]=0f,["mod"]=0f},
        new Dictionary<string, float>(){["price"]=15f,["mod"]=2f},
        new Dictionary<string, float>(){["price"]=250f,["mod"]=3f},
        new Dictionary<string, float>(){["price"]=3000f,["mod"]=6f},
    };
    List<Dictionary<string, float>> _activeModificators = new List<Dictionary<string, float>>()
    {
        new Dictionary<string, float>(){["price"]=0f,["mod"]=3f},
        new Dictionary<string, float>(){["price"]=15f,["mod"]=3f},
        new Dictionary<string, float>(){["price"]=250f,["mod"]=6f},
        new Dictionary<string, float>(){["price"]=3000f,["mod"]=20f},
    };
    int _currentPassiveModificator;
    int _currentActiveModificator;
    public GameObject GetGameObject()
    {
        return gameObject;
    }
    public void SaveData()
    {
        PlayerPrefs.SetInt("_currentPassiveModificator", _currentPassiveModificator);
        PlayerPrefs.SetInt("_currentActiveModificator", _currentActiveModificator);
        ResourceManager.instance.SaveData();
        UpdateUI();
    }
    public void LoadData()
    {
        _currentPassiveModificator = PlayerPrefs.GetInt("_currentPassiveModificator",0);
        _currentActiveModificator = PlayerPrefs.GetInt("_currentActiveModificator",0);
        UpdateUI();
    }
    public void Interact(Player player)
    {
        shopUI.SetActive(true);
        player.FixCamera();
    }
    public void ExitShop()
    {
        shopUI.SetActive(false);
        player.UnFixCamera();
    }

    public void UpdateInteractionIcon(TextMeshProUGUI text)
    {
        text.text = gameObject.name;
    }
    public void UpdateUI()
    {
        beerBarelPriceText.text = beerBarelPrice.ToString();
        print(_currentPassiveModificator);
        if(_currentPassiveModificator < _passiveModificators.Count-1)
            passivePriceText.text = _passiveModificators[1 + _currentPassiveModificator]["price"].ToString();
        else
            passivePriceText.text = "None";
        if (_currentActiveModificator < _activeModificators.Count - 1)
            activePriceText.text = _activeModificators[1+_currentActiveModificator]["price"].ToString();
        else
            activePriceText.text = "None";
    }
    

    public void BuyPassiveMod()
    {
        if (_currentPassiveModificator >= _passiveModificators.Count-1) return;
        if (_passiveModificators[1+_currentPassiveModificator]["price"] > ResourceManager.instance.coins) return;
        _currentPassiveModificator++;
        ResourceManager.instance.SubstractMoney(_passiveModificators[_currentPassiveModificator]["price"]);
        ResourceManager.instance.AddPassiveClicks(_passiveModificators[_currentPassiveModificator]["mod"]);
        SaveData();
    }
    public void BuyActiveMod()
    {
        if (_currentActiveModificator >= _activeModificators.Count-1) return;
        if (_activeModificators[1+_currentActiveModificator]["price"] > ResourceManager.instance.coins) return;
        _currentActiveModificator++;
        ResourceManager.instance.SubstractMoney(_activeModificators[_currentActiveModificator]["price"]);
        ResourceManager.instance.AddPassiveClicks( _activeModificators[_currentActiveModificator]["mod"]);
        SaveData();
    }
    public void BuyBeerBarel()
    {
        if (beerBarelPrice > ResourceManager.instance.coins) return;
        ResourceManager.instance.SubstractMoney(beerBarelPrice);
        ResourceManager.instance.AddBarel();
        SaveData();
    }
    private void Start()
    {
        LoadData();
        UpdateUI();
    }
}

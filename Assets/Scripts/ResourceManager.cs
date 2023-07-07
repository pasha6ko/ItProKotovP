using System.Collections;
using UnityEngine;
public class ResourceManager : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI coinsText, beerBarelText;

    public float coins { get; private set; }
    public float workTime;
    float _beerBarelCount;
    private float _passiveClicks;
    private float _activeClicks;
    [SerializeField] Transform hand;
    static public ResourceManager instance { get; private set; }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }
    public void SubstractMoney(float value)
    {
        coins -= value;
        UpdateUIElements();
    }
    public void Sell()
    {
        coins += _activeClicks;
        UpdateUIElements();
        SaveData();
    }
    public void Start()
    {    
        print($"{PlayerPrefs.HasKey("coins")}:has coins");
        LoadData();
        /*if (PlayerPrefs.HasKey("coins")) LoadData();
        else
        {
            _activeClicks = 1;
            _passiveClicks = 0;
            coins = 0;
            _beerBarelCount = 2;
            SaveData();
        
        }*/
        UpdateUIElements();
    }
    public void SaveData()
    {
        print("SaveData");
        PlayerPrefs.SetFloat("coins", coins);
        PlayerPrefs.SetFloat("_beerBarelCount", _beerBarelCount);
        PlayerPrefs.SetFloat("_passiveClicks", _passiveClicks);
        PlayerPrefs.SetFloat("_activeClicks", _activeClicks);
        PlayerPrefs.Save();
        UpdateUIElements();
    }
    public void LoadData()
    {
        print("LoadData");
        coins = PlayerPrefs.GetFloat("coins",0);
        _beerBarelCount = PlayerPrefs.GetFloat("_beerBarelCount",2);
        _passiveClicks = PlayerPrefs.GetFloat("_passiveClicks",0);
        _activeClicks = PlayerPrefs.GetFloat("_activeClicks",3);
        print($"Beer {_beerBarelCount}");
        UpdateUIElements();
    }
    public void ClearData()
    {
        PlayerPrefs.DeleteAll();
    }
    public void AddBarel(float value = 1f)
    {
        _beerBarelCount += value;
        UpdateUIElements();
    }
    public bool SubstractBarel(float value = 0.25f)
    {
        if (_beerBarelCount < value) return false;
        _beerBarelCount -= value;
        UpdateUIElements();
        return true;
    }
    public void AddPassiveClicks(float value)
    {
        _passiveClicks += value;
    }
    public void MultiplyPassiveClicks(float value)
    {
        _passiveClicks *= value;
    }
    public void AddActiveClicks(float value)
    {
        _activeClicks += value;
    }

    public void MultiplyActiveClicks(float value)
    {
        _activeClicks *= value;
    }
    void UpdateUIElements()
    {
        coinsText.text = Mathf.FloorToInt(coins).ToString();
        beerBarelText.text = _beerBarelCount.ToString();
    }
    public IEnumerator Work()
    {
        while (true)
        {
            coins += _passiveClicks;
            UpdateUIElements();
            yield return new WaitForSecondsRealtime(1f);
        }
    }

}

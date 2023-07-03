using UnityEngine;
using UnityEngine.UI;
public class SliderColorControlller : MonoBehaviour
{
    // Start is called before the first frame update
    Slider slider;
    void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void OnChangeSliderValue(Image fill)
    {
        float value = slider.value;
        if (value > 0) fill.color = Color.Lerp(Color.red, Color.green, value);
    }

    // Update is called once per frame
    void Update()
    {

    }
}

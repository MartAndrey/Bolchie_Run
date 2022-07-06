using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarType { HealthBar, ManaBar }

public class PlayerBar : MonoBehaviour
{
    private Slider _slider;

    public BarType type;

    // Start is called before the first frame update
    void Start()
    {
        _slider = GetComponent<Slider>(); 

        switch (type)
        {
            case BarType.HealthBar:
                _slider.maxValue = PlayerController.MAX_HEATH;
                break;

            case BarType.ManaBar:
                _slider.maxValue = PlayerController.MAX_MANA;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (type)
        {
            case BarType.HealthBar:
                _slider.value = PlayerController.sharedInstance.GetHealth();
                break;

            case BarType.ManaBar:
                _slider.value = PlayerController.sharedInstance.GetMana();
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderStatController : MonoBehaviour
{
    [SerializeField] private Slider sliderHealth;
    [SerializeField] private Slider sliderSpeed;
    [SerializeField] private Slider sliderAttack;

    public void SetHealth(int health)
    {
        sliderHealth.value = health;
    }

    public void SetSpeed(float speed)
    {
        sliderSpeed.value = speed;
    }

    public void SetAttack(int attack) 
    {
        sliderAttack.value = attack;
    }
}

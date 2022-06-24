using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MP : MonoBehaviour
{
    public Slider Slider;
    public Slider Slider2;
    public Vector3 Offset;
    public Vector3 Offset2;
    private void Start()
    {
        Slider.gameObject.SetActive(true);
        Slider2.gameObject.SetActive(true);
    }
    public void SetHealth(int health, int maxHealth)
    {
        Slider.value = health;
        Slider.maxValue = maxHealth;
        Slider.fillRect.GetComponentInChildren<Image>().color = Color.red;
    }
    public void SetMana(int mana, int maxMana)
    {
        Slider2.value = mana;
        Slider2.maxValue = maxMana;
        Slider2.fillRect.GetComponentInChildren<Image>().color = Color.blue;
    }
    void Update()
    {
        Slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
        Slider2.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset2);
    }
}

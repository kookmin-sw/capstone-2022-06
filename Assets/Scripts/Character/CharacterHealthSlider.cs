using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterHealthSlider : MonoBehaviour
{
    public Slider characterSlider3D;
    Slider characterSlider2D;

    public int health;

    void Start()
    {
        characterSlider2D = GetComponent<Slider>();

        characterSlider2D.maxValue = health;
        characterSlider3D.maxValue = health;
    }

    void Update()
    {
        characterSlider2D.value = health;
        characterSlider3D.value = characterSlider2D.value;
    }
}

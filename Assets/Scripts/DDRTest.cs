using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class DDRTest: MonoBehaviour
{
    public Image[] images;
    
    private void Awake()
    {
        foreach (var image in images)
        {
            image.color = Color.clear;
        }
    }

    private void Update()
    {
        if (DDR.current.select.isPressed) {
            images[0].color = Color.yellow;
        } else {
            images[0].color = Color.clear;
        }
    }
}
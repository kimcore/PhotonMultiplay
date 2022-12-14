using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class DDR
{
    public static DDR current
    {
        get {
            InputDevice device = InputSystem.GetDevice("ShanWan USB Dance");

            if (device == null) return null;

            return new DDR(device);
        }
    }

    public ButtonControl start;
    public ButtonControl select;
    
    public ButtonControl up;
    public ButtonControl down;
    public ButtonControl left;
    public ButtonControl right;

    public ButtonControl upLeft;
    public ButtonControl upRight;
    public ButtonControl downLeft;
    public ButtonControl downRight;

    private DDR(InputDevice device)
    {
        start = device.GetChildControl<ButtonControl>("button10");
        select = device.GetChildControl<ButtonControl>("button9");

        up = device.GetChildControl<ButtonControl>("trigger");
        down = device.GetChildControl<ButtonControl>("button2");
        left = device.GetChildControl<ButtonControl>("button3");
        right = device.GetChildControl<ButtonControl>("button4");
        
        upLeft = device.GetChildControl<ButtonControl>("button7");
        upRight = device.GetChildControl<ButtonControl>("button8");
        downLeft = device.GetChildControl<ButtonControl>("button5");
        downRight = device.GetChildControl<ButtonControl>("button6");
    }
}

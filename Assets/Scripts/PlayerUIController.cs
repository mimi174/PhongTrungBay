﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Gắn cho obj Player
//Xử lý UI để hiển thị phần hướng dẫn

public class PlayerUIController : MonoBehaviour
{
    public GameObject InstructionDisplay;
    public GameObject PressToRead;

    public void DisplayInstruction(string text)
    {
        if (InstructionDisplay == null) return;

        Text textUI = InstructionDisplay.GetComponentInChildren<Text>();
        if (textUI == null) return;

        InstructionDisplay.SetActive(true);
        textUI.text = text;
    }

    public void DisposeInstruction()
    {
        InstructionDisplay.SetActive(false);
    }

    public void DisplayPressToReadMessage()
    {
        PressToRead.SetActive(true);
    }
    public void DisposePressToReadMessage()
    {
        PressToRead.SetActive(false);
    }
}

  using System.Collections;
  using System.Collections.Generic;
  using UnityEngine;
  using UnityEngine.UI;
  
  public class PlayerUIController : MonoBehaviour
  {
      public GameObject InstructionDisplay;
      // Start is called before the first frame update
      void Start()
      {
          
      }
  
      // Update is called once per frame
      void Update()
      {
          
      }
  
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
  }

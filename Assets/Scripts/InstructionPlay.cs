 using System.Collections;
  using System.Collections.Generic;
  using System.Linq;
  using UnityEngine;

//Gắn cho obj Player
//Xử lý tương tác với các instruction 
public class InstructionPlay : MonoBehaviour
  {
      private PlayerUIController _uiController;
      private PlayerMovement1 _playerMovement;
      private bool isRunning = false;
      private List<string> _listOfText;
      private Coroutine currentCoroutine;
      private int _textCount = 0;
      // Start is called before the first frame update
      void Start()
      {
          _uiController = GetComponent<PlayerUIController>();
          _playerMovement = GetComponent<PlayerMovement1>();
      }

     // Update is called once per frame
void Update()
     {
             if (Input.GetKeyDown(KeyCode.Space) && isRunning)
                 {
        _textCount++;
        Debug.Log("Text count: " + _textCount);
                     if (_textCount >= _listOfText.Count)
                         {
            isRunning = false;
            _playerMovement.canMove = true;
            _textCount = 0;
            _uiController.DisposeInstruction();
                         }
                     else
                         {
            _uiController.DisplayInstruction(_listOfText[_textCount]);
                         }
                 }
         }

void OnTriggerEnter(Collider other)
     {
             if (other.gameObject.tag == "Instruction")
                 {
        var instructionCheckpoint = other.gameObject.GetComponent<InstructionCheckpoint>();
                     if (instructionCheckpoint == null) return;
        
        var instruction = instructionCheckpoint.GetInstruction();
                     if (instruction == null || instruction.ListOfText == null && instruction.ListOfText.Count > 0) return;
        
                     if (_uiController == null) return;
        
        _playerMovement.canMove = false;
        isRunning = true;
        _listOfText = instruction.ListOfText;
        _uiController.DisplayInstruction(_listOfText[_textCount]);
                 }
         }
 }

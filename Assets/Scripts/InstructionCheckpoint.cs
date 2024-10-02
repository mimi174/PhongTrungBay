using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionCheckpoint : MonoBehaviour
{
    [SerializeField]
    private Instruction instruction;
    private bool isCheck = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Instruction GetInstruction()
    {
        if (isCheck) return null;

        isCheck = true;
        return instruction;

    }
}

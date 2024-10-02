using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Instruction", menuName = "Instruction")]
public class Instruction : ScriptableObject
{
    public List<string> ListOfText;
}

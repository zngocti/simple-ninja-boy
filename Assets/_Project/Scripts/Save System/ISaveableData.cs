using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveableData
{
    public void SetVariablesToSave();
    public void OnSave(SaveManager manager);
    public void OnLoad(SaveManager manager);
}

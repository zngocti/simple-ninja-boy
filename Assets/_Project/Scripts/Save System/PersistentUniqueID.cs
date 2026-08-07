using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentUniqueID : MonoBehaviour
{
    // The id must be serialized
    [SerializeField] string _id = string.Empty;

    public string ID { get => _id; }

    private void OnValidate()
    {
        if (Application.isPlaying || IsPrefabMode())
        {
            return;
        }

        if (string.IsNullOrEmpty(_id))
        {
            Debug.Log("String Null or Empty");
            GenerateID();
            return;
        }
        else if (Event.current != null)
        {
            if (Event.current.type == EventType.ExecuteCommand && Event.current.commandName == "Duplicate")
            {
                //Ctrl+D
                //Main Menu 'Edit > Duplicate'
                Debug.Log("ExecuteCommand - Duplicate");
                GenerateID();

            }
            else if (Event.current.type == EventType.ExecuteCommand && Event.current.commandName == "Paste")
            {
                //Ctrl+V
                //Main Menu 'Edit > Paste'
                Debug.Log("ExecuteCommand - Paste");
                GenerateID();

            }
            else if (Event.current.type == EventType.ValidateCommand && Event.current.commandName == "Paste")
            {
                Debug.Log("ValidateCommand - Paste");
                GenerateID();

            }
            else if (Event.current.type == EventType.KeyDown && (Event.current.modifiers == EventModifiers.Control && Event.current.keyCode == KeyCode.Y))
            {
                //Ctrl+Y
                Debug.Log("KeyDown - Redo");
                GenerateID();
            }
        }
    }

    bool IsPrefabMode()
    {
        // If the object doesn't have a scene then it's a prefab
        return string.IsNullOrEmpty(gameObject.scene.name) || string.IsNullOrEmpty(gameObject.scene.path);
    }

    [ContextMenu("Generate New ID")]
    private void GenerateID()
    {
        _id = System.Guid.NewGuid().ToString();
    }
}

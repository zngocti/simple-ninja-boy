using UnityEngine;

[RequireComponent(typeof(PersistentUniqueID))]
public class SaveableGameObject : MonoBehaviour, ISaveableData
{
     PersistentUniqueID _persistentID;

    public string SaveID
    {
        get
        {
            if (_persistentID == null)
            {
                _persistentID = GetComponent<PersistentUniqueID>();
            }

            return _persistentID.ID;
        }
    }

    void Awake()
    {
        if (!_persistentID)
        {
            _persistentID = GetComponent<PersistentUniqueID>();
        }
    }

    public void OnLoad(SaveManager manager)
    {
        manager.GameObjectData.LoadData(this);
    }

    public void OnSave(SaveManager manager)
    {
        manager.GameObjectData.SaveData(this);
    }

    public void SetVariablesToSave()
    {
        if (!_persistentID)
        {
            _persistentID = GetComponent<PersistentUniqueID>();
        }
    }
}

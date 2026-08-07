using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveableGameObjectData
{
    [SerializeField] List<string> _id = new List<string>();

    [SerializeField] List<bool> _isGameObjectActive = new List<bool>();
    [SerializeField] List<Vector3> _position = new List<Vector3>();

    public void SaveData(SaveableGameObject data)
    {
        int index = _id.IndexOf(data.SaveID);

        if (index > -1)
        {
            _isGameObjectActive[index] = data.gameObject.activeSelf;
            _position[index] = data.transform.position;
        }
        else
        {
            _id.Add(data.SaveID);
            _isGameObjectActive.Add(data.gameObject.activeSelf);
            _position.Add(data.transform.position);
        }
    }

    public void LoadData(SaveableGameObject data)
    {
        int index = _id.IndexOf(data.SaveID);

        if (index < 0)
        {
            return;
        }

        data.gameObject.SetActive(_isGameObjectActive[index]);
        data.transform.position = _position[index];
    }
}

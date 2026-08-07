using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatsSaveableData
{
    [SerializeField] List<string> _id = new List<string>();
    [SerializeField] List<Vector3Int> _stats = new List<Vector3Int>();

    public void SaveData(PlayerStats data)
    {
        int index = _id.IndexOf(data.SaveID);

        if (index > -1)
        {
            _stats[index] = new Vector3Int(data.CurrentHealth, data.CurrentAttack, data.CurrentMagic);
        }
        else
        {
            _id.Add(data.SaveID);
            _stats.Add(new Vector3Int(data.CurrentHealth, data.CurrentAttack, data.CurrentMagic));
        }
    }

    public bool Stats(string id, out Vector3Int stats)
    {
        int index = _id.IndexOf(id);

        if (index < 0)
        {
            stats = Vector3Int.zero;
            return false;
        }

        stats = _stats[index];
        return true;
    }
}

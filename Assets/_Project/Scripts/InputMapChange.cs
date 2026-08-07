using System.Collections;
using UnityEngine;

public class InputMapChange : MonoBehaviour
{
    [SerializeField] string _actionMapName;

    public void ChangeActionMapInstant()
    {
        InputMapController.Instance?.SwitchToMapInstant(_actionMapName);
    }

    public void ChangeActionMapInstant(string newActionMap)
    {
        InputMapController.Instance?.SwitchToMapInstant(newActionMap);     
    }

    public void ChangeActionMap()
    {
        InputMapController.Instance?.SwitchToMap(_actionMapName);
    }

    public void ChangeActionMap(string newActionMap)
    {
        InputMapController.Instance?.SwitchToMap(newActionMap);
    }
}

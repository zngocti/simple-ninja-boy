using UnityEngine;

public class InputMapChange : MonoBehaviour
{
    [SerializeField] string _actionMapName;

    public void ChangeActionMap()
    {
        InputMapController.Instance?.SwitchToMap(_actionMapName);
    }

    public void ChangeActionMap(string newActionMap)
    {
        InputMapController.Instance?.SwitchToMap(newActionMap);     
    }
}

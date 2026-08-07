using System.Collections;
using UnityEngine;

public class InputMapChange : MonoBehaviour
{
    [SerializeField] string _actionMapName;

    public void ChangeActionMapInstant()
    {
        InputMapController.Instance?.SwitchToMap(_actionMapName);
    }

    public void ChangeActionMapInstant(string newActionMap)
    {
        InputMapController.Instance?.SwitchToMap(newActionMap);     
    }

    public void ChangeActionMap()
    {
        StartCoroutine(ChangeActionMapAfterFrame());
    }

    public void ChangeActionMap(string newActionMap)
    {
        StartCoroutine(ChangeActionMapAfterFrame(newActionMap));
    }

    IEnumerator ChangeActionMapAfterFrame()
    {
        yield return null;
        ChangeActionMapInstant();
    }

    IEnumerator ChangeActionMapAfterFrame(string newActionMap)
    {
        yield return null;
        ChangeActionMapInstant(newActionMap);
    }
}

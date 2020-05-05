using UnityEngine;

public class EventTest : MonoBehaviour
{
    private int count;

    public void TestEventHandler()
    {
        Debug.Log($"Event triggered {(++count).ToString()}");
    }
}
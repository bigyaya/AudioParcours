using UnityEngine;

public class TestScript : MonoBehaviour, IUsable
{
    public void Use()
    {
        Debug.Log("TestScript.Use() called");
    }
}

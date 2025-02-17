using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class GameEvent: MonoBehaviour
{
    public string eventName;
    public float eventLength;
    public bool isActive = false;
}
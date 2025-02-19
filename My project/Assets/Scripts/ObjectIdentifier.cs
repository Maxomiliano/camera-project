using UnityEngine;
using TMPro;

public class ObjectIdentifier : MonoBehaviour
{
    [SerializeField] private string _objectName;
    [SerializeField] private string _objectAction;

    public string GetObjectName()
    {
        return _objectName;
    }

    public string GetObjectAction() 
    {
        return _objectAction;
    }
}

using UnityEngine;
using TMPro;

public class ObjectIdentifier : MonoBehaviour
{
    [SerializeField] private string _objectName;
    [SerializeField] private string _objectAction;

    public string ObjectName { get; private set; }
    public string ObjectAction { get; private set; }

    private void Awake()
    {
        ObjectName = _objectName;
        ObjectAction = _objectAction;
    }
}

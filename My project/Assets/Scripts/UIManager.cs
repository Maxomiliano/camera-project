using System;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject _inventoryPanel;

    private bool _anyPanelOpen = false;

    public bool AnyPanelOpen { get => _anyPanelOpen; set => _anyPanelOpen = value; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _inventoryPanel.SetActive(false);
        UpdateCursorState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) TogglePanel(_inventoryPanel);
    }

    private void TogglePanel(GameObject panel)
    {
        bool newState = !panel.activeSelf;
        panel.SetActive(newState);
        UpdateCursorState();
    }

    private void UpdateCursorState()
    {
        _anyPanelOpen = _inventoryPanel.activeSelf; // || otroPanel.activeSelf
        Cursor.visible = _anyPanelOpen;
        Cursor.lockState = _anyPanelOpen ? CursorLockMode.None : CursorLockMode.Locked;
    }

    /*
    public bool AnyPanelOpen()
    {
        return _inventoryPanel.activeSelf;
    }
    */
}

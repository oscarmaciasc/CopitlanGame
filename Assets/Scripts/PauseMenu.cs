using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject MapButton;
    [SerializeField] private GameObject InventoryButton;
    [SerializeField] private GameObject BalloonButton;
    [SerializeField] private GameObject InfoButton;
    [SerializeField] private GameObject BackButton;
    [SerializeField] private GameObject MapPanel;
    [SerializeField] private GameObject InventoryPanel;
    [SerializeField] private GameObject BalloonPanel;
    [SerializeField] private GameObject InfoPanel;
    [SerializeField] private GameObject InvResourcesPanel;
    [SerializeField] private GameObject InvFlutesPanel;
    [SerializeField] private GameObject InvPartituresPanel;
    [SerializeField] private GameObject PauseMenuPanel;

    void Start()
    {
        ActivateMapPanel();
    }

    public void ActivateMapPanel() {
        MapPanel.SetActive(true);
        InventoryPanel.SetActive(false);
        BalloonPanel.SetActive(false);
        InfoPanel.SetActive(false);
    }

    public void ActivateInventoryPanel() {
        MapPanel.SetActive(false);
        InventoryPanel.SetActive(true);
        ActivateInvResourcesPanel();
        BalloonPanel.SetActive(false);
        InfoPanel.SetActive(false);
    }

    public void ActivateBalloonPanel() {
        MapPanel.SetActive(false);
        InventoryPanel.SetActive(false);
        BalloonPanel.SetActive(true);
        InfoPanel.SetActive(false);
    }

    public void ActivateInfoPanel() {
        MapPanel.SetActive(false);
        InventoryPanel.SetActive(false);
        BalloonPanel.SetActive(false);
        InfoPanel.SetActive(true);
    }

    public void ActivateInvResourcesPanel() {
        Debug.Log("ActivateInvResourcePanel");
        InvResourcesPanel.SetActive(true);
        InvFlutesPanel.SetActive(false);
        InvPartituresPanel.SetActive(false);
    }

    public void ActivateInvFlutesPanel() {
        InvResourcesPanel.SetActive(false);
        InvFlutesPanel.SetActive(true);
        InvPartituresPanel.SetActive(false);
    }

    public void ActivateInvPartituresPanel() {
        InvResourcesPanel.SetActive(false);
        InvFlutesPanel.SetActive(false);
        InvPartituresPanel.SetActive(true);
    }

    public void DeactivatePauseMenuPanel() {
        PauseMenuPanel.gameObject.SetActive(false);
        GameManager.instance.pPressed = false;
    }
}

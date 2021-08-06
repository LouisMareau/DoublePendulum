using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PendulumUI : MonoBehaviour
{
    [Header("TABS")]
    public List<GameObject> tabs;
    [Space]
    public GameObject tabTemplate;

    [Header("REFERENCES")]
    public PendulumManager manager;
    public PendulumUIInfo info;
    public Transform tabsT;
    [Space]
    public Pendulum selectedSinglePendulum;

    #region UNITY METHODS
    private void OnEnable() {
        // Play short animation for showing tabs (one after another)
        //* TO FIX... ___>
        // Sequence sequence = DOTween.Sequence();
        // foreach (GameObject tab in tabs) {
        //     tab.SetActive(true);
        //     sequence.Append(tab.transform.DOLocalMoveY(10, .2f));
        // }
        // sequence.Play();
        //* <_____________
    }

    private void OnDisable() {
        // Play short animation for hiding tabs (one after another)
        //* TO FIX... ___>
        // Sequence sequence = DOTween.Sequence();
        // foreach (GameObject tab in tabs) {
        //     sequence.Append(tab.transform.DOLocalMoveY(10, .2f).OnComplete(() => tab.SetActive(false)));
        // }
        // sequence.Play();
        //* <_____________
    }

    private void Awake() {
        // We initialize the UI
        Init();
        // We hide all tabs at launch
        HideTabs();
        // We hide the info at launch
        HideInfo();
    }
    #endregion

    private void Init() {
        // We initialize the list
        tabs = new List<GameObject>();
        // We add all the already existing tabs in the list
        for (int i = 0; i < tabsT.childCount; i++) {
            tabs.Add(tabsT.GetChild(i).gameObject);
        }
        // Set DOTweens capacity
        DOTween.SetTweensCapacity(2000,1000);
    }

    #region TABS
    /// <summary>
    /// Hides all the tabs (stick them to the very top of the screen, leaving just the color showing)
    /// </summary>
    public void HideTabs() {
        foreach (GameObject tab in tabs) {
            tab.SetActive(false);
        }
    }

    /// <summary>
    /// Shows all the tabs
    /// </summary>
    public void ShowTabs() {
        // We show all tabs
        foreach (GameObject tab in tabs) {
            tab.SetActive(true);
        }
    }

    public void RevealTabs() {
        // We bring all the tabs down to show all the buttons
        Sequence sequence = DOTween.Sequence();
        foreach (GameObject tab in tabs) {
            sequence.Join(tab.transform.DOLocalMoveY(-110, 0.1f).SetDelay(0.05f));
        }
        sequence.Play().OnComplete(() => sequence.Kill());
    }

    public void ConcealTabs() {
        // We bring back all the tabs back to hide all the buttons
        Sequence sequence = DOTween.Sequence();
        foreach (GameObject tab in tabs) {
            sequence.Join(tab.transform.DOLocalMoveY(-40, 0.1f).SetDelay(0.05f));
        }
        sequence.Play().OnComplete(() => sequence.Kill());
    }
    
    public void RemoveTab(GameObject tab) {
        // We first remove then destroy the tab
        tabs.Remove(tab);
        Destroy(tab);
    }
    #endregion

    #region INFO
    public void HideInfo() { info.gameObject.SetActive(false); }
    public void ShowInfo() { info.gameObject.SetActive(true); }
    public void ToggleInfo() { info.gameObject.SetActive(!info.gameObject.activeSelf); }
    #endregion
}
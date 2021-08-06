using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    [Header("GENERAL UI")]
    public PendulumUI pendulumUI;

    [Header("PLAY / PAUSED STATE")]
    public GameObject playState;

    [Header("CONTROLS")]
    private Dictionary<string,GameObject> controls;

    #region UNITY METHODS
    private void Awake() {
        Init();
    }
    #endregion

    private void Init() {
        // PLAY / PAUSED state
        playState.SetActive(true);
        playState.GetComponent<TextMeshProUGUI>().text = string.Format("<b>PAUSED</b>");
    
        // CONTROLS
        controls = new Dictionary<string, GameObject>();
        Transform t = GameObject.Find("Controls").transform;
        controls.Add("Resume", t.Find("[Space] Resume").gameObject);
        controls.Add("Pause", t.Find("[Space] Pause").gameObject);
        controls.Add("Edit", t.Find("[E] Edit").gameObject);
        controls.Add("Features", t.Find("[H] Features").gameObject);
        controls.Add("Info", t.Find("[I] Info").gameObject);
        // We set all the controls to not show when the starting message is displayed (at launch)
        controls["Resume"].SetActive(false);
        controls["Pause"].SetActive(false);
        controls["Edit"].SetActive(false);
        controls["Features"].SetActive(false);
        controls["Info"].SetActive(false);
        // We hide the play state UI
        playState.SetActive(false);
        // We hide the pendulum UI
        pendulumUI.HideTabs();
    }

    public void OnStateSwitch() {
        // PLAY / PAUSED State
        playState.GetComponent<TextMeshProUGUI>().text = string.Format("<b>{0}</b>", GameManager.state.ToString());

        switch (GameManager.state) {
            case GameState.PLAY:
                controls["Resume"].SetActive(false);
                controls["Pause"].SetActive(true);
                controls["Edit"].SetActive(false);
                controls["Features"].SetActive(false);
                controls["Info"].SetActive(true);
                pendulumUI.HideTabs();
            break;

            case GameState.PAUSED:
                controls["Resume"].SetActive(true);
                controls["Pause"].SetActive(false);
                controls["Edit"].SetActive(true);
                controls["Features"].SetActive(true);
                controls["Info"].SetActive(true);
                pendulumUI.ShowTabs();
            break;

            case GameState.FROZEN:
                controls["Resume"].SetActive(false);
                controls["Pause"].SetActive(false);
                controls["Edit"].SetActive(false);
                controls["Features"].SetActive(false);
                controls["Info"].SetActive(false);
                pendulumUI.HideTabs();
            break;
        }
    }
}
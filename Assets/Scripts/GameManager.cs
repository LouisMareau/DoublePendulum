using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum GameState
{
    PLAY,
    PAUSED,
    FROZEN
}

public class GameManager : MonoBehaviour
{
    [Header("STATES")]
    public static GameState state;

    [Header("START ORIGIN")]
    public static Transform origin;

    [Header("CONSTANTS")]
    public float g = Const.g;
    public float dampening = Const.dampening;

    [Header("UI")]
    public GameObject startingMessage;
    public GameUI UI;

    #region UNITY METHODS
    private void OnValidate() {
        Init();
    }

    private void Awake() {
        Init();
    }

    private void Start() {
        // We start with the game PAUSED
        state = GameState.PAUSED;
    }

    private void Update() 
    {
        if (startingMessage.activeSelf) {
            state = GameState.FROZEN;
        }

        if (state != GameState.FROZEN) {
            // PLAY/PAUSE State Input
            if (Input.GetKeyDown(KeyCode.Space)) {
                OnStateSwitch();
                UI.OnStateSwitch();
            }

            // EDIT Mode
            if (Input.GetKeyDown(KeyCode.E)) {

            }
        }
        // If the stating message is still on, we wait for a user input to do anything
        else {
            if (Input.anyKeyDown) {
                // We hide the starting message
                startingMessage.SetActive(false);
                // We set the game to pause
                state = GameState.PAUSED;
                // Game UI
                UI.playState.SetActive(true);
                UI.OnStateSwitch();
            }
        }
    }
    #endregion

    private void Init() {
        // We find the origin transform in the hierarchy
        origin = GameObject.Find("Origin").transform;

        // We set the static constants
        Const.g = g;
        Const.dampening = dampening;
    }

    private void OnStateSwitch() {
        if (state == GameState.PLAY) {
            state = GameState.PAUSED;
        }
        else {
            state = GameState.PLAY;
        }

        UI.OnStateSwitch();
    }
}
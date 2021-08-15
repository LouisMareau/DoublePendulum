using UnityEngine;

namespace DoublePendulumProject.Gameplay 
{
    using UI;

    public enum GameState
    {
        PLAY,
        PAUSED,
        EDIT,
        INACTIVE
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
            // As long as the starting message hasn't been removed, we set the state to FROZEN (disabling state-based inputs)
            if (startingMessage.activeSelf) {
                state = GameState.INACTIVE;

                // We wait for a user input to enable state-based inputs
                if (Input.anyKeyDown) {
                    // We hide the starting message
                    startingMessage.SetActive(false);
                    // We set the game to pause
                    state = GameState.PAUSED;
                    // We update the game UI
                    UI.OnStateSwitch();
                }
            }
            else {
                //* STATE SWITCH (PAUSE/EDIT <=> PLAY)
                // Space: PAUSED/EDIT (any) <=> PLAY
                if (Input.GetKeyDown(KeyCode.Space)) {
                    OnStateSwitch();
                }
                // E: PAUSED <=> EDIT
                if (state != GameState.PLAY && Input.GetKeyDown(KeyCode.E)) {
                    OnToggleEdit();
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

        /// <summary>
        /// Method that should be called when the state of the game changes (FROZEN => PAUSED <=> PLAY).
        /// </summary>
        private void OnStateSwitch() {
            // If the state is set to PLAY, we switch it to PAUSED
            if (state == GameState.PLAY) { state = GameState.PAUSED; }
            // If the state is set to PAUSED, we switch it to PLAY
            else { state = GameState.PLAY; }

            // We make sure to update the UI by calling the OnStateSwitch() method 
            UI.OnStateSwitch();
        }

        private void OnToggleEdit() {
            if (state == GameState.PAUSED) { state = GameState.EDIT; }
            else if (state == GameState.EDIT) { state = GameState.PAUSED; }

            UI.OnStateSwitch();
        }
    }

}
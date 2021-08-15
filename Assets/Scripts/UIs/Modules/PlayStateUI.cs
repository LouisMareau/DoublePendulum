using UnityEngine;
using TMPro;
using DG.Tweening;

namespace DoublePendulumProject.UI.Modules
{
    using Gameplay;

    /// <summary>
    /// This class is used to set and control the display of the Play/Pause/Edit field on the UI at runtime.
    /// </summary>
    public class PlayStateUI : MonoBehaviour, IModuleUI
    {
        /// <summary>
        /// The root game object associated with this UI element.
        /// </summary>
        [Header("CORE")]
        public GameObject obj;

        /// <summary>
        /// The text field (TextMeshProUGUI) displaying the state of the game (PLAY or PAUSED).
        /// </summary>
        private TextMeshProUGUI field;

        #region UNITY METHODS
        private void Awake() {
            // We initialize the object
            Init();
        }
        #endregion

        /// <summary>
        /// Initializes the object.
        /// </summary>
        public void Init() {
            // We check if the obj is NULL (in which case, we make sure we have a reference to the root object)
            if (obj == null) { obj = GameObject.Find("_PLAYSTATE"); }
            // We define the TMPro field
            field = obj.GetComponent<TextMeshProUGUI>();
        }

        /// <summary>
        /// Define what is happening when the game state changes (INACTIVE => PAUSED/EDIT <=> PLAY)
        /// </summary>
        public void OnStateSwitch() {
            // We do something different for each game state
            switch (GameManager.state) {
                //* INACTIVE: Do nothing.
                case GameState.INACTIVE: break;
                //* PLAY: We fade out the text and scale it up
                case GameState.PLAY:
                    OnPlay();
                    break;
                //* EDIT: We change the text field value
                case GameState.EDIT:
                    OnEdit();
                    break;
                //* PAUSE: We fade in the text and scale it down
                case GameState.PAUSED:
                    OnPause();
                    break;
            }
        }

        /// <summary>
        /// Defines what is happening when the state (GameManager.state) is set to PLAY.
        /// </summary>
        public void OnPlay() {
            //* We make sure the text reads "PLAY"
            // (We do not need the reference to GameManager (too much work) => field.text = GameManager.state.ToString())
            field.text = "PLAY";

            // We tween a fade out and slightly scale up (uniformely) the object 
            field.DOFade(0, .7f).From(1);
            field.transform.DOScale(1.3f, .5f).From(1.1f);
        }

        /// <summary>
        /// Defines what is happening when the state (GameManager.state) is set to PAUSED.
        /// </summary>
        public void OnPause() {
            //* We make sure the text reads "PAUSED"
            field.text = "PAUSED";

            // We tween a fade in and slightly scale down (uniformely) the object
            field.DOFade(1, .5f).From(0);
            field.transform.DOScale(1, .5f).From(1.3f);
        }

        // Defines what is happening when the state (GameManager.state) is set to EDIT.
        public void OnEdit() {
            //* We make sure the text reads "EDIT MODE"
            field.text = "EDIT MODE";

            // We tween the scale slightly (uniformely)
            field.transform.DOScale(1.1f, .5f).From(1);
        }
    }

}
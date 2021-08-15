using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoublePendulumProject.UI.Modules
{
    using Gameplay;

    /// <summary>
    /// This class is used to set and control the display of controls (depending on the game state) on the UI at runtime. <br/>
    /// Since the controls change often depending on the state of the simulation, the display of controls/shortcuts are very important.
    /// The panel displayed with the list of the current controls available will be triggered by pressing the [ H ] key.
    /// </summary>
    public class ControlsUI : MonoBehaviour, IModuleUI
    {
        /// <summary>
        /// The root game object associated with this UI element.
        /// </summary>
        [Header("CORE")]
        public GameObject obj;

        /// <summary>
        /// A list (Dictionary) of all the controls. Use controls["key"] to access the value associated to that key (ex: controls["Add"] will give "Press [ A ] to add a double pendulum with default settings.")
        /// </summary>
        public Dictionary<string, string> controls;

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
            if (obj == null) { obj = GameObject.Find("_CONTROLS"); }

            // We intialize the dictionary
            controls = new Dictionary<string, string>();
            SetControls();
        }

        private void SetControls() {
            // We set up all the controls
            controls.Add("Play/Pause", "Press <b>[ Space ]</b> to switch between the modes PLAY and PAUSED.");
            controls.Add("Edit", "Press <b>[ E ]</b> while in PAUSED mode to toggle the EDIT mode.");
            controls.Add("Add", "Press <b>[ A ]</b> to add a double pendulum with default settings.");
            controls.Add("Clone", "Press <b>[ C ]</b> to clone the currently selected double pendulum (keeping the same values as the original).");
            controls.Add("Help", "Press <b>[ H ]</b> to open the help panel, for a list of all the controls and shortcuts available.");
            controls.Add("Info", "Press <b>[ I ]</b> to open the information panel.");
            controls.Add("Menu", "Press <b>[ Esc ]</b> to open the main menu.");
            controls.Add("Mass+", "Slide <b>[ Mouse Scroll ]</b> forward to increase the mass by 0.001 unit.");
            controls.Add("Mass-", "Slide <b>[ Mouse Scroll ]</b> backward to decrease the mass by 0.00i unit.");
            controls.Add("Mass++", "Press <b>[ Left Shift ]</b> and slide <b>[ Mouse scroll ]</b> forward to increase the mass by 0.01 unit.");
            controls.Add("Mass--", "Press <b>[ Left Shift ]</b> and slide <b>[ Mouse scroll ]</b> backward to decrease the mass by 0.01 unit.");
            controls.Add("Mass+++", "Press <b>[ Left Shift ]</b> + <b>[ Left Ctrl ]</b> and slide <b>[ Mouse scroll ]</b> forward to increase the mass by 0.1 unit.");
            controls.Add("Mass---", "Press <b>[ Left Shift ]</b> + <b>[ Left Ctrl ]</b> and slide <b>[ Mouse scroll ]</b> backward to increase the mass by 0.1 unit.");
            controls.Add("FreeMove", "Press <b>[ Mouse Left ]</b> to drag around the masses.");
            //* WRITE MORE HERE !
        }

        /// <summary>
        /// Define what is happening when the game state changes (INACTIVE => PAUSED/EDIT <=> PLAY)
        /// </summary>
        public void OnStateSwitch() {
            // We do something different for each game state
            switch (GameManager.state) {
                //* INACTIVE: Do nothing.
                case GameState.INACTIVE: break;
                //* PLAY: We switch to controls that can be used when the simulation is running
                case GameState.PLAY:
                    OnPlay();
                    break;
                //* EDIT: We switch to controls that can be used when the simulation is paused and in edit mode
                case GameState.EDIT:
                    OnEdit();
                    break;
                //* PAUSE: We switch to controls that can be used when the simulation is paused
                case GameState.PAUSED:
                    OnPause();
                    break;
            }
        }

        public void OnPlay() {
            // We concatenate all the strings into a well structured list
            string s = CreateString(controls["Play/Pause"], controls["Info"], controls["Menu"]);
        }

        public void OnEdit() {
            // We concatenate all the strings into a well structured list
        }

        public void OnPause() {
            // We concatenate all the strings into a well structured list
            string s = CreateString(controls["Play/Pause"], controls["Info"], controls["Menu"]);
        }
        
        private string CreateString(params string[] args) {
            // We initialize an empty string
            string s = string.Empty;
            
            foreach (string arg in args) {
                s += arg + "\n";
            }

            return s;
        }
    }

    

}


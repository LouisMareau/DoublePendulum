using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        /// The text field (TextMeshProUGUI) displaying the the list of currently available controls.
        /// </summary>
        public TextMeshProUGUI field;

        /// <summary>
        /// A list (Dictionary) of all the controls. Use controls["key"] to access the game object associated to that key (ex: controls["Add"] will give the GameObject)
        /// </summary>
        public Dictionary<string, GameObject> controls;

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
            // We define the TMPro field
            field = obj.GetComponentInChildren<TextMeshProUGUI>();

            // We intialize the dictionary
            controls = new Dictionary<string, GameObject>();
            SetControls();
        }

        private void SetControls() {
            // We find the list in the hierarchy
            Transform list = obj.transform.Find("List");
            // We set up all the controls
            controls.Add("Edit", list.Find("E").gameObject);
            controls.Add("Add", list.Find("A").gameObject);
            controls.Add("Clone", list.Find("C").gameObject);
            controls.Add("Help", list.Find("H").gameObject);
            controls.Add("Info", list.Find("I").gameObject);
            controls.Add("Menu", list.Find("Esc").gameObject);
            controls.Add("Mass+", list.Find("Mass+").gameObject);
            controls.Add("Mass-", list.Find("Mass++").gameObject);
            controls.Add("Mass++", list.Find("Mass+++").gameObject);
            controls.Add("Mass--", list.Find("Mass-").gameObject);
            controls.Add("Mass+++", list.Find("Mass--").gameObject);
            controls.Add("Mass---", list.Find("Mass---").gameObject);
            controls.Add("FreeMove", list.Find("FreeMove").gameObject);
            //* WRITE MORE HERE (... if needed) !
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
            GameObject[] s = {
                controls["Play/Pause"],
                controls["Info"],
                controls["Menu"]
            };
            field.text = CreateString(s);
        }

        public void OnEdit() {
            // We concatenate all the strings into a well structured list
            GameObject[] s = {
                controls["Play/Pause"],
                controls["Edit"],
                controls["Add"],
                controls["Clone"],
                controls["Info"],
                controls["Menu"]
            };
            field.text = CreateString(s);
        }

        public void OnPause() {
            // We concatenate all the strings into a well structured list
            GameObject[] s = {
                controls["Play/Pause"],
                controls["Edit"],
                controls["Add"],
                controls["Clone"],
                controls["Info"],
                controls["Menu"],
                controls["Mass+"],
                controls["Mass-"],
                controls["Mass++"],
                controls["Mass--"],
                controls["Mass+++"],
                controls["Mass---"],
                controls["FreeMove"]
            };
            field.text = CreateString(s);
        }
        
        private string CreateString(params GameObject[] args) {
            // We initialize an empty string
            string s = string.Empty;
            
            foreach (GameObject arg in args) {
                s += arg + "\n";
            }

            return s;
        }
    }

    

}


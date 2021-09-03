using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace DoublePendulumProject.UI.Modules
{
    using Gameplay;

    /// <summary>
    /// This class is used to set and control the display of the information panel on the UI at runtime.
    /// The information panel should always be displayable, no matter the state of the game (except during INACTIVE state, which freeses all controls). 
    /// </summary>
    public class InfoUI : MonoBehaviour, IModuleUI
    {
        /// <summary>
        /// The root game object associated with this UI element.
        /// </summary>
        [HideInInspector]
        public GameObject obj;

        /// <summary>
        /// References to the pendulums labels (one for each set of fields).
        /// </summary>
        private Dictionary<string, InfoUILabel> labels;

        /// <summary>
        /// Reference to the PendulumA fields (TextMeshPro fields).
        /// </summary>
        private Dictionary<string, TextMeshProUGUI> fieldsA;

        /// <summary>
        /// Reference to the PendulumB fields (TextMeshPro fields).
        /// </summary>
        private Dictionary<string, TextMeshProUGUI> fieldsB;

        /// <summary>
        /// Reference to the Pendulum Manager.
        /// </summary>
        private PendulumManager pendulumManager;


        #region INITIALIZATION

        /// <summary>
        /// Initializes the object.
        /// </summary>
        public void Init() {
            // We check if the obj is NULL (in which case, we make sure we have a reference to the root object)
            if (obj == null) { obj = GameObject.Find("_INFO"); }
            // We make sure we have the reference to the pendulum manager
            if (pendulumManager == null) { pendulumManager = GameObject.Find("_Managers").transform.Find("Pendulum Manager").GetComponent<PendulumManager>(); }

            // We define all the dictionaries
            labels = new Dictionary<string, InfoUILabel>();
            fieldsA = new Dictionary<string, TextMeshProUGUI>();
            fieldsB = new Dictionary<string, TextMeshProUGUI>();


            Transform a = obj.transform.Find("Parameters A");
            Transform labelA = a.Find("Label");
            labels.Add("a", new InfoUILabel(labelA.GetComponentInChildren<TextMeshProUGUI>(), labelA.GetComponentInChildren<Image>()));
            fieldsA.Add("length", GetValue(a.Find("_Length")));
            fieldsA.Add("mass", GetValue(a.Find("_Mass")));
            fieldsA.Add("angle", GetValue(a.Find("_Angle")));
            fieldsA.Add("velocity", GetValue(a.Find("_Velocity")));

            Transform b = obj.transform.Find("Parameters B");
            Transform labelB = b.Find("Label");
            labels.Add("b", new InfoUILabel(labelB.GetComponentInChildren<TextMeshProUGUI>(), labelB.GetComponentInChildren<Image>()));
            fieldsB.Add("length", GetValue(b.Find("_Length")));
            fieldsB.Add("mass", GetValue(b.Find("_Mass")));
            fieldsB.Add("angle", GetValue(b.Find("_Angle")));
            fieldsB.Add("velocity", GetValue(b.Find("_Velocity")));
        }

        private void Awake() {
            // We initialize the object
            Init();
        }

        #endregion

        #region UPDATE

        private void Update() {
            UpdateInfo();
        }

        private void UpdateInfo() {
            // We get a reference to the currently selected double pendulum
            DoublePendulum dp = pendulumManager.selectedDoublePendulum.GetComponent<DoublePendulum>();

            // Update labels
            labels["a"].label.text = string.Format("[ Double Pendulum #{0} : Pendulum A ]", pendulumManager.selectedIndex);
            labels["b"].label.text = string.Format("[ Double Pendulum #{0} : Pendulum B ]", pendulumManager.selectedIndex);
            labels["a"].image.color = labels["b"].image.color = dp.color;

            // Update fields A fields
            fieldsA["length"].text = dp.pendulumA.length.ToString();
            fieldsA["mass"].text = dp.pendulumA.mass.ToString();
            fieldsA["angle"].text = dp.pendulumA.angle.ToString();
            fieldsA["velocity"].text = dp.pendulumA.velocity.ToString();
            // Update fields B fields
            fieldsB["length"].text = dp.pendulumB.length.ToString();
            fieldsB["mass"].text = dp.pendulumB.mass.ToString();
            fieldsB["angle"].text = dp.pendulumB.angle.ToString();
            fieldsB["velocity"].text = dp.pendulumB.velocity.ToString();
        }

        #endregion

        #region STATE SWITCH

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
            
        }

        public void OnEdit() {

        }

        public void OnPause() {

        }

        #endregion

        #region MISC

        public TextMeshProUGUI GetValue(Transform parent) {
            return parent.Find("Value").GetComponent<TextMeshProUGUI>();
        }

        #endregion
    }

}
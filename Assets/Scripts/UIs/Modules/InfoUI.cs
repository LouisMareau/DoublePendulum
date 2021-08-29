using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        [Header("CORE")]
        public GameObject obj;

        /// <summary>
        /// Reference to the PendulumA fields (TextMeshPro fields).
        /// </summary>
        private Dictionary<string, TextMeshProUGUI> fieldsA;

        /// <summary>
        /// Reference to the PendulumB fields (TextMeshPro fields).
        /// </summary>
        private Dictionary<string, TextMeshProUGUI> fieldsB;


        #region INITIALIZATION

        /// <summary>
        /// Initializes the object.
        /// </summary>
        public void Init() {
            // We check if the obj is NULL (in which case, we make sure we have a reference to the root object)
            if (obj == null) { obj = GameObject.Find("_INFO"); }

            // We define both dictionaries
            fieldsA = new Dictionary<string, TextMeshProUGUI>();
            fieldsB = new Dictionary<string, TextMeshProUGUI>();

            Transform a = obj.transform.Find("Parameters A");
            fieldsA.Add("length", GetValue(a.Find("_Length")));
            fieldsA.Add("mass", GetValue(a.Find("_Mass")));
            fieldsA.Add("angle", GetValue(a.Find("_Angle")));
            fieldsA.Add("velocity", GetValue(a.Find("_Velocity")));

            Transform b = obj.transform.Find("Parameters B");
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
            
        }

        #endregion

        #region STATE SWITCH

        /// <summary>
        /// Define what is happening when the game state changes (INACTIVE => PAUSED/EDIT <=> PLAY)
        /// </summary>
        public void OnStateSwitch() {

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
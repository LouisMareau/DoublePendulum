using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        /// Reference to the PendulumA game object.
        /// </summary>
        public GameObject pendulumAObj;

        /// <summary>
        /// Reference to the PendulumB gameObject.
        /// </summary>
        public GameObject pendulumBObj;


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
            if (obj == null) { obj = GameObject.Find("_INFO"); }
        }

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
    }

}
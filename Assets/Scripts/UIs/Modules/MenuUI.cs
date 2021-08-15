using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoublePendulumProject.UI.Modules
{
    using Gameplay;

    /// <summary>
    /// This class is used to set and control the display of the menu on the UI at runtime. <br/>
    /// The menu panel should always be displayable, no matter the state of the game (INACTIVE state included). 
    /// </summary>
    public class MenuUI : MonoBehaviour, IModuleUI
    {
        /// <summary>
        /// The root game object associated with this UI element.
        /// </summary>
        [Header("CORE")]
        public GameObject obj;

        #region UNITY METHODS
        private void Awake() {
            // We initialize the object
            Init();
        }
        #endregion

        public void Init() {
            // We check if the obj is NULL (in which case, we make sure we have a reference to the root object)
            if (obj == null) { obj = GameObject.Find("_MENU"); }
        }
        
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoublePendulumProject.UI.Modules
{
    using Gameplay;

    /// <summary>
    /// This class is used to set and control the display of the tabs on the UI at runtime. <br/>
    /// Tabs should only be displayed during the PAUSED and EDIT game states. 
    /// </summary>
    public class TabsUI : MonoBehaviour, IModuleUI
    {
        /// <summary>
        /// The root game object associated with this UI element.
        /// </summary>
        [HideInInspector]
        public GameObject obj;

        /// <summary>
        /// Prefab of the tab.
        /// </summary>
        [Header("TABS")]
        public GameObject template;

        /// <summary>
        /// List of all the tabs (each tab being associated to a double pendulum).
        /// </summary>
        public List<GameObject> tabs;

        /// <summary>
        /// The currently selected tab (must be the same as the currently selected double pendulum).
        /// </summary>
        private GameObject currentTab;

        /// <summary>
        /// The parent that will hold all the tabs in the scene.
        /// </summary>
        private Transform parent;

        /// <summary>
        /// Rerefence to the Pendulum Manager.
        /// </summary>
        private PendulumManager pendulumManager;

        #region INITIALIZATION

        public void Init() {
            // We check if the obj is NULL (in which case, we make sure we have a reference to the root object)
            if (obj == null) { obj = GameObject.Find("_TABS"); }
            // We make sure the parent that should contain all the tabs isn't NULL
            if (parent == null) { parent = obj.transform.Find("List"); }
            // We make sure we have the reference to the pendulum manager
            if (pendulumManager == null) { pendulumManager = GameObject.Find("_Managers").transform.Find("Pendulum Manager").GetComponent<PendulumManager>(); }

            // We setup the intial tab that will be displayed with the intial double pendulum
            if (pendulumManager.selectedDoublePendulum != null) { 
                //* DO THINGS HERE...
            }
        }

        private void Awake() {
            // We initialize the object
            Init();
        }

        #endregion

        #region STATE SWITCH        

        public void OnStateSwitch() {

        }

        public void OnPlay() {

        }

        public void OnEdit() {

        }

        public void OnPause() {

        }

        #endregion
    }

}
using UnityEngine;
using UnityEngine.UI;

namespace DoublePendulumProject.UI.Modules 
{
    using Gameplay;

    public class TabUI : MonoBehaviour
    {

        /// <summary>
        /// The root game object associated with this UI element.
        /// </summary>
        [HideInInspector]
        public GameObject obj;
        
        /// <summary>
        /// Reference to the global tabs UI.
        /// </summary>
        private TabsUI tabsUI;

        /// <summary>
        /// Reference to the tab's associated double pendulum.
        /// </summary>
        private DoublePendulum doublePendulum;

        /// <summary>
        /// Reference to the tab's background image.
        /// </summary>
        private Image background;

        /// <summary>
        /// Reference to the tab's color image (associated with the color of the double pendulum it is referencing).
        /// </summary>
        private Image color;

        /// <summary>
        /// Reference to the Remove (x) button used to delete the tab and its associated double pendulum.
        /// </summary>
        private Button remove;

        /// <summary>
        /// Reference to the Pendulum A button used to select the pendulum A (first pendulum) of the associated double pendulum.
        /// </summary>
        private Button pendulumA;

        /// <summary>
        /// Reference to the Pendulum B button used to select the pendulum B (second pendulum) of the associated double pendulum.
        /// </summary>
        private Button pendulumB;

        #region INITIALIZATION

        private void Init() {
            // We check if the obj is NULL (in which case, we make sure we have a reference to the root object)
            if (obj == null) { obj = this.gameObject; }
            // We get the reference to the global tabs UI
            if (tabsUI == null) { tabsUI = GameObject.Find("_UIs").transform.Find("GameUI").Find("_TabsUI").GetComponent<TabsUI>(); }
        }

        private void Awake() {
            // We initialize the object
            Init();
        }

        #endregion


    }

}
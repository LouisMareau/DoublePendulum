using UnityEngine;

namespace DoublePendulumProject.Gameplay
{
    using UI;
    
    public class DoublePendulum : MonoBehaviour
    {
        [Header("CORE")]
        public Color color;

        [Header("PENDULUMS")]
        public PendulumA pendulumA;
        public PendulumB pendulumB;

        [Header("REFERENCES")]
        public PendulumRenderer pendulumRenderer;
        public PendulumUITab associatedTab;
        [HideInInspector] public PendulumUI UI;
        [HideInInspector] public PendulumManager manager;

        #region UNITY METHODS
        protected void Awake() {
            // We find the references in the scene
            UI = GameObject.Find("_UIs").transform.GetChild(1).GetComponent<PendulumUI>();
            manager = GameObject.Find("_Managers").transform.GetChild(1).GetComponent<PendulumManager>();

            color = pendulumRenderer.globalColor;
        }
        #endregion

        private void Init(Color color) {
            this.color = color;
            pendulumRenderer.Init(color);

            //* TAB
            // We instantiate a new tab for the pendulum
            GameObject tabInstance = Instantiate<GameObject>(UI.tabTemplate, Vector3.zero, Quaternion.identity, UI.tabsT);
            // We change the name of the tab
            tabInstance.name = string.Format("Double Pendulum {0} [TAB]", UI.tabs.Count);
            // We add a tab to the UI list
            UI.tabs.Add(tabInstance);
            // We set the ref associatedTab
            PendulumUITab tab = tabInstance.GetComponent<PendulumUITab>();
            associatedTab = tab;
            // We initialize the tab
            tab.Init(this);
            // We set the tab's color
            tab.SetTabColor(pendulumRenderer.globalColor);
        }

        public void UpdatePendulums() {
            pendulumA.UpdatePendulum();
            pendulumB.UpdatePendulum();
        }

        /// <summary>
        /// Creates a default (angle and velocity values set to 0) pendulum object and its associated UI tab.
        /// </summary>
        /// <param name="color">The color of the pendulum.</param>
        public void Create(Color color) {
            // Since we are adding a new pendulum, we reset all the values
            pendulumA.Reset();
            pendulumB.Reset();

            this.color = color;
        }

        /// <summary>
        /// This method will not be creating a new instance but will instead clone all the values from a template pendulum.
        /// </summary>
        /// <param name="template">The pendulum that is going to share its values.</param>
        /// <param name="color">The color of the pendulum.</param>
        public void Clone(DoublePendulum template, Color color) {
            // We clone the values from the template and update the renders
            pendulumA.angle = template.pendulumA.angle;
            pendulumA.velocity = template.pendulumA.velocity;
            pendulumA.length = template.pendulumA.length;
            pendulumA.mass = template.pendulumA.mass;

            pendulumB.angle = template.pendulumB.angle;
            pendulumB.velocity = template.pendulumB.velocity;
            pendulumB.length = template.pendulumB.length;
            pendulumB.mass = template.pendulumB.mass;
            UpdatePendulums();

            this.color = color;
        }
    }

}

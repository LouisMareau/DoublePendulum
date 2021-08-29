using UnityEngine;

namespace DoublePendulumProject.UI 
{

    using Modules;

    /// <summary>
    /// Game UI class will target UI elements that are not part of the pendulums (play/pause buttons, menus, interactions between UIs, etc...) 
    /// </summary>
    public class GameUI : MonoBehaviour
    {
        [Header("MODULES")]
        public PlayStateUI playStateUI;
        public ControlsUI controlsUI;
        public InfoUI infoUI;
        public MenuUI menuUI;
        public TabsUI tabsUI;

        #region UNITY METHODS
        private void Awake() {
            // We intialize the object
            Init();
        }

        private void Update() {
            // H: HELP Panel
            if (Input.GetKeyDown(KeyCode.H)) {
                controlsUI.obj.SetActive(!controlsUI.obj.activeSelf);
            }

            // I: INFO Panel
            if (Input.GetKeyDown(KeyCode.I)) {
                infoUI.obj.SetActive(!infoUI.obj.activeSelf);
            }

            // ESC: MENU Panel
            if (Input.GetKeyDown(KeyCode.Escape)) {
                menuUI.obj.SetActive(!menuUI.obj.activeSelf);
            }
        }
        #endregion

        public void Init() {
            // We fetch the UI elements in the scene in case there are NULL
            if (playStateUI == null) { playStateUI = this.transform.Find("_PlayStateUI").GetComponent<PlayStateUI>(); }
            if (controlsUI == null) { controlsUI = this.transform.Find("_ControlsUI").GetComponent<ControlsUI>(); }
            if (infoUI == null) { infoUI = this.transform.Find("_InfoUI").GetComponent<InfoUI>(); }
            if (menuUI == null) { menuUI = this.transform.Find("_MenuUI").GetComponent<MenuUI>(); }
            if (tabsUI == null) { tabsUI = this.transform.Find("_TabsUI").GetComponent<TabsUI>(); }
        }

        public void OnStateSwitch() {
            playStateUI.OnStateSwitch();
            controlsUI.OnStateSwitch();
            infoUI.OnStateSwitch();
            menuUI.OnStateSwitch();
            tabsUI.OnStateSwitch();
        }
    }

}
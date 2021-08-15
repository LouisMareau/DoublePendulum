namespace DoublePendulumProject.UI.Modules {

    interface IModuleUI {
        void Init();
        void OnStateSwitch();
        void OnPlay();
        void OnEdit();
        void OnPause();
    }

}
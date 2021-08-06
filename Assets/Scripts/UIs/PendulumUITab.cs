using UnityEngine;
using UnityEngine.UI;
public class PendulumUITab : MonoBehaviour
{
    [Header("REFERENCES")]
    public PendulumManager manager;
    public PendulumUI UI;
    public DoublePendulum doublePendulum;
    [Space]
    public Image background;
    public Image tabColor;

    [Header("BUTTONS")]
    public Button remove;
    public Button btnA;
    public Button btnB;

    /// <summary>
    /// Initialize the tab object.
    /// </summary>
    /// <param name="pendulum">The pendulum associated with this tab.</param>
    public void Init(DoublePendulum doublePendulum) {
        this.manager = doublePendulum.manager;
        this.UI = doublePendulum.UI;
        this.doublePendulum = doublePendulum;

        // On 'remove' button click, we remove the pendulum
        remove.onClick.AddListener(Remove);
        // On 'ButtonA' (PendulumA), we select the pendulumA of the selected pendulum
        btnA.onClick.AddListener(() => Select(doublePendulum.pendulumA));
        // On 'ButtonB' (PendulumB), we select the pendulumB of the selected pendulum
        btnB.onClick.AddListener(() => Select(doublePendulum.pendulumB));
    }

    public void Select(Pendulum pendulum) {
        //* WE MUST BE IN EDIT MODE!
        if (this.doublePendulum.manager.isEdit) {
            //* On click, we first update the selected pendulum in UI
            UI.selectedSinglePendulum = pendulum;
            //* Then, we update the "selected" pendulum if it isn't already the case in the manager
            manager.selectedDoublePendulum = this.doublePendulum.gameObject;
            //* Then, if the tab isn't "selected", we do so and "deselect" all the others (should be gray and offset)
            //* Then, we need to update all the info on the info panel (UIInfo)
            //* Then, we need to update the UI to accomodate sliders and toggles for value twicks (in UIInfo ??)
        }
        else { Debug.Log("You must be in EDIT Mode to do this action! Please, switch to EDIT Mode and try again."); }
    }

    public void Disable() {
        
    }

    public void Remove() { manager.RemovePendulum(doublePendulum.gameObject); }

    /// <summary>
    /// Sets the color of the tab associated to the referenced pendulum.
    /// </summary>
    /// <param name="color">The color to set the tab to.</param>
    public void SetTabColor(Color color) { tabColor.color = color; }
}

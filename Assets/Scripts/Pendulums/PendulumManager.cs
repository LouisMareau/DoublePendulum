using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Global class used to manage all pendulums on screen.
/// </summary>
public class PendulumManager : MonoBehaviour
{
    [Header("DOUBLE PENDULUMS")]
    public List<GameObject> doublePendulums;
    public GameObject selectedDoublePendulum; // Can only be != NULL during EDIT mode
    private int selectedIndex;
    [Space]
    public GameObject template;

    [Header("COLORS")]
    [Range(1f, 10f)] public float colorRange = 1f; // Set to lower values to expend the spectrum

    [Header("REFERENCES")]
    public PendulumUI UI;
    [Space]
    public Transform doublePendulumsT;

    [Header("MISC")]
    public int maxPendulumCount = 6;
    [Space]
    public bool isEdit = false;

    #region UNITY METHODS
    private void Awake() {
        // We search for all the pendulums already in the scene and add them to the list
        doublePendulums = GetAllPendulums();
        selectedIndex = 0;
    }

    private void Start() {
        // We select the first pendulum in the list
        if (doublePendulums.Count > 0) {
            selectedDoublePendulum = doublePendulums[selectedIndex];
        } else {
            Debug.LogError("Cannot select a pendulum from an empty list.");
        }
    }

    private void Update() {
        // We can show infos at any time if the user pressed the "I" key (Paused simulation or not)
        if (Input.GetKeyDown(KeyCode.I)) {
            UI.ToggleInfo();
        }

        if (GameManager.state == GameState.PAUSED) {
            // When the user presses the "E" key while the simulation is PAUSED, we toggle 'isEdit' and pass the first pendulum in Edit mode (only one pendulum can be in Edit mode at the same time) if isEdit equals TRUE
            if (Input.GetKeyDown(KeyCode.E)) {
                // We toggle Edit mode
                ToggleEditMode();

                if (isEdit) { UI.RevealTabs(); }
                else { UI.ConcealTabs(); }
            }

            // When the "A" key is pressed, we add/create a new pendulum and add it to the scene
            if (Input.GetKeyDown(KeyCode.A)) { AddNewPendulum(); }
            // When the "C" key is pressed, we clone the last pendulum and add it to the scene
            if (Input.GetKeyDown(KeyCode.C)) { ClonePendulum(selectedDoublePendulum); }

            if (isEdit) {
                // On Tab (during Edit mode), we go through and select the next pendulum in the list
                if (Input.GetKeyDown(KeyCode.Tab)) {
                    // If the "Shift" key is held, we go backwards instead
                    if (Input.GetKey(KeyCode.LeftShift)) { selectedDoublePendulum = GetPrevious(); }
                    else { selectedDoublePendulum = GetNext(); }
                }
            }
        }
        else if (GameManager.state == GameState.PLAY) {
            // We force Edit mode to stop
            isEdit = false;
        }

        // We check for info
        if (UI.info.gameObject.activeSelf) {
            UI.info.UpdateInfo(selectedDoublePendulum.GetComponent<DoublePendulum>().pendulumB);
        }
    }
    #endregion

    /// <summary>
    /// Gets all the pendulums (game objects) in the scene (under the root trasnform "Pendulums").
    /// </summary>
    public List<GameObject> GetAllPendulums() {
        List<GameObject> pendulums = new List<GameObject>();

        Transform pendulumsT = GameObject.Find("Pendulums").transform;
        for (int i = 0; i < pendulumsT.childCount; i++) {
            pendulums.Add(pendulumsT.GetChild(i).gameObject);
        }

        return pendulums;
    }

    #region EDIT MODE
    private void ToggleEditMode() {
        isEdit = !isEdit;
    }

    private GameObject GetNext() {
        selectedIndex++;
        if (selectedIndex > doublePendulums.Count-1) { selectedIndex = 0; }
        return doublePendulums[selectedIndex]; 
    }

    private GameObject GetPrevious() {
        selectedIndex--;
        if (selectedIndex < 0) { selectedIndex = doublePendulums.Count-1; }
        return doublePendulums[selectedIndex];
    }
    #endregion

    /// <summary>
    /// Adds a new pendulum in the scene and adds it to the list.
    /// </summary>
    public GameObject AddNewPendulum() {
        // We make sure we do not exceed the maximum amount of available pendulums at once in the scene
        if (doublePendulums.Count < maxPendulumCount) {
            GameObject instance = Instantiate<GameObject>(this.template, GameManager.origin.position, template.transform.rotation, doublePendulumsT);
            // We add the new pendulum to the list
            doublePendulums.Add(instance);
            // We change the name of the game object by changing its index
            instance.name = string.Format("Double Pendulum {0}", doublePendulums.Count.ToString());
            
            
            // We call the Create() method from the pendulum object
            // (It will reset the pendulum values and setup a new associated tab for the UI)
            DoublePendulum pendulumObj = instance.GetComponent<DoublePendulum>();
            // We setup the color for the pendulum
            float h = 1.0f - (float)((doublePendulums.Count-1) / (maxPendulumCount * colorRange));
            Color color = Color.HSVToRGB(h, 1, 1);
            // We create the pendulum
            pendulumObj.Create(color);

            // We make sure the new pendulum is now the selected one
            return selectedDoublePendulum = instance;
        }
        else return null;
    }

    /// <summary>
    /// Clones the pendulum template, adds it to the scene and to the list.
    /// </summary>
    /// <param name="template">The template to clone from.</param>
    public GameObject ClonePendulum(GameObject template) {
        // We make sure we do not exceed the maximum amount of available pendulums at once in the scene
        if (doublePendulums.Count < maxPendulumCount) {
            GameObject instance = Instantiate<GameObject>(template, GameManager.origin.position, template.transform.rotation, doublePendulumsT);
            // We add the new clone to the list
            doublePendulums.Add(instance);
            // We change the name of the game object by changing its index
            instance.name = string.Format("Double Pendulum {0}", doublePendulums.Count.ToString());

            // We call the Clone() method from the pendulum object
            // (It will clone all the selected pendulum values and setup a new associated tab for the UI)
            DoublePendulum pendulumObj = instance.GetComponent<DoublePendulum>();
            // We setup the the color for the pendulum
            float h = 1.0f - (float)((doublePendulums.Count-1) / (maxPendulumCount * colorRange));
            Color color = Color.HSVToRGB(h, 1, 1);
            // We clone the pendulum
            pendulumObj.Clone(selectedDoublePendulum.GetComponent<DoublePendulum>(), color);

            return selectedDoublePendulum = instance;
        }
        else return null;
    }

    /// <summary>
    /// Removes the pendulum passed in parameter from the scene and removes it from the list.
    /// </summary>
    /// <param name="pendulum">The pendulum to remove.</param>
    public void RemovePendulum(GameObject pendulum) {
        if (pendulum != null) {
            // We need to remove the pendulum from the list first
            doublePendulums.Remove(pendulum);
            // We need to remove the tab from the list as well
            UI.RemoveTab(pendulum.GetComponent<DoublePendulum>().associatedTab.gameObject);
            // We can new destroy the pendulum game object
            Destroy(pendulum);
        }
    }
}
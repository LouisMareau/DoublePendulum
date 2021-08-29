using UnityEngine;
using TMPro;

namespace DoublePendulumProject.UI.Modules 
{

    public class ControlKeyUI : MonoBehaviour
    {

        /// <summary>
        /// The key associated to the input. 
        /// </summary>
        [Header("LABELS")]
        public string input;

        /// <summary>
        /// Does the input need the [ Ctrl ] modifier ?
        /// </summary>
        [Space]
        public bool useCtrl;
        /// <summary>
        /// Does the input need the [ Shift ] modifier ?
        /// </summary>
        public bool useShift;

        /// <summary>
        /// The input-related label.
        /// </summary>
        [Space]
        public string label;

        /// <summary>
        /// Reference to the input (TextMeshPro field)
        /// </summary>
        [Header("UI FIELDS")]
        public TextMeshProUGUI inputField;

        /// <summary>
        /// Shift fields contain the [ Shift ] object + the [ + ] object.
        /// </summary>
        [Space]
        public GameObject[] shiftField;
        
        /// <summary>
        /// Ctrl fields contain the [ Ctrl ] object + the [ + ] object.
        /// </summary>
        public GameObject[] ctrlField;

        /// <summary>
        /// Reference to the label (TextMeshPro field).
        /// </summary>
        [Space]
        public TextMeshProUGUI labelField;
        
        #region UNITY METHODS
        private void OnValidate() {
            // We check for empty input
            if (input != null && input != "") {
                if (inputField != null) { inputField.text = input; }
            }

            // We check for empty label
            if (label != null && label != "") {
                if (labelField != null) { labelField.text = label; }
            }

            // We check for [ CTRL ] modifier
            if (useCtrl) {
                if (ctrlField != null && ctrlField.Length == 2) {
                    foreach (GameObject obj in ctrlField) {
                        obj.SetActive(true);
                    }
                }
            } else {
                if (ctrlField != null && ctrlField.Length == 2) {
                    foreach (GameObject obj in ctrlField) {
                        obj.SetActive(false);
                    }
                }
            }

            // We check for [ SHIFT ] modifier
            if (useShift) {
                if (shiftField != null && shiftField.Length == 2) {
                    foreach (GameObject obj in shiftField) {
                        obj.SetActive(true);
                    }
                }
            } else {
                if (shiftField != null && shiftField.Length == 2) {
                    foreach (GameObject obj in shiftField) {
                        obj.SetActive(false);
                    }
                }
            }
        }
        #endregion

    }

}
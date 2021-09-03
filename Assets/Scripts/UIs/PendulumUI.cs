using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace DoublePendulumProject.UI 
{
    using Gameplay;
    
    public class PendulumUI : MonoBehaviour
    {
        [Header("TABS")]
        public List<GameObject> tabs;
        [Space]
        public GameObject tabTemplate;

        [Header("REMOVE ACTION")]
        public bool isRemoveBtnActive = false;

        [Header("REFERENCES")]
        public PendulumManager manager;
        public Transform tabsT;
        [Space]
        public Pendulum selectedSinglePendulum;

        #region UNITY METHODS
        private void OnEnable() {
            // Play short animation for showing tabs (one after another)
            //* TO FIX... ___>
            // Sequence sequence = DOTween.Sequence();
            // foreach (GameObject tab in tabs) {
            //     tab.SetActive(true);
            //     sequence.Append(tab.transform.DOLocalMoveY(10, .2f));
            // }
            // sequence.Play();
            //* <_____________
        }

        private void OnDisable() {
            // Play short animation for hiding tabs (one after another)
            //* TO FIX... ___>
            // Sequence sequence = DOTween.Sequence();
            // foreach (GameObject tab in tabs) {
            //     sequence.Append(tab.transform.DOLocalMoveY(10, .2f).OnComplete(() => tab.SetActive(false)));
            // }
            // sequence.Play();
            //* <_____________
        }

        private void Start() {
            // We initialize the UI
            Init();
        }
        #endregion

        private void Init() {
            // We initialize the list
            tabs = new List<GameObject>();
            // We add all the already existing tabs in the list
            for (int i = 0; i < tabsT.childCount; i++) {
                tabs.Add(tabsT.GetChild(i).gameObject);
            }
            // We hide the tabs
            DeactivateTabs();

            // We set the selectedSinglePendulum to be the current selected double pendulum's bob (B)
            selectedSinglePendulum = manager.selectedDoublePendulum.GetComponent<DoublePendulum>().pendulumB;

            // Set DOTweens capacity
            DOTween.SetTweensCapacity(5000,1000);
        }

        #region TABS
        /// <summary> 
        /// Deactivates all the tabs (GameObject.activeSelf = false).
        /// </summary>
        public void DeactivateTabs() {
            foreach (GameObject tab in tabs) {
                tab.SetActive(false);
            }
        }

        /// <summary> 
        /// Activates all the tabs (GameObject.activeSelf = true).
        /// </summary>
        public void ActivateTabs() {
            // We show all tabs
            foreach (GameObject tab in tabs) {
                tab.SetActive(true);
            }
        }
        
        /// <summary>
        /// Shows all the tabs (slides them downwards to see the entirety of the tab).
        /// </summary>
        public void ShowTabs() {
            // We bring all the tabs down to show all the buttons
            Sequence sequence = DOTween.Sequence();
            foreach (GameObject tabGO in tabs) {
                PendulumUITab tab = tabGO.GetComponent<PendulumUITab>();
                // If the tab is the "selected" tab, we enable it, otherwise, we disable the rest
                if (tab.doublePendulum == manager.selectedDoublePendulum.GetComponent<DoublePendulum>()) { 
                    tab.EnableTab();
                    sequence.Join(tabGO.transform.DOLocalMoveY(-110, 0.1f).SetDelay(0.05f)); 
                }
                else { 
                    tab.DisableTab();
                    sequence.Join(tabGO.transform.DOLocalMoveY(-70, 0.1f).SetDelay(0.05f)); 
                }
                
            }
            sequence.Play().OnComplete(() => sequence.Kill());
        }
        
        /// <summary>
        /// Hides all the tabs (stick them to the very top of the screen, leaving just the color showing).
        /// </summary>
        public void HideTabs() {
            // We bring back all the tabs back to hide all the buttons
            Sequence sequence = DOTween.Sequence();
            foreach (GameObject tabGO in tabs) {
                PendulumUITab tab = tabGO.GetComponent<PendulumUITab>();
                // We disable every tab
                tab.DisableTab();
                sequence.Join(tabGO.transform.DOLocalMoveY(-40, 0.1f).SetDelay(0.05f));
            }
            sequence.Play().OnComplete(() => sequence.Kill());
        }
        
        /// <summary>
        /// Updates the display of all tabs (the selected tab is more visible than the others).
        /// </summary>
        public void UpdateTabs() {
            // It is the same as showing all the tabs for the first time (values unchanged will not animate)
            ShowTabs(); 
        }

        /// <summary>
        /// Removes the tab in parameter.
        /// </summary>
        /// <param name="tab">The tab to remove.</param>
        public void RemoveTab(GameObject tab) {
            // We first remove then destroy the tab
            tabs.Remove(tab);
            Destroy(tab);
        }

        
        #region REMOVE BUTTONS
        public void ShowRemoveButtons() {
            foreach (GameObject tabGO in tabs) {
                PendulumUITab tab = tabGO.GetComponent<PendulumUITab>();
                // We show the remove button
                tab.ShowRemoveBtn();
            }
        }

        public void HideRemoveButtons() {
            foreach (GameObject tabGO in tabs) {
                PendulumUITab tab = tabGO.GetComponent<PendulumUITab>();
                // We hide the remove button
                tab.HideRemoveBtn();
            }
        }

        public void ToggleRemoveButtons() { isRemoveBtnActive = !isRemoveBtnActive; }
        #endregion
        #endregion
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DoublePendulumProject.Gameplay 
{
    using UI;

    public class PendulumInteract : MonoBehaviour
    {
        [Header("REFERENCES")]
        public PendulumUI UI;
        public GameManager gameManager;
        public PendulumManager manager;
        [Space(10)]
        public DoublePendulum doublePendulum;

        private Pendulum pendulum;

        [Header("MASS")]
        public float massIncrement = 0.01f;

        private void Awake() {
            pendulum = transform.parent.GetComponent<Pendulum>();
        }

        private Vector2 GetNewBobPosition() {
            return ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - pendulum.origin);
        }

        private void SetNewAngleFromMouse() {
            float newAngle = Mathf.Atan2(GetNewBobPosition().x, GetNewBobPosition().y);
            pendulum.angle = newAngle;
        }

        private void OnMouseDrag() {
            // We update the UI selectedSinglePendulum to be this object (we can do that anytime during runtime)
            UI.selectedSinglePendulum = pendulum;
            manager.selectedDoublePendulum = doublePendulum.gameObject;

            if (manager.isEdit) {
                UI.UpdateTabs();
            }

            if (GameManager.state == GameState.PAUSED) {
                // We use camera screen to world point conversion to get the position of the new bob and assign that value to the pendulum's length
                pendulum.length = GetNewBobPosition().magnitude;
                // We update the angle
                SetNewAngleFromMouse();

                // pendulumA.velocity = 0;
                // pendulumB.velocity = 0;
                // pendulumA.acceleration = 0;
                // pendulumB.acceleration = 0;

                //pendulumB.trail.Clear();

                // We make sure we update both pendulums to reflect each others modifications
                doublePendulum.UpdatePendulums();
            }
        }

        private void OnMouseOver() 
        {
            if (GameManager.state == GameState.PAUSED) {
                if (Input.GetAxis("Mouse ScrollWheel") > 0) {
                    if (Input.GetKey(KeyCode.LeftShift)) {
                        if (Input.GetKey(KeyCode.LeftAlt)) { pendulum.mass += massIncrement * 100f; }
                        else { pendulum.mass += massIncrement * 10f; }
                    }
                    else { pendulum.mass += massIncrement; }
                    pendulum.UpdateScale();
                }
                if (Input.GetAxis("Mouse ScrollWheel") < 0) {
                    if (Input.GetKey(KeyCode.LeftShift)) { 
                        if (Input.GetKey(KeyCode.LeftAlt)) { pendulum.mass -= massIncrement * 100f; }
                        else { pendulum.mass -= massIncrement * 10f; } 
                    }
                    else { pendulum.mass -= massIncrement; }
                    pendulum.UpdateScale();
                }
            }    
        }
    }

}
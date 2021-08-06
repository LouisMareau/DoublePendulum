using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumInteract : MonoBehaviour
{
    [Header("REFERENCES")]
    public PendulumUI UI;
    public GameManager gameManager;
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
        if (GameManager.state == GameState.PAUSED) {
            pendulum.length = GetNewBobPosition().magnitude;
            SetNewAngleFromMouse();

            // pendulumA.velocity = 0;
            // pendulumB.velocity = 0;
            // pendulumA.acceleration = 0;
            // pendulumB.acceleration = 0;

            //pendulumB.trail.Clear();

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumA : Pendulum
{
    [Header("REFERENCES")]
    private PendulumB pendulumB;

    #region UNITY METHODS
    private void OnValidate() {
        pendulumB = doublePendulum.pendulumB;

        // We update this pendulum
        UpdatePendulum();
        // We update the second pendulum as well 
        pendulumB.UpdatePendulum();
    }

    private void Awake() {
        pendulumB = doublePendulum.pendulumB;
    }

    private void FixedUpdate() {
        if (GameManager.state == GameState.PLAY) {
            this.velocity += CalculatePendulumA_AngularAcceleration() * Time.fixedDeltaTime;
            this.angle += this.velocity;

            Dampen(Const.dampening);
            
            UpdatePendulum();
        }
    }
    #endregion

    public void UpdatePendulum() {
        origin = GameManager.origin.position;
        SetLineAndBob();
    }

    private float CalculatePendulumA_AngularAcceleration() {
        float numerator1 = -Const.g * (2 * this.mass + pendulumB.mass) * Mathf.Sin(this.angle);
        float numerator2 = -pendulumB.mass * Const.g * Mathf.Sin((this.angle - 2 * pendulumB.angle));
        float numerator3 = -2 * Mathf.Sin((this.angle - pendulumB.angle)) * pendulumB.mass;
        float numerator4 = pendulumB.velocity * pendulumB.velocity * pendulumB.length + this.velocity * this.velocity * this.length * Mathf.Cos(this.angle - pendulumB.angle);
        float denominator = this.length * (2 * this.mass + pendulumB.mass - pendulumB.mass * Mathf.Cos(2 * this.angle - 2 * pendulumB.angle));

        return (numerator1 + numerator2 + numerator3 * numerator4) / denominator;
    }

    public override void Reset() {
        base.Reset();
        UpdatePendulum();
    }
}
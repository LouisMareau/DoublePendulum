using UnityEngine;

namespace DoublePendulumProject.Gameplay 
{

    public class PendulumB : Pendulum
    {
        [Header("REFERENCES")]
        private PendulumA pendulumA;

        [Header("TRAIL")]
        public TrailRenderer trail;

        #region UNITY METODS
        private void OnValidate() {
            pendulumA = doublePendulum.pendulumA;
            // We update this pendulum
            UpdatePendulum();
            // We update the second pendulum as well
            pendulumA.UpdatePendulum();
        }

        private void Awake() {
            pendulumA = doublePendulum.pendulumA;
        }

        private void FixedUpdate() {
            if (GameManager.state == GameState.PLAY) {
                this.velocity += CalculatePendulumB_AngularAcceleration() * Time.fixedDeltaTime;
                this.angle += this.velocity;

                Dampen(Const.dampening);

                this.UpdatePendulum();
            }
        }
        #endregion

        public void UpdatePendulum() {
            origin = new Vector2(pendulumA.x, pendulumA.y);
            SetLineAndBob();
        }

        private float CalculatePendulumB_AngularAcceleration() {
            float numerator1 = 2 * Mathf.Sin(pendulumA.angle - this.angle);
            float numerator2 = pendulumA.velocity * pendulumA.velocity * pendulumA.length * (pendulumA.mass + this.mass);
            float numerator3 = Const.g * (pendulumA.mass + this.mass) * Mathf.Cos(pendulumA.angle);
            float numerator4 = this.velocity * this.velocity * this.length * this.mass * Mathf.Cos(pendulumA.angle - this.angle);
            float denominator = this.length * (2 * pendulumA.mass + this.mass - this.mass * Mathf.Cos(2 * pendulumA.angle - 2 * this.angle));

            return (numerator1 * (numerator2 + numerator3 + numerator4)) / denominator;
        }

        public override void Reset() {
            base.Reset();
            UpdatePendulum();
        }
    }

}
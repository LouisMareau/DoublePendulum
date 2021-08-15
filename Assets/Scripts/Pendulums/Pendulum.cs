using UnityEngine;

namespace DoublePendulumProject.Gameplay
{

    public class Pendulum : MonoBehaviour
    {
        [Header("REFERENCES")]
        public DoublePendulum doublePendulum;
        public GameObject line;
        public GameObject bob;
        [Space]
        public Transform bobRenderer;
        public CircleCollider2D bobCollider;
        [Space]
        public TrailRenderer trailRenderer;

        [Header("DATA POINTS")]
        [HideInInspector] public Vector2 origin;
        [HideInInspector] public float x;
        [HideInInspector] public float y;
        
        [Header("BODY VARIABLES")]
        public float length;
        public float mass;
        [Space(10)]
        public float minLength;
        public float maxLength;
        public float minMass;
        public float maxMass;

        [Header("ANGULAR MOMENTUM")]
        public float angle;
        public float velocity;

        public float AngleToRadians(float angle) {
            return angle * (Mathf.PI / 180);
        }

        public void UpdateScale() {
            // Set sprite scale (on the transform)
            bobRenderer.localScale = new Vector2(mass, mass);
            // Set circle collider radius
            bobCollider.radius = mass;
            // Set trail start width
            trailRenderer.startWidth = mass;
        }

        public void SetLineAndBob() 
        {
            // We calculate the end of the line
            x = length * Mathf.Sin(angle);
            y = length * Mathf.Cos(angle);

            // We update the position of the object
            this.transform.position = origin;

            // Update line position and lineRenderer points
            this.line.GetComponent<LineRenderer>().SetPosition(0, Vector2.zero);
            this.line.GetComponent<LineRenderer>().SetPosition(1, new Vector2(x,y));

            // Update bob position and scale
            this.bob.transform.localPosition = new Vector2(x,y);
            UpdateScale();
        }
        
        public void Dampen(float dampenValue) { this.angle *= 1 - dampenValue; }

        public virtual void Reset() {
            angle = 0;
            velocity = 0;
        }
    }

}
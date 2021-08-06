using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{
    [Header("REFERENCES")]
    public DoublePendulum doublePendulum;
    public GameObject line;
    public GameObject bob;


    [Header("DATA POINTS")]
    [HideInInspector] public Vector2 origin;
    public float x;
    public float y;
    
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

    #region UNITY METHODS
    #endregion

    public float AngleToRadians(float angle) {
        return angle * (Mathf.PI / 180);
    }

    public void UpdateScale() {
        this.bob.transform.localScale = new Vector2(mass/4, mass/4);
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

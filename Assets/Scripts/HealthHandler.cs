using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthHandler : MonoBehaviour
{

    public float maxHealth;
    public GUIStyle style;
    private float health;
    Rect box = new Rect(10.0f, Screen.height - 30.0f, 100.0f, 20.0f);

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0.0f) {
            Kill();
        }
    }

    void OnGUI()
    {
        GUI.Label(box, "Health: " + health.ToString(), style);
    }

    public void Damage(float damage)
    {
        print("oof");
        health = Mathf.Clamp(health - damage, 0.0f, maxHealth);
    }

    public void Heal(float damage)
    {
        health = Mathf.Clamp(health + damage, 0.0f, maxHealth);
    }

    public void Kill()
    {
        print("Dead");
    }
}

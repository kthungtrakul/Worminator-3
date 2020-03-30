using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public static float CurrentHealth { get; set; } = 100f;
    public static float MaxHealth { get; set; } = 100f;

    public RectTransform healthBarGauge;

    public static bool Dead { get; set; } = false;

    private float MaxHealthPos
    {
        get
        {
            return 0f;
        }
    }
    private float NoHealthPos
    {
        get
        {
            return -816f;
        }
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    // Update is called once per frame
    protected virtual void LateUpdate()
    {
        if (CurrentHealth > MaxHealth) CurrentHealth = MaxHealth;
        MoveBar();
    }

    protected virtual void MoveBar()
    {
        float currentPos = CalculateFloatBar.Compute(CurrentHealth, 0, MaxHealth, NoHealthPos, MaxHealthPos);
        Image healthBarImage = healthBarGauge.GetComponent<Image>();
        healthBarGauge.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, currentPos, 855f); //the last parameter is the width of the Rect the gauge is contained in.
        if ((0.5f * MaxHealth) <= CurrentHealth && CurrentHealth <= MaxHealth) healthBarImage.color = new Color(CalculateFloatBar.Compute(CurrentHealth, 0.5f * MaxHealth, MaxHealth, 1f, 0), 1, 0, 0.75f);
        if ((0.25f * MaxHealth) <= CurrentHealth && CurrentHealth < (0.5f * MaxHealth)) healthBarImage.color = new Color(1, CalculateFloatBar.Compute(CurrentHealth, 0.25f * MaxHealth, 0.5f * MaxHealth, 0, 1f), 0, 0.75f);
    }
}

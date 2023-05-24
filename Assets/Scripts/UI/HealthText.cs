using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
  public Vector3 floatingSpeed = new Vector3(0, 80, 0);
  private RectTransform textTransform;
  public float fadingTime = 1f;
  public TextMeshProUGUI textMeshProHealth;
  private float timeElapsed = 0f;
  private Color color;
  private void Awake()
  {
    textTransform = GetComponent<RectTransform>();
    textMeshProHealth = GetComponent<TextMeshProUGUI>();
  }

  private void Update()
  {
    textTransform.position += floatingSpeed * Time.deltaTime;
    timeElapsed += Time.deltaTime;

    if (timeElapsed < fadingTime)
    {
      float fadeAlpha = color.a * (1 - (timeElapsed / fadingTime));
      textMeshProHealth.color = new Color(color.r, color.g, color.b, fadeAlpha);
    }
    else
    {
      Destroy(gameObject);
    }
  }
}

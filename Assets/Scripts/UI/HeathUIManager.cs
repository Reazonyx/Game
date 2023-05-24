using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

[RequireComponent(typeof(PlayerHealth),(typeof(Animator)))]
public class UIManager : MonoBehaviour
{
    private PlayerHealth playerHealth;
    public GameObject healTextPrefab;
    public GameObject damageTextPrefab;
    [FormerlySerializedAs("gameCanvas")] public Canvas healthCanvas;
    public Camera followCamera;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        var objectWithTag = GameObject.FindWithTag("HealthCanvas");
        if (objectWithTag == null) return;
        healthCanvas = objectWithTag.GetComponent<Canvas>();
        
        followCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }

    private void OnEnable()
    {
        playerHealth.playerDamaged.AddListener(PlayerDamaged);
        playerHealth.playerHealed.AddListener(PlayerHealed);
    }

    private void OnDisable()
    {
        playerHealth.playerDamaged.RemoveListener(PlayerDamaged);
        playerHealth.playerHealed.RemoveListener(PlayerHealed);
    }

    private void PlayerDamaged(GameObject player, int damageReceived)
    {
        Vector3 spawnPosition = followCamera.WorldToScreenPoint(player.transform.position);
        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, healthCanvas.transform)
            .GetComponent<TMP_Text>();
        tmpText.text = damageReceived.ToString();
    }

    private void PlayerHealed(GameObject player, int healReceived)
    {
        Vector3 spawnPosition = followCamera.WorldToScreenPoint(player.transform.position);
        TMP_Text tmpText = Instantiate(healTextPrefab, spawnPosition, Quaternion.identity, healthCanvas.transform)
            .GetComponent<TMP_Text>();
        tmpText.text = healReceived.ToString();
    }
}
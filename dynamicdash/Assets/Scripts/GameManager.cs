using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int _score;
    private void OnEnable()
    {
        CollisionDetection.OnObjectCollected += UpdateUI;
        PlayerController.OnReachedFinishLine += Victory;
    }

    private void OnDisable()
    {
        CollisionDetection.OnObjectCollected -= UpdateUI;
        PlayerController.OnReachedFinishLine -= Victory;
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void Victory()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void UpdateUI()
    {
        _score++;
        _scoreText.text = $" Score: {_score}";
    }
}

using TMPro;
using UnityEngine;
using Zenject;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    [Inject] private ScoreManager _scoreManager;

    private void Update()
    {
        UpdateScoreDisplay();
    }

    public void UpdateScoreDisplay()
    {
        if (_scoreText != null && _scoreManager != null)
        {
            _scoreText.text = $"{_scoreManager.TotalScore}";
        }
        else 
        {
            Debug.Log("UI missing");
        }
    }
}
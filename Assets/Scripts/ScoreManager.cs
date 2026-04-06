using UnityEngine;

public class ScoreManager
{
    private int _currentFrameScore = 0;     
    private int _pendingBonus = 0;          
    private int _bonusValue = 0;             

    public int TotalScore { get; private set; }

    public void ScoreLogic(int fallenPins, bool isFirstThrow, bool isStrike, bool isSpare)
    {
        if (isFirstThrow)
            _currentFrameScore = fallenPins;
        else
            _currentFrameScore += fallenPins;

        if (_pendingBonus > 0)
        {
            TotalScore += fallenPins * (_pendingBonus == 2 ? 2 : 1);
            _pendingBonus--;
        }

        if (isStrike || !isFirstThrow)
        {
            if (isStrike)
            {
                TotalScore += 10;
                _pendingBonus += 2;
                Debug.Log("Strike!");
            }
            else if (isSpare)
            {
                TotalScore += 10;
                _pendingBonus += 1;
                Debug.Log("Spare!");
            }
            else
            {
                TotalScore += _currentFrameScore;
            }

            _currentFrameScore = 0;
        }

        Debug.Log($"Total Score: {TotalScore}");
    }
}
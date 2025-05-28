using TMPro;
using UnityEngine;

public class PlayerScore : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI playerScore;
    private float score = 0;
    public int displaydScore = 0;
    

    
    void Update()
    {
        score += Time.deltaTime * 2;
        displaydScore = (int)score;
        playerScore.SetText(displaydScore.ToString());
    }
}

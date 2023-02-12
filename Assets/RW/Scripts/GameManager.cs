using UnityEngine;

/*<summary>
 Manages scores and misses.
</summary> */ 


public enum State { Menu, Level };

public class GameManager : MonoBehaviour
{
    public GameObject menu;
    public GameObject level;
    public State gameState;
    public GameObject[] sabers;
    public int highScore { get; set; } = 0;
    public int score { get; set; } = 0;
    public int misses = 0;
    public int maxMisses = 20;//to test

    public void Start()
    {
       // ChangeState(gameState);
    }

    public void Update()
    {
        if (gameState == State.Menu && sabers[0].transform.parent && sabers[1].transform.parent){
            ChangeState(State.Level);
        }
        if (misses> maxMisses){
            if(score > highScore){
                highScore = score;
            }
            ChangeState(State.Menu);
        } 
    }

    public void ChangeState(State state)
    {
        gameState = state;
        if (state == State.Menu){
            menu.SetActive(true);
            level.SetActive(false);
        }
        if(state==State.Level){
            score=0;
            misses=0;
            menu.SetActive(false);
            level.SetActive(true);
        }
    }
}

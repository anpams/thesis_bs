using TMPro; //text mesh
using UnityEngine;

public class TrapMisses : MonoBehaviour
{
    public GameObject gameManager;
    public GameObject quickMessage;

    void OnTriggerEnter(Collider other)
    {
        if (gameManager){
            gameManager.GetComponent<GameManager>().misses +=1;
        }

        //Create message
        GameObject textMessage = Instantiate(quickMessage);
        textMessage.transform.position= gameObject.transform.position;
        textMessage.GetComponent<TextMeshPro>().text="Missed!";

        //destroy missed gameObject
        Destroy(other.gameObject); //is not working??
    }
}

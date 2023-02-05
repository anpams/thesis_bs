using UnityEngine;

/// <summary>
/// Generate Veggies based on instructions of a Song.
/// </summary>
public class VeggieGenerator : MonoBehaviour
{
    // Prefab to be spawned.
    //public GameObject[] veggies;
    public GameObject[] cubes;

    // Interval between notes
    public float BPM = 172f;

    // Counter to spawn another note.
    private float counter = 0f;

    // How often cubes appear.
    public float StartCutoff = 0.3f; //change to beat
    private float cutoff;
    
    // Have 4 unique starting positions for notes.
    private float[,] startPositions =
    {
       // {0, 0, 0 },
        {-0.75f, -0.50f, 0 },
        {0.75f, -0.50f, 0 },
        //{-0.35f, -.35f, 0 },
       // {0.35f, -.35f, 0 },
      //  {0.0f, 0f, 0 }
    };

    // Reset the rate veggies appear at the start.
    void OnEnable()
    {
        cutoff = StartCutoff;
    }

    void Update()
    {
        counter += Time.deltaTime;
        float beatInterval = 60.0f/BPM;

        if (counter> beatInterval){ //appear with beat
            counter=0f;

            if(Random.Range(0.0f, 1.0f)< cutoff){
                CreateVeggie();
            }
            cutoff += 0.01f; //move them?
        }
    }

    void CreateVeggie()
    {
       // if(veggies.Length ==0) return;
        if(cubes.Length==0) return;
        Debug.Log("cubes.Length" + cubes.Length);
        //instatiate random model
      /*  int randomVeggie = Random.Range (0, veggies.Length -1);
        GameObject veggie = Instantiate(veggies[randomVeggie]);
        veggie.transform.position= transform.position; */
        int randomCube = Random.Range (0, cubes.Length);
        Debug.Log("int randomCube: " +randomCube);

        GameObject cube = Instantiate(cubes[randomCube]);
        cube.transform.position= transform.position;

        //choose line
        int pos = Random.Range(0,2);
        Vector3 destination= transform.position + new Vector3(startPositions[pos,0], startPositions[pos,1], startPositions[pos,2]);

        //add a behaviour component
        //        VeggieBehaviour comp=(VeggieBehaviour) veggie.AddComponent(typeof(VeggieBehaviour));

        VeggieBehaviour comp=(VeggieBehaviour) cube.AddComponent(typeof(VeggieBehaviour));
        comp.movement = new Vector3(0,0,-6);
        comp.destination = destination;
    }
}

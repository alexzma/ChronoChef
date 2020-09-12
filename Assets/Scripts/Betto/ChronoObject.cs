using UnityEditor;
using UnityEngine;

// Used for objects that are affected by chrono bombs
public class ChronoObject : MonoBehaviour
{
    [SerializeField] private Transform[] timePrefabs;
    [SerializeField] private int startField;
    private int timeState;
    private int numStates;
    //private Collider2D collider;
    private SpriteRenderer spriteRenderer;
    //private Transform transform;
    // Start is called before the first frame update
    void Awake()
    {
        this.numStates = timePrefabs.Length;
        this.timeState = startField;
        //this.collider = GetComponent<Collider2D>();
        this.spriteRenderer = GetComponent<SpriteRenderer>();

        //transform = GetComponent<Transform>();
        //PrefabUtility.InstantiatePrefab(timePrefabs[this.timeState], transform);
        GameObject child = Instantiate(timePrefabs[timeState].gameObject, transform.position, Quaternion.identity) as GameObject;
        child.transform.parent = this.transform;
        string name = child.transform.name;
        if (name.Contains("(Clone)"))
            child.transform.name = name.Substring(0, name.Length - 7);
    }

    public int GetTimeState()
    {
        return timeState;
    }

    // Advances or retreats the time state of the object
    public void ChangeTimeState(int timeChanged)
    {
        this.timeState = Mathf.Clamp(this.timeState + timeChanged, 0, this.numStates - 1);
        if (transform.childCount == 1)
        {
            Destroy(transform.GetChild(0).gameObject);
            //PrefabUtility.InstantiatePrefab(timePrefabs[this.timeState], transform);
            GameObject child = Instantiate(timePrefabs[timeState].gameObject, transform.position, Quaternion.identity) as GameObject;
            child.transform.parent = this.transform;
        }
        else
        {
            Debug.LogError("There is more than 1 children on this chronoable object");
        }

        //spriteRenderer.sprite = timeSprites.spriteInfos[this.timeState].sprite;
        //collider.enabled = timeSprites.spriteInfos[this.timeState].isObstacle;
    }
}

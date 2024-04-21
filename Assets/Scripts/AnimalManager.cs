using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AnimalManager : MonoBehaviour
{
    // Animal name. Used to determine the animation state names.
    [SerializeField]
    private string animalName = "Lion";
    // Simple timer.
    private bool timeOn = false;
    private float countDown = 0;
    // The start position.
    private Vector3 home = new Vector3();
    // The player.
    private GameObject player;
    // Current Speed.
    private float currentSpeed = 3;
    // A reference to the animator.
    private Animator anim;
    // Store the current AnimatorStateInfo.
    AnimatorStateInfo currentStateInfo;
    // Hashes to animator parameters.
    private int walkHash = Animator.StringToHash("Walk");
    // Start is called before the first frame update
    void Start()
    {
        home = transform.position;
        home.y = home.y + 1;
        player = GameObject.FindWithTag("Player");
        anim = GetComponent<Animator>();
        // Set an initial wait time between 1 and 6 seconds.
        timeOn = true;
        countDown = Random.Range(1.0f, 6.0f);
        currentSpeed = Random.Range(2.0f, 12.0f);
    }
    // Update is called once per frame
    void Update()
    {
        // Simple countdown timer to pause the animal.
        if (timeOn)
        {
            countDown = countDown - Time.deltaTime;
            if (countDown <= 0)
            {
                // End timer.
                timeOn = false;
                transform.position = home;
            }
            else
            {
                return;
            }
        }
        AnimalWalk();
    }
    private void AnimalWalk()
    {
        // Determine the direction to the player.
        Vector3 direction = player.transform.position - transform.position;
        direction.y = 0;
        // Calculates the length of the relative position vector
        float distance = direction.magnitude;
        // Face in the right direction.
        if (direction.magnitude > 0)
        {
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = rotation;
        }
        // Calculate the normalised direction to the target from a game object.
        Vector3 normDirection = direction / distance;
        // Set the walk parameter for the animator.
        anim.SetInteger(walkHash, 1);
        // Move the game object.
        currentStateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (currentStateInfo.IsName(animalName + "Walk"))
        {
            transform.position = transform.position + normDirection * currentSpeed * Time.deltaTime;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Projectile"))
        {
            // Destory the projectile.
            Destroy(collision.gameObject);
            // hide the animal.
            transform.position = new Vector3(0, -1000, 0);
            // Set a wait time between 1 and 6 seconds.
            timeOn = true;
            countDown = Random.Range(2.0f, 8.0f);
            currentSpeed = Random.Range(2.0f, 12.0f);
        }
    }
}
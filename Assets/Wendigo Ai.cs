using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;  // Import UI namespace to interact with the Image

[RequireComponent(typeof(NavMeshAgent))]
public class WendigoAI : MonoBehaviour
{
    public string playerTag = "Player";  // Tag to search for player
    public float detectionRadius = 1f;  // Radius for detecting the player
    public float speed = 2f;  // Movement speed
    public float spawnTime = 5f;  // Time in seconds before the Wendigo spawns
    public Image jumpscareImage;  // The Image UI component for the jumpscare

    private bool playerDetected = false;  // Is the player detected?
    private bool wendigoSpawned = false;  // Has the Wendigo spawned?
    private Transform player;  // Player's Transform reference
    private NavMeshAgent navMeshAgent;  // NavMeshAgent for AI movement
    private float timer;  // Timer for spawning the Wendigo

    private void Start()
    {
        // Initialize the NavMeshAgent
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = speed;

        // Try to find the player in the scene by tag
        player = GameObject.FindGameObjectWithTag(playerTag)?.transform;

        // Set up the detection radius as a trigger collider
        SphereCollider detectionCollider = gameObject.AddComponent<SphereCollider>();
        detectionCollider.isTrigger = true;
        detectionCollider.radius = detectionRadius;

        // Initialize the timer
        timer = spawnTime;

        // Make the jumpscare image invisible at the start
        if (jumpscareImage != null)
        {
            jumpscareImage.enabled = false;  // Disable the image initially
        }
    }

    private void Update()
    {
        // Handle the timer countdown
        if (!wendigoSpawned)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                // Spawn the Wendigo
                wendigoSpawned = true;
                StartChasingPlayer();
            }
        }

        // If the Wendigo has spawned and the player is detected, follow the player
        if (wendigoSpawned && playerDetected && player != null)
        {
            FollowPlayer();
        }
    }

    private void StartChasingPlayer()
    {
        if (player != null)
        {
            // Start moving towards the player
            navMeshAgent.SetDestination(player.position);
        }
    }

    private void FollowPlayer()
    {
        if (player == null) return;
        navMeshAgent.SetDestination(player.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerDetected = true;
        }

        // If the Wendigo touches the player, trigger the jumpscare
        if (other.CompareTag(playerTag) && wendigoSpawned)
        {
            TriggerJumpscare();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            playerDetected = false;
        }
    }

    private void TriggerJumpscare()
    {
        // Make the jumpscare image visible
        if (jumpscareImage != null)
        {
            jumpscareImage.enabled = true;  // Enable the image to show the jumpscare
        }

        // Optionally, you can play sound, animation, or other effects here
        Debug.Log("Wendigo has touched the player! Jumpscare triggered!");

        // Optionally, you can destroy the Wendigo or perform other actions
        Destroy(gameObject);  // Destroy the Wendigo after the jumpscare (optional)
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}

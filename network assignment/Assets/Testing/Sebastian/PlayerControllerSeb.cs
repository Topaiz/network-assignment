using Alteruna;
using UnityEngine;
using Avatar = Alteruna.Avatar;


[RequireComponent(typeof(Avatar)), RequireComponent(typeof(CharacterController))]
public class PlayerControllerSeb : MonoBehaviour
{
    [SerializeField] private Avatar avatar;

    // [SerializeField] private Spawner spawner;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private GameObject bulletPrefab;
    // private int BulletIndex;

    void Start()
    {
        //InitializeComponents(); // Would make sure things don't brake, but at a small performance cost, maybe better things break early.

        // spawner.SpawnableObjects.Clear();
        // spawner.SpawnableObjects.Add(bulletPrefab);
        // BulletIndex = spawner.SpawnableObjects.IndexOf(bulletPrefab);

        if (!avatar.IsMe)
        {
            return;
        }
    }

    void Update()
    {
        if (!avatar.IsMe)
        {
            return;
        }

        Vector2 inputVec = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        characterController.Move(inputVec * (speed * Time.deltaTime));
        if (inputVec != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(transform.forward, inputVec);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Transform spawnTransform = transform;
        Instantiate(bulletPrefab, spawnTransform.position, spawnTransform.rotation);
        //spawner.Spawn(BulletIndex, spawnTransform.position, spawnTransform.rotation);
    }

    private void Reset()
    {
        InitializeComponents();
    }

    void InitializeComponents()
    {
        if (avatar == null)
        {
            avatar = GetComponent<Avatar>();
        }

        // if (spawner == null)
        // {
        //     spawner = GetComponent<Spawner>();
        // }

        if (characterController == null)
        {
            characterController = GetComponent<CharacterController>();
        }
    }
}
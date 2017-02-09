using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour
{

    public float speed = 10f;

    private float lastSynchronizationTime = 0f;
    private float syncDelay = 0f;
    private float syncTime = 0f;
    private Vector2 syncStartPosition = Vector2.zero;
    private Vector2 syncEndPosition = Vector2.zero;

    private float syncgravityScale = 1f;

    private Vector3 syncPosition = Vector3.zero;
    private Vector3 syncVelocity = Vector3.zero;
    private float gravityScale = 1f;
    private RaycastHit _hit;
    private ManaBar _mana;
    private bool _isServer;

    void Awake()
    {
        lastSynchronizationTime = Time.time;

    }

    void FixedUpdate()
    {
        if (!GetComponent<NetworkView>().isMine)
            SyncedMovement();
    }

    private void SyncedMovement()
    {
        syncTime += Time.deltaTime;
        GetComponent<Rigidbody2D>().position = Vector2.Lerp(syncStartPosition, syncEndPosition, syncTime / syncDelay);
        GetComponent<Rigidbody2D>().gravityScale = syncgravityScale;
    }

    void OnSerializeNetworkView(BitStream stream, NetworkMessageInfo info)
    {
        syncPosition = GetComponent<Rigidbody2D>().position;
        syncVelocity = GetComponent<Rigidbody2D>().velocity;
        gravityScale = GetComponent<Rigidbody2D>().gravityScale;

        stream.Serialize(ref syncPosition);
        stream.Serialize(ref syncVelocity);
        stream.Serialize(ref gravityScale);

        if (stream.isReading)
        {

            syncTime = 0f;
            syncDelay = Time.time - lastSynchronizationTime;
            lastSynchronizationTime = Time.time;

            syncEndPosition = syncPosition + syncVelocity * syncDelay;
            syncStartPosition = GetComponent<Rigidbody2D>().position;
            syncgravityScale = gravityScale;
        }

    }

    void Update()
    {

    }
    void Start()
    {

    }


}

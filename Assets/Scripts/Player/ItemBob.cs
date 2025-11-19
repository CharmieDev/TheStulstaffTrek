using System;
using Unity.Cinemachine;
using UnityEngine;

public class ItemBob : MonoBehaviour
{
    [SerializeField] private PlayerController player;

    [Header("Bobbing Settings")]
    public float bobFrequency = 6f;   // how fast the bob cycles
    public float bobAmplitude = 0.05f; // bob height
    public float smooth = 10f;        // how snappy the transition is
    public float stepThreshold = 0.9f;// how deep in the sine wave to trigger
    
    private bool stepped;
    private float bobTimer;
    private Vector3 startPos;
    
    private void Start()
    {
        startPos = transform.localPosition;
    }

    private void Update()
    {
        // Are we moving?
        if (player.Rb.linearVelocity.magnitude > 0.1f)
        {
            bobTimer += Time.deltaTime * bobFrequency;
            float bobValue = Mathf.Sin(bobTimer);
            
            float offsetY = Mathf.Sin(bobTimer) * bobAmplitude;
            float offsetX = Mathf.Cos(bobTimer * 0.5f) * (bobAmplitude * 0.5f);

            // Apply offset smoothly
            Vector3 targetPos = startPos + new Vector3(offsetX, offsetY, 0f);
            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * smooth);
            
            if (!stepped && bobValue < -stepThreshold)
            {
                player.PlayFootstep();
                stepped = true;
            }
            else if (stepped && bobValue > 0)
            {
                stepped = false; // ready for next step
            }
        }
        else
        {
            // Reset when standing still
            stepped = false;
            bobTimer = 0f;
            transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, Time.deltaTime * smooth);
        }
    }
}

using UnityEngine;

public class BellRandomizer : Singleton<BellRandomizer>
{
    public bool BellActive;
    [SerializeField] private float frequency = 0.25f, chance = 0.5f;
    private float timer;
    
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (!BellActive) return;
        if (timer <= 0)
        {
            if (Random.Range(0f, 1f) < chance)
            {
                AudioManager.Instance.PlaySound(AudioManager.Sounds.Bell, transform);
            }
            timer = frequency;
        }
        
        
    }
}

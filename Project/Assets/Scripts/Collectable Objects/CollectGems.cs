
using Sound;
using UnityEngine;

namespace Collectable_Objects
{
    public class CollectGems : MonoBehaviour
    {
        private AudioManager _audioManager;

        private void Start()
        {
            _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        }
    
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                Destroy(gameObject);
                Debug.Log("Gem collected!");
                AIEnemy.Instance.ReturnToPatrol();
                _audioManager.PlaySoundEffect(_audioManager.gemCollect);
            }
        }
    }
}

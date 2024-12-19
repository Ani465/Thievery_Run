
using Sound;
using UnityEngine;

namespace Collectable_Objects
{
    public class CollectMoney : MonoBehaviour
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
                Debug.Log("Coin collected!");
                Destroy(gameObject);
                ScoreManager.Instance.AddScore();
                _audioManager.PlaySoundEffect(_audioManager.coinCollect);
            }
        }
    }
}

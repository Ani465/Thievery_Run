using System.Collections;
using Sound;
using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        public float moveSpeed = 5f;
        public float stunDuration = 2f; 
        public float knockBackDistance = 0.5f; // Distance to step back upon collision
        public GameObject stunEffect; // Reference to the animated GameObject
        private bool _isStunned;
        private AudioManager _audioManager;
    
        private void Start()
        {
            _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
            stunEffect.SetActive(false);
        }

        private void Update()
        {
            if (!_isStunned)
            {
                PlayerMove();
            }
        }

        private void PlayerMove()
        {
            transform.Translate(Vector3.forward * (moveSpeed * Time.deltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                StartCoroutine(StunPlayer());
                KnockBack();
            }
        }

        private IEnumerator StunPlayer()
        {
            _audioManager.PlaySoundEffect(_audioManager.stunned);
            _isStunned = true;
            stunEffect.SetActive(true); // Show the stun effect
            yield return new WaitForSeconds(stunDuration);
            _isStunned = false;
            stunEffect.SetActive(false); // Hide the stun effect
        }

        private void KnockBack()
        {
            // Move the player backward upon collision
            transform.Translate(-Vector3.forward * knockBackDistance);
        }
    }
}
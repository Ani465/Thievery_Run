using Sound;
using UI;
using UnityEngine;
using UnityEngine.AI;

    [RequireComponent(typeof(NavMeshAgent))]
    public class AIEnemy : MonoBehaviour
    {
        public static AIEnemy Instance;
        private NavMeshAgent _agent;
        public Transform wayPoints;
        private int _currentWayPoint;
        private const float WaitAtPoint = 1.5f;
        private float _waitTime;
        private float _suspiciousTime;
        private const float ChaseRange = 4f;
        private const float ApprehendRange = 1f;
        private GameObject _player;
        private GameUIManager _screenManager;
        private AudioManager _audioManager;

        private void Awake()
        {
            Instance = this;
        }
        
        private void Start()
        {
            _audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
            _agent = GetComponent<NavMeshAgent>();
            _player = GameObject.FindGameObjectWithTag("Player");
            _screenManager = FindObjectOfType<GameUIManager>();
            _currentWayPoint = 0;
        }
        
        public void AssignWaypoints(Transform waypoints)
        {
            this.wayPoints = waypoints;
        }
        
        private void Update()
        {
            var distanceToPlayer = Vector3.Distance(transform.position, _player.transform.position);

            if (distanceToPlayer <= ApprehendRange)
            {
                Apprehend();
            }
            else if (distanceToPlayer <= ChaseRange)
            {
                ChasePlayer();
            }
            else 
            {
                Patrol();
            }
        }

        private void Patrol()
        {
            if (_agent.remainingDistance < _agent.stoppingDistance)
            {
                _waitTime -= Time.deltaTime;
                if (_waitTime <= 0)
                {
                    _currentWayPoint++;
                    if (_currentWayPoint >= wayPoints.childCount)
                    {
                        _currentWayPoint = 0;
                    }
                    
                    _agent.SetDestination(wayPoints.GetChild(_currentWayPoint).position);
                    _waitTime = WaitAtPoint;
                }
            }
        }

        private void ChasePlayer()
        {
            _agent.SetDestination(_player.transform.position);
            _audioManager.PlaySoundEffect(_audioManager.whistle);
        }

        private void Apprehend()
        {
            _screenManager.GameOver();
        }

        public void ReturnToPatrol()
        {
            Patrol();
        }
    }
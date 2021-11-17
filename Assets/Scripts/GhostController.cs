using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class SpawnEvent
{
    public string eventName;
    public string holdItem;
    public Transform spawn;
    public Transform target;

    public bool spawnGhostWhenNotLooking;
    public bool moveGhostWhenNotLooking;
    public bool hideGhostOnEvent;
}

public class GhostController : MonoBehaviour
{
    public List<SpawnEvent> spawnEvents;

    private NavMeshAgent _agent;
    private Animator _animator;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        _animator.SetTrigger("hide");

        if (spawnEvents.Count > 0)
        {
            for (int i = 0; i < spawnEvents.Count; i++)
            {
                SpawnEvent spawnEvent = spawnEvents[i];
                if (!string.IsNullOrEmpty(spawnEvent.eventName))
                {
                    EventManager.Subscribe(spawnEvent.eventName, SpawnGhostEvent);
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (spawnEvents.Count > 0)
        {
            for (int i = 0; i < spawnEvents.Count; i++)
            {
                SpawnEvent spawnEvent = spawnEvents[i];
                if (!string.IsNullOrEmpty(spawnEvent.eventName))
                {
                    EventManager.Unsubscribe(spawnEvent.eventName, SpawnGhostEvent);
                }
            }
        }
    }

    public void SpawnGhostEvent(params object[] data)
    {
        string eventName = (string)data[0];
        SpawnEvent spawnEvent = spawnEvents.Find(x => x.eventName == eventName);

        if (spawnEvent != null)
        {
            if (!string.IsNullOrEmpty(spawnEvent.holdItem))
            {
                HoldItem(spawnEvent.holdItem);
            }

            if (spawnEvent.hideGhostOnEvent)
            {
                _animator.SetTrigger("hide");
            }
            else
            {
                StartCoroutine(SpawnGhostRoutine(spawnEvent));
            }

            EventManager.Unsubscribe(eventName, SpawnGhostEvent);
        }
    }

    public void HoldItem(string item)
    {
        // TODO: setup system to allow the ghost to hold any item in the game
    }

    private IEnumerator SpawnGhostRoutine(SpawnEvent spawnEvent)
    {
        Transform spawnPoint = spawnEvent.spawn;
        Transform targetPoint = spawnEvent.target;

        // spawn the ghost on the transform
        transform.position = spawnPoint.position;

        Debug.LogFormat("GHOST: spawning ghost at {0}", spawnPoint.position);

        if (spawnEvent.spawnGhostWhenNotLooking)
        {
            while (PlayerCanSeeGhost())
            {
                yield return null;
            }
        }

        _animator.SetTrigger("reveal");

        if (targetPoint != null)
        {
            if (spawnEvent.moveGhostWhenNotLooking)
            {
                while (!PlayerCanSeeGhost())
                {
                    yield return null;
                }
            }

            _agent.SetDestination(targetPoint.position);

            while (_agent.pathPending)
            {
                yield return null;
            }

            Debug.LogFormat("GHOST: moving to position {0}", targetPoint.position);
            while (_agent.remainingDistance > 3)
            {
                yield return null;
            }

            if (!spawnEvent.hideGhostOnEvent)
            {
                _animator.SetTrigger("hide");
            }
        }
    }

    private bool PlayerCanSeeGhost()
    {
        return GetComponent<Renderer>().isVisible;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;
public class ImageTarget : MonoBehaviour
{
    // true if image was tracked once
    private bool _wasTracked = false;
    
    //sound when image gets tracked
    [SerializeField] private AudioClip _trackSound;

    public enum TrackingStatusFilter
    {
        Tracked,
        Tracked_ExtendedTracked,
        Tracked_ExtendedTracked_Limited
    }

    /// <summary>
    /// A filter that can be set to either:
    /// - Only consider a target if it's in view (TRACKED)
    /// - Also consider the target if's outside of the view, but the environment is tracked (EXTENDED_TRACKED)
    /// - Even consider the target if tracking is in LIMITED mode, e.g. the environment is just 3dof tracked.
    /// </summary>
    public TrackingStatusFilter StatusFilter = TrackingStatusFilter.Tracked_ExtendedTracked_Limited;
    public UnityEvent OnTargetFound;
    public UnityEvent OnTargetLost;


    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;
    protected TrackableBehaviour.StatusInfo m_PreviousStatusInfo;
    protected TrackableBehaviour.StatusInfo m_NewStatusInfo;
    protected bool m_CallbackReceivedOnce = false;

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();

        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.RegisterOnTrackableStatusChanged(OnTrackableStatusChanged);
            mTrackableBehaviour.RegisterOnTrackableStatusInfoChanged(OnTrackableStatusInfoChanged);
        }

    }
    
    
    // returns if object has been tracked once
    public bool GetWasTracked()
    {
        return _wasTracked;
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
        {
            mTrackableBehaviour.UnregisterOnTrackableStatusInfoChanged(OnTrackableStatusInfoChanged);
            mTrackableBehaviour.UnregisterOnTrackableStatusChanged(OnTrackableStatusChanged);
        }
    }

    void OnTrackableStatusChanged(TrackableBehaviour.StatusChangeResult statusChangeResult)
    {
        m_PreviousStatus = statusChangeResult.PreviousStatus;
        m_NewStatus = statusChangeResult.NewStatus;

        Debug.LogFormat("Trackable {0} {1} -- {2}",
            mTrackableBehaviour.TrackableName,
            mTrackableBehaviour.CurrentStatus,
            mTrackableBehaviour.CurrentStatusInfo);

        HandleTrackableStatusChanged();
    }

    void OnTrackableStatusInfoChanged(TrackableBehaviour.StatusInfoChangeResult statusInfoChangeResult)
    {
        m_PreviousStatusInfo = statusInfoChangeResult.PreviousStatusInfo;
        m_NewStatusInfo = statusInfoChangeResult.NewStatusInfo;
        
        HandleTrackableStatusInfoChanged();
    }

    protected virtual void HandleTrackableStatusChanged()
    {
        if (!ShouldBeRendered(m_PreviousStatus) &&
            ShouldBeRendered(m_NewStatus))
        {
            OnTrackingFound();
        }
        else if (ShouldBeRendered(m_PreviousStatus) &&
                 !ShouldBeRendered(m_NewStatus))
        {
            OnTrackingLost();
        }
        else
        {
            if (!m_CallbackReceivedOnce && !ShouldBeRendered(m_NewStatus))
            {
                // This is the first time we are receiving this callback, and the target is not visible yet.
                // --> Hide the augmentation.
                OnTrackingLost();
            }
        }

        m_CallbackReceivedOnce = true;
    }

    protected virtual void HandleTrackableStatusInfoChanged()
    {
        if (m_NewStatusInfo == TrackableBehaviour.StatusInfo.WRONG_SCALE)
        {
            Debug.LogErrorFormat("The target {0} appears to be scaled incorrectly. " + 
                                 "This might result in tracking issues. " + 
                                 "Please make sure that the target size corresponds to the size of the " + 
                                 "physical object in meters and regenerate the target or set the correct " +
                                 "size in the target's inspector.", mTrackableBehaviour.TrackableName);
        }
    }

    protected bool ShouldBeRendered(TrackableBehaviour.Status status)
    {
        if (status == TrackableBehaviour.Status.DETECTED ||
            status == TrackableBehaviour.Status.TRACKED)
        {
            // always render the augmentation when status is DETECTED or TRACKED, regardless of filter
            return true;
        }

        if (StatusFilter == TrackingStatusFilter.Tracked_ExtendedTracked)
        {
            if (status == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                // also return true if the target is extended tracked
                return true;
            }
        }

        if (StatusFilter == TrackingStatusFilter.Tracked_ExtendedTracked_Limited)
        {
            if (status == TrackableBehaviour.Status.EXTENDED_TRACKED ||
                status == TrackableBehaviour.Status.LIMITED)
            {
                // in this mode, render the augmentation even if the target's tracking status is LIMITED.
                // this is mainly recommended for Anchors.
                return true;
            }
        }

        return false;
    }

    protected virtual void OnTrackingFound()
    {
        //only when image is tracked first: play tracking clip at camera position
        if (!_wasTracked)
        {
            AudioSource.PlayClipAtPoint(_trackSound, Camera.main.transform.position);
        }
        
        _wasTracked = true;

        if (mTrackableBehaviour)
        {
            var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

            // Enable rendering:
            foreach (var component in rendererComponents)
                component.enabled = true;

            // Enable colliders:
            foreach (var component in colliderComponents)
                component.enabled = true;

            // Enable canvas':
            foreach (var component in canvasComponents)
                component.enabled = true;
        }

        if (OnTargetFound != null)
            OnTargetFound.Invoke();
    }

    protected virtual void OnTrackingLost()
    {
        if (mTrackableBehaviour)
        {
            var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

            // Disable rendering:
            foreach (var component in rendererComponents)
                component.enabled = false;

            // Disable colliders:
            foreach (var component in colliderComponents)
                component.enabled = false;

            // Disable canvas':
            foreach (var component in canvasComponents)
                component.enabled = false;
        }

        if (OnTargetLost != null)
            OnTargetLost.Invoke();
    }
}


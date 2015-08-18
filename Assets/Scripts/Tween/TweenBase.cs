using UnityEngine;
using System;
using System.Collections;

public abstract class TweenBase<T> : MonoBehaviour {
    [SerializeField]
    protected T from, to;

    [SerializeField]
    float duration = 0f, wait = 0f;

    public bool destroyOnFinished = false;

    [HideInInspector]
    public Action OnFinished;
    
    public static Child Begin<Child>(GameObject gameObject, T to, float duration) where Child : TweenBase<T>
    {
        var retInstance = gameObject.AddComponent<Child>();
        retInstance.from = retInstance.Initial();
        retInstance.to = to;
        retInstance.duration = duration;

        return retInstance;
    }

    protected float startingTime;
	// Use this for initialization
	void Start () {
        startingTime = Time.time;
	}

    void Update()
    {
        float currentTime = Time.time;
        if (currentTime - startingTime < wait) return;

        if (currentTime - startingTime > duration + wait)
        {
            // finish
            TweenProgress(1f);

            if (destroyOnFinished) Destroy(this);
            else enabled = false;

            OnFinished();

            return;
        }

        TweenProgress((currentTime - startingTime - wait) / duration);
    }


    protected abstract void TweenProgress(float progressRate);
    protected abstract T Initial();
    
}

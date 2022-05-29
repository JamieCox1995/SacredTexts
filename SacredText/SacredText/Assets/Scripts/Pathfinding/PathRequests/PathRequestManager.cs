using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRequestManager : Singleton<PathRequestManager>
{
    private Queue<PathRequest> RequestQueue = new Queue<PathRequest>();
    private PathRequest currentRequest;

    private Pathfinding pathfinding;

    private bool isProcessingPath;

    public void Initialize(Pathfinding _Pathfinding)
    {
        pathfinding = _Pathfinding;
    }

    public static void RequestPath(Vector3 _PathStart, Vector3 _PathEnd, Action<Vector3[], bool> _Callback)
    {
        PathRequest request = new PathRequest(_PathStart, _PathEnd, _Callback);

        Instance.RequestQueue.Enqueue(request);
        Instance.ProcessNext();
    }

    private void ProcessNext()
    {
        if(isProcessingPath || RequestQueue.Count == 0)
        {
            return;
        }

        // Getting the next path in the queue to process
        currentRequest = RequestQueue.Dequeue();
        isProcessingPath = true;
        pathfinding.FindPath(currentRequest.PathStart, currentRequest.PathEnd);
    }

    public void FinishProcessingPath(Vector3[] _Path, bool _Success)
    {
        currentRequest.RequestCallback(_Path, _Success);
        isProcessingPath = false;
        ProcessNext();
    }

    struct PathRequest
    {
        public Vector3 PathStart;
        public Vector3 PathEnd;
        public Action<Vector3[], bool> RequestCallback;

        public PathRequest(Vector3 _PathStart, Vector3 _PathEnd, Action<Vector3[], bool> _Callback)
        {
            PathStart = _PathStart;
            PathEnd = _PathEnd;
            RequestCallback = _Callback;
        }
    }
}

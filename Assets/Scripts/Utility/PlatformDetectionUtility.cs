using UnityEngine;

public static class PlatformDetectionUtility
{
    public static bool IsStandingOnPlatform(Collider2D sourceCollider, LayerMask platformMask, float requiredPlatformOverlap, out Transform platformTransform)
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(sourceCollider.bounds.center, sourceCollider.bounds.size, 0f, platformMask);

        float maxOverlap = 0f;
        Transform bestMatch = null;

        foreach (Collider2D hit in hits)
        {
            if (hit == null)
                continue;

            if (TryCalculateIntersection(sourceCollider.bounds, hit.bounds, out Bounds intersection))
            {
                float intersectionArea = intersection.size.x * intersection.size.y;
                float sourceArea = sourceCollider.bounds.size.x * sourceCollider.bounds.size.y;
                float overlapRatio = intersectionArea / sourceArea;

                if (overlapRatio >= requiredPlatformOverlap && overlapRatio > maxOverlap)
                {
                    maxOverlap = overlapRatio;
                    bestMatch = hit.transform;
                }
            }
        }

        platformTransform = bestMatch;
        return platformTransform != null;
    }

    private static bool TryCalculateIntersection(Bounds a, Bounds b, out Bounds result)
    {
        float minX = Mathf.Max(a.min.x, b.min.x);
        float minY = Mathf.Max(a.min.y, b.min.y);
        float maxX = Mathf.Min(a.max.x, b.max.x);
        float maxY = Mathf.Min(a.max.y, b.max.y);

        if (maxX > minX && maxY > minY)
        {
            result = new Bounds();
            result.SetMinMax(new Vector3(minX, minY), new Vector3(maxX, maxY));
            return true;
        }

        result = new Bounds();
        return false;
    }
}

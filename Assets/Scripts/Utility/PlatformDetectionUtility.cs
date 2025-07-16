using System.Collections.Generic;
using UnityEngine;

public static class PlatformDetectionUtility
{
    public static bool IsStandingOnPlatform(Collider2D sourceCollider, LayerMask platformMask, float requiredPlatformOverlap, out Transform platformTransform)
    {
        Collider2D[] hits = Physics2D.OverlapBoxAll(sourceCollider.bounds.center, sourceCollider.bounds.size, 0f, platformMask);


        float sourceArea = sourceCollider.bounds.size.x * sourceCollider.bounds.size.y;

        Dictionary<Transform, float> platformOverlaps = new Dictionary<Transform, float>();

        foreach (Collider2D hit in hits)
        {
            if (hit == null || hit == sourceCollider)
                continue;

            Transform groupRoot = GetPlatformGroup(hit);

            if (TryCalculateIntersection(sourceCollider.bounds, hit.bounds, out Bounds intersection))
            {
                float intersectionArea = intersection.size.x * intersection.size.y;

                if (!platformOverlaps.ContainsKey(groupRoot))
                    platformOverlaps[groupRoot] = 0f;

                platformOverlaps[groupRoot] += intersectionArea;
            }
        }

        platformTransform = null;
        float maxOverlap = 0f;

        foreach (var item in platformOverlaps)
        {
            float overlapArea = item.Value / sourceArea;

            if (overlapArea>= requiredPlatformOverlap && overlapArea > maxOverlap)
            {
                maxOverlap = overlapArea;
                platformTransform = item.Key;
            }
        }

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

    private static Transform GetPlatformGroup(Collider2D collider)
    {
        var group = collider.GetComponentInParent<PlatformGroup>();
        return group != null ? group.transform : collider.transform.root;
    }
}

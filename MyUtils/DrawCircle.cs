// For use with Unity game engine. Attach to a game object with a LineRenderer component

using UnityEngine;

public class DrawCircle : MonoBehaviour {
    public float radius = 5f; // Public field to show in inspector to easily set.
    public int numSegments = 384;
    private LineRenderer line;

    private void Awake() {
        line = GetComponent<LineRenderer>();
    }

    private void Start() {
        Draw(transform.position.x, transform.position.y, radius, numSegments);
    }

    private void Update() {
    }

    // numSegments is how many positions to fill and thus how much smoother it looks
    private void Draw(float posX, float posY, float r, int nSegments) {
        line.numPositions = nSegments + 1;

        int lastIndex = 0;
        Vector3 firstPos = new Vector3(0, 0, 0);

        for(int i = 0; i < nSegments; i++) {
            float theta = 2.0f * Mathf.PI * i / nSegments;
            float x = r * Mathf.Cos(theta); // you can draw ellipsis by multiplying float x and y by a value
            float y = r * Mathf.Sin(theta);

            if (i == 0)
                firstPos = new Vector3(x + posX, y + posY, 0);

            line.SetPosition(i, new Vector3(x + posX, y + posY, 0));

            lastIndex = i;
        }

        line.SetPosition(lastIndex + 1, firstPos);
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Graphic))]
public class UiOutline : BaseMeshEffect {
    public Color outlineColor = Color.black;
    public Vector2 outlineOffset = new Vector2(1f, -1f);

    public override void ModifyMesh(VertexHelper vh) {
        if (!IsActive()) {
            return;
        }

        var verts = new List<UIVertex>();
        vh.GetUIVertexStream(verts);

        var neededCapacity = verts.Count * 5;
        if (verts.Capacity < neededCapacity) {
            verts.Capacity = neededCapacity;
        }

        var original = verts.Count;
        var count = 0;

        for (var i = 0; i < original; ++i) {
            var vert = verts[count];
            verts.Add(vert);

            var position = vert.position;
            position.x += outlineOffset.x;
            position.y += outlineOffset.y;
            vert.position = position;
            var color = outlineColor;
            color.a = vert.color.a;
            vert.color = color;
            verts[count] = vert;
            ++count;
        }

        vh.Clear();
        vh.AddUIVertexTriangleStream(verts);
    }
}

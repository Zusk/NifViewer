# Niobject `bhkConvexVerticesShape`

A convex shape built from vertices. Note that if the shape is used in
a non-static object (such as clutter), then they will simply fall
through ground when they are under a bhkListShape.

## Attributes
- **inherit**: `bhkConvexShape`
- **module**: `BSHavok`
- **name**: `bhkConvexVerticesShape`
- **versions**: `#BETHESDA#`

## Fields
- **Vertices Property** (`bhkWorldObjCInfoProperty`)
- **Normals Property** (`bhkWorldObjCInfoProperty`)
- **Num Vertices** (`uint`)
  - Number of vertices.
- **Vertices** (`Vector4`)
  - Attributes: `length`=`Num Vertices`
  - Vertices. Fourth component is 0. Lexicographically sorted.
- **Num Normals** (`uint`)
  - The number of half spaces.
- **Normals** (`Vector4`)
  - Attributes: `length`=`Num Normals`
  - Half spaces as determined by the set of vertices above. First three components define the normal pointing to the exterior, fourth component is the signed distance of the separating plane to the origin: it is minus the dot product of v and n, where v is any vertex on the separating plane, and n is the normal. Lexicographically sorted.


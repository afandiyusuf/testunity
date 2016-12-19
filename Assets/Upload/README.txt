====================
 SUMMARY
====================

A vertex-based implementation of a simple 2D free-drawing tool.

====================
 FEATURES
====================

- Lines can have any color or size.
- Fast, responsive and reasonably efficient line drawing.
- Lightweight, simple API; very easily integrated into larger projects.

====================
 CHANGELOG
====================

Version 1.03 (17th November 2013):
- Fixed a bug where colors do not function properly when running on Windows OS.
- Application now works with the free version of Unity as well.

Version 1.02 (08th November 2013):
- Resubmission with contact information.

Version 1.01 (18th October 2013):
- Initial release version.

====================
 API
====================

/// <summary>
/// Initializes a new instance of the <see cref="ContinuousLine.Line"/> class.
/// </summary>
/// <param name='color'>
/// The color of this line.
/// </param>
/// <param name='size'>
/// The size of this line.
/// </param>
/// <param name='initialPosition'>
/// The starting position of the this line.
/// </param>
public Line(Color color, int size, Vector2 initialPosition);

/// <summary>
/// Adds a point to this line.
/// </summary>
/// <param name='position'>
/// The (x, y) coordinates of a new point on this line.
/// </param>
public void AddPoint(Vector2 position);

/// <summary>
/// Clears all the points on this line.
/// </summary>
public void ClearPoints();

/// <summary>
/// Draw this line.
/// </summary>
public void Draw();

/// <summary>
/// Assigns the material for drawing the lines.
/// </summary>
/// <param name='lineMaterial'>
/// The line material to use.
/// </param>
public static void AssignLineMaterial(ref Material lineMaterial);

====================
 NOTES
====================

- Open the DemoScene and refer to the sample OnScreenDrawing script for an example of usage.
- If you write your own scripts, remember to include the 'ContinuousLine' namespace.

====================
 LIMITATIONS
====================

- Currently only supports the filled, rounded brush.
- Performance may suffer if there is an absurdly large number of lines because the underlying
  implementation is vertex-based. You may have to set a maximum bound on the number of lines
  and/or the number of points you can add to an existing line.

====================
 CONTACT
====================

- For any enquiries, feature requests and/or bug reports, please contact me at xyresic.th@gmail.com.

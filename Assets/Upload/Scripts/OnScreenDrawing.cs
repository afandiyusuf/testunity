/* 
 * This script demonstrates how the Line class can be used
 * to implement on-screen drawing.
 * 
 * Press and hold the left mouse button to start drawing lines.
 * You can draw multiple lines of different colors and sizes.
 * When you're done, press 'space' to clear the screen.
 * 
 * Last updated 18th October 2013.
 * 
 */


using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ContinuousLine;


/// <summary>
/// This class implements on-screen drawing as a post-render process.
/// </summary>
public class OnScreenDrawing : MonoBehaviour
{
	/// <summary>
	/// The color of the line.
	/// </summary>
	public Color lineColor;
	
	/// <summary>
	/// The size of the line.
	/// </summary>
	[Range (-1, 20)] public int lineSize = 5;
	
	/// <summary>
	/// The line material (shader) used for drawing the lines.
	/// </summary>
	public Material lineMaterial;
	
	/// <summary>
	/// The previous mouse position.
	/// </summary>
	private Vector2 prevPos = Vector2.zero;
	
	/// <summary>
	/// The current mouse position.
	/// </summary>
	private Vector2 currPos = Vector2.zero;
	
	/// <summary>
	/// The list of continuous lines.
	/// </summary>
	private List<Line> lines = new List<Line>();
	
	/// <summary>
	/// The current line being drawn.
	/// </summary>
	private Line currLine;
	public int sortingLayer;
	private bool pressed;
	
	void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		if (Physics.Raycast (ray, 1000, 1 << sortingLayer)) {
			// Left mouse button pressed.
			if (Input.GetButtonDown ("Fire1")) {
				pressed = true;
				prevPos = Input.mousePosition;
				currLine	= new Line (lineColor, lineSize, prevPos);

				lines.Add (currLine);
			}
		
			// Left mouse button held down.
			else if (Input.GetButton ("Fire1")) {
				if (pressed) {
					currPos = Input.mousePosition;
					if (currPos != prevPos) {
						currLine.AddPoint (currPos);
						prevPos = currPos;
					}
				}
			}
		
			// Left mouse button released.
			else if (Input.GetButtonUp ("Fire1")) {
				pressed = false;
			}
			
            /*
			// Spacebar pressed.
			else if (Input.GetKeyDown (KeyCode.Space)) {
				for (int i = 0; i < lines.Count; i++)
					lines [i].ClearPoints ();
				lines.Clear ();
			}
		
            
			// Change sizes.
			else if (Input.GetKeyDown ("1")) {
				lineSize = 1;
			} else if (Input.GetKeyDown ("2")) {
				lineSize = 5;
			} else if (Input.GetKeyDown ("3")) {
				lineSize = 15;
			}
		
			// Change colors
			else if (Input.GetKeyDown (KeyCode.Q)) {
				lineColor = new Color (9.0f, 112.0f, 84.0f) / 255.0f;
			} else if (Input.GetKeyDown (KeyCode.W)) {
				lineColor = new Color (255.0f, 222.0f, 0.0f) / 255.0f;
			} else if (Input.GetKeyDown (KeyCode.E)) {
				lineColor = new Color (101.0f, 153.0f, 255.0f) / 255.0f;
			} else if (Input.GetKeyDown (KeyCode.A)) {
				lineColor = new Color (255.0f, 153.0f, 0.0f) / 255.0f;
			} else if (Input.GetKeyDown (KeyCode.S)) {
				lineColor = new Color (210.0f, 67.0f, 60.0f) / 255.0f;
			} else if (Input.GetKeyDown (KeyCode.D)) {
				lineColor = new Color (0.0f, 0.0f, 0.0f) / 255.0f;
			}
            */
		} else {
			pressed = false;
		}
	}

    public void ClearLine()
    {
        for (int i = 0; i < lines.Count; i++)
            lines[i].ClearPoints();
        lines.Clear();
    }
	void OnPostRender()
	{
		// Assign and set the current line material.
		Line.AssignLineMaterial(ref lineMaterial);
		lineMaterial.SetPass(0);
		
		// Setup the matrix stacks.
		GL.PushMatrix();
		GL.LoadPixelMatrix();
		
		// Setup the viewport.
		GL.Viewport(new Rect(0, 0, Screen.width, Screen.height));
		
		// Draw the lines.
		for (int i = 0; i < lines.Count; i++)
			lines[i].Draw();
		
		// Restore the matrix stacks.
		GL.PopMatrix();
	}
}

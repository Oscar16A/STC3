﻿using UnityEngine;

/*
	Help create an infinite background
*/

public static class RendererExtensions
{
	public static bool IsVisibleFrom( this Renderer renderer, Camera camera)
	{
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
	}
}
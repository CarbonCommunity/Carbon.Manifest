using System;
using UnityEngine;

[Serializable]
public class TOD_DayParameters
{
	[Tooltip ("Color of the sun spot.\nLeft value: Sun at zenith.\nRight value: Sun at horizon.")]
	public Gradient SunColor = null;

	[Tooltip ("Color of the light that hits the ground.\nLeft value: Sun at zenith.\nRight value: Sun at horizon.")]
	public Gradient LightColor = null;

	[Tooltip ("Color of the god rays.\nLeft value: Sun at zenith.\nRight value: Sun at horizon.")]
	public Gradient RayColor = null;

	[Tooltip ("Color of the light that hits the atmosphere.\nLeft value: Sun at zenith.\nRight value: Sun at horizon.")]
	public Gradient SkyColor = null;

	[Tooltip ("Color of the clouds.\nLeft value: Sun at zenith.\nRight value: Sun at horizon.")]
	public Gradient CloudColor = null;

	[Tooltip ("Color of the atmosphere fog.\nLeft value: Sun at zenith.\nRight value: Sun at horizon.")]
	public Gradient FogColor = null;

	[Tooltip ("Color of the ambient light.\nLeft value: Sun at zenith.\nRight value: Sun at horizon.")]
	public Gradient AmbientColor = null;

	[Tooltip ("Intensity of the light source.")]
	[TOD_Min (0f)]
	public float LightIntensity = 1f;

	[Tooltip ("Opacity of the shadows dropped by the light source.")]
	[Range (0f, 1f)]
	public float ShadowStrength = 1f;

	[Tooltip ("Brightness multiplier of the ambient light.")]
	[Range (0f, 8f)]
	public float AmbientMultiplier = 1f;

	[Tooltip ("Brightness multiplier of the reflection probe.")]
	[Range (0f, 1f)]
	public float ReflectionMultiplier = 1f;
}

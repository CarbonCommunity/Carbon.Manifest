using System;
using UnityEngine;

[Serializable]
public class TOD_DayParameters
{
	[Tooltip ("Color of the sun spot.\nLeft value: Sun at zenith.\nRight value: Sun at horizon.")]
	public Gradient SunColor;

	[Tooltip ("Color of the light that hits the ground.\nLeft value: Sun at zenith.\nRight value: Sun at horizon.")]
	public Gradient LightColor;

	[Tooltip ("Color of the god rays.\nLeft value: Sun at zenith.\nRight value: Sun at horizon.")]
	public Gradient RayColor;

	[Tooltip ("Color of the light that hits the atmosphere.\nLeft value: Sun at zenith.\nRight value: Sun at horizon.")]
	public Gradient SkyColor;

	[Tooltip ("Color of the clouds.\nLeft value: Sun at zenith.\nRight value: Sun at horizon.")]
	public Gradient CloudColor;

	[Tooltip ("Color of the atmosphere fog.\nLeft value: Sun at zenith.\nRight value: Sun at horizon.")]
	public Gradient FogColor;

	[Tooltip ("Color of the ambient light.\nLeft value: Sun at zenith.\nRight value: Sun at horizon.")]
	public Gradient AmbientColor;

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

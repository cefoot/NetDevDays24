using System;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using StereoKit;
using StereoKit.Framework;

namespace DevDayTalk
{

	class Program
	{
		private static float speed = 0.3f;

		static void Main(string[] args)
		{
			// Initialize StereoKit
			SKSettings settings = new SKSettings
			{
				appName = "DevDayTalk",
				assetsFolder = "Assets",
				mode = AppMode.XR
			};
			var passthrough = SK.GetOrCreateStepper<PassthroughMetaExt>();
			if (!SK.Initialize(settings))
				return;
			passthrough.Enabled = true;

			var points = 0;

			// Create assets used by the app
			Pose cubePose = new Pose(0, 0, -0.5f);
			Pose menuPose = new Pose(.5f, 0, -0.5f, Quat.LookAt(new Vec3(.5f, 0, -0.5f), Input.Head.position));
			Model cube = Model.FromMesh(
				Mesh.GenerateRoundedCube(Vec3.One * 0.1f, 0.02f),
				Material.UI);

			Matrix floorTransform = Matrix.TS(0, -1.5f, 0, new Vec3(30, 0.1f, 30));
			Material floorMaterial = new Material("floor.hlsl");
			floorMaterial.Transparency = Transparency.Blend;

			var boxgame = new BoxGame();
			// Core application loop
			SK.Run(() =>
			{
				if ((Device.DisplayBlend == DisplayBlend.Opaque || !passthrough.Enabled) && !Device.Name.ToLower().Contains("moto"))
					Mesh.Cube.Draw(floorMaterial, floorTransform);
				boxgame.Step();
			});
		}

		private static void ResetCubePose(ref Pose cubePose)
		{
			var rand = new Random();
			cubePose.position = new Vec3((float)rand.NextDouble() * .4f - .2f, (float)rand.NextDouble() * .4f - .2f, -0.5f);
		}
	}
}
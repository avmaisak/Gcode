using Common.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.TestSuite
{
	[TestClass]
	public class ReflectionTests
	{
		[TestMethod]
		public void ReflectionTests1()
		{
			var s = new
			{
				Name = "Name obj",
				Description = "Description obj"
			};

			var props = ReflectionUtils.GetProperties(s);
			Assert.IsNotNull(props);
		}
		[TestMethod]
		public void ReflectionTests2()
		{
			var s = new
			{
				Name = "Name obj",
				Description = "Description obj"
			};

			var props = ReflectionUtils.GetProperties(s);
			foreach (var VARIABLE in props)
			{

			}
			Assert.IsTrue(props.Count == 2);
		}
		[TestMethod]
		public void ReflectionTests3()
		{
			var s = new
			{
				Name = "Name obj",
				Description = "Description obj",
				Num = 999
			};

			var props = ReflectionUtils.GetProperties(s);
			Assert.IsTrue(props.Count == 3);
			Assert.IsTrue(props[0].Key == "Name");
			Assert.IsTrue(props[0].Value == "Name obj");

			Assert.IsTrue(props[1].Key == "Description");
			Assert.IsTrue(props[1].Value == "Description obj");

			Assert.IsTrue(props[2].Key == "Num");
			Assert.IsTrue(props[2].Value == "999");

		}
	}
}

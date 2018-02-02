using System;
using Gcode.Common.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.TestSuite.Utils
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
		[TestMethod]
		public void ReflectionTests4() {

			for (var i = 0; i < 100500; i++)
			{
				var s = new {
					Name = $"Name obj {i}"
				};

				var props = ReflectionUtils.GetProperties(s);
				Assert.IsNotNull(props);
				Assert.IsTrue(props[0].Key == "Name");
				Assert.IsTrue(props[0].Value == $"Name obj {i}");

			}
		}

		[TestMethod]
		public void ReflectionTests5() {

			for (var i = 0; i < 100500; i++)
			{
				var s = new {
					Name = Convert.ToDouble(0.15)
				};

				var props = ReflectionUtils.GetProperties(s);
				Assert.IsNotNull(props);
				var key = props[0].Key;
				var value = props[0].Value;

				Assert.AreEqual("Name",key );
				Assert.AreEqual("0.15",value );
			}
		}
	}
}

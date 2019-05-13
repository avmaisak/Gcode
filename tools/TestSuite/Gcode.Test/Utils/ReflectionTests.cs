using System;
using System.Collections.Generic;
using Gcode.Utils.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Gcode.Test.Utils
{
	[TestClass]
	public class ReflectionTests
	{
		[TestMethod]
		public void ReflectionTestsInit1()
		{
			var s = new
			{
				Name = "Name obj",
				Description = "Description obj"
			};

			var props = s.GetProperties();
			Assert.IsInstanceOfType(props, typeof(List<KeyValuePair<string, string>>));
		}
		[TestMethod]
		public void ReflectionTestsInit2()
		{
			var s = new
			{
				Name = "Name obj",
				Description = @"At vero eos et accusamus et iusto odio dignissimos ducimus qui blanditiis praesentium voluptatum deleniti atque corrupti quos dolores et quas molestias excepturi sint occaecati cupiditate non provident, similique sunt in culpa qui officia deserunt mollitia animi, id est laborum et dolorum fuga. Et harum quidem rerum facilis est et expedita distinctio. Nam libero tempore, cum soluta nobis est eligendi optio cumque nihil impedit quo minus id quod maxime placeat facere possimus, omnis voluptas assumenda est, omnis dolor repellendus. Temporibus autem quibusdam et aut officiis debitis aut rerum necessitatibus saepe eveniet ut et voluptates repudiandae sint et molestiae non recusandae. Itaque earum rerum hic tenetur a sapiente delectus, ut aut reiciendis voluptatibus maiores alias consequatur aut perferendis doloribus asperiores repellat"
			};

			var props = s.GetProperties();
			Assert.IsNotNull(props);
		}
		[TestMethod]
		public void ReflectionTests1()
		{
			var s = new
			{
				Name = "Name obj",
				Description = "Description obj"
			};

			var props = s.GetProperties();
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

			var props = s.GetProperties();
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

			var props = s.GetProperties();
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

				var props = s.GetProperties();
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

				var props = s.GetProperties();
				Assert.IsNotNull(props);
				var key = props[0].Key;
				var value = props[0].Value;

				Assert.AreEqual("Name",key );
				Assert.AreEqual("0.15",value );
			}
		}
	}
}

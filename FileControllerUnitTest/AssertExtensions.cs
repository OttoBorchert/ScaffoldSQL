using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace FileControllerUnitTest
{
	public static class AssertExtensions
	{

		public static void DoesNotThrow(Action toTest)
		{
			try
			{
				toTest();
			}
			catch (Exception e)
			{
				Assert.Fail("Expected no excpetion to be thrown.\n" + e.Message + "\n" + e.StackTrace);
			}
		}

		public static void DoesNotThrow(Action toTest, params Type[] exception)
		{
			try
			{
				toTest();
			}
			catch (Exception e)
			{
				foreach (var ex in exception)
				{
					if (e.GetType() == ex || e.GetType().IsAssignableTo(ex))
					{
						Assert.Fail($"Expected no excpetion matching or inheriting from the type {ex} to be thrown.\n{e}");
					}
				}
				// Success! It did not throw that exception!
			}
		}

	}
}

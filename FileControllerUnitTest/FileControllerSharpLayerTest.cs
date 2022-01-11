using Microsoft.VisualStudio.TestTools.UnitTesting;
using ScaffoldingSQLProject.Controllers;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Text.RegularExpressions;

namespace FileControllerUnitTest
{
	[TestClass]
	public class FileControllerSharpLayerTest
	{
		class TestingFileData : FileController.IFileDataProvider
		{
			public string QuestionPath => Path.Combine(Directory.GetCurrentDirectory(), "TestResources", "questions");

			public string DbPath => Path.Combine(Directory.GetCurrentDirectory(), "TestResources", "databases");
		}

		static readonly string p_testDB = Path.Combine("TestResources", "TestDatabases");

		static void InitDirectory()
		{
			var fd = new TestingFileData();
			FileController.FileData.Configure(fd);

			foreach (string file in Directory.GetFiles(p_testDB))
			{
				if (File.Exists(Path.Combine(fd.DbPath, Path.GetFileName(file))))
				{
					File.Delete(Path.Combine(fd.DbPath, Path.GetFileName(file)));
				}
			}
		}

		static void AssertHashTableDataTypes(Hashtable hashtable)
		{
			Dictionary<string, Type> requiredBareMinimum = new Dictionary<string, Type>()
			{
				{ "File",             typeof(string)       },
				{ "Number",           typeof(string)       },
				{ "Question",         typeof(string)       },
				{ "TestCaseEnable",   typeof(bool)         },
				{ "ParsonsEnable",    typeof(bool)         },
				{ "SecretWord",       typeof(string)       },
				{ "SecretWordParson", typeof(string)       },
				{ "TestCases",        typeof(List<string>) },
				{ "Parsons",          typeof(List<string>) },
				{ "Database",         typeof(List<string>) }
			};

			foreach (var kv in requiredBareMinimum)
			{
				if (!hashtable.ContainsKey(kv.Key))
				{
					Assert.Fail($"Expected the key {kv.Key} in hashtable.");
				}
				Assert.AreEqual(kv.Value, hashtable[kv.Key].GetType());
			}
		} 

		[TestMethod]
		public void TestReadQuestionFullyPopulated()
		{
			InitDirectory();
			string filename = "ValidFullyPopulated.txt";
			Hashtable hashtable = null;

			AssertExtensions.DoesNotThrow(() =>
			{
				hashtable = FileController.GetFileContents(filename);
			});

			AssertHashTableDataTypes(hashtable);
		}

		[TestMethod]
		public void TestReadQuestionFullyPopulatedAbsurd()
		{
			InitDirectory();
			string filename = "ValidFullyPopulatedAbsurd.txt";
			Hashtable hashtable = null;

			AssertExtensions.DoesNotThrow(() =>
			{
				hashtable = FileController.GetFileContents(filename);
			});

			AssertHashTableDataTypes(hashtable);
		}

		[TestMethod]
		public void TestReadQuestionFullyPopulatedInvalid()
		{
			InitDirectory();
			string filename = "InvalidFullyPopulated.txt";
			Hashtable hashtable = null;

			Assert.ThrowsException<InvalidDataException>(() =>
			{
				hashtable = FileController.GetFileContents(filename);
			});
		}

		[TestMethod]
		public void TestDatabaseList()
		{
			InitDirectory();
			AssertExtensions.DoesNotThrow(() =>
			{
				FileController.DatabaseList();
			});
			string dir = new TestingFileData().DbPath;
			string filecontrol = string.Join('\n', FileController.DatabaseList());
			string manual = string.Join('\n', Directory.GetFiles(dir));
			Assert.AreEqual(manual, filecontrol);
		}

		[TestMethod]
		public void TestQuesitonList()
		{
			InitDirectory();
			AssertExtensions.DoesNotThrow(() =>
			{
				FileController.FileList();
			});
			var re = new Regex(@"[0-9]+\.[0-9]+\.txt");
			string dir = new TestingFileData().QuestionPath;
			string filecontrol = string.Join('\n', FileController.FileList());
			string manual = string.Join('\n', Directory.GetFiles(dir).Where(s => re.IsMatch(Path.GetFileName(s))).ToList());
			Assert.AreEqual(manual, filecontrol);
		}

		[TestMethod]
		public void TestWriteDatabase()
		{
			InitDirectory();
			const string database = "chinook.db";
			const string resources = "TestResources";
			const string path = "TestDatabases";
			// Create file
			IFormFile file;
			using (var stream = File.OpenRead(Path.Combine(resources, path, database)))
			{
				file = new FormFile(stream, 0, stream.Length, "", database);
			}
			// Run asserts
			AssertExtensions.DoesNotThrow(() => FileController.WriteDatabase(file));
			Assert.AreEqual(true, File.Exists(Path.Combine(new TestingFileData().DbPath, database)), $"Unable to find database {database}");
		}

		[TestMethod]
		public void TestWriteQuestion()
		{
			InitDirectory();
			string filename = "0.0.txt";
			string contents = "Unvalidated content. Should be accepted, should be handled by QuestionMaker";

			AssertExtensions.DoesNotThrow(() => FileController.WriteQuestion(filename, contents));
			Assert.AreEqual(true, File.Exists(Path.Combine(new TestingFileData().QuestionPath, filename)), "File was not written.");
		}
	}
}

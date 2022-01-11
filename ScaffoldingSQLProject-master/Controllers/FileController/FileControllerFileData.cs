using System;
using System.IO;
using System.Linq;

namespace ScaffoldingSQLProject.Controllers
{

	public partial class FileController
	{
        static string P_QuestionDirectory => FileData.QuesitonDirectory;
        static string P_DbPath => FileData.DbPath;
        public static readonly string p_questionFileExtension = ".txt";

        public interface IFileDataProvider
        {
            /// <summary>
            /// Path to the question direcotry
            /// </summary>
            string QuestionPath { get; }
            /// <summary>
            /// Path to the data bases
            /// </summary>
            string DbPath { get; }
        }

        /// <summary>
        ///     The default implementation for IFileDataProvider
        /// </summary>
        internal class FileDataProvider : IFileDataProvider
        {
            public string QuestionPath => Path.Combine(Directory.GetCurrentDirectory(), "Resources", "questions");
            public string DbPath => Path.Combine(Directory.GetCurrentDirectory(), "Resources", "databases");
        }

        /// <summary>
        ///     FileData is designed to provide the needed information for the data, while also allowing for unit test to cheange the direcotry that is read from.
        /// </summary>
        public static class FileData
        {
            /// <summary>
            ///     This method should ONLY be called by the unit testing enviroment.
            /// </summary>
            public static void Configure(IFileDataProvider provider)
            {
                // Disable configure iff DEBUG compiler flag is not set
#if DEBUG
				FileData.provider = provider;
#else
                throw new InvalidOperationException("Cannot change the FileData configuration outside of a unit test. Aborting.");
#endif
			}

			/// <summary>
			///     The directory the questions are located in
			/// </summary>
			public static string QuesitonDirectory => provider.QuestionPath;

            /// <summary>
            ///     The directory the databases are in.
            /// </summary>
            public static string DbPath => provider.DbPath;

            /// <summary>
            ///     The data provider. By default, it will be the FileDataProvider.
            /// </summary>
            static IFileDataProvider provider = new FileDataProvider();
        }
    }
}

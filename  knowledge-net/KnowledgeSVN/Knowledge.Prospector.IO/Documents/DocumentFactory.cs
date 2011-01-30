using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Knowledge.Prospector.Common.Utilites;

namespace Knowledge.Prospector.IO.Documents
{
	public class DocumentFactory
	{
		#region Singletone
		private static readonly DocumentFactory _Instance = new DocumentFactory();

		public static DocumentFactory Instance
		{
			get { return _Instance; }
		}

		private DocumentFactory()
		{ }
		#endregion
		
		#region Encoding methods

		protected const int BufferLength = 1000;
		protected static bool IsReadable(char[] data, int readedCount)
		{
			int success = 0;
			for (int i = 0; i < readedCount; i++)
			{
				//Symbol
				if (0x0 <= data[i] && data[i] <= 0x40)
					success++;

				//English char
				if (0x41 <= data[i] && data[i] <= 0x7a)
					success++;

				//Russian char
				if (0x410 <= data[i] && data[i] <= 0x44f)
					success++;
			}
			return success > readedCount * 0.95;
		}

		protected static bool CheckEncoding(string fileName, Encoding encoding)
		{
			char[] buffer = new char[BufferLength];

			using (StreamReader sr = new StreamReader(fileName, encoding))
			{

				int readedCount = sr.ReadBlock(buffer, 0, BufferLength);

				if (IsReadable(buffer, readedCount))
					return true;

				sr.Close();
			}
			return false;
		}

		protected static StreamReader FindEncoding(string fileName)
		{
			Encoding[] encodings = new Encoding[]
			{
				//Cyrillic DOS
				Encoding.GetEncoding(866),

				//Cyrillic WIN
				Encoding.GetEncoding(1251),

				Encoding.UTF8,
				
				Encoding.Unicode,
				
				Encoding.UTF7
			};

			foreach (Encoding encoding in encodings)
			{
				if (CheckEncoding(fileName, encoding))
				{
					return new StreamReader(fileName, encoding);
				}
			}
			return null;
		}

		#endregion
		
		
		public IDocument GetDocument(string fileName)
		{
			StreamReader streamReader = FindEncoding(PathUtils.Decode(fileName));
			if (streamReader == null)
				throw new Exception(string.Format("Connot determinate encoding for specified file: {0}", fileName));

			IDocument document = new TextDocument(streamReader);
			document.Info[DocumentInfo.SourceFileName] = fileName;
			document.Info[DocumentInfo.ParsedDateTime] = DateTime.Now.ToString();

			return document;
		}
	}
}

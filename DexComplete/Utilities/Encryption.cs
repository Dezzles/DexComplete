using System;
using System.Collections.Generic;
using System.Linq;

using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace DexComplete.Utilities
{
	public static class Encryption
	{
		public static string GetMd5Hash(string input)
		{
			MD5 md5Hash = MD5.Create();
			// Convert the input string to a byte array and compute the hash. 
			byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

			// Create a new Stringbuilder to collect the bytes 
			// and create a string.
			StringBuilder sBuilder = new StringBuilder();

			// Loop through each byte of the hashed data  
			// and format each one as a hexadecimal string. 
			for (int i = 0; i < data.Length; i++)
			{
				sBuilder.Append(data[i].ToString("x2"));
			}

			// Return the hexadecimal string. 
			return sBuilder.ToString();
		}/**/

		public static string GenerateText(int Length)
		{
			Random r = new Random();
			StringBuilder sb = new StringBuilder();
			for (int i = 0; i < 16; ++i)
			{
				int u = r.Next(62);
				if (u < 26)
				{
					sb.Append((char)('a' + u));
				}
				else if (u < 52)
				{
					sb.Append((char)('A' + u - 26));
				}
				else
				{
					sb.Append((char)('0' + u - 52));
				}
			}
			return sb.ToString();
		}

		public static Boolean isAlphaNumeric(string strToCheck)
		{
			Regex rg = new Regex(@"^[a-zA-Z0-9\s]*$");
			return rg.IsMatch(strToCheck);
		}

		

	}
}
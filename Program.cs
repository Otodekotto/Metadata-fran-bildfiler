using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;

namespace Lab3Csharp
{
    class Program
    {
        static void Main(string[] args)
        {
            int calculateLittleEndian(string binaryNumbers)
            {
                if (binaryNumbers.Length % 8 != 0)
                {
                    Console.WriteLine("ERROR, binaryNumbers is not in bytes...");
                    return -1;
                }

                List<string> stringList = new List<string>();

                string tempString = "";
                foreach (char letter in binaryNumbers)
                {
                    tempString += letter;

                    if (tempString.Length == 8)
                    {
                        stringList.Add(tempString);
                        tempString = "";
                    }
                }

                stringList.Reverse();

                string finalString = "";
                foreach (string byteText in stringList)
                {
                    foreach (char bit in byteText)
                    {
                        finalString += bit;
                    }
                }

                return Convert.ToInt32(finalString, 2);
            }

            string png2Img = @"..\..\..\Lab3ScreenDump-900x300.png";
            string pngImg = @"..\..\..\Test_400x200.png";
            string bmpImg = @"..\..\..\Test_400x200.bmp";
            string jpgImg = @"..\..\..\Test_400x200.jpg";
            string gifImg = @"..\..\..\Test_400x200.gif";
            string notfoundImg = @"..\..\..\Test_400x200";
            // change File.ReadAllBytes input to one of the above to try out diffrent immage;

            string pngSignature = "10001001";
            string bmpSignature = "01000010";

            try
            {
                byte[] fileBytes = File.ReadAllBytes(pngImg);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < 500; i++)
                {
                    sb.Append(Convert.ToString(fileBytes[i], 2).PadLeft(8, '0'));
                }

                string sbString = sb.ToString();

                if (sbString.Substring(0, 8) == pngSignature)
                {
                    Console.WriteLine("image is png!");
                    int width = Convert.ToInt32(sbString.Substring(128, 32), 2);
                    int height = Convert.ToInt32(sbString.Substring(160, 32), 2);
                    Console.WriteLine("Width: " + width + " and height: " + height);
                }
                else if (sbString.Substring(0, 8) == bmpSignature)
                {
                    Console.WriteLine("image is bmp");
                    int width = calculateLittleEndian(sbString.Substring(18 * 8, 32));
                    int height = calculateLittleEndian(sbString.Substring(22 * 8, 32));
                    Console.WriteLine("Width: " + width + " and height: " + height);

                }
                else
                {
                    Console.WriteLine("This is not a valid .bmp or .png file!");
                }
            }

            catch (FileNotFoundException)
            {
                Console.WriteLine("File not found");
            }
            
        }
    } 
}

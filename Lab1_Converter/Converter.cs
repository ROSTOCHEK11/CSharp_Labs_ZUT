using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Csh_lab1;

internal class Converter
{
    internal static void CsvToHtml(string FilePathCsv, string FilePathHtml)
    {

        string[] CsvLines = File.ReadAllLines(FilePathCsv); // запись всего цсв в массив
        // открываем Хтмл файл для вписывания в него данных
        using (StreamWriter FileHtml = new StreamWriter(FilePathHtml))
        {
            // стиль
            FileHtml.WriteLine("<html>");
            FileHtml.WriteLine("<head>");
            FileHtml.WriteLine("<style>\ntable {\n  font-family: arial, sans-serif;" +
                "\n  border-collapse: collapse;\n  width: 100%;\n}\n\ntd, th {\n  border: 1px solid #dddddd;" +
                "\n  text-align: left;\n  padding: 8px;\n}\n\ntr:nth-child(even) {\n  background-color: #dddddd;\n}");
            FileHtml.WriteLine("<title>Converter from Csv to Html</title>");
            FileHtml.WriteLine("</style>");
            FileHtml.WriteLine("</head>");
            FileHtml.WriteLine("<body>");
            FileHtml.WriteLine("<table border=\"1\">");
            FileHtml.WriteLine("<tr>");

            string[] BoldHeaders = CsvLines[0].Split(";"); // записываем в массив хеддеры / 0 - первая линия
            foreach (var header in BoldHeaders) // помещаем каждый хеддер в колоку и записываем в ряд, чтобы дать ему жирный стиль
            {
                FileHtml.WriteLine("<th>" + header + "</th>");
            }
            FileHtml.WriteLine("</tr>");


            string[] lines = CsvLines.Skip(1).ToArray();
            foreach (var line in lines)
            {
                FileHtml.WriteLine("<tr>");

                string[] data = line.Split(';');

                foreach (var element in data)
                {
                    
                    FileHtml.WriteLine("<td>" + element + "</td>");
                }

                FileHtml.WriteLine("</tr>");
            }

            FileHtml.WriteLine("</table>");
            FileHtml.WriteLine("</body>");
            FileHtml.WriteLine("</html>");
        }

    }
}

// td - data
// th - row
// tr - col